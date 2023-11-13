using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SemaforoParametTP
{
    public class Program
    {
        static void Main()
        {
            // Ruta del archivo de texto
            string filePath = @"C:\Users\Agustin\source\repos\aaasdasdasd\SemaforoParametTP\Configs.txt";

            
                // Lee todas las líneas del archivo
                string[] lines = File.ReadAllLines(filePath);

                // Crea una matriz para almacenar los datos
                string[,] matriz = new string[lines.Length, 6];

                // Procesa cada línea
                for (int i = 0; i < lines.Length; i++)
                {
                    // Divide la línea en partes usando ';' como delimitador
                    string[] parts = lines[i].Split(';');

                    // Copia los valores en la matriz
                    for (int j = 0; j < parts.Length; j++)
                    {
                        matriz[i, j] = parts[j];
                    }
                }

                // Crea los objetos Step y Secuencia
                Step step1 = new Step();
                Step step2 = new Step();
                Step step3 = new Step();

                
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    // Contadores para cada secuencia
                    int verdeCount = 0;
                    int rojoCount = 0;

                    for (int j = 0; j < matriz.GetLength(1); j++)
                    {
                        // Verifica si la letra es 'V' o 'R' en la columna 0
                        if (matriz[i, j] == "V")
                        {
                            verdeCount++;
                        }
                        else if (matriz[i, j] == "R")
                        {
                            rojoCount++;
                        }
                    }

                    // Asigna los valores de verdeCount y rojoCount a los objetos Step
                    if (i == 0)
                    {
                        step1.Verde = new Secuencia(verdeCount);
                        step1.Rojo = new Secuencia(rojoCount);
                    }
                    else if (i == 1)
                    {
                        step2.Verde = new Secuencia(verdeCount);
                        step2.Rojo = new Secuencia(rojoCount);
                    }
                    else if (i == 2)
                    {
                        step3.Verde = new Secuencia(verdeCount);
                        step3.Rojo = new Secuencia(rojoCount);
                    }
                }
                Console.WriteLine($"Step1: Verde={step1.Verde.MaxCount}, Rojo={step1.Rojo.MaxCount}");
                Console.WriteLine($"Step2: Verde={step2.Verde.MaxCount}, Rojo={step2.Rojo.MaxCount}");
                Console.WriteLine($"Step3: Verde={step3.Verde.MaxCount}, Rojo={step3.Rojo.MaxCount}");

            Semaforo semaforo1 = new Semaforo();
            Semaforo semaforo2 = new Semaforo();
            Semaforo semaforo3 = new Semaforo();
            Semaforo semaforo4 = new Semaforo();
            Semaforo semaforo5 = new Semaforo();

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    // Obtener el objeto Step correspondiente
                    Step step;
                    if (i == 0) step = step1;
                    else if (i == 1) step = step2;
                    else step = step3;

                    // Inicializar semaforo con un valor por defecto
                    Semaforo semaforo = null;

                    if (j == 0) semaforo = semaforo1;
                    else if (j == 1) semaforo = semaforo2;
                    else if (j == 2) semaforo = semaforo3;
                    else if (j == 3) semaforo = semaforo4;
                    else if (j == 4) semaforo = semaforo5;

                    // Verifica si la letra es 'V' o 'R' en la columna 0
                    if (matriz[i, j] == "V" && semaforo != null)
                    {
                        // Llama al método Subscribe para semaforoj con los argumentos actuales de i y j
                        semaforo.Subscribir(step, "V");
                        semaforo.Nombre = "semaforo "+j;
                    }
                    else if (matriz[i, j] == "R" && semaforo != null)
                    {
                        // Llama al método Subscribe para semaforoj con los argumentos actuales de i y j
                        semaforo.Subscribir(step, "R");
                        semaforo.Nombre = "semaforo " +j;
                    }
                }
            }


            Controller controlador = new Controller(step1, step2, step3);



            Thread TC = new Thread(controlador.ControllerThread);
            Thread T1 = new Thread(semaforo1.Ejecutar);
            Thread T2 = new Thread(semaforo2.Ejecutar);
            Thread T3 = new Thread(semaforo3.Ejecutar);
            Thread T4 = new Thread(semaforo4.Ejecutar);
            Thread T5 = new Thread(semaforo5.Ejecutar);

            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T5.Start();

            Thread.Sleep(1000);

            Console.WriteLine("Esperando que el controlador empiece a secuenciar la programación");
            //Thread T1 = new Thread();

            Console.ReadLine(); // Para que la consola no se cierre inmediatamente
        }

        
    }
}
