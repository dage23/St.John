using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lEstructurasLineales
{
    public class cListaDoblementeEnlazada<T> : iEstructuraDatosLineales<T>, IEnumerable<T> where T : IComparable
    {
        private cNodo<T> nInicio { get; set; }
        static int iTamano { get; set; }
        //public cListaDoblementeEnlazada()
        //{
        //  nInicio = null;
        //  iTamano = 0;
        //}
        public void Agregar(T value)
        {            
            if (iTamano == 0)
            {
                nInicio = new cNodo<T>(value);
                iTamano = 1;
            }
            else
            {
                var nNodoAuxiliar = new cNodo<T>(value);
                var nNodoActual = nInicio;
                while (nNodoActual.nSiguiente != null)
                {
                    nNodoActual = nNodoActual.nSiguiente;
                }
                nNodoActual.nSiguiente = nNodoAuxiliar;
                nNodoAuxiliar.nAnterior = nNodoActual;
                iTamano++;
            }            
        }
        public void Eliminar(T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var nNodoActual = nInicio;
            while (nNodoActual != null)
            {
                yield return nNodoActual.sInformacion;
                nNodoActual = nNodoActual.nSiguiente;
            }
        }
        public T Get(int iCodigo)//Buscando por codigo
        {
            var GAux = nInicio;
            for (int i = 0; i < iCodigo - 1; i++)
            {
                GAux = GAux.nSiguiente;
            }
            return GAux.sInformacion;
        }
        public T GetNombre(string EmpleadoNombre)//Buscando por codigo
        {
            var GAux = nInicio;
            for (int i = 0; i < iTamano - 1; i++)
            {
                GAux = GAux.nSiguiente;
            }
            return GAux.sInformacion;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Buscar(T value)
        {
            throw new NotImplementedException();
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }
    }
}
