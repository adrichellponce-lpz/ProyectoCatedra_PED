using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class Nodo
    {
        public Cancion Data { get; set; }
        public Nodo Prev { get; set; }
        public Nodo Next { get; set; }

        public Nodo(Cancion data)
        {
            Data = data;
            Prev = null;
            Next = null;
        }
    }
}
