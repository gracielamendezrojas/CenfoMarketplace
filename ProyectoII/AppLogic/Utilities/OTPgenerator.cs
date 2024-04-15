using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Utilities
{
    public static class OTPgenerator 
    {
        private static int codeSize = 4;
        private static Random random = new Random();

        public static string GenerateCode()
        {
            const string chars = "abcdfghijklmnopqrxyz0123456789";
            return new string(Enumerable.Repeat(chars, codeSize)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
