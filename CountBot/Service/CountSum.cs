using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountBot.Service
{
    public class CountSum
    {
        public int Counter(string numbers)
        {
            int sum = 0;
            var tokens = numbers.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                if (int.TryParse(token, out int number))
                {
                    sum += number;
                }
            }
            return sum;
        }
    }
}
