using System;
using System.Collections.Generic;
using System.Linq;

namespace PolyalphaCipher
{
    class Program
    {
        static void Main()
        {

            Dictionary<char, char> M2 = new Dictionary<char, char>();
            M2.Add('a', 'd');
            M2.Add('b', 'k');
            M2.Add('c', 'v');
            M2.Add('d', 'q');
            M2.Add('e', 'f');
            M2.Add('f', 'i');
            M2.Add('g', 'b');
            M2.Add('h', 'j');
            M2.Add('i', 'w');
            M2.Add('j', 'p');
            M2.Add('k', 'e');
            M2.Add('l', 's');
            M2.Add('m', 'c');
            M2.Add('n', 'x');
            M2.Add('o', 'h');
            M2.Add('p', 't');
            M2.Add('q', 'm');
            M2.Add('r', 'y');
            M2.Add('s', 'a');
            M2.Add('t', 'u');
            M2.Add('u', 'o');
            M2.Add('v', 'l');
            M2.Add('w', 'r');
            M2.Add('x', 'g');
            M2.Add('y', 'z');
            M2.Add('z', 'n');

            Console.WriteLine("1)Encrypt\n2)Decrypt");

            int choice = Int32.Parse(Console.ReadLine());
            string plainText, cipherText, file;
            if(choice == 1)
            {
                Console.WriteLine("Enter the path of the text file you wish to encrypt:");
                file = Console.ReadLine();
                plainText = System.IO.File.ReadAllText(file).ToLower();
                cipherText = Encrypt(plainText, M2, 1);
                Console.WriteLine("Ciphertext: " + cipherText);
                System.IO.File.WriteAllTextAsync("cipherTextOut.txt", cipherText);
            }

            else if (choice == 2)
            {
                Console.WriteLine("Enter the path of the text file you wish to decrypt:");
                file = Console.ReadLine();
                cipherText = System.IO.File.ReadAllText(file).ToLower();
                plainText = Decrypt(cipherText, M2, 1);
                Console.WriteLine("PlainText: " + plainText);
                System.IO.File.WriteAllTextAsync("plainTextOut.txt", plainText);
            }

           else
            {
                Console.WriteLine("Please enter a valid choice.");
            }


        }

        public static string LeftShiftThree(char t)
        {
            int value = (int) t - 3;
            if(value < 97)
            {
               value = 123 - (97 - value);
            }

            return ((char)value).ToString();
        }

        public static string RightShiftThree(char t)
        {
            int value = (int)t + 3;
            if (value > 122)
            {
                value = 96 + (value - 122);
            }
            return ((char)value).ToString();
        }

        public static string RightShiftFive(char t)
        {
            int value = (int)t + 5;
            if(value > 122)
            {
                value = 96 + (value - 122);
            }
            return ((char)value).ToString();
        }

        public static string LeftShiftFive(char t)
        {
            int value = (int)t - 5;
            if (value < 97)
            {
                value = 123 - (97 - value);
            }

            return ((char)value).ToString();
        }

        public static string Encrypt(string plainText, Dictionary<char, char> M2, int count)
        {
            if(plainText == "")
            {
                return "";
            }

            if(plainText[0] == ' ')
            {
                return " " + Encrypt(plainText.Substring(1), M2, count);
            }

            if(count % 5 == 1 || count % 5 ==  3)
            {
                return M2[plainText[0]] + Encrypt(plainText.Substring(1), M2, ++count);
            }

            if (count % 5 == 2 || count % 5 == 0)
            {
                return RightShiftFive(plainText[0]) + Encrypt(plainText.Substring(1), M2, ++count);
            }

            return LeftShiftThree(plainText[0]) + Encrypt(plainText.Substring(1), M2, ++count);
        }

        public static string Decrypt(string cipherText, Dictionary<char, char> M2, int count)
        {
            if (cipherText == "")
            {
                return "";
            }

            if (cipherText[0] == ' ')
            {
                return " " + Decrypt(cipherText.Substring(1), M2, count);
            }

            if (count % 5 == 1 || count % 5 == 3)
            {
                return M2.FirstOrDefault(pair => pair.Value == cipherText[0]).Key + Decrypt(cipherText.Substring(1), M2, ++count);
            }

            if (count % 5 == 2 || count % 5 == 0)
            {
                return LeftShiftFive(cipherText[0]) + Decrypt(cipherText.Substring(1), M2, ++count);
            }

            return RightShiftThree(cipherText[0]) + Decrypt(cipherText.Substring(1), M2, ++count);
        }

    }
}
