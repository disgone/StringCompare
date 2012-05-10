using System;
using System.Collections.Generic;
using System.Web;

namespace StringCompare
{
    public static class Levenshtein
    {
    	public static int GetDistance(string a, string b)
    	{
            if (a == null || b == null)
            {
                return -1;
            }

            int n = a.Length;
            int m = b.Length;

            if (n == 0)
                return m;
            else if (m == 0)
                return n;

            if (n > m)
            {
                string tmp = a;
                a = b;
                b = tmp;
                n = m;
                m = b.Length;
            }

            int[] d = new int[n + 1];
            int[] p = new int[n + 1];
            int[] _ph; // placeholder

            int i, j;
            char t_j;
            int cost;

            for (i = 0; i < n; i++)
            {
                p[i] = i;
            }

            for (j = 1; j <= m; j++)
            {
                t_j = b[j - 1];
                d[0] = j;

                for (i = 1; i <= n; i++)
                {
                    cost = a[i - 1] == t_j ? 0 : 1;
                    d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + cost);
                }

                _ph = p;
                p = d;
                d = _ph;
            }

            return p[n];
    	}
    }
}