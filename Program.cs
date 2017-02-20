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


            Dictionary<int, int> mapping = createdMapping(100, rng);

            List<BitArray> pop = initPop(10, rng);
            //List<BitArray> offSpring = createOffspring(pop,Fun1);
            //pop.ForEach(x => print(x));
            //Console.WriteLine("scrabmebemllb");
            //pop = scramble(pop, rng);
            //pop.ForEach(x => print(x));
            Function f = new Function1();
            Console.WriteLine(f.Evaluate(pop.First()));
            Console.ReadLine();

            pop.ForEach(geno =>
            {
                print(geno);
                Console.WriteLine();
            });
            Console.ReadLine();
        }

        private static List<BitArray> createOffspring(List<BitArray> pop,Random rng, Function f)
        {
            //pop = scramble(pop,rng);
            //for (int i = 0; i < pop.Count; i = i+2)
            //{
            //    Tuple<BitArray, BitArray> children;
            throw new NotImplementedException();
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



        static bool randomBool(Random rng)
        {

            return rng.NextDouble() > 0.5;
        }

        public static double BlockCalc(bool i1, bool i2, bool i3, bool i4, double d)
        {
            int cox = (i1 ? 1 : 0) + (i2 ? 1 : 0) + (i3 ? 1 : 0) + (i4 ? 1 : 0);
            if (cox == 4) return 4;
            else return (4 - d - ((4 - d) / 3) * cox);
        }

        static Dictionary<int, int> createdMapping(int k, Random rng)
        {
            Dictionary<int, int> mapping = new Dictionary<int, int>();
            List<int> indices = new List<int>();

            for (int i = 0; i < k; i++)
            {
                indices.Add(i);
            }
            List<int> scrambledIndices = indices.ToList().OrderBy(a => rng.Next()).ToList();

            foreach (int index in indices)
            {
                mapping[index] = scrambledIndices[index];
            }
            return mapping;
        }


    }
}
