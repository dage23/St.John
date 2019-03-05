using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace St.John.Controllers
{
    public class FarmaController : Controller
    {
        // GET: Farma
        public ActionResult Index()
        {
            return View();
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
