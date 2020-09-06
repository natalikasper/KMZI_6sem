using System.Collections.Generic;

namespace Lab4.Cezar
{
    class Caesar : List<Clent>
    {
        public Caesar()
        {
            //в конструкторе формир.коллекцию лент
            Add(new Clent("abcdefghijklmnopqrstuvwxyz"));
            Add(new Clent("ABCDEFGHIJKLMNOPQRSTUVWXYZÄÜÖß"));
        }

        public string CaesarChipher(string message, int key)
        {

            string res = "";
            string tmp = "";
            for (int i = 0; i < message.Length; i++)
            {
                foreach (Clent v in this)
                {
                    tmp = v.Change(message.Substring(i, 1), key);
                    //нужная лента найдена, замена символу опредлена
                    if (tmp != "")
                    {
                        res += tmp;
                        break;
                    }
                }
                if (tmp == "")
                    //незнакомый символ оставляем без изменений
                    res += message.Substring(i, 1);
            }
            return res;
        }
    }
}
