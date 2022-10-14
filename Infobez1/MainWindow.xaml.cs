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
using System.Numerics;
using System.Text.RegularExpressions;

namespace Infobez1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        char[] EN = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        char[] RU = { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

        bool IsRU = false; bool IsEN = false;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Decipher_Click(object sender, RoutedEventArgs e)
        {
            bool check = Language_Check(Message.Text);
            bool check1 = Regex.IsMatch(Key.Text, @"^[0-9]+$");
            if (Key.Text.Contains("-"))
                check1 = true;

            if (!check1)
            {
                MessageBox.Show("Ключ содержит запрещенные символы!");
                Key.Text = "";
            }
            else
            {
                if(!check)
                {
                    MessageBox.Show("Текст не соответсвтует выбранному языку или заданным параметрам!");
                    Cipher.Text = "";
                }
                else
                {
                    var dechipher = new List<string>();
                    dechipher = Dechipher();
                    string combinedString = string.Join(" ", dechipher);
                    Dechipherr.Text = combinedString;
                }
            }
        }

        private List<string> Dechipher()
        {
            var dechipher = new List<string>();
            BigInteger key = 0;
            try
            {
                key = BigInteger.Parse(Key.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ключ был обнулен, так как он содержит запрещенные символы!");
                Key.Text = "";
            }
            if (key < 0)
            {
                key = key * (-1);
                key = Calculate_key(key);
                key = key * (-1);
            }
            else key = Calculate_key(key);
            string[] words = Cipher.Text.Split(' ');
            for (int n = 0; n < words.Length; n++)
            {
                char[] word = words[n].ToCharArray();
                string chip = new string(Words_dechipher(word, (int)key));
                dechipher.Add(chip);
            }
            return dechipher;
        }

        private char[] Words_dechipher(char[] word, int key)
        {
            char[] chipher = new char[word.Length];
            if (key > 0)
            {
                chipher = Word_minus_operation(word, key);
            }
            if (key < 0)
            {
                key = key * (-1);
                chipher = Word_plus_operation(word, key);
            }

            return chipher;
        }


        private void Cipher_Click(object sender, RoutedEventArgs e)
        {
            bool check = Language_Check(Message.Text);
            bool check1 = Regex.IsMatch(Key.Text, @"^[0-9]+$");
            //bool check2 = Regex.IsMatch(Key.Text, @"^[a-zA-zа-яА-я]+$"); 
            if (Key.Text.Contains("-"))
              check1 = true;
            //if (check2)
                //check1 = false;

            if (!check1)
            {
                MessageBox.Show("Ключ содержит запрещенные символы!");
                Key.Text = "";
            }
            else
            {
                //почему открывается только один раз??????????????!!
                if (check == false)
                {
                   /* ToolTip cm = this.FindResource("toolTrip") as ToolTip;
                    cm.PlacementTarget = Message;
                    cm.IsOpen = true;
                    cm.StaysOpen = false; */
                    MessageBox.Show("Текст не соответсвтует выбранному языку или заданным параметрам!");
                    Message.Text = "";
                }
                if (check == true)
                {
                    var chipher = new List<string>();
                    chipher = Chipher();
                    string combinedString = string.Join(" ", chipher);
                    Cipher.Text = combinedString;
                }
            }
        }

        private List<string> Chipher()
        {
            var chipher = new List<string>();
            BigInteger key = 0;
            try
            {
                key = BigInteger.Parse(Key.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ключ был обнулен, так как он содержит запрещенные символы!");
                Key.Text = "";
            }
            if (key<0)
            {
                key = key * (-1);
                key = Calculate_key(key);
                key = key * (-1);
            }
            else key = Calculate_key(key);
            //MessageBox.Show("key == " + key);
            string[] words = Message.Text.Split(' ');
            for (int n = 0; n < words.Length; n++)
            {
                char[] word = words[n].ToCharArray();
                string chip = new string(Words_chipher(word, (int)key));
                chipher.Add(chip);
                //chipher.Add(" ");
            }
            return chipher;
        }

        private char[] Words_chipher(char[] word, int key)
        {
            char[] chipher = new char[0];

            if (key > 0)
            {
                chipher = Word_plus_operation(word, key);
            }

            if (key < 0)
            {
                key = key * (-1);
                chipher = Word_minus_operation(word, key);
            }

            return chipher;
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
            IsRU = true;
            IsEN = false;
        }

        private void EN_Click(object sender, RoutedEventArgs e)
        {
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
                if (str.Contains("ё"))
                    rus_count++;
                if (angl_count > 0 && rus_count > 0) return false;
                else if (angl_count > 0 && IsEN == true) return true;
                else if (rus_count > 0 && IsRU == true) return true;
                else return false;
                // if (angl_count < rus_count) return Language.Russian;
                //return Language.Unknown;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "";
            Cipher.Text = "";
            Dechipherr.Text = "";
            Key.Text = "";
        }

        private BigInteger Calculate_key (BigInteger key)
        {
            BigInteger k;
            if (key == 0)
                MessageBox.Show("Помни, что ключ не может быть равен 0!");
            else
            {
                if (IsEN)
                {
                    if (key > EN.Length)
                    {

                        k = key / EN.Length;
                        key = key - (EN.Length * k);
                        //MessageBox.Show("key == " + key);

                    }
                }
                if (IsRU)
                {
                    if (key > RU.Length)
                    {

                        k = key / RU.Length;
                        key = key - (RU.Length * k);
                        //MessageBox.Show("key == " + key);
                    }
                }


                if (IsEN)
                {
                    if (key > EN.Length)
                    {
                        k = key / EN.Length;
                        key = key - (EN.Length * k);
                        //MessageBox.Show("key == " + key);

                    }
                }
                if (IsRU)
                {
                    if (key > RU.Length)
                    {

                        k = key / RU.Length;
                        key = key - (RU.Length * k);
                        //MessageBox.Show("key == " + key);
                    }
                }
            }
            return key;
        }

        public char[] Word_plus_operation(char[] word, int key)
        {
            char[] chipher = new char[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                if (IsEN == true)
                {
                    for (int j = 0; j < EN.Length; j++)
                    {
                        if (word[i] == EN[j])
                        {
                            if ((j + key) >= EN.Length)
                            {
                                int difference = (j + key) - EN.Length;
                                chipher[i] = EN[difference];

                            }
                          /*  else if ((j + key) == EN.Length)
                                chipher[i] = EN[EN.Length-1]; */
                            else chipher[i] = EN[j + key];
                        }
                    }
                }
                if (IsRU == true)
                {
                    for (int j = 0; j < RU.Length; j++)
                    {
                        if (word[i] == RU[j])
                        {
                            if ((j + key) >= RU.Length)
                            {
                                int difference = (j + key) - RU.Length;
                                chipher[i] = RU[difference];

                            }
                           /* else if ((j + key) == RU.Length)
                                chipher[i] = RU[RU.Length-1]; */
                            else chipher[i] = RU[j + key];
                        }
                    }
                }
            }
            return chipher;
        }

        public char[] Word_minus_operation(char[] word, int key)
        {
            char[] chipher = new char[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                if (IsEN == true)
                {
                    for (int j = 0; j < EN.Length; j++)
                    {
                        if (word[i] == EN[j])
                        {
                            //abcdefghijklmnopqrstuvwxyz0123456789
                            //fghijklmnopqrstuvwxyz0123456789abcde
                            //abcdefghijklmnopqrstuvwxyz0123454321
                            if ((j - key) < 0)
                            {
                                int difference = EN.Length - (key - j);
                                if (difference < 0)
                                {
                                    difference = j + key - EN.Length;
                                    difference = EN.Length - difference;
                                }
                                chipher[i] = EN[difference];

                            }
                            else chipher[i] = EN[j - key];
                        }
                    }
                }
                if (IsRU == true)
                {
                    for (int j = 0; j < RU.Length; j++)
                    {
                        if (word[i] == RU[j])
                        {
                            if ((j - key) < 0)
                            {
                                int difference = RU.Length - (key - j);
                                if (difference<0)
                                {
                                    difference = j + key - RU.Length;
                                    difference = RU.Length - difference;
                                }
                                chipher[i] = RU[difference];

                            }
                            else chipher[i] = RU[j - key];
                        }
                    }
                }
            }
            return chipher;
        }
    }
}

