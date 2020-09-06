using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab11
{
    class Hash 
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку для хеширования: ");
            string str = Console.ReadLine();

            Console.WriteLine("MD5: ");
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            long OldTick = DateTime.Now.Ticks;
            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();
            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            Console.WriteLine(hash);
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTick) / 1000} мс\n\n");

            long Ticks = DateTime.Now.Ticks;
            Console.WriteLine("SHA1: ");
            string salt2 = CreateSalt(15);
            Console.WriteLine("Соль: " + salt2);
            Console.WriteLine(HashSHa1(str, salt2));
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - Ticks) / 1000} мс\n\n");

            Console.WriteLine("SHA-256: ");
            long OldTicks = DateTime.Now.Ticks;
            string salt = CreateSalt(15);
            string hash2 = GenerateSHA256(str, salt);

            Console.WriteLine("M:  " + str + "\nСоль: " + salt + "\nХэш:  " + hash2);
            Console.WriteLine($"Время: {(DateTime.Now.Ticks - OldTicks) / 1000} мс\n\n");
            Console.ReadKey();
        }

        public static string CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string HashSHa1(string str, string salt)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash3 = sha1.ComputeHash(Encoding.UTF8.GetBytes(str+salt));
                var sb = new StringBuilder(hash3.Length * 2);
                foreach (byte b in hash3)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string GenerateSHA256(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256hashstring = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ToHex(hash);
        }

        public static string ToHex(byte[] ba)
        {

            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

    }
}
