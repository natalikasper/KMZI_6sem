using System;
using System.Linq;

namespace Test
{
    class Trisemus
    {
        static void Main(string[] args)
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyzÄÜÖß,.".ToLower().ToCharArray();
            
            //размерность таблицы
            Console.WriteLine("Символов в алфавите: " + alphabet.Length);
            int rows = 0, columns;
            bool isValidTable;
            do
            {
                Console.Write("Количество колонок в таблице: ");
                isValidTable = int.TryParse(Console.ReadLine(), out columns) && columns > 1;
                if (!isValidTable)
                {
                    Console.WriteLine("Необходимо ввести число больше 1");
                }
                else
                {
                    rows = alphabet.Length / columns;
                    if (rows * columns == alphabet.Length)
                        isValidTable &= rows > 1;
                    if (!isValidTable)
                        Console.WriteLine("Необходимо ввести число колонок таким образом, " +
                            "чтобы число строк таблицы было больше 1 и таблица могла вмещать в себе все символы алфавита");
                    else
                        Console.WriteLine("Размерность таблицы:\n [колонок, строк] \n[{0}, {1}]\n", columns, rows);
                }
            }
            while (!isValidTable);

            //ключевое слово
            char[] keyWord;
            bool isValidKeyWord;
            do
            {
                Console.Write("Введите ключевое слово: ");
                keyWord = Console.ReadLine().ToLower().Distinct().ToArray();
                isValidKeyWord = keyWord.Length > 0 && keyWord.Length <= alphabet.Length;
                if (!isValidKeyWord)
                {
                    Console.WriteLine("Ключевое слово не может быть пустой строкой или содержать" +
                        " число уникальных символов больше размера алфавита");
                }
                else
                {
                    isValidKeyWord &= !keyWord.Except(alphabet).Any();
                    if (!isValidKeyWord)
                    {
                        Console.WriteLine("Ключевое слово не может содержать символы, которых нет в алфавите");
                    }
                }
            }
            while (!isValidKeyWord);

            // Создаем таблицу
            var table = new char[rows, columns];

            // Вписываем слово
            for (var i = 0; i < keyWord.Length; i++)
            {
                table[i / columns, i % columns] = keyWord[i];
            }

            alphabet = alphabet.Except(keyWord).ToArray();

            // Вписываем алфавит
            for (var i = 0; i < alphabet.Length; i++)
            {
                int position = i + keyWord.Length;
                table[position / columns, position % columns] = alphabet[i];
            }

            //исходный текст
            string message;
            bool isValidMessage;
            do
            {
                Console.Write("Введите сообщение: ");
                message = Console.ReadLine().ToLower();
                isValidMessage = !string.IsNullOrEmpty(message);
                if (!isValidMessage)
                {
                    Console.WriteLine("Сообщение не может быть пустой строкой");
                }
            }
            while (!isValidMessage);

            // Создаем место для будущего зашифрованного сообщения
            var result = new char[message.Length];

            long sc = DateTime.Now.Ticks;
            // Шифрование
            for (var k = 0; k < message.Length; k++)
            {
                char symbol = message[k];
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            symbol = table[(i + 1) % rows, j]; // Смещаемся циклически на следующую строку таблицы и запоминаем новый символ
                            i = rows; // Завершаем цикл по строкам
                            break; // Завершаем цикл по колонкам
                        }
                    }
                }
                result[k] = symbol;
            }

            Console.WriteLine("Зашифрованное сообщение: " + new string(result));
            long sc2 = (DateTime.Now.Ticks - sc) / 1000;
            Console.WriteLine("Время выполнения расшифрования:" + sc2 + "\n");

            long sec = DateTime.Now.Ticks;
            //расшифровка
            var result2 = new char[result.Length];
            for (var k = 0; k < result.Length; k++)
            {
                char symbol = result[k];
                // Пытаемся найти символ в таблице
                for (var i = 0; i < rows; i++)
                {
                    for (var j = 0; j < columns; j++)
                    {
                        if (symbol == table[i, j])
                        {
                            symbol = table[(i - 1) % rows, j]; // Смещаемся циклически на следующую строку таблицы и запоминаем новый символ
                            i = rows; // Завершаем цикл по строкам
                            break; // Завершаем цикл по колонкам
                        }
                    }
                }
                result2[k] = symbol;
            }
            Console.WriteLine("Расшифрованное сообщение: " + new string(result2));
            long sec2 = (DateTime.Now.Ticks - sec)/1000;
            Console.WriteLine("Время выполнения расшифрования:" + sec2);

            Console.WriteLine();
            Console.ReadKey();
        }
    }
}