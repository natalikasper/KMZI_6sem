using System;
using System.Collections.Generic;
using System.Text;

namespace KMZI6X
{
    public class EnigmaMachine
    {
        private Dictionary<Char, Char> plugBoard;

        private Rotor[] rotors;
        private Rotor reflector;

        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const string rotorIconf = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        private const string rotorIIconf = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        private const string rotorIIIconf = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        private const string rotorIVconf = "ESOVPZJAYQUIRHXLNFTGKDCMWB";
        private const string rotorBetaconf = "LEYJVCNIXWPBQMDRTAKZFGUHOS";
        private const string rotorGammaconf = "FSOKANUERHMBTIYCWLQPZXVGJD";
        
        private const string reflectorAconf = "EJMZALYXVBWFCRQUONTSPIKHGD";
        private const string reflectorBconf = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        private const string reflectorCconf = "FVPJIAOYEDRZXWGCTKUQSBNMHL";
        private const string reflectorCDUNNconf = "RDOBJNTKVEHMLFCWZAXGYIPSUQ";
        private class Rotor
        {
            private int outerPosition;
            public char outerChar { get; set; }
            private string wiring;
            private char turnOver;
            public string name { get; }
            public char ring { get; set; }

            public int[] map { get; }
            public int[] revMap { get; }
            public Rotor(string w, char to, string n)
            {
                turnOver = to;
                outerPosition = 0;

                ring = 'A';
                name = n;

                map = new int[26];
                revMap = new int[26];

                setWiring(w);
            }

            public void setWiring(string newW)
            {
                wiring = newW;
                outerChar = wiring.ToCharArray()[outerPosition];
                for (int i = 0; i < 26; i++)
                {
                    int match = ((int)wiring.ToCharArray()[i]) - 65;
                    map[i] = (26 + match - i) % 26;
                    revMap[match] = (26 + i - match) % 26;
                }
            }

            public void setOuterPosition(int i)
            {
                outerPosition = i;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }

            public int getOuterPosition()
            {
                return outerPosition;
            }

            public void setOuterChar(char c)
            {
                outerChar = c;
                outerPosition = alphabet.IndexOf(outerChar);
            }

            public void step()
            {
                outerPosition = (outerPosition + 4) % 26;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public void step2()
            {
                outerPosition = (outerPosition + 1) % 26;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public void step3()
            {
                outerPosition = (outerPosition + 1) % 26;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public bool isInTurnOver()
            {
                return outerChar == turnOver;
            }
        }

        private void rotateRotors(Rotor[] r)
        {
            if (r.Length == 3)
            {
                if (r[1].isInTurnOver())
                {
                    r[0].step3();
                    r[1].step2();
                }
                else if (r[2].isInTurnOver())
                {
                    r[1].step2();
                }
                r[2].step();
            }
        }

        private char rotorMap(char c, bool reverse)
        {
            int cPos = (int)c - 65;
            if (!reverse)
            {
                for (int i = rotors.Length - 1; i >= 0; i--)
                {
                    cPos = rotorValue(rotors[i], cPos, reverse);
                }
            }
            else
            {
                for (int i = 0; i < rotors.Length; i++)
                {
                    cPos = rotorValue(rotors[i], cPos, reverse);
                }
            }

            return alphabet.ToCharArray()[cPos];
        }
        private int rotorValue(Rotor r, int cPos, bool reverse)
        {
            int rPos = (int)r.ring - 65;
            int d;
            if (!reverse)
                d = r.map[(26 + cPos + r.getOuterPosition() - rPos) % 26];
            else
                d = r.revMap[(26 + cPos + r.getOuterPosition() - rPos) % 26];

            return (cPos + d) % 26;
        }
        private char reflectorMap(char c)
        {
            int cPos = (int)c - 65;
            cPos = (cPos + reflector.map[cPos]) % 26;
            return alphabet.ToCharArray()[cPos];
        }

        public EnigmaMachine()
        {
            plugBoard = new Dictionary<char, char>();
            //сиволы на которые ротор поворачивается
            Rotor rIV = new Rotor(rotorIVconf, 'J', "IV");
            Rotor rII = new Rotor(rotorIIconf, 'E', "II");
            Rotor rIII = new Rotor(rotorIIIconf, 'V', "III");
            rotors = new Rotor[] { rIV, rIII, rII };
            reflector = new Rotor(reflectorCDUNNconf, ' ', "");
        }

        public void setReflector(char conf)
        {
            if (conf != 'A' && conf != 'B' && conf != 'C' && conf != 'D')
            {
                throw new ArgumentException("Invalid argument");
            }

            string wiring = "";
            switch (conf)
            {
                case 'A':
                    wiring = reflectorAconf;
                    break;
                case 'B':
                    wiring = reflectorBconf;
                    break;
                case 'C':
                    wiring = reflectorCconf;
                    break;
                case 'D':
                    wiring = reflectorCDUNNconf;
                    break;
            }
            reflector.setWiring(wiring);
        }

        public void setSettings(char[] rings, char[] grund)
        {
            if (rings.Length != rotors.Length || grund.Length != rotors.Length)
            {
                throw new ArgumentException("Invalid argument lengths");
            }

            for (int i = 0; i < rotors.Length; i++)
            {
                rotors[i].ring = Char.ToUpper(rings[i]);
                rotors[i].setOuterChar(Char.ToUpper(grund[i]));
            }
        }

        public void setSettings(char[] rings, char[] grund, string rotorOrder)
        {
            Rotor rI = null;
            Rotor rII = null;
            Rotor rIII = null;
            Rotor rBeta = new Rotor(rotorBetaconf, 'Z', "Beta");
            Rotor rGamma = new Rotor(rotorGammaconf, 'Z', "Gamma");
            Rotor rIV = new Rotor(rotorIVconf, 'J', "IV");
            // Get the current ordering
            for (int i = 0; i < rotors.Length; i++)
            {
                if (rotors[i].name == "I")
                    rI = rotors[i];
                if (rotors[i].name == "II")
                    rII = rotors[i];
                if (rotors[i].name == "III")
                    rIII = rotors[i];
                if (rotors[i].name == "IV")
                    rIV = rotors[i];
            }

            string[] order = rotorOrder.Split('-');

            // Set the new ordering
            for (int i = 0; i < order.Length; i++)
            {
                if (order[i] == "I")
                    rotors[i] = rI;
                if (order[i] == "II")
                    rotors[i] = rII;
                if (order[i] == "III")
                    rotors[i] = rIII;
                if (order[i] == "IV")
                    rotors[i] = rIV;
                if (order[i] == "Gamma" || order[i] == "GAMMA")
                    rotors[i] = rGamma;
                if (order[i] == "Beta" || order[i] == "BETA")
                    rotors[i] = rBeta;
            }

            setSettings(rings, grund);
        }

        public void setSettings(char[] rings, char[] grund, string rotorOrder, char reflectorConf)
        {
            setReflector(reflectorConf);
            setSettings(rings, grund, rotorOrder);
        }

        public string runEnigma(string msg)
        {
            StringBuilder encryptedMessage = new StringBuilder();

            msg = msg.ToUpper();

            foreach (char c in msg)
            {
                encryptedMessage.Append(encryptChar(c));
            }

            return encryptedMessage.ToString();
        }

        // Encrypts (or decrypts) a single character
        private char encryptChar(char c)
        {
            rotateRotors(rotors);

            // Into plugboard from keyboard <--
            if (plugBoard.ContainsKey(c))
            {
                c = plugBoard[c];
            }
            c = rotorMap(c, false);
            c = reflectorMap(c);
            c = rotorMap(c, true);
            if (plugBoard.ContainsKey(c))
            {
                c = plugBoard[c];
            }
            return c;
        }

        public void addPlug(char c, char cc)
        {
            if (Char.IsLetter(c) && Char.IsLetter(cc))
            {
                c = Char.ToUpper(c);
                cc = Char.ToUpper(cc);
                if (c != cc && !plugBoard.ContainsKey(c))
                {
                    plugBoard.Add(c, cc);
                    plugBoard.Add(cc, c);
                }
            }
            else
            {
                throw new ArgumentException("Invalid character");
            }
        }
    }
}

