﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace St.John.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Pendiente...";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Pendiente...";

            return View();
        }
    }
}