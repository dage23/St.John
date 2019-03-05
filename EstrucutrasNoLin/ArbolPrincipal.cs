using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    class ArbolPrincipal
    {
        public class cNodo<T>
        {
            public T sInformacion { get; set; }
            public cNodo<T> nDerecha { get; set; }
            public cNodo<T> nIzquierda { get; set; }

            public cNodo(T value)
            {
                sInformacion = value;
                nDerecha = null;
                nIzquierda = null;
            }
        }

    }
}
