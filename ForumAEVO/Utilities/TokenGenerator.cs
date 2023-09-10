using System;
using System.Text;

namespace ForumAEVO.Utilities
{
    public class TokenGenerator // chamando a classe string fakeToken = TokenGenerator.GenerateFakeToken();
    {
            private static readonly Random _random = new Random();

            public static string GenerateFakeToken(int length = 32)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var tokenChars = new char[length];

                for (int i = 0; i < length; i++)
                {
                    tokenChars[i] = chars[_random.Next(chars.Length)];
                }

                return new string(tokenChars);
            }
        }
}
