using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace Euston_Leisure_Messaging
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ShowMessage : Window
    {
        private Message message;
        private FormatMessage formatMessage;
        private static Dictionary<string, int> hashtags = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
        private static Dictionary<string, int> urls = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
        private static Dictionary<string, int> mentions = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
        private static Dictionary<string, int> sirs = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

        public static Dictionary<string, int> Hashtags { get => hashtags; }
        public static Dictionary<string, int> URLS { get => urls; }
        public static Dictionary<string, int> Mentions { get => mentions; set => mentions = value; }
        public static Dictionary<string, int> SIRS { get => sirs; set => sirs = value; }

        internal ShowMessage(FormatMessage f)
        {
            InitializeComponent();
            formatMessage = f;
            message = f.Message;

            if (message.Type == Type.Email)
            {
                JSONHandler.WriteEmail(formatMessage);
                if (formatMessage.IsSIR)
                    outputText.Text = formatMessage.EmailBody;
                else
                    outputText.Text = formatMessage.EmailBody;
            }
            else
            {
                var text = TextSpeak(formatMessage.FormatBody(message.Body.text, 1));
                outputText.Text = text;
                JSONHandler.Write(message, formatMessage, this);
            }
        }

        public string TextSpeak(string text)
        {
            Dictionary<String, String> dict = new Dictionary<string, string>();
            var strLines = File.ReadLines(@"C:\Users\40203\textwords.csv");

            foreach (var line in strLines)
            {
                var splits = line.Split(',');
                if (splits.Length > 2)
                    dict.Add(splits[0], splits[1] + splits[2]);
                else
                    dict.Add(splits[0], splits[1]);
            }

            foreach (var word in dict)
            {
                if (text.Contains(" "+ word.Key))
                {
                    //need to replace the next line with a replace function in a replace text method rather than store it into an array
                    text = text.Replace(word.Key, word.Key + "<" + word.Value + ">");
                    Console.WriteLine(text);
                    continue;
                }
            }
            return text;

        }
    }
}
