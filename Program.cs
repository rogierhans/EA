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
            List<BitArray> pop = initPop(10, rng);
            pop.ForEach(x => print(x));
            Console.WriteLine("scrabmebemllb");
            pop = scramble(pop, rng);
            pop.ForEach(x => print(x));
        }

        private static void print(BitArray genotype)
        {
            for (int i = 0; i < genotype.Count; i++)
            {
                Console.Write(genotype[i] ? 1 : 0);
            }
            Console.WriteLine();
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
        static List<BitArray> initPop(int n, Random rng)
        {
            List<BitArray> pop = new List<BitArray>();
            for (int i = 0; i < n; i++)
            {
                pop.Add(init(rng));
            }
            return pop;
        }

        static List<BitArray> scramble(List<BitArray> pop, Random rng)
        {
            return pop.OrderBy(a => rng.Next()).ToList();
        }

        //asssume parents have smae lnegeht else fukc youself
        static Tuple<BitArray, BitArray> twoCut(BitArray parent1, BitArray parent2, Random rng)
        {
            BitArray child1 = new BitArray(parent1.Count);
            BitArray child2 = new BitArray(parent1.Count);
            int rInt1 = rng.Next(parent1.Count);
            int rInt2 = rng.Next(parent1.Count);
            int low = rInt1 < rInt2 ? rInt1 : rInt2;
            int high = rInt1 < rInt2 ? rInt2 : rInt1;

            for (int i = 0; i < parent1.Count; i++)
            {
                if (i < low)
                {
                    child1[i] = parent1[i];
                    child2[i] = parent2[i];
                }
                else if (i >= low && i <= high)
                {

                    child1[i] = parent1[i];
                    child2[i] = parent2[i];
                }
                else
                {

                }
            }
            return new Tuple<BitArray, BitArray>(child1, child2);
        }
    }
}
