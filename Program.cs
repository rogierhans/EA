using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rng = new Random();

            BitArray genotype = init(rng);

        }
        static BitArray init(Random rng)
        {
            BitArray genotype = new BitArray(100);
            for (int i = 0; i < genotype.Count; i++)
            {
                genotype[i] = rng.NextDouble() > 0.5;
            }
            return genotype;
        }
        static List<BitArray> initPop(int n)
        {
            List<BitArray> pop = new List<BitArray>;
            return pop;

        }

        static void scramble(List<BitArray> pop, Random rnd)
        {
            pop = pop.OrderBy(a => rng.Next()).ToList();
        }


    }
}
