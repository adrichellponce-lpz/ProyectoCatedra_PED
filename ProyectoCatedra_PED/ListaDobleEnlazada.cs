using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class ListaDobleEnlazada
    {
        private Nodo head;//Apunta al primer nodo de la lista
        private Nodo tail;//Apunta al último nodo de la lista
        private Nodo current;//Guarda la canció actual seleccionada o reproduciéndose

        public void Agregar(Cancion c)
        {
            Nodo nuevo = new Nodo(c);//Crea un nuevo nodo con la canción recibida
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
            return current?.Data;//Devuelve la canción actual si existe
        }

        public Cancion Siguiente()
        {
            if (current?.Next != null)//Verifica que exista una canción siguiente
            {
                current = current.Next;
                return current.Data;
            }
            return null;
        }

        public Cancion Anterior()
        {
            if (current?.Prev != null)//Verifica si existe una canción anterior
            {
                current = current.Prev;
                return current.Data;
            }
            return null;
        }

        public List<Cancion> ObtenerTodas()
        {
            List<Cancion> lista = new List<Cancion>();//Crea una lista donde se almacenarán las canciones
            Nodo temp = head;                         //Comienza desde el primer nodo
            while (temp != null)                      //Recorre toda la lista hasta llegar al final
            {
                lista.Add(temp.Data); //Agrega la canción actual a la lista
                temp = temp.Next; //Avanza al siguiente nodo
            }
            return lista;
        }

        public Cancion SeleccionarPorIndice(int indice)
        {
            Nodo temp = head;
            int i = 0;//Contador para comparar posiciones
            while (temp != null)//Recorre toda la lista
            {
                if (i == indice)//Si encuentra el índice solicitado actualiza el nodo actual y retorna la canción encontrada
                {
                    current = temp;
                    return temp.Data;
                }
                temp = temp.Next;
                i++;
            }
            return null;
        }

        public void EliminarActual()
        {
            if (current == null) return;//Si no existe una canción actual, no hace nada

            // Si existe un nodo anterior, conecta el anterior con el siguiente
            if (current.Prev != null)
                current.Prev.Next = current.Next;
            else
                head = current.Next;

            // Si existe un siguiente nodo,conecta el siguiente con el nodo anterior
            if (current.Next != null)
                current.Next.Prev = current.Prev;
            else
                tail = current.Prev; // si era el último nodo, actualiza la cols

            // Mueve el puntero actual al siguiente nodo disponible y si no existe, vuelve al inicio
            current = current.Next ?? head;
        }
    }
}
