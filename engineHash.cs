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
        private const int ONE = 1, TWO = 2;
        private int mod = BASEMOD;

        public Hash() {
            Debug.WriteLine("Hash class successfully initialized");
        }
        /**
         * Convert a text into a hash
         * @param  {string} string text          the text to convert
         * @return {Hash_Return}        the hash
         */
        public Hash_Return getHash(string text) {
            string initial_hash = calcHash(text);

            string return_hash = calcAndConvertHash(initial_hash);

            Hash_Return hash_return = new Hash_Return(addStartingHash(return_hash), isRight(return_hash), String.Empty);

            return hash_return;
        }
        /**
         * Convert the thing into a String, then calc it, return an error if you can't convert the thing into a String
         * @param  {any} any text          the thing to convert
         * @return {Hash_Return}     the hash
         */
        public Hash_Return getHashFromAnyType(dynamic text) {
            try {
                string text_converted = text.ToString();

                string initial_hash = calcHash(text_converted);

                string return_hash = calcAndConvertHash(initial_hash);

                Hash_Return hash_return = new Hash_Return(addStartingHash(return_hash), isRight(return_hash), String.Empty);
                return hash_return;
            } catch (InvalidCastException ex) {
                // catch a !IConvertible exception
                Hash_Return hash_return = new Hash_Return(String.Empty, false, "!IConvertible " + ex);
                return hash_return;
            } catch (Exception ex) {
                // catch a generic exception
                Hash_Return hash_return = new Hash_Return(String.Empty, false, "Generic ex " + ex);
                return hash_return;
            }
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
        /**
         * calc and conert the array encrypted to an ${NBCHAR} array length
         * @param  {[type]} string hash          the text encrypted
         * @return {[type]}        the converted array
         */
        private string calcAndConvertHash(string hash) {
            int[] values = parse(hash);

            values = calcAllCells(values);

            return convertArraytoString(values);
        }
        private int[] parse(string chain) {
            List<int> return_list = new List<int>();

            for (int index = 0; index < chain.Length; index += 2) {
                return_list.Add(appendValue(chain, index));
            }
            return return_list.ToArray();
        }
        private int appendValue(string chain, int index) {
            int temp_value = (int)chain[index];
            if (!isOutOfRange(chain, (index + 1))) {
                temp_value += (int)chain[index + 1];
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
            /**TODO :
             *  calc one cell :D
             */
            int first_index = getIndexInRange(p_index, values.Length);
            int second_index = getIndexInRange(p_index + ONE, values.Length);
            int third_index = getIndexInRange(p_index + TWO, values.Length);

            

            return getIntInRange(temp_value);
        }
        private int getIndexInRange(int init_val, int range_max) {
            return Math.Abs(init_val - range_max);
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
        private string error;
        public Hash_Return(string hash, bool isRight, string error) {
            this.hash = hash;
            this.isRight = isRight;
            this.error = error;
        }
        public string Hash {
            get { return this.hash; }
        }
    	public bool IsRight {
    		get { return this.isRight; }
    	}
        public string Error {
            get { return this.error; }
        }
    }
}