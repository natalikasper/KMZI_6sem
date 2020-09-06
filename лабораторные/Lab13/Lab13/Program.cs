using System;
using System.Numerics;
using System.Collections.Generic;

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            int y2;
            int a = -1, b = 1, p = 751, k = 9, l = 7, d = 12;
            string P = "56, 332";
            string Q = "69, 241";
            string R = "83, 373";
            Dictionary<char, (int x, int y)> hash = new Dictionary<char, (int x, int y)>();
            Dictionary<char, int> hash2;
            Dictionary<(int x, int y), char> obrhash = new Dictionary<(int x, int y), char>();

            Console.WriteLine("\tЗадание 1.1. Точки ЭК для значений x из промежутка [106, 140]: ");
            for (int i = 106; i <= 140; i++)
            {
                y2 = ((i * i * i - i + a) % p);
                Console.Write(y2 + ", ");
            }

            Console.WriteLine("\n\n\tЗадание 1.2. Результат выполнения операций над кривой: ");
            Console.Write("1)Результат kP: ");
            string[] numb = P.Split(',');
            int xP1 = int.Parse(numb[0]);
            int yP1 = int.Parse(numb[1]);
            string ResultMultiply = '(' + Multiply(k, xP1, yP1) + ')';
            Console.WriteLine(ResultMultiply + " ");

            Console.Write("\n2)Результат P + Q: ");
            string[] numbers = P.Split(',');
            string[] numbers2 = Q.Split(',');
            int xP = int.Parse(numbers[0]);
            int xQ = int.Parse(numbers2[0]);
            int yP = int.Parse(numbers[1]);
            int yQ = int.Parse(numbers2[1]);
            string ResultSum = '(' + SumTwoPoints(xP, xQ, yP, yQ) + ')';
            Console.WriteLine(ResultSum + " ");

            Console.Write("\n3)Результат kP + lQ - R: ");
            string[] n = P.Split(',');
            int xP2 = int.Parse(n[0]);
            int yP2 = int.Parse(n[1]);
            string[] Expr1 = Multiply(k, xP2, yP2).Split(',');
            string[] n2 = Q.Split(',');
            int xQ2 = int.Parse(n2[0]);
            int yQ2 = int.Parse(n2[1]);
            string[] Expr2 = Multiply(l, xQ2, yQ2).Split(',');
            string[] Expr3 = SumTwoPoints(int.Parse(Expr1[0]), int.Parse(Expr2[0]), int.Parse(Expr1[1]), int.Parse(Expr2[1])).Split(',');
            string[] n3 = R.Split(',');
            int xR = int.Parse(n3[0]);
            int yR = int.Parse(n3[1]);
            string ResultExpr = '(' + SumTwoPoints(int.Parse(Expr3[0]), xR, int.Parse(Expr3[1]), mod(-yR, p)) + ')';
            Console.WriteLine(ResultExpr + " ");

            Console.Write("\n4)Результат P - Q + R: ");
            string[] num = P.Split(',');
            int xP3 = int.Parse(num[0]);
            int yP3 = int.Parse(num[1]);
            string[] num2 = Q.Split(',');
            int xQ3 = int.Parse(num2[0]);
            int yQ3 = int.Parse(num2[1]);
            string[] Expr = SumTwoPoints(xP3, xQ3, yP3, mod(-yQ3, p)).Split(',');
            string[] num3 = R.Split(',');
            int xR3 = int.Parse(num3[0]);
            int yR3 = int.Parse(num3[1]);
            string ResultExpr2 = '(' + SumTwoPoints(int.Parse(Expr[0]), xR3, int.Parse(Expr[1]), yR3) + ')';
            Console.WriteLine(ResultExpr2 + " ");

            Console.WriteLine("\n\tЗадание 2.Зашифровать данные: ");
            hash = new Dictionary<char, (int x, int y)>();
            
            hash.Add('К', (200, 30));
            hash.Add('А', (189, 297));
            hash.Add('С', (206, 645));
            hash.Add('П', (205, 379));
            hash.Add('Е', (194, 546));
            hash.Add('Р', (206, 106));
            obrhash.Add((200, 30), 'К');
            obrhash.Add((189, 297), 'А');
            obrhash.Add((206, 645), 'С');
            obrhash.Add((205, 379), 'П');
            obrhash.Add((194, 546), 'Е');
            obrhash.Add((206, 106), 'Р');

            string text = "КАСПЕР";
            string TextToDecrypt = "";
            (int x, int y) cort;
            int xG = 0;
            int yG = 1;
            string[] numbersQ = Multiply(d, xG, yG).Split(',');
            int xQ5 = int.Parse(numbersQ[0]);
            int yQ5 = int.Parse(numbersQ[1]);
            foreach (char s in text)
            {
                hash.TryGetValue(s, out cort);
                string numbersC1 = Multiply(k, xG, yG);
                string[] numbersExpr1 = Multiply(k, xQ5, yQ5).Split(',');
                string numbersC2 = SumTwoPoints(cort.x, int.Parse(numbersExpr1[0]), cort.y, int.Parse(numbersExpr1[1]));
                TextToDecrypt += numbersC1 + ' ' + numbersC2 + ' ';
            }
            TextToDecrypt = TextToDecrypt.Remove(TextToDecrypt.Length - 1);
            Console.WriteLine(TextToDecrypt);

            Console.WriteLine("\n\tЗадание 3. Расшифровать данные: ");
            string[] text2 = TextToDecrypt.Split(' ');
            string TextToEncrypt = "";
            for (int i = 0; i < text2.Length; i += 2)
            {
                string[] numbe1 = text2[i].ToString().Split(',');
                string[] numbe2 = text2[i + 1].ToString().Split(',');
                string[] numbersC1 = Multiply(d, int.Parse(numbe1[0]), int.Parse(numbe1[1])).Split(',');
                string[] result = SumTwoPoints(int.Parse(numbe2[0]), int.Parse(numbersC1[0]), int.Parse(numbe2[1]), mod(-int.Parse(numbersC1[1]), p)).Split(',');
                (int x, int y) cort2 = (int.Parse(result[0]), int.Parse(result[1]));
                char s;
                obrhash.TryGetValue(cort2, out s);
                TextToEncrypt += s;
            }
            Console.WriteLine(TextToEncrypt);

            Console.WriteLine("\n\tЗадание 5. Верификация ЭЦП");
            Init();
            WithString("КАСПЕР");
            Console.ReadLine();
        }
        private static Dictionary<char, int> hash2;
        public static string SumTwoPoints(int xP, int xQ, int yP, int yQ)
        {
            int p = 751;
            BigInteger lyambda;
            int raznX = xQ - xP;
            int raznY = yQ - yP;
            if (raznX < 0)
            {
                raznX += p;
            }
            if (raznY < 0)
            {
                raznY += p;
            }
            if (xP == 0 & yP == 0)
            {
                return xQ.ToString() + ',' + yQ.ToString();
            }
            if (xQ == 0 & yQ == 0)
            {
                return xP.ToString() + ',' + yP.ToString();
            }
            BigInteger xR = 0, yR = 0;
            if (xP == xQ && yP != yQ || (yP == 0 && yQ == 0 && xP == xQ))
            { }
            else
            {
                if (xP == xQ && yP == yQ)
                {
                    lyambda = (3 * BigInteger.Pow(xP, 2) - 1) * (Foo(2 * yP, p));
                }
                else
                {
                    lyambda = (raznY) * Foo(raznX, p);
                }
                xR = (BigInteger.Pow(lyambda, 2) - xP - xQ);
                yR = yP + lyambda * (xR - xP);
                xR = xR % p < 0 ? (xR % p) + p : xR % p;
                yR = -yR % p < 0 ? (-yR % p) + p : (-yR % p);
            }
            string Result = xR.ToString() + ',' + yR.ToString();
            return Result;
        }
        public static int Foo(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }
        public static int GCD(int a, int b, out int x, out int y)
        {
            int p = 751;
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
            return d % p;
        }
        public static string Multiply(int k, int xP, int yP)
        {
            string[] numbers = { "", "" };
            int xQ = xP;
            int yQ = yP;
            string[] result = { "" };
            string[] addend = { xQ.ToString(), yQ.ToString() };
            while (k > 0)
            {
                if ((k & 1) > 0)
                {
                    if (result.Length == 2)
                    {
                        result = SumTwoPoints(int.Parse(result[0]), int.Parse(addend[0]), int.Parse(result[1]), int.Parse(addend[1])).Split(',');
                    }
                    else
                    {
                        result = addend;
                    }
                }
                addend = SumTwoPoints(int.Parse(addend[0]), int.Parse(addend[0]), int.Parse(addend[1]), int.Parse(addend[1])).Split(',');
                k >>= 1;
            }
            return result[0] + "," + result[1];
        }
        public static int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
        public static void Init()
        {
            hash2 = new Dictionary<char, int>();
            hash2.Add('К', 200);
            hash2.Add('А', 189);
            hash2.Add('С', 206);
            hash2.Add('П', 205);
            hash2.Add('Е', 194);
            hash2.Add('Р', 206);
        }
        private static string WithString(string str)
        {
            int hashstr = 0;
            int outval;
            int q = 13;
            foreach (char s in str)
            {
                hash2.TryGetValue(s, out outval);
                hashstr += outval % q;
            }
            int d = 4;
            string[] numbersQ = Multiply(d, 416, 55).Split(',');
            string[] numbersG = Multiply(6, 416, 55).Split(',');
            int x = int.Parse(numbersG[0]);
            int r = mod(x, q) + 4;
            int t = 11 % q;
            int sign = mod((t * (hashstr + d * r)), q);
            string Sign = r.ToString() + ',' + sign.ToString();
            Console.WriteLine(Sign);
            int w = mod(Foo(sign, q), q);
            int u1 = mod((w * hashstr), q);
            int u2 = mod((w * r), q);
            string[] Expr1 = { "", "" };
            string[] Expr2 = { "", "" };
            Expr1 = Multiply(u1, int.Parse(numbersG[0]), int.Parse(numbersG[1])).Split(',');
            Expr2 = Multiply(u2, int.Parse(numbersQ[0]), int.Parse(numbersQ[1])).Split(',');
            string[] result;
            result = SumTwoPoints(int.Parse(Expr1[0]), int.Parse(Expr2[0]), int.Parse(Expr1[1]), int.Parse(Expr2[1])).Split(',');
            int v = mod(int.Parse(result[0]), q);
            bool f = v == r;
            string Verify = f.ToString();
            Console.WriteLine(Verify);
            return Verify;
        }
    }
}