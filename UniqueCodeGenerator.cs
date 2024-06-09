using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizFlash
{
    internal class UniqueCodeGenerator
    {
        private static Random random = new Random();
        private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string GenerateUniqueCode()
        {
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                int index = random.Next(characters.Length);
                code.Append(characters[index]);
            }

            return code.ToString();
        }
    }
}
