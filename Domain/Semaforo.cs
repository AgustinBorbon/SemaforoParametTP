using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domain
{
    public class Semaforo
    {
        public string Nombre { get; set; }
        public Color ColorActual = new Color();

        public Button Boton = new Button();

        public List<string> Colores = new List<string>();

        private List<SemaphoreSlim> semaphores = new List<SemaphoreSlim>();

        public Semaforo()
        {


        }

        public Semaforo(params SemaphoreSlim[] lista)
        {
            foreach (var semaphore in lista)
            {
                semaphores.Add(semaphore);
            }
        }

        public void Subscribe(Step paso, ColorLuz color)
        {
            if (color == ColorLuz.Rojo)
                semaphores.Add(paso.Rojo.GetSemaphore());
            if (color == ColorLuz.Verde)
                semaphores.Add(paso.Verde.GetSemaphore());

            Colores.Add(color.ToString());
        }

        public void Ejecutar()
        {
            while(true)
            {
                for (int i = 0; i < semaphores.Count; i++)
                {
                    Console.WriteLine($"{Nombre} esperando el color {Colores[i]}");

                    var sem = semaphores[i];

                    if (Colores[i] == "Rojo")
                        Boton.BackColor = Color.Red;
                    else
                        Boton.BackColor = Color.Green;

                    sem.Wait();
                }
            }
        }
    }

    public enum ColorLuz
    {
        Verde,
        Rojo
    }
}
