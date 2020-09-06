using System;
using System.Collections.Generic;
using System.Windows;

namespace KMZI8
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sequence.Text = "";
            Sequence2.Text = "";
            int[] result = Bbs();
            foreach (int x in result)
            {
                Sequence.Text += x + "  ";
                Sequence2.Text += x % 2 + "  ";
            }
        }
        
        public static int n = 209;  //19, 11
        public static int x = 3;

        public int getRandNum()
        {
            int nextRandNum = (x * x) % n;
            x = nextRandNum;
            return nextRandNum;
        }

        int[] Bbs()
        {
            int[] result = new int[15];
            for (int i = 0; i < 15; i++)
            {
                int z = getRandNum();
                result[i] = z;
            }
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RC4Window rC4 = new RC4Window();
            rC4.Show();
        }
    }
}
