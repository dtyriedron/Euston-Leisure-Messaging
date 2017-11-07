using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Euston_Leisure_Messaging
{
    
    class Body
    {
        public String[] text;
        private Type type;
        private Message message;

        public Body(String[] s, Type t)
        {
            this.text = s;
            this.type = t;
        }

        public Type Type { get => type; set => type = value; }
    }
}
