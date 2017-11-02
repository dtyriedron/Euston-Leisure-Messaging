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
        public String text;
        private Type type;

        public Body(String s, Type t)
        {
            this.text = s;
            this.type = t;
        }

        public Type Type { get => type; set => type = value; }

        public static String getBetween(String text, String strStart, String strEnd)
        {
            int start, end;
            if (text.Contains(strStart) && text.Contains(strEnd))
            {
                start = text.IndexOf(strStart, 0) + strStart.Length;
                end = text.IndexOf(strEnd, start);
                return text.Substring(start, end - start);
            }
            else
            {
                return "";
            }
        }

        public void MessageHandler()
        {
            if (type.Equals(Type.SMS))
            {
                String phoneNum = getBetween(text, "+", " ");
                String textMessage = text.Substring(0, Math.Min(140, text.Length));
            }
            else if (type.Equals(Type.Email))
            {
                String Email = getBetween(text, "@", ".");
                //put in a new line worth 20 characters for the subject
                String emailMessage = text.Substring(0, Math.Min(1028, text.Length));
            }
            else if(type.Equals(Type.Tweet))
            {
                String tweetMessage = text.Substring(0, Math.Min(140, text.Length));
                String twitterid = tweetMessage.Substring(0, Math.Min(16, tweetMessage.Length));
                twitterid = getBetween(tweetMessage, "@", " ");
                String hashTag = getBetween(tweetMessage, "#", " ");
            }
        }

    }
}
