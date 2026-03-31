using NAudio;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Prueba de Github

namespace ProyectoCatedra_PED
{
    public class Cancion
    {
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string RutaArchivo { get; set; }

        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;

        public Cancion(string titulo, string artista, string rutaArchivo)
        {
            Titulo = titulo;
            Artista = artista;
            RutaArchivo = rutaArchivo;
        }

        public void Reproducir()
        {
            audioFile = new AudioFileReader(RutaArchivo);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFile);
            waveOut.Play();
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
            return audioFile?.TotalTime ?? TimeSpan.Zero;
        }

        public TimeSpan TiempoActual()
        {
            return audioFile?.CurrentTime ?? TimeSpan.Zero;
        }

        public override string ToString()
        {
            return $"{Titulo} - {Artista}";
        }
    }
}
