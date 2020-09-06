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
using RC4_Testing;
namespace KMZI8
{
    /// <summary>
    /// Логика взаимодействия для RC4Window.xaml
    /// </summary>
    public partial class RC4Window : Window
    {
        public RC4Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextToDecode.Text = "";
            string[] s = Key.Text.Split(' ');
            byte[] key = new byte[s.Length*2];
            for(int i=0;i<s.Length;i++)
            {
                byte[] temp = BitConverter.GetBytes(int.Parse(s[i]));
                key[i * 2] = temp[0];
                key[i * 2 + 1] = temp[1];
            }
            RC4 rc4 = new RC4(key);
            string text = TextToEncode.Text;
            char[] result = new char[text.Length];
            for(int i=0;i<text.Length;i++)
            {
                byte[] temp = BitConverter.GetBytes(text[i]);
                result[i] =BitConverter.ToChar(rc4.Encode(temp, 2),0);
                TextToDecode.Text += result[i].ToString();
;            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TextToEncode.Text = "";
            string[] s = Key.Text.Split(' ');
            byte[] key = new byte[s.Length * 2];
            for (int i = 0; i < s.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(int.Parse(s[i]));
                key[i * 2] = temp[0];
                key[i * 2 + 1] = temp[1];
            }
            RC4 rc4 = new RC4(key);
            string text = TextToDecode.Text;
            char[] result = new char[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(text[i]);
                result[i] = BitConverter.ToChar(rc4.Decode(temp, 2), 0);
                TextToEncode.Text += result[i];
;
            }
        }
    }
}
