using System;
using System.IO;
using System.Collections.Generic;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            //[421,457]         [2,457]
            Console.WriteLine("1. Поиск простых чисел на интервале.");
            for (int j = 0; j < 2; j++)
            {
                int count = 0;
                Console.WriteLine("Введите начало интервала: ");
                int s1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите конец интервала: ");
                int s2 = int.Parse(Console.ReadLine());
                Console.WriteLine("Простые числа на интервале [{0}, {1}]:", s1, s2);
                for (int i = s1; i <= s2; i++)
                {
                    if (IsPrimeNumber(i))
                    {
                        count++;
                        Console.WriteLine(i);
                    }
                }
                Console.WriteLine("Количество простых чисел:{0}", count.ToString());
            }

            Console.WriteLine("\n\n2. Нахождение НОД для 2 чисел:");
            Console.WriteLine("a = ");
            int A = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("b = ");
            int B = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Наибольший общий делитель чисел {0} и {1} равен {2}", A, B, GCD(A, B));

            Console.WriteLine("\n2. Нахождение НОД для 3 чисел:");
            Console.WriteLine("c = ");
            int c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("d = ");
            int d = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("e = ");
            int e = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Наибольший общий делитель чисел {0}, {1} и {2} равен {3}", c, d, e, GCD(GCD(c,d),e));

            //обратное число по модулю
            Console.WriteLine("\n\n3.Нахождение обратного числа по модулю: ");
            Console.WriteLine("a = ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("n = ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Обратное число по модулю, при a = {0}, n = {1} равно: {2}", a, n, GetReverse(a, n));
            Console.WriteLine("\nОбратное число по модулю, при а = {0}, n = {1}, с помощью расширенного алгоритма Евклида равно:{2}", a, n, RE(a, n));

            Console.WriteLine("\n\nЯвляется ли конкатенация m и n простым числом?");
            Console.WriteLine("Введите число для проверки:");
            int concat = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("concat(m,n) = {0} ", concat);
            Console.WriteLine(IsPrimeNumber(concat));

            Console.WriteLine("\n\nРазложение числа на простые сомножители:");
            Console.Write("\nВведите число: ");
            int num = int.Parse(Console.ReadLine());
            Console.Write("{0} = 1", num);
            for (int i = 0; num % 2 == 0; num /= 2)
            {
                Console.Write(" * {0}", 2);
            }
            for (int i = 3; i <= num;)
            {
                if (num % i == 0)
                {
                    Console.Write(" * {0}", i);
                    num /= i;
                }
                else
                {
                    i += 2;
                }
            }

            Console.ReadKey();
        }

        //циклич.алгоритм нахождения нод
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
      
        public static int GetReverse(int a, int n)
        {
            for (int x = 1; x < Int32.MaxValue; x++)
            {
                if ((a * x) % n == 1)
                    return x;
            }
            return 0;
        }
        public static bool IsPrimeNumber(int n)
        {
            var result = true;
            if (n > 1)
            {
                for (int i = 2; i < n; i++)
                {
                    if (n % i == 0)
                    {
                        result = false;
                        break;
                    }
                    else
                        result = true;
                }
            }
            else
                result = false;
            return result;
        }
        
        public static int RE(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }

        public static int GCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
    }
}