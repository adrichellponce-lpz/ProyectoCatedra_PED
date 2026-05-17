using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class ArbolBusqueda
    {
        private NodoArbol raiz;

        public void Insertar(Cancion c)
        {
            raiz = InsertarRec(raiz, c);
        }

        private NodoArbol InsertarRec(NodoArbol nodo, Cancion c)
        {
            if (nodo == null) return new NodoArbol(c);

            // Comparación por título 
            int comparacion = string.Compare(c.Titulo, nodo.Data.Titulo, StringComparison.OrdinalIgnoreCase);

            if (comparacion < 0)
                nodo.Izquierda = InsertarRec(nodo.Izquierda, c);
            else if (comparacion > 0)
                nodo.Derecha = InsertarRec(nodo.Derecha, c);

            return nodo;
        }
        public Cancion Buscar(string titulo)
        {
            return BuscarRec(raiz, titulo);
        }

        private Cancion BuscarRec(NodoArbol nodo, string titulo)
        {
            if (nodo == null) return null;

            int comparacion = string.Compare(titulo, nodo.Data.Titulo, StringComparison.OrdinalIgnoreCase);

            if (comparacion == 0)
                return nodo.Data;
            else if (comparacion < 0)
                return BuscarRec(nodo.Izquierda, titulo);
            else
                return BuscarRec(nodo.Derecha, titulo);

        }
        // Recorrido in-order para obtener canciones ordenadas por título
        public List<Cancion> ObtenerOrdenadas()
        {
            List<Cancion> lista = new List<Cancion>();
            InOrder(raiz, lista);
            return lista;
        }

        private void InOrder(NodoArbol nodo, List<Cancion> lista)
        {
            if (nodo != null)
            {
                InOrder(nodo.Izquierda, lista);
                lista.Add(nodo.Data);
                InOrder(nodo.Derecha, lista);
            }
        }
    }
}
