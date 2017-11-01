using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euston_Leisure_Messaging
{
    class Message
    {
        private Type type;
        private Body Body;
        public Message(Type t, Body b)
        {
            this.Type = t;
            this.Body = b;
        }

        public Type Type { get => type; set => type = value; }

        
    }
}
