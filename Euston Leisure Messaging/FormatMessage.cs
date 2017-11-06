using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Euston_Leisure_Messaging
{
    class FormatMessage
    {

        public Type type;
        public Body body;
        private Message message;
        public String messageText;
        //public Regex phoneregex = new Regex(@"(?<=+)\w+");
        //public Regex emailregex = new Regex(@"(?<=@>=?)\w+");
        public Regex hashregex = new Regex(@"(?<=#)\w+");

        private static string[] matches = new string[60];
        public static string[] Matches { get => matches; set => matches = value; }

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

        public string[] findRegex(string text, Regex regex)
        {
            var matches = regex.Matches(text);
            int index = 0;
            foreach (Match m in matches)
            {
                Matches[index] = m.Value;
                Console.WriteLine("added match:   " + Matches[index]);
                index++;
            }
            return Matches;
        }

        public void MessageHandler()
        {
            if (message.Type.Equals(Type.SMS))
            {
                //String phoneNum = getBetween(messageText, '+', ' ');
                String textMessage = messageText.Substring(0, Math.Min(140, messageText.Length));
            }
            else if (message.Type.Equals(Type.Email))
            {
                //String Email = getBetween(messageText, '@', '.');
                //put in a new line worth 20 characters for the subject
                String emailMessage = messageText.Substring(0, Math.Min(1028, messageText.Length));
            }
            else if (message.Type.Equals(Type.Tweet))
            {
                String tweetMessage = messageText.Substring(0, Math.Min(140, messageText.Length));
                String twitterid = tweetMessage.Substring(0, Math.Min(16, tweetMessage.Length));
                twitterid = findRegex(tweetMessage, new Regex(@"(?<=@)\w+"))[0];
                Console.WriteLine(twitterid);
                //store the hashtag and its count in an array if its already there then count++                
                foreach (var hashtag in findRegex(tweetMessage, hashregex))
                {
                      Console.WriteLine("foreach");

                    try
                    {
                        if (Test.Hashtags.ContainsKey(hashtag))
                        {
                            Console.WriteLine("adding to dictionary count");
                            Test.Hashtags.TryGetValue(hashtag, out int val);
                            Test.Hashtags[hashtag] = val + 1;
                            Console.WriteLine(Test.Hashtags[hashtag]);
                        }
                        else
                        {
                            Console.WriteLine("adding to dictionary");
                            Test.Hashtags.Add(hashtag, 1);
                            Console.WriteLine("added to the dictionary");
                        }
                    }
                    catch (Exception ex)
                    {}
                            
                }
            }
        }
    }
}
