using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Euston_Leisure_Messaging
{
    static class JSONHandler
    {

        public static void WriteEmail(Message message, FormatMessage formatMessage)
        {
            JObject text;
            if (formatMessage.IsSIR)
            {
                 text = new JObject(
                      new JProperty("ID", formatMessage.MessageID),
                      new JProperty("sender", formatMessage.GarbageRemoval(formatMessage.messageText[0])),
                      new JProperty("subject", formatMessage.GarbageRemoval(message.Body.text[1])),
                      new JProperty("code", formatMessage.GarbageRemoval(message.Body.text[2])),
                      new JProperty("Nature of Incident", formatMessage.GarbageRemoval(message.Body.text[3])),
                      new JProperty("text", formatMessage.FormatBody(message.Body.text, 4)));
            }
            else
            {
                text = new JObject(
                    new JProperty("ID", formatMessage.MessageID),
                    new JProperty("sender", formatMessage.GarbageRemoval(formatMessage.messageText[0])),
                    new JProperty("subject", formatMessage.FormatBody(message.Body.text, 1)),
                    new JProperty("text", formatMessage.FormatBody(message.Body.text, 2)));
            }

            File.WriteAllText(@"C:\Users\40203\" + formatMessage.MessageID + ".json", text.ToString());

        }

        public static void Write(Message message, FormatMessage formatMessage)
        {
            JObject text = new JObject(
                new JProperty("ID", formatMessage.MessageID),
                new JProperty("sender", formatMessage.GarbageRemoval(formatMessage.messageText[0])),
                new JProperty("text", formatMessage.FormatBody(message.Body.text, 1)));

            File.WriteAllText(@"C:\Users\40203\" + formatMessage.MessageID + ".json", text.ToString());
        }
    }
}
