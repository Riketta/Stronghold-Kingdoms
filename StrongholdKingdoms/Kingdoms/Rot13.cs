namespace Kingdoms
{
    using System;

    internal static class Rot13
    {
        public static string Transform(string value)
        {
            char[] chArray = value.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                int num2 = chArray[i];
                if ((num2 >= 0x61) && (num2 <= 0x7a))
                {
                    if (num2 > 0x6d)
                    {
                        num2 -= 13;
                    }
                    else
                    {
                        num2 += 13;
                    }
                }
                else if ((num2 >= 0x41) && (num2 <= 90))
                {
                    if (num2 > 0x4d)
                    {
                        num2 -= 13;
                    }
                    else
                    {
                        num2 += 13;
                    }
                }
                chArray[i] = (char) num2;
            }
            return new string(chArray);
        }
    }
}

