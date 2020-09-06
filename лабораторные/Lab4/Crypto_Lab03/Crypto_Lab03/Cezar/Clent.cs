using System;

namespace Lab4.Cezar
{
    class Clent
    {
        string message;

        //к-р, кот.приним.строку любой длины
        public Clent(string message)
        {
            this.message = message;
        }
        //ф-ция замены символа symbol на символ со смещением
        //вернет новый символ в закальцованной ленте
        //закальцованная лента - м.склеить 2 конца (начало и конец алфавита)
        public string Change(string symbol, int key)
        {
            int position = message.IndexOf(symbol);
            //символ в этой ленте не найден
            if (position == -1)
                return "";

            position = (position + key) % message.Length;

            if (position < 0)
                position += message.Length;

            return message.Substring(position, 1);
        }
    }
}
