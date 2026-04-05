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
        private ListaDobleEnlazada playlist = new ListaDobleEnlazada(); // Estructura de datos principal

        public Form1()
        {
            InitializeComponent();
            timerCancion.Interval = 1000;
            timerCancion.Tick += TimerCancion_Tick;
            this.BackColor = System.Drawing.Color.Black;
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

        private void btnReproducir_Click(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();

            if (actual != null)
            {
                actual.Reproducir(); // Inicia NAudio
                timerCancion.Start();// Inicia contador visual
            }

        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            Cancion actual = playlist.Actual();

            if (actual != null)
            {
                actual.Detener(); // Libera el archivo de audio
                timerCancion.Stop(); // Detiene contador
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



    }
}
