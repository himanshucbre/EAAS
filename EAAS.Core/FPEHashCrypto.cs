using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EAAS.Core
{
    public class FPEHashCrypto : SecurityAlgorithm
    {
        private string key;

        public FPEHashCrypto(string key)
        {
            this.key = key;
        }

        public string Process(string message, string key, Mode mode, Dictionary<char, int>  ceaserKey)
        {
            caesarAlphabet = ceaserKey;
            //Key:Charcater
            //Value:Position
            Dictionary<char, string> characterPositionsInMatrix = new Dictionary<char, string>();

            //Key:Position
            //Value:Charcater
            Dictionary<string, char> positionCharacterInMatrix = new Dictionary<string, char>();

            

            FillMatrix(key.Distinct().ToArray(), characterPositionsInMatrix, positionCharacterInMatrix);

            var matches = Regex.Matches(message, "[A-Za-z0-9]+");
            var symbols = Regex.Matches(message, @"[^a-zA-Z0-9]");
            string result = "";
            var wordIndex = 0;
            foreach (var match in matches)
            {
                var wordToRead = ((Capture)match).Value;
                var currentSymbol = symbols.Count <= wordIndex ? string.Empty : symbols[wordIndex].Value;
                wordIndex++;
                for (int i = 0; i < wordToRead.Length; i += 2)
                {
                    if (Convert.ToInt32(wordToRead.Length - i) == 1)
                    {
                        //call caesar cipher for last character of odd word
                        result += GetCeaserCipher(wordToRead.Substring(i, Convert.ToInt32(wordToRead.Length - i)).ToString(), mode);
                    }
                    else
                    {
                        string substring_of_2 = wordToRead.Substring(i, 2);//get characters from text by pairs

                        //get Row & Column of each character
                        string rc1 = characterPositionsInMatrix[substring_of_2[0]];
                        string rc2 = characterPositionsInMatrix[substring_of_2[1]];

                        if (rc1.Equals(rc2))
                        {
                            result += GetCeaserCipher(substring_of_2, mode);
                        }
                        else
                        {
                            if (rc1[0] == rc2[0])//Same Row, different Column
                            {
                                int newC1 = 0, newC2 = 0;

                                switch (mode)
                                {
                                    case Mode.Encrypt://Increment Columns
                                        newC1 = (int.Parse(rc1[1].ToString()) + 1) % 6;
                                        newC2 = (int.Parse(rc2[1].ToString()) + 1) % 6;
                                        break;
                                    case Mode.Decrypt://Decrement Columns
                                        newC1 = (int.Parse(rc1[1].ToString()) - 1) % 6;
                                        newC2 = (int.Parse(rc2[1].ToString()) - 1) % 6;
                                        break;
                                }

                                newC1 = RepairNegative(newC1);
                                newC2 = RepairNegative(newC2);

                                result += positionCharacterInMatrix[rc1[0].ToString() + newC1.ToString()];
                                result += positionCharacterInMatrix[rc2[0].ToString() + newC2.ToString()];
                            }

                            else if (rc1[1] == rc2[1])//Same Column, different Row
                            {
                                int newR1 = 0, newR2 = 0;

                                switch (mode)
                                {
                                    case Mode.Encrypt://Increment Rows
                                        newR1 = (int.Parse(rc1[0].ToString()) + 1) % 6;
                                        newR2 = (int.Parse(rc2[0].ToString()) + 1) % 6;
                                        break;
                                    case Mode.Decrypt://Decrement Rows
                                        newR1 = (int.Parse(rc1[0].ToString()) - 1) % 6;
                                        newR2 = (int.Parse(rc2[0].ToString()) - 1) % 6;
                                        break;
                                }
                                newR1 = RepairNegative(newR1);
                                newR2 = RepairNegative(newR2);

                                result += positionCharacterInMatrix[newR1.ToString() + rc1[1].ToString()];
                                result += positionCharacterInMatrix[newR2.ToString() + rc2[1].ToString()];
                            }

                            else//different Row & Column
                            {
                                //1st character:row of 1st + col of 2nd
                                //2nd character:row of 2nd + col of 1st
                                result += positionCharacterInMatrix[rc1[0].ToString() + rc2[1].ToString()];
                                result += positionCharacterInMatrix[rc2[0].ToString() + rc1[1].ToString()];
                            }
                        }
                    }
                }
                result += currentSymbol;
            }
            return result;
        }

        private void FillMatrix(IList<char> key, Dictionary<char, string> characterPositionsInMatrix, Dictionary<string, char> positionCharacterInMatrix)
        {
            char[,] matrix = new char[6, 6];
            int keyPosition = 0, charPosition = 0;
            List<char> alphabetPF = alphabet.Keys.ToList();

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (charPosition < key.Count)
                    {
                        matrix[i, j] = key[charPosition];//fill matrix with key
                        alphabetPF.Remove(key[charPosition]);
                        charPosition++;
                    }

                    else//key finished...fill with rest of alphabet
                    {
                        matrix[i, j] = alphabetPF[keyPosition];
                        keyPosition++;
                    }

                    string position = i.ToString() + j.ToString();
                    //store character positions in dictionary to avoid searching everytime
                    characterPositionsInMatrix.Add(matrix[i, j], position);
                    positionCharacterInMatrix.Add(position, matrix[i, j]);
                }
            }
        }

        private string GetCeaserCipher(string message, Mode mode)
        {
            string result = string.Empty;

            foreach (char c in message)
            {
                var charposition = caesarAlphabet[c];
                switch (mode)
                {
                    case Mode.Encrypt:
                        result += caesarAlphabet.ElementAt((int)charposition).Key.ToString();
                        break;
                    case Mode.Decrypt:
                        int indexValue = caesarAlphabet.Keys.ToList().IndexOf(c);
                        result += caesarAlphabet.FirstOrDefault(x => x.Value == indexValue).Key.ToString();
                        break;
                }
            }
            return result;
        }

        private int RepairNegative(int number)
        {
            if (number < 0)
            {
                number += 6;
            }

            return number;
        }
    }
}
