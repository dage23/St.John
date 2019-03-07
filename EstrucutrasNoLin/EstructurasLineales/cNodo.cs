using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lEstructurasLineales
{
    public class cNodo<T>
    {
        public T sInformacion { get; set; }
        public cNodo<T> nSiguiente { get; set; }
        public cNodo<T> nAnterior { get; set; }

        public cNodo(T value)
        {
            sInformacion = value;
            nSiguiente = null;
            nAnterior = null;
        }
    }
}
