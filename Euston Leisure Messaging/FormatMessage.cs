using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Euston_Leisure_Messaging
{
    class FormatMessage
    {

        public Type type;
        public Body body;
        private bool isSIR = false;
        private Message message;
        public String[] MessageText;
        private string messageID;
        public string EmailBody;

        private static string[] matches = new string[60];
        public static string[] Matches { get => matches; set => matches = value; }

        internal Message Message { get => message; set => message = value; }
        public string MessageID { get => messageID; set => messageID = value; }
        public bool IsSIR { get => isSIR; set => isSIR = value; }

        public FormatMessage(String i, String[] m)
        {
            MessageID = i;
            MessageText = m;
            Message = new Message(GetMessageType(MessageID), new Body(MessageText, GetMessageType(MessageID)));
            MessageHandler();
        }

        private Type GetMessageType(string messageID)
        {
            if (messageID.Contains("S") && messageID.Length == 10)
            {
                return Type.SMS;
            }
            else if (messageID.Contains("E") && messageID.Length == 10)
            {
                return Type.Email;
            }
            else if (messageID.Contains("T") && messageID.Length == 10)
            {
                return Type.Tweet;
            }
            else
            {
                throw new Exception("Invalid ID!");
            }            
        }

        private MatchCollection FindRegex(string text, Regex regex)
        {
            return regex.Matches(text);
        }

        private void Validate(string text, Regex regex)
        {
            if(!String.IsNullOrWhiteSpace(text))
            {
                if(!regex.IsMatch(text))
                {
                    throw new Exception("Invalid regex!");
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

        private void ReplaceURLS()
        {
            Regex regex = new Regex(@"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)", RegexOptions.IgnoreCase);
            var matches = regex.Matches(EmailBody);

            foreach (Match URL in matches)
            {
                Console.WriteLine(URL.Value);
                try
                {
                    //replace the words in the text to have the expanded abbreviation = myKey.Key + " <" + myKey.Value + "> ";
                    EmailBody = EmailBody.Replace(URL.Value, "<URL Quarantined>");
                    if (ShowMessage.URLS.ContainsKey(URL.Value))
                    {
                        Console.WriteLine("adding to the email dict count");
                        ShowMessage.URLS.TryGetValue(URL.Value, out int val);
                        ShowMessage.URLS[URL.Value] = val + 1;
                        Console.WriteLine(ShowMessage.URLS[URL.Value]);
                    }
                    else
                    {
                        Console.WriteLine("adding to the email dict");
                        ShowMessage.URLS.Add(URL.Value, 1);
                        var myKey = ShowMessage.URLS.FirstOrDefault(x => x.Key == URL.Value);
                        Console.WriteLine("replacing: " + myKey.Key);                                              
                        Console.WriteLine("added to the email dict");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void MessageHandler()
        {
            if (message.Type.Equals(Type.SMS))
            { 
                string PhoneNum = GarbageRemoval(MessageText[0]);
                Validate(PhoneNum, new Regex(@"^(?:\(?)(?:\+| 0{2})([0-9]{3})\)?([0-9]{2})([0-9]{7})$"));
                string PhoneBody = FormatBody(MessageText, 1);
                if (PhoneBody.Length > 140)
                    throw new Exception("message is too long");
            }
            else if (message.Type.Equals(Type.Email))
            {               
                string Email = GarbageRemoval(MessageText[0]);
                Validate(Email, new Regex(@"(?<email>\w+@\w+\.[a-z]{0,3})"));

                EmailBody = FormatBody(MessageText, 2);

                if (EmailBody.Length > 1028)
                    throw new Exception("Email is too long");

                string Subject = GarbageRemoval(MessageText[1]);
                if (Subject.Length > 20)
                    throw new Exception("Subject too long");
                if (Subject.Contains("SIR"))
                {
                    isSIR = true;
                    EmailBody = FormatBody(MessageText, 4);
                    //check the date is valid
                    var dateRegex = new Regex(@"(\d+)[-.\/](\d+)[-.\/](\d+)");
                    if (!dateRegex.IsMatch(Subject))
                        throw new Exception("date invalid");
                    //check if the centre code is valid
                    var codeRegex = new Regex(@"^\d(\d|(?<!-)-)*\d$|^\d$");
                    if (!codeRegex.IsMatch(GarbageRemoval(MessageText[2])))
                        throw new Exception("code invalid");
                    //check the report is one of the valid reports
                    ArrayList validReports = new ArrayList() {"Theft of Properties", "Staff Attack", "Device Damage", "Raid", "Customer Attack", "Staff Abuse", "Bomb Threat", "Terrorism", "Suspicious Incident", "Sport Injury", "Personal Info Leak"};
                    if (!validReports.Contains(GarbageRemoval(MessageText[3])))
                        throw new Exception("invalid Nature of Incident");
                    else
                    {
                        string searchKey = GarbageRemoval(MessageText[3]);
                            if (ShowMessage.SIRS.ContainsKey(searchKey))
                            {
                                ShowMessage.SIRS.TryGetValue(searchKey, out int val);
                                ShowMessage.SIRS[searchKey] = val + 1;
                            }
                            else
                                ShowMessage.SIRS.Add(searchKey, 1);
                    }
                    ReplaceURLS();
                }
                else
                ReplaceURLS();
            }
            else if (message.Type.Equals(Type.Tweet))
            {
                string TwitterId = GarbageRemoval(MessageText[0]);
                Validate(TwitterId, new Regex(@"(?<=@)\w+"));
                if (TwitterId.Length > 15)
                    throw new Exception("invalid twitterid!!");

                string tweetBody = FormatBody(MessageText, 1);

                if (tweetBody.Length > 140)
                    throw new Exception("Tweet too long");

                var regex = new Regex(@"(?<=@)\w+");
                var matches = regex.Matches(tweetBody);

                foreach (Match searchKey in matches)
                    if (ShowMessage.Mentions.ContainsKey(searchKey.Value))
                    {
                        ShowMessage.Mentions.TryGetValue(searchKey.Value, out int val);
                        ShowMessage.Mentions[searchKey.Value] = val + 1;
                    }
                else
                    ShowMessage.Mentions.Add(searchKey.Value, 1);

                Console.WriteLine(TwitterId);

                //store the hashtag and its count in an array if its already there then count++
                regex = new Regex(@"(?<=#)\w+");
                matches = regex.Matches(tweetBody);
                
                foreach (Match hashtag in matches)
                {
                      Console.WriteLine("heeeeeeeeeee"+hashtag);
                    try
                    {
                        if (ShowMessage.Hashtags.ContainsKey(hashtag.Value))
                        {
                            Console.WriteLine("adding to dictionary count");
                            ShowMessage.Hashtags.TryGetValue(hashtag.Value, out int val);
                            ShowMessage.Hashtags[hashtag.Value] = val + 1;
                            Console.WriteLine(ShowMessage.Hashtags[hashtag.Value]);
                        }
                        else 
                        {
                            Console.WriteLine("adding to dictionary");
                            ShowMessage.Hashtags.Add(hashtag.Value, 1);
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
