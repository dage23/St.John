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
namespace St.John.Controllers
{
    public class FarmaController : Controller
    {
        public ActionResult Index()
        {
            return View(new List<DatosFarma>());            
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
           
            List<DatosFarma> ListaDrogas = new List<DatosFarma>();
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
                                string existecia = fields[5];
                                ListaDrogas.Add(new DatosFarma
                                {
                                    Codigo = Id,
                                    Nombre = nombre,
                                    Descricpion = descripcion,
                                    Origen = casa,
                                    Precio = precio,
                                    Existencia = existecia
                                });
                                var DrogaActual = new DatosFarma
                                {
                                    Nombre = nombre,
                                    Codigo = Id
                                };
                                Datos.Instance.ArbolDrogas.Insertar(DrogaActual);
                            }
                        }                           
                    }
                    bandera = false;
                }
            }
            return View(ListaDrogas);
        }
        // GET: Farma/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Farma/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Farma/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
    }
}
