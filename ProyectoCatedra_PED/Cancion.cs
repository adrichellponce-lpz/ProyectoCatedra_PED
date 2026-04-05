using NAudio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCatedra_PED
{
    public class Cancion
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string RutaArchivo { get; set; }

        private WaveOutEvent waveOut;// Manejador de salida de audio
        private AudioFileReader audioFile;// Lector del archivo MP3

        public Cancion(string titulo, string artista, string rutaArchivo)
        {
            Titulo = titulo;
            Artista = artista;
            RutaArchivo = rutaArchivo;
        }

        public void Reproducir()
        {

            audioFile = new AudioFileReader(RutaArchivo);// Carga el archivo
            waveOut = new WaveOutEvent();               // Inicializa el dispositivo
            waveOut.Init(audioFile);                    // Relaciona el archivo y la salida
            waveOut.Play();                             // Inicia reproducción
        }

        public void Detener()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                audioFile.Dispose();
            }
        }

        public TimeSpan Duracion()
        {
            return audioFile?.TotalTime ?? TimeSpan.Zero;// Tiempo total de la cancion
        }

        public TimeSpan TiempoActual()
        {
            return audioFile?.CurrentTime ?? TimeSpan.Zero;// Segundo actual
        }

        public override string ToString()
        {
            return $"{Titulo} - {Artista}";// Formato para el ListBox
        }
        public void IrA(TimeSpan tiempo)
        {
            if (audioFile != null)
            {
                audioFile.CurrentTime = tiempo;// Salto temporal en la cancion
            }
        }

    }
}
