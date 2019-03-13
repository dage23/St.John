using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;


namespace St.John.Models
{
    public class Cliente: IComparable, IEnumerable
    {
        [Display(Name = "Nombre")]
        public string NombreCliente { get; set; }

        [Display(Name = "Direccion")]
        public string DireccionCliente { get; set; }

        [Display(Name = "NIT")]
        public string NitCliente { get; set; }

        [Display(Name = "Droga")]
        public string DrogaCliente { get; set; }            
        [Display(Name = "Total")]
        public string TotalCliente { get; set; }
        [Display(Name = "Cantidad")]
        public int CantDrogas { get; set; }
        public int CompareTo(object obj)
        {
            var vComparador = (Cliente)obj;
            return NombreCliente.CompareTo(vComparador.NombreCliente);
        }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}