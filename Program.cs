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
            List<BitArray> pop = initPop(1000, rng);
            //pop.ForEach(x => print(x));
            //Console.WriteLine("scrabmebemllb");
            //pop = scramble(pop, rng);
            //pop.ForEach(x => print(x));

            pop.ForEach(geno =>
            {
                print(geno);
                Console.WriteLine(Fun1(geno));

                Console.WriteLine(Fun2(geno));

                Console.WriteLine(Fun3(geno));

                Console.WriteLine(Fun4(geno));
                Console.WriteLine();
            });
            Console.ReadLine();
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
                genotype[i] = randomBool(rng);
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

                    child1[i] = parent2[i];
                    child2[i] = parent1[i];
                }
                else
                {

                    child1[i] = parent1[i];
                    child2[i] = parent2[i];
                }
            }
            return new Tuple<BitArray, BitArray>(child1, child2);
        }
        static Tuple<BitArray, BitArray> uniCut(BitArray parent1, BitArray parent2, Random rng)
        {
            BitArray child1 = new BitArray(parent1.Count);
            BitArray child2 = new BitArray(parent1.Count);

            for (int i = 0; i < parent1.Count; i++)
            {
                if (randomBool(rng))
                {
                    child1[i] = parent1[i];
                    child2[i] = parent2[i];
                }
                else
                {
                    child1[i] = parent2[i];
                    child2[i] = parent1[i];
                }
            }
            return new Tuple<BitArray, BitArray>(child1, child2);
        }


        static bool randomBool(Random rng)
        {

            return rng.NextDouble() > 0.5;
        }

        static double Fun1(BitArray genotype)
        {

            double fitness = 0;
            for (int i = 0; i < genotype.Count; i++)
            {
                if (genotype[i]) fitness++;
            }
            return fitness;
        }
        static double Fun2(BitArray genotype)
        {

            double fitness = 0;
            for (int i = 0; i < genotype.Count; i++)
            {
                if (genotype[i]) fitness += i + 1;
            }
            return fitness;
        }
        static double Fun3(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += BlockCalc(genotype[i], genotype[i + 1], genotype[i + 2], genotype[i + 3], 1);
            }
            return fitness;
        }
        static double Fun4(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += BlockCalc(genotype[i], genotype[i + 1], genotype[i + 2], genotype[i + 3], 2.5);
            }
            return fitness;
        }
        static double BlockCalc(bool i1, bool i2, bool i3, bool i4, double d)
        {
            int cox = (i1 ? 1 : 0) + (i2 ? 1 : 0) + (i3 ? 1 : 0) + (i4 ? 1 : 0);
            if (cox == 4) return 4;
            else return (4 - d - ((4 - d) / 3) * cox);
        }




    }
}
