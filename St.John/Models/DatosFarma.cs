using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace St.John.Models
{
    public class DatosFarma
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descricpion { get; set; }
        public string Origen { get; set; }
        public double Precio { get; set; }
        public int Existencia { get; set; }
    }
}