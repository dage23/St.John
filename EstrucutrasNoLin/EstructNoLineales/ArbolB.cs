using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    public class ArbolB<TK, TP> where TK : IComparable<TK>
    {
        public NodoBArbol<TK, TP> Raiz { get; private set; }
        public int Grado { get; private set; }
        public int Altura { get; private set; }
        public ArbolB()
        {

        }
        public ArbolB(int Grado)
        {
            if (Grado < 2)
            {
                throw new ArgumentException("Grado de arbol debe de ser mayor a 2");
            }
            this.Raiz = new NodoBArbol<TK, TP>(Grado);
            this.Grado = Grado;
            this.Altura = 1;
        }
        public Indice<TK, TP> Buscar(TK Id)
        {
            return this.BusquedaInterna(this.Raiz, Id);
        }

        public void Insertar(TK nuevoID, TP nuevoApuntador)
        {
            if (!this.Raiz.ValoresMaximosAlcanzados)
            {
                this.InsertarEnNodoNoLLeno(this.Raiz, nuevoID, nuevoApuntador);
                return;
            }

            NodoBArbol<TK, TP> viejaRaiz = this.Raiz;
            this.Raiz = new NodoBArbol<TK, TP>(this.Grado);
            this.Raiz.ListaHijos.Add(viejaRaiz);
            this.DividirHijo(this.Raiz, 0, viejaRaiz);
            this.InsertarEnNodoNoLLeno(this.Raiz, nuevoID, nuevoApuntador);
            this.Altura++;
        }

        public void Eliminar(TK IdParaEliminar)
        {
            this.EliminarInterno(this.Raiz, IdParaEliminar);

            if (this.Raiz.ListaValores.Count == 0 && !this.Raiz.EsHoja)
            {
                this.Raiz = this.Raiz.ListaHijos.Single();
                this.Altura--;
            }
        }

        public void EliminarInterno(NodoBArbol<TK, TP> NodoTemporal, TK IdParaEliminar)
        {
            int i = NodoTemporal.ListaValores.TakeWhile(indice => IdParaEliminar.CompareTo(indice.ID) > 0).Count();

            if (i < NodoTemporal.ListaValores.Count && NodoTemporal.ListaValores[i].ID.CompareTo(IdParaEliminar) == 0)
            {
                this.EliminarIDNodo(NodoTemporal, IdParaEliminar, i);
                return;
            }

            if (!NodoTemporal.EsHoja)
            {
                this.EliminarIDSubarbol(NodoTemporal, IdParaEliminar, i);
            }
        }

        private void EliminarIDSubarbol(NodoBArbol<TK, TP> NodoPadre, TK IdParaEliminar, int IndiceSubarbolEnNodo)
        {
            NodoBArbol<TK, TP> NodoTemporalHijo = NodoPadre.ListaHijos[IndiceSubarbolEnNodo];

            if (NodoTemporalHijo.ValoresMinimosContenidos)
            {
                int IndiceIzquierdo = IndiceSubarbolEnNodo - 1;
                NodoBArbol<TK, TP> HermanoIzquierdo = IndiceSubarbolEnNodo > 0 ? NodoPadre.ListaHijos[IndiceIzquierdo] : null;

                int IndiceDerecho = IndiceSubarbolEnNodo + 1;
                NodoBArbol<TK, TP> HermanoDerecho = IndiceSubarbolEnNodo < NodoPadre.ListaHijos.Count - 1 ? NodoPadre.ListaHijos[IndiceDerecho] : null;

                if (HermanoIzquierdo != null && HermanoIzquierdo.ListaValores.Count > this.Grado - 1)
                {
                    NodoTemporalHijo.ListaValores.Insert(0, NodoPadre.ListaValores[IndiceSubarbolEnNodo]);
                    NodoPadre.ListaValores[IndiceSubarbolEnNodo] = HermanoIzquierdo.ListaValores.Last();
                    HermanoIzquierdo.ListaValores.RemoveAt(HermanoIzquierdo.ListaValores.Count - 1);

                    if (!HermanoIzquierdo.EsHoja)
                    {
                        NodoTemporalHijo.ListaHijos.Insert(0, HermanoIzquierdo.ListaHijos.Last());
                        HermanoIzquierdo.ListaHijos.RemoveAt(HermanoIzquierdo.ListaHijos.Count - 1);
                    }
                }
                else if (HermanoDerecho != null && HermanoDerecho.ListaValores.Count > this.Grado - 1)
                {
                    NodoTemporalHijo.ListaValores.Add(NodoPadre.ListaValores[IndiceSubarbolEnNodo]);
                    NodoPadre.ListaValores[IndiceSubarbolEnNodo] = HermanoDerecho.ListaValores.First();
                    HermanoDerecho.ListaValores.RemoveAt(0);

                    if (!HermanoDerecho.EsHoja)
                    {
                        NodoTemporalHijo.ListaHijos.Add(HermanoDerecho.ListaHijos.First());
                        HermanoDerecho.ListaHijos.RemoveAt(0);
                    }
                }
                else
                {
                    if (HermanoIzquierdo != null)
                    {
                        NodoTemporalHijo.ListaValores.Insert(0, NodoPadre.ListaValores[IndiceSubarbolEnNodo]);

                        var ViejosValores = NodoTemporalHijo.ListaValores;

                        NodoTemporalHijo.ListaValores = HermanoIzquierdo.ListaValores;
                        NodoTemporalHijo.ListaValores.AddRange(ViejosValores);

                        if (!HermanoIzquierdo.EsHoja)
                        {
                            var ViejosHijos = NodoTemporalHijo.ListaHijos;
                            NodoTemporalHijo.ListaHijos = HermanoIzquierdo.ListaHijos;
                            NodoTemporalHijo.ListaHijos.AddRange(ViejosHijos);
                        }

                        NodoPadre.ListaHijos.RemoveAt(IndiceIzquierdo);
                        NodoPadre.ListaValores.RemoveAt(IndiceSubarbolEnNodo);
                    }
                    else
                    {
                        NodoTemporalHijo.ListaValores.Add(NodoPadre.ListaValores[IndiceSubarbolEnNodo]);
                        NodoTemporalHijo.ListaValores.AddRange(HermanoDerecho.ListaValores);

                        if (!HermanoDerecho.EsHoja)
                        {
                            NodoTemporalHijo.ListaHijos.AddRange(HermanoDerecho.ListaHijos);
                        }

                        NodoPadre.ListaHijos.RemoveAt(IndiceDerecho);
                        NodoPadre.ListaValores.RemoveAt(IndiceSubarbolEnNodo);
                    }
                }
            }
            this.EliminarInterno(NodoTemporalHijo, IdParaEliminar);
        }

        private void EliminarIDNodo(NodoBArbol<TK, TP> NodoTemp, TK IdAEliminar, int IndiceIDenNodo)
        {
            if (NodoTemp.EsHoja)
            {
                NodoTemp.ListaValores.RemoveAt(IndiceIDenNodo);
                return;
            }

            NodoBArbol<TK, TP> HijoPredecesor = NodoTemp.ListaHijos[IndiceIDenNodo];
            if (HijoPredecesor.ListaValores.Count >= this.Grado)
            {
                Indice<TK, TP> IndicePredecesor = this.BorrarPredecesor(HijoPredecesor);
                NodoTemp.ListaValores[IndiceIDenNodo] = IndicePredecesor;
            }
            else
            {
                NodoBArbol<TK, TP> HijoSucesor = NodoTemp.ListaHijos[IndiceIDenNodo + 1];

                if (HijoSucesor.ListaValores.Count >= this.Grado)
                {
                    Indice<TK, TP> Sucesor = this.BorrarSucesor(HijoPredecesor);
                    NodoTemp.ListaValores[IndiceIDenNodo] = Sucesor;
                }
                else
                {
                    HijoPredecesor.ListaValores.Add(NodoTemp.ListaValores[IndiceIDenNodo]);
                    HijoPredecesor.ListaValores.AddRange(HijoSucesor.ListaValores);
                    HijoPredecesor.ListaHijos.AddRange(HijoSucesor.ListaHijos);

                    NodoTemp.ListaValores.RemoveAt(IndiceIDenNodo);
                    NodoTemp.ListaHijos.RemoveAt(IndiceIDenNodo + 1);

                    this.EliminarInterno(HijoPredecesor, IdAEliminar);
                }
            }
        }

        private Indice<TK, TP> BorrarPredecesor(NodoBArbol<TK, TP> Nodo)
        {
            if (Nodo.EsHoja)
            {
                var Resultado = Nodo.ListaValores[Nodo.ListaValores.Count - 1];
                Nodo.ListaValores.RemoveAt(Nodo.ListaValores.Count - 1);
                return Resultado;
            }
            return this.BorrarPredecesor(Nodo.ListaHijos.Last());
        }

        private Indice<TK, TP> BorrarSucesor(NodoBArbol<TK, TP> Nodo)
        {
            if (Nodo.EsHoja)
            {
                var Resultado = Nodo.ListaValores[0];
                Nodo.ListaValores.RemoveAt(0);
                return Resultado;
            }
            return this.BorrarPredecesor(Nodo.ListaHijos.First());
        }

        private Indice<TK, TP> BusquedaInterna(NodoBArbol<TK, TP> NodoTempo, TK Id)
        {
            int i = NodoTempo.ListaValores.TakeWhile(indice => Id.CompareTo(indice.ID) > 0).Count();

            if (i < NodoTempo.ListaValores.Count && NodoTempo.ListaValores[i].ID.CompareTo(Id) == 0)
            {
                return NodoTempo.ListaValores[i];
            }
            return NodoTempo.EsHoja ? null : this.BusquedaInterna(NodoTempo.ListaHijos[i], Id);
        }

        private void DividirHijo(NodoBArbol<TK, TP> NodoPapa, int IndiceNodoaDividir, NodoBArbol<TK, TP> NodoaDividir)
        {
            var NuevoNodo = new NodoBArbol<TK, TP>(this.Grado);

            NodoPapa.ListaValores.Insert(IndiceNodoaDividir, NodoaDividir.ListaValores[this.Grado - 1]);
            NodoaDividir.ListaHijos.Insert((IndiceNodoaDividir), NuevoNodo);

            NuevoNodo.ListaValores.AddRange(NodoaDividir.ListaValores.GetRange(this.Grado, this.Grado - 1));

            NodoaDividir.ListaValores.RemoveRange(this.Grado - 1, this.Grado);

            if (NodoaDividir.EsHoja)
            {
                NuevoNodo.ListaHijos.AddRange(NodoaDividir.ListaHijos.GetRange(this.Grado, this.Grado));
                NodoaDividir.ListaHijos.RemoveRange(this.Grado, this.Grado);
            }
        }

        private void InsertarEnNodoNoLLeno(NodoBArbol<TK, TP> NodoTempo, TK nuevaID, TP nuevoApuntador)
        {
            int PosicionaInsertar = NodoTempo.ListaValores.TakeWhile(entry => nuevaID.CompareTo(entry.ID) >= 0).Count();

            if (NodoTempo.EsHoja)
            {
                NodoTempo.ListaValores.Insert(PosicionaInsertar, new Indice<TK, TP>() { ID = nuevaID, Apuntador = nuevoApuntador });
                return;
            }

            NodoBArbol<TK, TP> Hijo = NodoTempo.ListaHijos[PosicionaInsertar-1];
            if (Hijo.ValoresMaximosAlcanzados)
            {
                this.DividirHijo(NodoTempo, PosicionaInsertar, Hijo);
                if (nuevaID.CompareTo(NodoTempo.ListaValores[PosicionaInsertar].ID) > 0)
                {
                    PosicionaInsertar++;
                }
            }

            this.InsertarEnNodoNoLLeno(NodoTempo.ListaHijos[PosicionaInsertar], nuevaID, nuevoApuntador);
        }
    }
}
