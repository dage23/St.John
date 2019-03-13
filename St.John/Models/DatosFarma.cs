﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace St.John.Models
{
    public class DatosFarma : IComparable
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descricpion { get; set; }
        public string Origen { get; set; }
        public string Precio { get; set; }
        public string Existencia { get; set; }

        public static Comparison<DatosFarma> PorNombre = delegate (DatosFarma s1, DatosFarma s2)
          {
              return s1.Nombre.CompareTo(s2.Nombre);
          };
        public int CompareTo(object obj)
        {
            var vComparador = (DatosFarma)obj;
            return Nombre.CompareTo(vComparador.Nombre);
        }
    }  
}