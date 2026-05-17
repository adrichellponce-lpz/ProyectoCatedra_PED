using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class QuickSort
    {
        //Metodo público que inicia el proceso de ordenanimento
        public void Ordenar(List<Cancion> lista, Func<Cancion, IComparable> criterio)
        {
            Quick(lista, 0, lista.Count - 1, criterio);
        }

        private void Quick(List<Cancion> lista, int inicio, int fin, Func<Cancion, IComparable> criterio)
        {
            if (inicio < fin)
            {
                int pivote = Particion(lista, inicio, fin, criterio);
                Quick(lista, inicio, pivote - 1, criterio);
                Quick(lista, pivote + 1, fin, criterio);
            }

        }
        private int Particion(List<Cancion> lista, int inicio, int fin, Func<Cancion, IComparable> criterio)
        {
            Cancion pivote = lista[fin];
            int i = inicio - 1;

            for (int j = inicio; j < fin; j++)
            {
                if (criterio(lista[j]).CompareTo(criterio(pivote)) <= 0)
                {
                    i++;
                    (lista[i], lista[j]) = (lista[j], lista[i]);
                }
            }

            (lista[i + 1], lista[fin]) = (lista[fin], lista[i + 1]);
            return i + 1;
        }
    }
}
