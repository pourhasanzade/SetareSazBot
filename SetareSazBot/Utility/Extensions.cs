using System;
using System.Collections.Generic;
using System.Linq;

namespace SetareSazBot.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Verify National Code
        /// </summary>
        /// <param name="nationalCode">National Code</param>
        /// <returns>
        /// Output will be <c>true</c> if the national code is correct, other wise it will be <c>false</c>
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public static bool IsValidNationalCode(this string nationalCode)
        {
            if (nationalCode.Length != 10) return false;

            //if the numbers are same
            var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
            if (allDigitEqual.Contains(nationalCode)) return false;


            //the national code algorithm
            var chArray = nationalCode.ToCharArray();
            var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
            var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
            var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
            var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
            var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
            var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
            var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
            var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
            var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
            var a = Convert.ToInt32(chArray[9].ToString());

            var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
            var c = b % 11;

            return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
        }

        public static bool IsValidNationalId(this string nationalId)
        {
            int L = nationalId.Length;
            if (L != 11 || long.Parse(nationalId) == 0) return false;
            if (int.Parse(nationalId.Substring(3, 6)) == 0) return false;
            int c = int.Parse(nationalId.Substring(10, 1));
            int d = int.Parse(nationalId.Substring(9, 1)) + 2;
            int[] z = { 29, 27, 23, 19, 17 };
            int s = 0;
            for (var i = 0; i < 10; i++)
                s += (d + int.Parse(nationalId.Substring(i, 1))) * z[i % 5];
            s = s % 11; if (s == 10) s = 0;
            return (c == s);
        }

        public static bool IsValiChannelId(this string channelId)
        {
            if (string.IsNullOrEmpty(channelId)) return false;
            if (channelId.StartsWith("@")) channelId = channelId.Remove(0, 1);

            if (channelId.Length <= 5 || channelId.Length >= 31) return false;



            foreach (var character in channelId.ToLower())
            {
                if (character >= 'a' && character <= 'z')
                {
                    // ok
                }
                else if (character >= '0' && character <= '9')
                {
                    // ok
                }
                else if (character == '_')
                {
                    // ok
                }
                else
                {
                    return false;
                }
            }

            return true;
        }


        public static bool IsValidVideoExtension(this string extension)
        {
            var validFormats = new List<string> { ".mp4", ".mkv", ".avi", ".3gp", ".wmv", ".webm", ".mov" };
            if (string.IsNullOrEmpty(extension)) return false;

            if (validFormats.Contains(extension)) return true;
            return false;
        }
    }
}