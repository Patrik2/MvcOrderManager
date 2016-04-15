using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OrdersAppTests
{
    class CustomSet<T> : DbSet<T> where T : class
    {
        


    }
}
