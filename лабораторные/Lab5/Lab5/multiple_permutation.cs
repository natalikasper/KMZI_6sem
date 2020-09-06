using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.XlsIO;
using System.IO;
using System.Text;

namespace Lab5
{
    class CharNum
    {
        #region Fields
        private char _ch;
        private int _numberInWord;
        #endregion Fieds

        #region Properties
        public char Ch
        {
            get { return _ch; }
            set
            {
                if (_ch == value)
                    return;
                _ch = value;
            }
        }
        public int NumberInWord
        {
            get { return _numberInWord; }
            set
            {
                if (_numberInWord == value)
                    return;
                _numberInWord = value;
            }
        }
        #endregion Properties
    }

    class multiple_permutation
    {
        static void Main(string[] args)
        {
            /*
            // Первый ключ, количество столбцов
            string firstKey = "Kasper";
            // Второй ключ, количество строк
            string secondKey = "Nat";
            // Предложение которое шифруем
            string stringUser = "Laboratornaya rabo";
            */
            // Первый ключ, количество столбцов
            string firstKey = (Console.ReadLine()).ToString();
            // Второй ключ, количество строк
            string secondKey = (Console.ReadLine()).ToString();
            // Предложение которое шифруем
            string stringUser = (Console.ReadLine()).ToString();

            // Матрица в которой производим шифрование
            char[,] matrix = new char[secondKey.Length, firstKey.Length];

            // Счетчик символов в строке
            int countSymbols = 0;

            // Переводим строки в массивы типа char
            char[] charsFirstKey = firstKey.ToLower().ToCharArray();
            char[] charsSecondKey = secondKey.ToLower().ToCharArray();
            char[] charStringUser = stringUser.ToLower().ToCharArray();

            // Создаем списки в которых будут храниться символы и порядковы номера символов
            List<CharNum> listCharNumFirst =
                new List<CharNum>(firstKey.Length);

            List<CharNum> listCharNumSecond =
                new List<CharNum>(secondKey.Length);

            // Заполняем символами из ключей
            listCharNumFirst = FillListKey(charsFirstKey);
            listCharNumSecond = FillListKey(charsSecondKey);

            // Заполняем порядковыми номерами
            listCharNumFirst = FillingSerialsNumber(listCharNumFirst);
            listCharNumSecond = FillingSerialsNumber(listCharNumSecond);

            ShowKey(listCharNumFirst, "Первый ключ: ");
            ShowKey(listCharNumSecond, "Второй ключ: ");

            long sec = DateTime.Now.Ticks;
            // Заполнение матрицы строкой пользователя
            for (int i = 0; i < listCharNumSecond.Count; i++)
            {
                for (int j = 0; j < listCharNumFirst.Count; j++)
                {
                    matrix[i, j] = charStringUser[countSymbols++];
                }
            }
            ShowMatrix(matrix, "Первоначальное значение: ");
            long sec2 = (DateTime.Now.Ticks - sec)/1000;
            Console.WriteLine("Время выполнения процесса построения исходной матрицы: " + sec2 + "\n");

            countSymbols = 0;

            // Заполнение матрицы с учетом шифрования. 
            // Переставляем столбцы по порядку следования в первом ключе. 
            // Затем переставляем строки по порядку следования во втором ключа. 
            long sc = DateTime.Now.Ticks;
            for (int i = 0; i < listCharNumSecond.Count; i++)
            {
                for (int j = 0; j < listCharNumFirst.Count; j++)
                {
                    matrix[listCharNumSecond[i].NumberInWord,
                       listCharNumFirst[j].NumberInWord] = charStringUser[countSymbols++];
                }
            }
            ShowMatrix(matrix, "Зашифрованное значение: ");
            long sc2 = (DateTime.Now.Ticks - sc) / 1000;
            Console.WriteLine("Время выполнения процесса зашифрования:" + sc2 + "\n");

            Console.ReadKey();
        }

        #region Methods
        // Возвращает порядковый номер символа по алфавиту.
        public static int GetNumberInThealphabet(char s)
        {
            string str = @"abcdefghijklmnopqrstuvwxyz";
            
            int number = str.IndexOf(s) / 2;

            return number;
        }
        // Заполнение символами списка с ключом.
        public static List<CharNum> FillListKey(char[] chars)
        {
            List<CharNum> listKey = new List<CharNum>(chars.Length);

            for (int i = 0; i < chars.Length; i++)
            {
                CharNum charNum = new CharNum()
                {
                    Ch = chars[i],
                    NumberInWord = GetNumberInThealphabet(chars[i])
                };
                listKey.Add(charNum);
            }
            return listKey;
        }
        // Отображение ключа.
        public static void ShowKey(List<CharNum> listCharNum, string message)
        {
            Console.WriteLine(message);

            foreach (var i in listCharNum)
            {
                Console.Write(i.Ch + " ");
            }
            Console.WriteLine();

            foreach (var i in listCharNum)
            {
                Console.Write(i.NumberInWord + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        // Заполнение символов ключей, порядковыми номерами.
        public static List<CharNum> FillingSerialsNumber(List<CharNum> listCharNum)
        {
            int count = 0;

            var result = listCharNum.OrderBy(a => a.NumberInWord);

            foreach (var i in result)
            {
                i.NumberInWord = count++;
            }

            return listCharNum;
        }

        // Отображение матрицы.
        public static void ShowMatrix(char[,] matrix, string message)
        {
            Console.WriteLine(message);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        #endregion Methods
    }
}