using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    internal class NodoArbol
    {
        public Cancion Data { get; set; }
        public NodoArbol Izquierda { get; set; }
        public NodoArbol Derecha { get; set; }

        public NodoArbol(Cancion data)
        {
            Data = data;
            Izquierda = null;
            Derecha = null;
        }
    }
}
