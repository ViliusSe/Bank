using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases
{
    public class IbanGenerator
    {
        public static string GenerateIban()
        {
            string result = "LT";

            Random random = new Random();
            result += random.Next(10, 99);

            for (int x = 0; x < 5; x++)
            {
                result += "-" + random.Next(1000, 9999);
            }
            return result;
        }



    }

}
