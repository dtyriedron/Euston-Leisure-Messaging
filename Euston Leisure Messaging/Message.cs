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
        private Body body;
        public Message(Type t, Body b)
        {
            this.type = t;
            this.body = b;
        }


        public Type Type { get => type; set => type = value; }
        public Body Body { get => body; set => body = value; }
    }
}
