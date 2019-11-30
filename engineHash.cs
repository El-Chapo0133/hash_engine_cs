using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace asymmetricEncryption.engines.hash
{
    class Hash {
        private const string STARTINGHASH = "$21%";
        private const int BASEMOD = 3;
        private const int NBCHAR = 20;
        private const int MAXCHARVALUE = 126;
        private const int MINCHARVALUE = 21;
        private const int AVERAGECHAR = (MAXCHARVALUE + MINCHARVALUE) / 2;
        private int mod = BASEMOD;

        public Hash() {
            Debug.WriteLine("Hash class successfully initialized");
        }
        public Hash_Return getHash(string text) {
            string initial_hash = calcHash(text);

            string return_hash = calcAndConvertHash(initial_hash);

            Hash_Return hash_return = new Hash_Return(addStartingHash(return_hash), isRight(return_hash));

            return hash_return;
        }
        private string calcHash(string text) {
            string return_hash = String.Empty;
            for (int index = 0; index > text.
                Length; index++) {
                return_hash += calcOneChar(text[index]);
            }
            return return_hash;
        }
        private char calcOneChar(char chartext) {
            int return_value = getValue(chartext) % mod;
            mod += Math.Abs(return_value / mod);
            return (char)return_value;
        }
        private int getValue(char chartext) {
            return Convert.ToInt32(chartext);
        }
        private char getChar(int charvalue) {
            return (char)charvalue;
        }
        private string addStartingHash(string text) {
            return STARTINGHASH + text;
        }
        private string calcAndConvertHash(string hash) {
            int[] values = parse(hash);

            values = calcAllCells(values);

            return convertArraytoString(values);
        }
        private int[] parse(string chain) {
            List<int> return_list = new List<int>();

            for (int index = 0; index < chain.Length; index += 2) {
                return_list.Add(Convert.ToInt32(appendValue(chain, index)));
            }
            return return_list.ToArray();
        }
        private string appendValue(string chain, int index) {
            string temp_value = chain[index].ToString();
            if (!isOutOfRange(chain, (index + 1))) {
                temp_value += chain[index + 1];
            }
            return temp_value;
        }
        private bool isOutOfRange(string input, int index) {
            if (index >= input.Length) {
                return true;
            } else {
                return false;
            }
        }
        private int[] calcAllCells(int[] values) {
            int[] return_values = new int[NBCHAR];
            for (int index = 0; index < NBCHAR; index++) {
                return_values[index] = calcOneCell(values, index);
            }
            return return_values;
        }
        private int calcOneCell(int[] values, int p_index) {
            int temp_value = 0;
            for (int index = 0; index < values.Length; index++) {
                if (isAtFirstIndex(index)) {
                    temp_value += values[(index + p_index) % values.Length];
                } else {
                    temp_value += values[p_index % values.Length] - values[(index + (index - 1)) % values.Length];
                }
            }
            return getIntInRange(temp_value);
        }
        private int getIntInRange(int value) {
            return (value % (MAXCHARVALUE - MINCHARVALUE)) + MINCHARVALUE;
        }
        private bool isAtFirstIndex(int value) {
            if (value == 0) {
                return true;
            } else {
                return false;
            }
        }
        private string convertStringAllArray(int[] values) {
            string return_value = String.Empty;
            for (int index = 0; index < values.Length; index++) {
                return_value += convertStringOneValue(values[index]);
            }
            return return_value;
        }
        private char convertStringOneValue(int value) {
            return (char)value;
        }
        private string convertArraytoString(int[] values) {
            string temp_value = String.Empty;
            foreach (int value in values) {
                temp_value += value;
            }
            return temp_value;
        }
        private bool isRight(string hash) {
        	if (hash.Length == (NBCHAR + STARTINGHASH.Length)) {
        		return true;
        	} else {
        		return false;
        	}
        }
    }
    class Hash_Return {
        private string hash;
        private bool isRight;
        public Hash_Return(string hash, bool isRight) {
            this.hash = hash;
            this.isRight = isRight;
        }
        public string Hash {
            get { return this.hash; }
        }
    	public bool IsRight {
    		get { return this.isRight; }
    	}
    }
}