using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        string[] lines = new string[30];
        public MainWindow()
        {
            InitializeComponent();
            inputMessagetbx.AcceptsReturn = true;
        }

        private void addMessagebtn_Click(object sender, RoutedEventArgs e)
        {            
            String ID = "";

            if (String.IsNullOrWhiteSpace(messageIDtbx.Text.ToString()))
            {
                ID = lines[0];
                Console.WriteLine(lines[0]);
                lines = lines.Skip(1).ToArray();
            }
            else
            {
                ID = messageIDtbx.Text;
                for (int i = 0; i < inputMessagetbx.LineCount; i++)
                {
                    lines[i] = inputMessagetbx.GetLineText(i);
                }
            }

            FormatMessage fm = null;
           try
           {
               fm = new FormatMessage(ID, lines);
           }
           catch (Exception ex)
           {
              MessageBox.Show(ex.Message);
                return;
           }

            ShowMessage sm = new ShowMessage(fm);
            sm.ShowDialog();
        }
            
        
            
        private void btn_stats_for_tweets_Click(object sender, RoutedEventArgs e)
        {
            DisplayTweetLists DL = new DisplayTweetLists();
            DL.ShowDialog();
        }

        private void btn_stats_for_emails_Click(object sender, RoutedEventArgs e)
        {
            DisplayEmailLists DEL = new DisplayEmailLists();
            DEL.ShowDialog();
        }

        private void btn_open_file_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if(openFileDialog.ShowDialog() == true)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (var sr = new StreamReader(openFileDialog.FileName))
                        {
                            int index = 0;
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                               // Console.WriteLine(line);
                                lines[index] = line;
                                index++;
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("could not read the file you chose");
                }
            }
        }
    }
}
