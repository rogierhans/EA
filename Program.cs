using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EA
{
    class Program
    {
        static void Main(string[] args)
        {

            //List<int> tegenVoorbeeld = new List<int>();
 








            Random rng = new Random();

            Dictionary<int, int> mapping = createdMapping(100, rng);

            //List<BitArray> offSpring = createOffspring(pop,Fun1);
            //pop.ForEach(x => print(x));
            //Console.WriteLine("scrabmebemllb");
            //pop = scramble(pop, rng);
            //pop.ForEach(x => print(x));
            Function f1 = new Function1();
            Function f2 = new Function2();
            Function f3 = new Function3();
            Function f4 = new Function4();
            Function f5 = new Function5(mapping);
            Function f6 = new Function6(mapping);
            CrossOver UX = new UniPoint();
            CrossOver TX = new TwoPoint();

            Table niels = new Table(f1);
            niels.ExportCSV();
        }


        //asssume parents have smae lnegeht else fukc youself
        //Is rpopven corrtly dont dublechek
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


    public class Population
    {
        List<GenoType> pop;
        Random rng;
        public Population(int n, Random r)
        {
            rng = r;
            pop = initPop(n);
        }

        List<GenoType> initPop(int n)
        {
            List<GenoType> pop = new List<GenoType>();
            for (int i = 0; i < n; i++)
            {
                GenoType geno = new GenoType(rng);
                pop.Add(geno);
            }
            return pop;
        }
        public void createOffspring(Random rng, Function f, CrossOver cs)
        {
            scramble();
            int popSize = pop.Count;
            for (int i = 0; i < popSize; i = i + 2)
            {
                GenoType p1 = pop[i];
                GenoType p2 = pop[i + 1];
                Tuple<BitArray, BitArray> childrenTuple = cs.Cut(p1.Bits, p2.Bits, rng);
                GenoType c1 = new GenoType(rng, childrenTuple.Item1);
                GenoType c2 = new GenoType(rng, childrenTuple.Item2);

                //Console.WriteLine("---------------------------------------");
                //Console.WriteLine("parent1: {0}", p1.print());
                //Console.WriteLine("parent2: {0}", p2.print());
                //Console.WriteLine("child1:  {0}", c1.print());
                //Console.WriteLine("child2:  {0}", c2.print());
                //Console.ReadLine();

                //aan dezelfde lijst toevoegen # gevaarlijk zeg jeez
                pop.Add(c1);
                pop.Add(c2);
            }

            //yolo hackaton ez
            pop = pop.OrderByDescending(x => f.Evaluate(x.Bits)).Take(popSize).ToList();


            ////dit is voor mijzelf dit is inefficient
            //double weightPopulation = 0;
            //pop.ForEach(x => weightPopulation += f.Evaluate(x.Bits));
            //Console.WriteLine(weightPopulation);
        }
        public void print()
        {
            pop.ForEach(geno =>
            {
                Console.WriteLine(geno.print());
            });
        }
        public void scramble()
        {
            pop = pop.OrderBy(a => rng.Next()).ToList();
        }

        internal bool convergecheck()
        {
            bool same = true;
            int i = 1;
            while (same && i < pop.Count())
            {
                same = sameGeno(pop[0], pop[i]);
                i++;
            }
            return same;

        }

        private bool sameGeno(GenoType genoType1, GenoType genoType2)
        {
            bool equal = true;
            int i = 0;
            while (equal && i < genoType1.Bits.Count)
            {
                equal = genoType1.Bits[i] == genoType2.Bits[i];
                i++;
            }
            return equal;
        }

        internal bool checkGlobalOptimum(Function f)
        {
            foreach (GenoType genoType in pop) {
                bool allOnes = true;
                foreach (bool bit in genoType.Bits) {
                    allOnes = allOnes && bit;
                }
                if (allOnes) return true;
            }
            return false;
        }
    }
    public class GenoType
    {
        Random rng;
        public BitArray Bits;
        public GenoType(Random r)
        {
            rng = r;
            Bits = init();

        }
        public GenoType(Random r, BitArray bits)
        {
            rng = r;
            Bits = bits;
        }
        BitArray init()
        {
            BitArray genotype = new BitArray(100);
            for (int i = 0; i < genotype.Count; i++)
            {
                genotype[i] = randomBool();
            }
            return genotype;
        }
        bool randomBool()
        {
            return rng.NextDouble() > 0.5;
        }
        public string print()
        {
            string s = "";
            for (int i = 0; i < Bits.Count; i++)
            {
                s += Bits[i] ? 1 : 0;
            }
            return s;
        }
    }

}
