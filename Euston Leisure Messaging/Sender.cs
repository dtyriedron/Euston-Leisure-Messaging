using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euston_Leisure_Messaging
{
    class Sender
    {
        Type type;
        String message;

        public Sender(Type t, String m)
        {
            this.type = t;
            this.message = m;
        }

        public void SenderType(String message)
        {
            String twitterID;
            if (getBetween(message, "@", " ").Length < 17)
            {
                   //get the sender for the tweet
            }
            String Email = getBetween(message, ".", " ");

            String phoneNum = getBetween(message, "+", " ");

        }

        public String getBetween(String message, String strStart, String strEnd)
        {
            int start, end;
            if (message.Contains(strStart) && message.Contains(strEnd))
            {
                start = message.IndexOf(strStart, 0) + strStart.Length;
                end = message.IndexOf(strEnd, start);
                return message.Substring(start, end - start);
            }
            else
            {
                return "";
            }
        }

        // maybe chack if it is of the international number form
        public bool isPhoneNumber(String message)
        {
            int count;
            for(int i =0; i<message.Length; i++)
            {
                
                    
                    //int.TryParse(message
            }
            return false;
        }
    }
}
