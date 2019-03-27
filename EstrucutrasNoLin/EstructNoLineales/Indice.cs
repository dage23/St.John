using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    public class Indice<TK, TP> : IEquatable<Indice<TK, TP>>
    {
        public TK ID { get; set; }
        public TP Apuntador { get; set; }
        public bool Equals(Indice<TK, TP> other)
        {
            return this.ID.Equals(other.ID) && this.Apuntador.Equals(other.Apuntador);
        }
    }
}
