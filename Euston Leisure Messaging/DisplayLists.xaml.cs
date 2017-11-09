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

namespace Euston_Leisure_Messaging
{
    /// <summary>
    /// Interaction logic for DisplayLists.xaml
    /// </summary>
    public partial class DisplayLists : Window
    {
        public DisplayLists()
        {
            InitializeComponent();

            foreach(var hashtag in ShowMessage.Hashtags.OrderByDescending(key => key.Value))
            {
                textBlock_trending.Text += hashtag.Key + " " + hashtag.Value + "\n";
            }

            foreach (var twitterid in ShowMessage.Mentions.OrderByDescending(key => key.Value))
            {
                textBlock_mentions.Text += twitterid.Key + " " + twitterid.Value + "\n";
            }
        }
    }
}
