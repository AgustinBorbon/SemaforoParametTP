using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain
{
    public class Secuencia
    {
        private SemaphoreSlim secuencia { get; set; }

        public int MaxCount { get; set; }

        public Secuencia(int maxCount)
        {
            MaxCount = maxCount;
            secuencia = new SemaphoreSlim(0, maxCount);
        }

        public SemaphoreSlim GetSemaphore()
        {
            return secuencia;
        }
    }
}
