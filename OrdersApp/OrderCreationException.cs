using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersApp
{

    [Serializable]
    public class OrderCreationException : Exception
    {
        public OrderCreationException() { }
        public OrderCreationException(string message) : base(message) { }
        public OrderCreationException(string message, Exception inner) : base(message, inner) { }
        protected OrderCreationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}
