using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAAS.Core
{
    public class SecurityAlgorithm
    {
        protected readonly Dictionary<char, int> alphabet;
        protected readonly Dictionary<char, int> caesarAlphabet;
        public SecurityAlgorithm()
        {
            caesarAlphabet = new Dictionary<char, int>();
            Dictionary<char, int> alphaNumericList = new Dictionary<char, int>();

            var count = 0;
            for (var i = 'a'; i <= 'z'; i++)
            {
                alphaNumericList.Add(i, count++);
            }
            for (var i = '0'; i <= '9'; i++)
            {
                alphaNumericList.Add(i, count++);
            }

            alphabet = new Dictionary<char, int>(alphaNumericList);

            //reShuffled list
            caesarAlphabet = alphaNumericList.Shuffle();
        }
    }
}
