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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Euston_Leisure_Messaging
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            inputMessagetbx.AcceptsReturn = true;
        }

        private void addMessagebtn_Click(object sender, RoutedEventArgs e)
        {
            string[] lines = new string[30];

            for (int i = 0; i < inputMessagetbx.LineCount; i++)
            {
                lines[i] = inputMessagetbx.GetLineText(i);
            }
            
            String ID = messageIDtbx.Text;

            FormatMessage fm = null;
            //try
            //{
               fm = new FormatMessage(ID, lines);
           // }
            //catch (Exception ex)
            //{
               // MessageBox.Show(ex.Message);
                //return;
            //}

            ShowMessage sm = new ShowMessage(fm);
            sm.ShowDialog();
        }

        private void btn_stats_Click(object sender, RoutedEventArgs e)
        {
            DisplayLists DL = new DisplayLists();
            DL.ShowDialog();
        }
    }
}
