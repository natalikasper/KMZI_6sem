using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            using (StreamReader sr = new StreamReader("F:\\6 семестр\\КМЗИ\\лабораторные\\lab2\\ConsoleApp2\\ConsoleApp2\\TextFile1.txt"))
            {
                double a1 = Shannon(GetText("F:\\6 семестр\\КМЗИ\\лабораторные\\lab2\\ConsoleApp2\\ConsoleApp2\\TextFile1.txt"), alphabet);
                Console.WriteLine("\nЭнтропия Шеннона:  " + a1);
                Console.WriteLine("Количество информации:    " + KolvoInfoShannon(sr.ReadToEnd(), a1));
                Console.WriteLine("Энтропия Хартли для алфавита:  " + Hartley(alphabet) + "\n");
            }
            
            string alphabet1 = "10";
                Console.WriteLine(ToBinAscii("KasperNataliaViktorovna"));
                double a2 = Shannon(ToBinAscii("KasperNataliaViktorovna"), alphabet1);
                Console.WriteLine("\nЭнтропия Шеннона для 2сс алфавита:  " + a2);
                Console.WriteLine("Количество информации:    " + KolvoInfoShannon(ToBinAscii("KasperNataliaViktorovna"), a2));
                Console.WriteLine("Энтропия Хартли для алфавита:  " + Hartley(alphabet1) + "\n");
            Console.WriteLine("0.1: " + EfectiveEntropy(0.1));
            Console.WriteLine("0.5: " + EfectiveEntropy(0.5));
            Console.WriteLine("0.5: " + EfectiveEntropy(1.0));
            Console.ReadLine();
        }

        static string GetText(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string str;
                    str = Regex.Replace(sr.ReadToEnd(), "[^a-zA-Z]", "");
                    return str;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
         
        static double Shannon(string text, string alphabet)
        {
            double shn = 0;
            for(int i = 0; i < alphabet.Length; i++)
            {
                int count = Regex.Matches(text, alphabet[i].ToString(), RegexOptions.IgnoreCase).Count;
                double p = (count == 0) ? 0 : (double)count / text.Length;
                if (p != 0)
                    shn += p * Math.Log(p, 2);
                Console.WriteLine(alphabet[i].ToString() + "     " + p);
            }
            return -shn;
        }

        static double KolvoInfoShannon(string text, double shn)
        {
            return text.Length * shn;
        }

        static double Hartley(string alphabet)
        {
            return Math.Log(alphabet.Length, 2);
        }

        static string ToBinAscii(string text)
        {
            string str = "";
            byte[] arr = Encoding.ASCII.GetBytes(text);
            int[] b = arr.Select(i => (int)i).ToArray();
            foreach (int i in b)
                str += Convert.ToString(i, 2);
            return str;
        }

       static double EfectiveEntropy(double error)
        {
            return 1 - (-error * Math.Log(error, 2) - (1 - error) * Math.Log((1 - error), 2));
        }
    }
}
