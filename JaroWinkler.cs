using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace StringCompare
{
    public class JaroWinkler
    {
        private string matcha;
        private string matchb;

        public JaroWinkler()
        {
            
        }

        public double GetSimilarity(string a, string b)
        {
            if(String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b))
                return 0;

            double weight = 0;
            int halflen = Math.Min(a.Length, b.Length) / 2 + 1;
            string commonA = GetCommonCharacters(a, b, halflen);
            int common = commonA.Length;

            if (common == 0)
                return 0;

            string commonB = GetCommonCharacters(b, a, halflen);
            if (common != commonB.Length)
                return 0;

            int transpositions = 0;
            for (int i = 0; i < common; i++)
            {
                if (commonA[i] != commonB[i])
                    transpositions++;
            }

            transpositions /= 2;
            weight = common / (3.0 * a.Length) + common / (3.0 * b.Length) + (common - transpositions) / (3.0 * common);

            return weight;
        }

        public string GetCommonCharacters(string a, string b, int distance)
        {
            string commons = "";
            char[] copy = b.ToCharArray();

            for (int i = 0; i < a.Length; i++)
            {
                char ch = a[i];
                bool found = false;
                for (int j = Math.Max(0, i - distance); !found && j < Math.Min(i + distance, b.Length); j++)
                {
                    if (copy[j] == ch)
                    {
                        found = true;
                        commons += ch;
                        copy[j] = '#';
                    }
                }
            }

            return commons;
        }
    }
}
