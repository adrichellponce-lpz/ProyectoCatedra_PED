using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class Nodo
    {
        public Cancion Data { get; set; }   // Objeto Cancion almacenado en el nodo
        public Nodo Prev { get; set; }      // Puntero al nodo anterior (enlace hacia atrás)
        public Nodo Next { get; set; }      // Puntero al siguiente nodo (enlace hacia adelante)

        public Nodo(Cancion data)
        {
            Data = data;    // Asigna la canción al crear el nodo
            Prev = null;    //Al inicializarse no se cuenta ni con un enlace previo ni uno siguiente
            Next = null;   // No cuenta ningun enlace posterior
        }
    }
}
