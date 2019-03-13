using System;
using System.Collections;
using System.Collections.Generic;

namespace lEstructurasLineales
{
    public class cListaDoblementeEnlazada<T> : iEstructuraDatosLineales<T>, IEnumerable<T> where T : IComparable
    {
        private cNodo<T> nInicio { get; set; }
        static int iTamano { get; set; }
        public T Buscar(Delegate comparer, T value)
        {
            var current = nInicio;
            var outputList = new cListaDoblementeEnlazada<T>();
            while (current != null)
            {
                if ((int)comparer.DynamicInvoke(current.sInformacion, value) == 0)
                {
                    return current.sInformacion;
                }
                current = current.nSiguiente;
            }
            return current.sInformacion;
        }
        public void Agregar(T value)
        {            
            if (iTamano == 0||(nInicio==null))
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
