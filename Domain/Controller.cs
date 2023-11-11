using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class Controller
    {
        private List<Step> pasos = new List<Step>();
        public Controller(params Step[] lista)
        {
            foreach (var item in lista)
            {
                pasos.Add(item);
            }
        }
        public void ControllerThread()
        {

            while (true)
            {
                foreach (var item in pasos)
                {
                    item.Rojo.GetSemaphore().Release(item.Rojo.MaxCount);
                    item.Verde.GetSemaphore().Release(item.Verde.MaxCount);
                    Console.WriteLine($"Se liberaron {item.Verde.MaxCount} verdes y {item.Rojo.MaxCount} rojos.");
                    Thread.Sleep(3000);

                }
            }
        }
    }
}
