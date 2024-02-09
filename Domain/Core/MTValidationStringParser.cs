using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Core
{
    /// <summary>
    /// Provides simple validation confirmation for SWIFT MT rules
    /// </summary>
    public class MTValidationStringParser
    {
        private static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        private static string GetNumbers(string str)
        {
            var stripped = Regex.Replace(str, "[^0-9]", "");
            return stripped;
        }
        public static bool FromXCharSet(string data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!Constants.XCharSet.Contains(data[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool FromYCharSet(string data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!Constants.YCharSet.Contains(data[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool FromZCharSet(string data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!Constants.ZCharSet.Contains(data[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }
        public static int ExtractTotalLineNumber(string validation)
        {
            string[] iterationArray = validation.Split('\n');
            int length = iterationArray.Length;
            foreach (string line in iterationArray)
            {
                if (line.Contains('*'))
                {
                    string[] splitArr = validation.Split('*');
                    string numberOfLines = GetNumbers(splitArr[0]);
                    length += int.Parse(numberOfLines);
                }
            }
            return length;
        }
        public static (int min, int max) ExtractTotalCharacterNumber(string validation)
        {
            int min = 0;
            int max = 0;

            string? digits = "";

            Boolean optional = false;

            Boolean multiplicative = false;
            int bracketedMin = 0;
            int bracketedMax = 0;


            foreach (char c in validation)
            {
                if (Char.IsDigit(c))
                {
                    digits += c;
                    continue;
                }
                else if (Char.IsLetter(c) && Constants.ValidationCharSet.Contains(c.ToString()))
                {
                    var computed = int.Parse(digits);
                    if (multiplicative)
                    {
                        if (optional) bracketedMax += computed;
                        else
                        {
                            bracketedMin += computed;
                            bracketedMax += computed;
                        }
                    }
                    else
                    {
                        if (optional) max += computed;
                        else
                        {
                            min += computed;
                            max += computed;
                        }
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '\n':

                            break;
                    }
                }
            }

            return (0, 0);
        }
        public static (int min, int max) RecurFindCharLength(string validation, (int min, int max) a)
        {
            string? digits = "";
            for (int i = 0; i < validation.Length; i++)
            {
                char c = validation[i];
                if (Char.IsDigit(c))
                {
                    digits += c;
                }
                else if (Char.IsLetter(c))
                {

                }
            }
            return a;
        }
    }
}
