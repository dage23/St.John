using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstrucutrasNoLin
{
    interface iEstructuraNoLineales<T>
    {
        void Insertar(T value);
        void Buscar(T value);
    }
}
