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
    /// Interaction logic for DisplayEmailLists.xaml
    /// </summary>
    public partial class DisplayEmailLists : Window
    {
        public DisplayEmailLists()
        {
            InitializeComponent();

            foreach (var SIR in ShowMessage.SIRS.OrderByDescending(key => key.Value))
            {
                textBlock_SIR.Text += SIR.Key + " " + SIR.Value + "\n";
            }

            foreach (var URL in ShowMessage.URLS.OrderByDescending(key => key.Value))
            {
                textBlock_URL.Text += URL.Key + " " + URL.Value + "\n";
            }
        }
    }
}
