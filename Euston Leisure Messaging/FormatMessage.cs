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
        private Message message;
        public FormatMessage(String i, String m)
        {
            String messageID = i;
            String message = m;
            getMessageType(messageID);

        }

        public void getMessageType(String messageID)
        {
            if (messageID.Contains("S"))
            {
               // message = new Message(Type.SMS, new Body(message, ));
            }
            else if (messageID.Contains("E"))
            {
                //message = new Message(Type.Email);
            }
            else if (messageID.Contains("T"))
            {
               // message = new Message(Type.Tweet);
            }
        }


        private void TextMessage(String message)
        {
            if(message.GetType().Equals(Type.SMS))
            {
                
            }
        }

        private void EmailMessage(String message)
        {
            if (message.GetType().Equals(Type.Email))
            {

            }
        }

        private void TweetMessage(String message)
        {
            if (message.GetType().Equals(Type.Tweet))
            {

            }
        }
    }
}
