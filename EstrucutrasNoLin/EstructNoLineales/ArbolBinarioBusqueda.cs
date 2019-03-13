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
            return default(T);//No se retorna value, es solo para que no de error a la ejecucción
        }
        private static int ComparerElements(IComparable value, IComparable sInformacion)
        {
            return value.CompareTo(sInformacion);
        }
        //    public cNodo<T> Encontrar(T value)
        //    {
        //        foreach (cNodo<T> items in Traversal(Raiz))
        //        {
        //            if (items.sInformacion.Equals(value))
        //            {
        //                return items;
        //            }
        //        }
        //        return null;                
        //    }
        //    private IEnumerable<cNodo<T>> Traversal(cNodo<T> Nodo)
        //    {
        //        if (Nodo.nIzquierda != null)
        //        {
        //            foreach (cNodo<T> NodoI in Traversal(Nodo.nIzquierda))
        //            {
        //                yield return NodoI;
        //            }
        //        }
        //        yield return Nodo;
        //        if (Nodo.nDerecha != null)
        //        {
        //            foreach (cNodo<T> NodoD in Traversal(Nodo.nDerecha))
        //            {
        //                yield return NodoD;
        //            }
        //        }
        //    }
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
                Raiz = new cNodo<T>(value);//Se manda la información a la raiz
            }
            else
            {
                cNodo<T> Temporal = Raiz;
                bool Encontrado = false;
                while (!Encontrado)//Recorrido del arbol
                {
                    int ValueComp = Temporal.sInformacion.CompareTo(value);//Comprara para devolver valores de -1 y +1
                    if (ValueComp > 0)//Valores -1 van a la izquierda
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
                    else if (ValueComp < 0)//Valores +1 van a la derecha
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
