using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic.Utilities
{
    public static class CodeGenerator
    {
        public static string GenerateCode(int type)
        {
            string code = "";
            switch (type)
            {
                case 1:
                    code = OTPgenerator.GenerateCode();
                    break;
                case 2:
                    code = HexaGenerator.GenerateCode();
                    break;
                default:
                    break;
            }

            return code;                
           
        }



    }



}
