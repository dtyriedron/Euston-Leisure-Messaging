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
        public String[] messageText;
        public Regex hashregex = new Regex(@"(?<=#)\w+");

        private static string[] matches = new string[60];
        public static string[] Matches { get => matches; set => matches = value; }

        internal Message Message { get => message; set => message = value; }

        public FormatMessage(String i, String[] m)
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

        public MatchCollection findRegex(string text, Regex regex)
        {
            return regex.Matches(text);
        }

        public void Validate(string text, Regex regex)
        {
            if(!String.IsNullOrWhiteSpace(text))
            {
                if(!regex.IsMatch(text))
                {
                    throw new Exception("Invalid input!");
                }
            }           
        }

        public string GarbageRemoval(string text)
        {
            return text.Replace("\r\n","");
        }

        public string FormatBody(string[] text, int line)
        {
            string newBody = "";
            for (int i = line; i < text.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(text[i]))
                {
                    newBody += " " + GarbageRemoval(text[i]);
                }
                else
                    continue;
            }
            return newBody;
        }
        public void MessageHandler()
        {
            if (message.Type.Equals(Type.SMS))
            { 
                string PhoneNum = GarbageRemoval(messageText[0]);
                Validate(PhoneNum, new Regex(@"^(?:\(?)(?:\+| 0{2})([0-9]{3})\)?([0-9]{2})([0-9]{7})$"));
                string PhoneBody = FormatBody(messageText, 1);
            }
            else if (message.Type.Equals(Type.Email))
            {               
                string Email = GarbageRemoval(messageText[0]);
                Validate(Email, new Regex(@"(?<email>\w+@\w+\.[a-z]{0,3})"));

                string Subject = GarbageRemoval(messageText[1]); // check the length of this              
                string EmailBody = FormatBody(messageText, 2);
            }
            else if (message.Type.Equals(Type.Tweet))
            {
                string TwitterId = GarbageRemoval(messageText[0]);
                Validate(TwitterId, new Regex(@"(?<=@)\w+"));
                Console.WriteLine(TwitterId);
                //store the hashtag and its count in an array if its already there then count++

                var regex = new Regex(@"(?<=#)\w+");
                var matches = regex.Matches(FormatBody(messageText, 1));
                     
                foreach (Match hashtag in matches)
                {
                      Console.WriteLine("heeeeeeeeeee"+hashtag);

                    try
                    {
                        if (Test.Hashtags.ContainsKey(hashtag.Value))
                        {
                            Console.WriteLine("adding to dictionary count");
                            Test.Hashtags.TryGetValue(hashtag.Value, out int val);
                            Test.Hashtags[hashtag.Value] = val + 1;
                            Console.WriteLine(Test.Hashtags[hashtag.Value]);
                        }
                        else 
                        {
                            Console.WriteLine("adding to dictionary");
                            Test.Hashtags.Add(hashtag.Value, 1);
                            Console.WriteLine("added to the dictionary");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());                   
                    }
                            
                }
            }
        }
    }
}
