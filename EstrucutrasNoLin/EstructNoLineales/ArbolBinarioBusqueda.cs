using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    public class ArbolBinarioBusqueda<T> : iEstructuraNoLineales<T> where T : IComparable
    {
        private Comparison<IComparable> Comparer = ComparerElements;

        public T Encontrar(Delegate comparer, T value)
        {
            cNodo<T> Aux = this.Raiz;
            while (Aux != null)
            {   
                if ((int)comparer.DynamicInvoke(Aux.sInformacion, value) == 0)
                {
                    return Aux.sInformacion;
                }
                else
                {
                    if ((int)comparer.DynamicInvoke(Aux.sInformacion, value)>0)
                    {
                        Aux = Aux.nIzquierda;
                    }
                    else if ((int)comparer.DynamicInvoke(Aux.sInformacion, value)<0)
                    {
                        Aux = Aux.nDerecha;
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            return default(T);
        }
        private static int ComparerElements(IComparable value, IComparable sInformacion)
        {
            return value.CompareTo(sInformacion);
        }
        private cNodo<T> Raiz { get; set; }
        public void Buscar(T value)
        {
            throw new NotImplementedException();
        }
        
        private ArbolBinarioBusqueda(cNodo<T> Raiz)
        {
            this.Raiz = Raiz;
        }
        public ArbolBinarioBusqueda()
        {

        }
        public void Insertar(T value)
        {
            if (Raiz == null)
            {
                Raiz = new cNodo<T>(value);
            }
            else
            {
                cNodo<T> Temporal = Raiz;
                bool Encontrado = false;
                while (!Encontrado)
                {
                    int ValueComp = Temporal.sInformacion.CompareTo(value);
                    if (ValueComp > 0)
                    {
                        if (Temporal.nIzquierda == null)
                        {
                            Temporal.nIzquierda = new cNodo<T>(value, Temporal);
                            return;
                        }
                        else
                        {
                            Temporal = Temporal.nIzquierda;
                        }
                    }
                    else if (ValueComp < 0)
                    {
                        if (Temporal.nDerecha == null)
                        {
                            Temporal.nDerecha = new cNodo<T>(value, Temporal);
                            return;
                        }
                        else
                        {
                            Temporal = Temporal.nDerecha;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
