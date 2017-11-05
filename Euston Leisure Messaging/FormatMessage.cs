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
            MessageHandler();
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

        public static String getBetween(String text, Char strStart, Char strEnd)
        {
            int start, end;
            if (text.Contains(strStart) && text.Contains(strEnd))
            {
                start = text.IndexOf(strStart, 0);
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
            if (message.Type.Equals(Type.SMS))
            {
                String phoneNum = getBetween(messageText, '+', ' ');
                String textMessage = messageText.Substring(0, Math.Min(140, messageText.Length));
            }
            else if (message.Type.Equals(Type.Email))
            {
                String Email = getBetween(messageText, '@', '.');
                //put in a new line worth 20 characters for the subject
                String emailMessage = messageText.Substring(0, Math.Min(1028, messageText.Length));
            }
            else if (message.Type.Equals(Type.Tweet))
            {
                String tweetMessage = messageText.Substring(0, Math.Min(140, messageText.Length));
                String twitterid = tweetMessage.Substring(0, Math.Min(16, tweetMessage.Length));
                twitterid = getBetween(tweetMessage, '@', ' ');
                int index = 0;

                if (tweetMessage.Contains('#'))
                {
                    //store the hashtag and its count in an array if its already there then count++
                    Test.IsHashtag[index] = getBetween(tweetMessage, '#', ' ');
                    Console.WriteLine(" HEEEEELLELELLLLLLLOOOOO:   "+Test.IsHashtag[index]);
                    foreach (var hashtag in Test.IsHashtag)
                    {
                       if (!Test.Hashtags.ContainsKey(hashtag))
                       {
                           Test.Hashtags.Add(hashtag, 1);
                       }
                        else
                       {
                           Test.Hashtags[hashtag]++;
                           Console.WriteLine(Test.Hashtags[hashtag]);
                       }
                            
                   }
                   index++;
                }

            }
        }
    }
}
