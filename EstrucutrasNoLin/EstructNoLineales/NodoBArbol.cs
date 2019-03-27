using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    public class NodoBArbol <TK,TP>
    {
        private int Grado;
        public List<NodoBArbol<TK,TP>> ListaHijos { get; set; }
        public List<Indice<TK, TP>> ListaValores { get; set; }
        public NodoBArbol(int Grado)
        {
            this.Grado = Grado;
            this.ListaHijos = ListaHijos;
            this.ListaValores = ListaValores;
        }
        public bool EsHoja
        {
            get { return this.ListaHijos.Count == 0; }
        }
        public bool ValoresMaximosAlcanzados
        {
            get { return this.ListaValores.Count == (2 * this.Grado) - 1; }
        }
        public bool ValoresMinimosContenidos
        {
            get { return this.ListaValores.Count == this.Grado - 1; }
        }
    }
}
