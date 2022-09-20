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

namespace Infobez1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] EN = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        char[] RU = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        bool IsRU = false; bool IsEN = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cipher_Click(object sender, RoutedEventArgs e)
        {
            bool check=Language_Check(Message.Text);

            //почему открывается только один раз??????????????!!
            if (check==false)
            {
                ToolTip cm = this.FindResource("toolTrip") as ToolTip;
                cm.PlacementTarget = Message;
                cm.IsOpen = true;
                cm.StaysOpen = false;
            }
            else
            {
                
                int key = Convert.ToInt32(Key.Text);

                char[] chipher = Message.Text.ToCharArray();
                char[] b = chipher.ToArray();

                Cipher.Text = Chipher();







            }
        }

        private string Chipher()
        {
            char[] message = Message.Text.ToCharArray();
            int key = Convert.ToInt32(Key.Text);
            char[] chipher = new char[Message.Text.Length];

            if (message.Length>EN.Length)


            for (int i = 1; i < Message.Text.Length; i++)
            {

                if (IsEN == true)
                {
                        for (int j = 0; j < EN.Length; j++)
                        {
                            if (message[i] == EN[j])
                            {
                                chipher[i] = EN[j + key];
                            }
                        }
                }
                if (IsRU==true)
                    {
                        for (int j = 0; j < RU.Length; j++)
                        {
                            if (message[i] == RU[j])
                            {
                                chipher[i] = RU[j + key];
                            }
                        }
                    }


            }
            string result = new string(chipher);
            return result;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Language_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }


        private void RU_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("RU");
            IsRU = true;
            IsEN = false;
        }

        private void EN_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("EN");
            IsEN = true;
            IsRU = false;
        }

        private bool Language_Check(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                MessageBox.Show("Empty!!");
                return false;
            }
            else
            {
                str.ToLower();

                byte[] b = System.Text.Encoding.Default.GetBytes(str);


                int angl_count = 0, rus_count = 0;

                foreach (byte bt in b)
                {
                    if ((bt >= 97) && (bt <= 122)) angl_count++;
                    if ((bt >= 192) && (bt <= 239)) rus_count++;
                }

                if (angl_count > rus_count && IsEN == true) return true;
                else if (angl_count < rus_count && IsRU == true) return true;
                else return false;
                // if (angl_count < rus_count) return Language.Russian;
                //return Language.Unknown;
            }
        }
    }
}
