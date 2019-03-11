using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;


namespace St.John.Models
{
    public class Cliente
    {
        [Display(Name = "Nombre de Cliente")]
        public string NombreCliente { get; set; }
        [Display(Name = "Direccion de Cliente")]
        public string DireccionCliente { get; set; }
        [Display(Name = "NIT de Cliente")]
        public string NitCliente { get; set; }
        [Display(Name = "Nombre de Droga")]
        public string DrogaCliente { get; set; }
        [Display(Name = "Total de Pedido")]
        public string TotalCliente { get; set; }
    }
}