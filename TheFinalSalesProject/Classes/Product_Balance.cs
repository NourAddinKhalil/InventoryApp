using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFinalSalesProject.Classes
{
    public class Product_Balance :IDisposable
    {
        private bool disposed;
        public int Product_ID { get; set; }
        public int Store_ID { get; set; }
        public double Balance { get; set; }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Dispose();
                }
            }
            disposed = true;
        }
        ~Product_Balance()
        {
            Dispose(false);
        }
    }
}
