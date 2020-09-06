using System.Text.RegularExpressions;
using System.Windows;

namespace KMZI6X
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnigmaMachine machine = new EnigmaMachine();
            if (!Regex.IsMatch(Decrypted.Text, @"^[a-zA-Z ]+$"))
            {
                Decrypted.Text = "Only letters A-Z is allowed, try again: ";
            }
            else
            {
                machine.setSettings(Ring.Text.ToCharArray(), StartPos.Text.ToCharArray(), Rotors.Text, Reflector.Text[0]);
                if (Plugs.Text != "")
                {
                    string[] plugs = Plugs.Text.Split(' ');
                    foreach (string plug in plugs)
                    {
                        char[] p = plug.ToCharArray();
                        machine.addPlug(p[0], p[1]);
                    }
                }
                Decrypted.Text = Decrypted.Text.Replace(" ", "").ToUpper();
                Encrypted.Text=machine.runEnigma(Decrypted.Text);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EnigmaMachine machine = new EnigmaMachine();
            if (!Regex.IsMatch(Encrypted.Text, @"^[a-zA-Z ]+$"))
            {
                Encrypted.Text = "Only letters A-Z is allowed, try again: ";
            }
            else
            {
                machine.setSettings(Ring.Text.ToCharArray(), StartPos.Text.ToCharArray(), Rotors.Text, Reflector.Text[0]);
                if (Plugs.Text != "")
                {
                    string[] plugs = Plugs.Text.Split(' ');
                    foreach (string plug in plugs)
                    {
                        char[] p = plug.ToCharArray();
                        machine.addPlug(p[0], p[1]);
                    }
                }
                Encrypted.Text = Encrypted.Text.Replace(" ", "").ToUpper();
                Decrypted.Text = machine.runEnigma(Encrypted.Text);
            }
        }
    }
}
