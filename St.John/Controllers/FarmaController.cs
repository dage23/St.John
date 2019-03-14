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

namespace St.John.Controllers
{
    public class FarmaController : Controller
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
                Regex CSV = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                bool bandera = true;
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        String[] fields = CSV.Split(row);
                        if (!bandera)
                        {
                            for (int i = 0; i < 1; i++)
                            {
                                fields[i] = fields[i].Trim();
                                string Id = fields[0];
                                string nombre = fields[1];
                                string descripcion = fields[2];
                                string casa = fields[3];
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
                                Datos.Instance.ListaDrogas.Agregar(DrogaListaActual);
                                var DrogaActual = new DatosFarma
                                {
                                    Nombre = nombre,
                                    Codigo = Id,
                                    Precio = precio,
                                };
                                Datos.Instance.ArbolDrogas.Insertar(DrogaActual);
                            }
                        }                           
                    }
                    bandera = false;
                }
            }
            return View(Datos.Instance.ListaDrogas);
        }
        // GET: Farma/Details/5
        public ActionResult ViewDetails()
        {
            return View(Datos.Instance.paco);
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
            var PedidoActual = new Cliente//Lista del cliente
            {
                NombreCliente = collection["NombreCliente"],
                DireccionCliente = collection["DireccionCliente"],
                NitCliente = collection["NitCliente"],
                DrogaCliente = collection["DrogaCliente"],
                CantDrogas = int.Parse(collection["CantDrogas"]),
            };
            if (PedidoActual.CantDrogas < 0)//Sii el usuario selecciona un valor menor a 0
            {
                PedidoActual.CantDrogas = PedidoActual.CantDrogas * -1;//Volverlo positivo
            }
            var BuscarDroga = new DatosFarma//Buscar la droga que el cliente desea comprar
            {
                Nombre = collection["DrogaCliente"],
            };
            var DrograEnLista = Datos.Instance.ArbolDrogas.Encontrar(DatosFarma.PorNombre, BuscarDroga);
            double Final = Convert.ToDouble(Convert.ToDouble(DrograEnLista.Precio) * PedidoActual.CantDrogas);//Se realiza la multiplicación del precio y cantidad
            var BuscarDrogaEnLista = new DatosFarma { };//Se mantienen los datos que se obtubieron al hacer la busqueda previa
            BuscarDrogaEnLista = Datos.Instance.ListaDrogas.Buscar(DatosFarma.PorNombre, DrograEnLista);
            var ExistenciaDroga = Convert.ToInt32(BuscarDrogaEnLista.Existencia) - PedidoActual.CantDrogas;
            BuscarDrogaEnLista.Existencia = ExistenciaDroga.ToString();
            if (Convert.ToInt32(BuscarDrogaEnLista.Existencia) <= 0)
            {
                BuscarDrogaEnLista.Existencia = (Reabastecer.Next(0, 15)).ToString();
            }
            Datos.Instance.paco.Agregar(BuscarDrogaEnLista);
            PedidoActual.TotalCliente = (Final).ToString();//Se guarda en la lista del cliente
            Datos.Instance.ListaClientes.Agregar(PedidoActual);
            return RedirectToAction("VerListadoPedidos");
        }
        public ActionResult VerListadoPedidos()
        {
            return View(Datos.Instance.ListaClientes);
        }        
    }
}
