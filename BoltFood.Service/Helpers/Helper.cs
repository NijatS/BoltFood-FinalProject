using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoltFood.Service.Extentions
{
    public static class Helper
    {
        public static bool CheckName(this string sentence)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (string.IsNullOrEmpty(sentence))
            {
                WriteSlowLine("Name is null!");
                return false;
            }
            if (char.IsLower(sentence[0]))
            {
                WriteSlowLine("The First Letter must be upperCase");
                return false;
            }
            if (sentence.Length < 3)
            {
                WriteSlowLine("Name Length must be bigger than 3");
                return false;
            }
            return true;
        }
        public static bool passwordCheck(this string sentence)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int upper = 0;
            int digit = 0;
            for (int i = 0; i < sentence.Length; i++)
            {
                if (!char.IsUpper(sentence[i]))
                {
                    upper++;
                }
                if (char.IsDigit(sentence[i]))
                {
                    digit++;
                }
            }
            if (sentence == null)
            {
                WriteSlowLine("Password is null!");
                return false;
            }
            if (upper == sentence.Length)
            {
                WriteSlowLine("The Password must contain minimum 1 upperCase");
                return false;
            }
            if (sentence.Length < 3)
            {
                WriteSlowLine("Name Length must be bigger than 3");
                return false;
            }
            if (digit == 0)
            {
                WriteSlowLine("Name Length must contain Digits");
                return false;
            }
            return true;
        }
        public static bool CheckUserName(this string sentence)  
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string pattern = "^([a-z0-9]+[-._])*([a-z0-9])+\\@+([a-z]+[.])*([a-z]){2,8}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(sentence))
            {
                WriteSlowLine("UserName must contain @ ");
                return false;
            }
            return true;
        }
        public static void WriteSlowLine(string sentence)
        {
            char[] chars = sentence.ToCharArray();

            foreach (Char item in chars)
            {
                Console.Write(item);
                Thread.Sleep(50);
            }
            Console.WriteLine();
        }
        public static void WriteSlow(string sentence, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            char[] chars = sentence.ToCharArray();
            foreach (Char item in chars)
            {
                Console.Write(item);
                Thread.Sleep(50);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WriteSlowLine(string sentence,ConsoleColor color)
        {
            Console.ForegroundColor = color;
            char[] chars = sentence.ToCharArray();
            foreach (Char item in chars)
            {
                Console.Write(item);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            
        }
    }
}
