using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ProyectoCatedra_PED
{
    public partial class Form1 : Form
    {
        private ListaDobleEnlazada playlist = new ListaDobleEnlazada(); 
        //Árbol de búsqueda para encontrar canciones más rápido por título
        private ArbolBusqueda arbolCanciones = new ArbolBusqueda();
        //Objeto encargado de ordenar canciones usando QuickSort
        private QuickSort quickSort = new QuickSort();



        public Form1()
        {
            InitializeComponent();
            timerCancion.Interval = 1000;
            timerCancion.Tick += TimerCancion_Tick;
            this.BackColor = System.Drawing.Color.Black;

            cmbOrdenar.Items.AddRange(new object[]
           {
              "Orden Original",
              "Ordenar por Título",
              "Ordenar por Artista"
           });
            cmbOrdenar.SelectedIndex = 0;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos MP3|*.mp3";// Filtro para solo mostrar audio
            ofd.Title = "Selecciona una canción";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string ruta = ofd.FileName;
                string titulo = Path.GetFileNameWithoutExtension(ruta);
                Cancion nueva = new Cancion(titulo, "Desconocido", ruta);
                playlist.Agregar(nueva); // Guarda en lista doble
                listboxPlaylist.Items.Add(nueva);// Muestra en interfaz
            }
        }

        private void TimerCancion_Tick(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();// Obtiene nodo actual
            if (actual != null)
            {
                TimeSpan actualTime = actual.TiempoActual();
                TimeSpan totalTime = actual.Duracion();

                lblInicio.Text = actualTime.ToString(@"mm\:ss");// Tiempo transcurrido
                lblFin.Text = totalTime.ToString(@"mm\:ss");// Tiempo total

                if (totalTime.TotalSeconds > 0)
                {
                    int porcentaje = (int)((actualTime.TotalSeconds / totalTime.TotalSeconds) * 100);
                    progressBarCancion.Value = Math.Min(porcentaje, 100); //mueve la barra
                }
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();
            if (actual == null) return;


            if (btnDetener.Text == "Pausa")
            {
                if (actual.EstaReproduciendo())
                {
                    actual.Pausar();//Pausa la reproducción de la canción
                    timerCancion.Stop(); // Detiene la actualización de la barra dw progreso
                    lblCancion.Text = $"Pausado: {actual.Titulo}";
                    btnDetener.Text = "Reanudar"; // cambia texto del botón
                    listboxPlaylist.SelectedIndex = -1;
                }
            }
            else
            {
                actual.Reproducir(); //Reanuda la reproducción 
                timerCancion.Start(); //Reanuda la actualización de la barra de  progreso
                lblCancion.Text = $"Reproduciendo: {actual.Titulo}";//Actualiza el label
                btnDetener.Text = "Pausa";
                listboxPlaylist.SelectedIndex = -1;
            }

        }

        private void progressBarCancion_Click(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();

            if (actual != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;


                double porcentaje = (double)me.X / progressBarCancion.Width;// Calcula posición del clic

                TimeSpan duracion = actual.Duracion();

                TimeSpan nuevoTiempo = TimeSpan.FromSeconds(duracion.TotalSeconds * porcentaje);

                actual.IrA(nuevoTiempo);// Salta al segundo elegido
            }
        }

        private void btnReproducir_Click_1(object sender, EventArgs e)
        {
            int indice = listboxPlaylist.SelectedIndex;//Obtiene la posición seleccionada en el listbox
            if (indice >= 0)
            {
                Cancion seleccionada = playlist.SeleccionarPorIndice(indice);//Busca la canción seleccionada en la playlist

                Cancion actual = playlist.Actual();//Obtiene la canción actual para detenerla
                if (actual != null)
                {
                    actual.Detener();
                }

                //  Reproducir la canción nueva desde el inicio
                seleccionada.Reproducir();
                lblCancion.Text = $"Reproduciendo: {seleccionada}";//Actualiza ellabel mostrando la canción actual
                timerCancion.Start();//Inicia la barra de progreso
            }
            else
            {
                MessageBox.Show("Selecciona una canción de la lista primero.");
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {

            Cancion actual = playlist.Actual(); //Obtiene la canción actual
            if (actual != null) actual.Detener(); //Detiene la canción actual antes de cambiar

            Cancion anterior = playlist.Anterior();//Obtiene la canción anterior y la reproduce
            if (anterior == null)
            {
                // Averiguamos cuál es el último índice disponible en el ListBox
                int ultimoIndice = listboxPlaylist.Items.Count - 1;

                if (ultimoIndice >= 0)
                {
                    // Forzamos a la playlist a irse directamente a la última canción
                    anterior = playlist.SeleccionarPorIndice(ultimoIndice);
                }
            }
            if (anterior != null)
            {
                anterior.Reproducir();
                lblCancion.Text = $"Reproduciendo: {anterior}";//Actualiza el texto
                timerCancion.Start();//Inicia el timer
                listboxPlaylist.SelectedIndex = listboxPlaylist.Items.IndexOf(anterior);
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();
            if (actual != null) actual.Detener(); // Detiene completamente la actual

            Cancion siguiente = playlist.Siguiente();//Obtiene la siguiente canción
            if (siguiente == null)
            {
                // Forzamos a la playlist a regresar a la primera canción (índice 0)
                siguiente = playlist.SeleccionarPorIndice(0);
            }
            if (siguiente != null)
            {
                siguiente.Reproducir(); // Reproduce la siguiente canción
                lblCancion.Text = $"Reproduciendo: {siguiente}";// Actualiza el label
                timerCancion.Start();
                listboxPlaylist.SelectedIndex = listboxPlaylist.Items.IndexOf(siguiente); ;
            }
        }

        private void listboxPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = listboxPlaylist.SelectedIndex;//Obtiene el índice seleccionado
            if (indice >= 0)
            {
                Cancion seleccionada = playlist.SeleccionarPorIndice(indice);
                lblCancion.Text = $"Seleccionada: {seleccionada}";// Muestra la canción seleccionada
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene el texto y elimina espacios innecesarios
            string titulo = txtBuscar.Text.Trim().ToLower();

            // Busca coincidencias parciales
            Cancion encontrada = playlist.ObtenerTodas()
                .FirstOrDefault(c => c.Titulo.ToLower().Contains(titulo));

            if (encontrada != null)
            {
                MessageBox.Show($"Canción encontrada: {encontrada.Titulo} - {encontrada.Artista}");

                // Selecciona automáticamente la canción encontrada
                listboxPlaylist.SelectedItem = encontrada;
            }
            else
            {
                MessageBox.Show("Canción no encontrada.");
            }
        }

        private void btnMostrarOrdenadas_Click(object sender, EventArgs e)
        {
            string opcion = cmbOrdenar.SelectedItem?.ToString() ?? "Orden Original";

            var listaCanciones = playlist.ObtenerTodas();

            switch (opcion)
            {
                case "Ordenar por Título":
                    quickSort.Ordenar(listaCanciones, c => c.Titulo);
                    break;

                case "Ordenar por Artista":
                    quickSort.Ordenar(listaCanciones, c => c.Artista);
                    break;

                case "Orden Original":
                    // No ordena, mantiene el orden original de la playlist
                    break;
            }
            // Actualiza el ListBox
            playlist = new ListaDobleEnlazada();
            listboxPlaylist.Items.Clear();
            foreach (var c in listaCanciones)
            {
                playlist.Agregar(c);
            }
            foreach (var c in listaCanciones)
                listboxPlaylist.Items.Add(c);
            listboxPlaylist.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            int indice = listboxPlaylist.SelectedIndex;
            if (indice >= 0)
            {
                // Obtener la canción seleccionada
                Cancion seleccionada = playlist.SeleccionarPorIndice(indice);

                // Si la canción está reproduciendose, se detiene
                if (seleccionada != null && seleccionada.EstaReproduciendo())
                {
                    seleccionada.Detener();
                }

                // Elimina la canción de la lista doblemente enlazada
                playlist.EliminarActual();

                // Elimina la canción del ListBox
                listboxPlaylist.Items.RemoveAt(indice);
                MessageBox.Show($" Canción Eliminada ");

            }
            else
            {
                MessageBox.Show("Selecciona una canción para eliminar.");
            }
        }

        private void btnDetener_Click_1(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();
            if (actual == null) return;


            if (btnDetener.Text == "Pausa")
            {
                if (actual.EstaReproduciendo())
                {
                    actual.Pausar();//Pausa la reproducción de la canción
                    timerCancion.Stop(); // Detiene la actualización de la barra dw progreso
                    lblCancion.Text = $"Pausado: {actual.Titulo}";
                    btnDetener.Text = "Reanudar"; // cambia texto del botón
                    listboxPlaylist.SelectedIndex = -1;
                }
            }
            else
            {
                actual.Reproducir(); //Reanuda la reproducción 
                timerCancion.Start(); //Reanuda la actualización de la barra de  progreso
                lblCancion.Text = $"Reproduciendo: {actual.Titulo}";//Actualiza el label
                btnDetener.Text = "Pausa";
                listboxPlaylist.SelectedIndex = -1;
            }
        }
    }
}
