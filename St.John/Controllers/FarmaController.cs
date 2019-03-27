using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using St.John.Models;
using System.Text.RegularExpressions;
using EstrucutrasNoLin;
using St.John.Gelpers;
using lEstructurasLineales;
using St.John.Controllers;

namespace St.John.Controllers
{
    public class FarmaController : BaseController
    {
        public ActionResult Index()
        {
            return View(Datos.Instance.ListaDrogas);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);
                string csvData = System.IO.File.ReadAllText(filePath);
                //csvData.Remove(1);
                Regex CSV = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                int NumeroApuntador = 0;
                int numeroAux = 0;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        int IDDrug;
                        String[] fields = CSV.Split(row);
                        fields[0] = fields[0].Trim();
                        string Id = fields[0];
                        string nombre = fields[1];
                        nombre = nombre.Replace('"', ' ');
                        string descripcion = fields[2];
                        descripcion = descripcion.Replace('"', ' ');
                        string casa = fields[3];
                        casa = casa.Replace('"', ' ');
                        string precio = fields[4];
                        precio = precio.Replace("$", "");
                        string existecia = fields[5];
                        var DrogaListaActual = new DatosFarma
                        {
                            Codigo = Id,
                            Nombre = nombre,
                            Descricpion = descripcion,
                            Origen = casa,
                            Precio = precio,
                            Existencia = existecia,
                        };
                        if (numeroAux != 0)
                        {
                            IDDrug = int.Parse(DrogaListaActual.Codigo);
                            Datos.Instance.ArbolBDrogas.Insertar(IDDrug, NumeroApuntador);
                            Datos.Instance.ListaDrogas.Agregar(DrogaListaActual);
                            var DrogaActual = new DatosFarma
                            {
                                Nombre = nombre,
                                Codigo = Id,
                                Precio = precio,
                                Existencia = existecia,
                            };
                            Datos.Instance.ArbolDrogas.Insertar(DrogaActual);
                            NumeroApuntador++;
                        }
                        numeroAux++;
                    }
                }
            }
            return View(Datos.Instance.ListaDrogas);
        }
        // GET: Farma/Details/5
        public ActionResult ViewDetails()
        {
            return View(Datos.Instance.ListaDePedidosDeDrogas);
        }
        // GET: Farma/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: Farma/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        // GET: Farma/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        // POST: Farma/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Pedido()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Pedido(FormCollection collection)
        {
            Random Reabastecer = new Random();
            var PedidoActual = new Cliente
            {
                NombreCliente = collection["NombreCliente"],
                DireccionCliente = collection["DireccionCliente"],
                NitCliente = collection["NitCliente"],
                DrogaCliente = collection["DrogaCliente"],
                CantDrogas = int.Parse(collection["CantDrogas"]),
            };
            var BuscarDroga = new DatosFarma
            {
                Nombre = collection["DrogaCliente"],
            };
            var DrograEnLista = Datos.Instance.ArbolDrogas.Encontrar(DatosFarma.PorNombre, BuscarDroga);
            if (PedidoActual.CantDrogas < 0 || (PedidoActual.CantDrogas > Convert.ToInt32(DrograEnLista.Existencia)))
            {
                Danger("No se cuenta con la cantidad de drogas solicitadas.");
                return RedirectToAction("Pedido");
            }
            DrograEnLista.Existencia = (Convert.ToInt32(DrograEnLista.Existencia) - PedidoActual.CantDrogas).ToString();
            double Final = Convert.ToDouble(Convert.ToDouble(DrograEnLista.Precio) * PedidoActual.CantDrogas);
            var BuscarDrogaEnLista = new DatosFarma { };
            BuscarDrogaEnLista = Datos.Instance.ListaDrogas.Buscar(DatosFarma.PorNombre, DrograEnLista);
            var ExistenciaDroga = Convert.ToInt32(BuscarDrogaEnLista.Existencia) - PedidoActual.CantDrogas;
            BuscarDrogaEnLista.Existencia = ExistenciaDroga.ToString();
            if (Convert.ToInt32(BuscarDrogaEnLista.Existencia) <= 0)
            {
                BuscarDrogaEnLista.Existencia = (Reabastecer.Next(0, 15)).ToString();
            }
            Datos.Instance.ListaDePedidosDeDrogas.Agregar(BuscarDrogaEnLista);
            PedidoActual.TotalCliente = (Final).ToString();
            Datos.Instance.ListaClientes.Agregar(PedidoActual);
            Success(PedidoActual.DrogaCliente + " agregado a su factura");
            return RedirectToAction("VerListadoPedidos");
        }
        public ActionResult VerListadoPedidos()
        {
            return View(Datos.Instance.ListaClientes);
        }
    }
}
