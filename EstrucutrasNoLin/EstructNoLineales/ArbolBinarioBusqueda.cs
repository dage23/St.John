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
                    if (ValueComp < 0)//Valores -1 van a la izquierda
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
                    else if (ValueComp > 0)//Valores +1 van a la derecha
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
                }
            }
        }            
    }
}
