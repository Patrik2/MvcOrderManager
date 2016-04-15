using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp
{
    interface ISoska
    {
        decimal GetVaha();
    }
    class Svatyna
    {
        public void Podstavec(ISoska soska)
        {
            var vaha = soska.GetVaha();
        }
    }
    public class ZlataSoska : ISoska
    {
        public decimal GetVaha()
        {
            return 45.124m;
        }
    }

    public class VreceSPieskom : ISoska
    {
        public decimal GetVaha()
        {
            return 45.124m;
        }
    }

}
