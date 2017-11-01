using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euston_Leisure_Messaging
{
    class Body
    {
        String text = "";
        Sender sender;

        public Body(String t, Sender s)
        {
            this.sender = s;
            this.text = t;
        }
    }
}
