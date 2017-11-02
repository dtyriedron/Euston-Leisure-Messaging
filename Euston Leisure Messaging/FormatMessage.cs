using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euston_Leisure_Messaging
{
    class FormatMessage
    {

        public Type type;
        public Body body;
        private Message message;
        public String messageText;

        internal Message Message { get => message; set => message = value; }

        public FormatMessage(String i, String m)
        {
            String messageID = i;
            messageText = m;
            Message = new Message(GetMessageType(messageID), new Body(messageText, GetMessageType(messageID)));
        }

        public Type GetMessageType(String messageID)
        {
            if (messageID.Contains("S"))
            {
                return Type.SMS;
            }
            else if (messageID.Contains("E"))
            {
                return Type.Email;
            }
            else if (messageID.Contains("T"))
            {
                return Type.Tweet;
            }
            return Type.NULL;
        }


        private void FormatBody(String messageText)
        {
            if(message.Type.Equals(Type.SMS))
            {
                body.Type.Equals(Type.SMS);
            }
            else if (message.Type.Equals(Type.Email))
            {
                body.Type.Equals(Type.Email);
            }
            else if (message.Type.Equals(Type.Tweet))
            {
                body.Type.Equals(Type.Tweet);
            }
        }
    }
}
