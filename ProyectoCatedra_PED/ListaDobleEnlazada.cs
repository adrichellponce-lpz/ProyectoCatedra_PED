using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Prueba de Github Lista Entrelazada

namespace ProyectoCatedra_PED
{
    public class ListaDobleEnlazada
    {
        private Nodo head;
        private Nodo tail;
        private Nodo current;

        public void Agregar(Cancion c)
        {
            Nodo nuevo = new Nodo(c);
            if (head == null)
            {
                head = tail = current = nuevo;
            }
            else
            {
                tail.Next = nuevo;
                nuevo.Prev = tail;
                tail = nuevo;
            }
        }

        public Cancion Actual()
        {
            return current?.Data;
        }

        public Cancion Siguiente()
        {
            if (current?.Next != null)
            {
                current = current.Next;
                return current.Data;
            }
            return null;
        }

        public Cancion Anterior()
        {
            if (current?.Prev != null)
            {
                current = current.Prev;
                return current.Data;
            }
            return null;
        }

        public List<Cancion> ObtenerTodas()
        {
            List<Cancion> lista = new List<Cancion>();
            Nodo temp = head;
            while (temp != null)
            {
                lista.Add(temp.Data);
                temp = temp.Next;
            }
            return lista;
        }
    }
}
