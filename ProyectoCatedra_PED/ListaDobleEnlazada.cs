using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class ListaDobleEnlazada
    {
        private Nodo head;      // Primer elemento de la lista
        private Nodo tail;      // Último elemento de la lista
        private Nodo current;   // Puntero a la canción que suena actualmente

        public void Agregar(Cancion c)
        {
            Nodo nuevo = new Nodo(c);
            if (head == null)
            {
                head = tail = current = nuevo;  // Si es la primera, todos apuntan al mismo
            }
            else
            {
                tail.Next = nuevo;  // Conecta el último actual con el nuevo
                nuevo.Prev = tail;  // Conecta el nuevo con el anterior
                tail = nuevo;       // El nuevo nodo pasa a ser el final
            }
        }

        public Cancion Actual()
        {
            return current?.Data;   // Retorna los datos del nodo actual (si existe)
        }

        public Cancion Siguiente()
        {
            if (current?.Next != null)
            {
                current = current.Next;// Desplaza el puntero hacia adelante
                return current.Data;
            }
            return null;// No hay más canciones adelante
        }

        public Cancion Anterior()
        {
            if (current?.Prev != null)
            {
                current = current.Prev;// Desplaza el puntero hacia atrás
                return current.Data;
            }
            return null;// No hay canciones atrás
        }

        public List<Cancion> ObtenerTodas()
        {
            List<Cancion> lista = new List<Cancion>();
            Nodo temp = head;
            while (temp != null)
            {
                lista.Add(temp.Data);   // Agrega a la lista temporal para la interfaz
                temp = temp.Next;       // Avanza al siguiente nodo
            }
            return lista;
        }
    }
}
