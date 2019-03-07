using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using St.John.Models;
using System.Text.RegularExpressions;

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
           
            List<DatosFarma> customers = new List<DatosFarma>();
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
                String[] fields = CSV.Split(csvData);
                for (int i = 5; i < (fields.Length)-3; i++)
                {
                //    fields[i]=fields[i].TrimStart(' ', '"');
                //    fields[i] = fields[i].TrimEnd('"');
                    fields[i] = fields[i].Trim();
                    string Id = fields[i];
                    i++;
                    string nombre = fields[i];
                    i++;
                    string descripcion = fields[i];
                    i++;
                    string casa = fields[i];
                    i++;
                    string precio = fields[i];
                    i++;
                    string existecia = fields[i];
                    customers.Add(new DatosFarma
                    {
                        Codigo = Id,
                        Nombre = nombre,
                        Descricpion = descripcion,
                        Origen = casa,
                        Precio = precio,
                        Existencia = existecia
                    });
                }
            }
            return View(customers);
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
    }
}
