using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using St.John.Models;
using EstrucutrasNoLin;
namespace St.John.Gelpers
{
    public class Datos
    {
        private static Datos _instance = null;
        public static Datos Instance
        {
            get
            {
                if (_instance == null) _instance = new Datos();
                {
                    return _instance;
                }
            }
        }
        public ArbolBinarioBusqueda<DatosFarma> ArbolDrogas = new ArbolBinarioBusqueda<DatosFarma>();
    }
}