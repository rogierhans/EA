using System;
using System.Collections.Generic;
using System.Linq;

using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace EA
{
    public class CrossOver
    {
        public virtual Tuple<BitArray, BitArray> Cut(BitArray parent1, BitArray parent2, Random rng) { return null;}
    }

    public class TwoPoint : CrossOver {

        public override Tuple<BitArray, BitArray> Cut(BitArray parent1, BitArray parent2, Random rng) {

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
        public override string ToString()
        {
            return "2X";
        }
    }
    public class UniPoint : CrossOver
    {

        public override Tuple<BitArray, BitArray> Cut(BitArray parent1, BitArray parent2, Random rng)
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
        bool randomBool(Random rng)
        {

            return rng.NextDouble() > 0.5;
        }

        public override string ToString()
        {
            return "UX";
        }
    }
}
