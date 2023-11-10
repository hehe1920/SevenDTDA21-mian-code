namespace SevenDTDMono.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class Extras
    {
        private const string ExtraChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static Random random = new Random();

        public static void LogAvailableBuffNames(string filePath)
        {
            SortedDictionary<string, BuffClass> dictionary = new SortedDictionary<string, BuffClass>(BuffManager.Buffs, StringComparer.OrdinalIgnoreCase);
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Buff Name,Damage Type,Description");
                    foreach (KeyValuePair<string, BuffClass> pair in dictionary)
                    {
                        if (pair.Key.Equals(pair.Value.LocalizedName))
                        {
                            writer.WriteLine(pair.Key ?? "");
                        }
                        else
                        {
                            writer.WriteLine(pair.Key + " (" + pair.Value.LocalizedName + ")");
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error occurred while logging buff classes: " + exception.Message);
            }
        }

        public static string ScrambleString(string input)
        {
            char[] chArray = input.ToCharArray();
            for (int i = chArray.Length - 1; i > 0; i--)
            {
                int index = random.Next(0, i + 1);
                char ch = chArray[i];
                chArray[i] = chArray[index];
                chArray[index] = ch;
            }
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < chArray.Length; j++)
            {
                builder.Append(chArray[j]);
                if (random.Next(0, 4) == 0)
                {
                    char ch2 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"[random.Next(0, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".Length)];
                    builder.Append(ch2);
                }
            }
            return builder.ToString();
        }
    }
}

