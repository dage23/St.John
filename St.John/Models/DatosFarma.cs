using System;
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
        public int CompareTo(object obj)
        {
            var vComparador = (DatosFarma)obj;
            return Codigo.CompareTo(vComparador.Codigo);
        }
    }  
}