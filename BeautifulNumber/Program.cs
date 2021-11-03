using System;

namespace BeautifulNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("Enter a integer number: ");
            int num = int.Parse(Console.ReadLine());

            bool result = Program.CheckBeautifulNumber(num);

            if(result)
            {
                Console.WriteLine("{0} is beautiful number!", num);
            }
            else
            {
                Console.WriteLine("{0} is not beautiful number!", num);
            }
        }

        public static bool CheckBeautifulNumber(int num)
        {
            char[] arrChar = num.ToString().ToCharArray();

            for (int i = 0; i < arrChar.Length; i++)
            {
                if(arrChar[i] == '6' || arrChar[i] == '8')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static int CountBeatifulNumberInArray(int[] arrNum)
        {
            int count = 0;

            for (int i = 0; i < arrNum.Length; i++)
            {
                if(CheckBeautifulNumber(arrNum[i]))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
