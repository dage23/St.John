using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    class cNodo<T>
    {
        public T sInformacion { get; set; }
        public cNodo<T> nDerecha { get; set; }
        public cNodo<T> nIzquierda { get; set; }
        public cNodo<T> nPadre { get; set; }
        public bool Raiz { get { return nPadre == null; } }
        public bool Hoja { get { return nIzquierda == null && nDerecha == null; } }
        public cNodo(T value, cNodo<T> nPadre, cNodo<T> nIzquierda ,cNodo<T> nDerecha)
        {
            sInformacion = value;
            this.nDerecha = nDerecha;
            this.nIzquierda = nIzquierda;
            this.nPadre = nPadre;
        }
        public cNodo(T value)
        {
            sInformacion = value;
        }
        public cNodo(T value, cNodo<T> nPadre)
        {
            sInformacion = value;
            this.nPadre = nPadre;
        }
    }
}
