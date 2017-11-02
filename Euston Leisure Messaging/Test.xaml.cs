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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private Message message;
        private FormatMessage formatMessage;

        internal Test(FormatMessage f)
        {
            InitializeComponent();
            formatMessage = f;
            message = f.Message;
            lbl1.Content = message.Body.text;
            JSONHandler();
        }
        public void JSONHandler()
        {
            JObject text = new JObject(
                new JProperty("text", message.Body.text));

            File.WriteAllText(@"C:\Users\40203\text.json", text.ToString());

            //writing json directory to a file
            using (StreamWriter file = File.CreateText(@"C:\Users\40203\text.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                text.WriteTo(writer);
            }

        }
    }
}
