using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA
{
    public class Function
    {
        virtual public double Evaluate(BitArray genotype) { return 0; }
        public double BlockCalc(bool i1, bool i2, bool i3, bool i4, double d)
        {
            int cox = (i1 ? 1 : 0) + (i2 ? 1 : 0) + (i3 ? 1 : 0) + (i4 ? 1 : 0);
            if (cox == 4) return 4;
            else return (4 - d - ((4 - d) / 3) * cox);
        }
    }

    


    public class Function1 : Function
    {
        public override double Evaluate(BitArray genotype)
        {

            double fitness = 0;
            for (int i = 0; i < genotype.Count; i++)
            {
                if (genotype[i]) fitness++;
            }
            return fitness;
        }
        public override string ToString()
        {
            return "USCO";
        }
    }
    public class Function2 : Function
    {
        public override double Evaluate(BitArray genotype)
        {

            double fitness = 0;
            for (int i = 0; i < genotype.Count; i++)
            {
                if (genotype[i]) fitness += i + 1;
            }
            return fitness;

        }
        public override string ToString()
        {
            return "LSCO";
        }
    }
    public class Function3 : Function
    {
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += BlockCalc(genotype[i], genotype[i + 1], genotype[i + 2], genotype[i + 3], 1);
            }
            return fitness;

        }
        public override string ToString()
        {
            return "DTT";
        }
    }



    public class Function4 : Function
    {
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += BlockCalc(genotype[i], genotype[i + 1], genotype[i + 2], genotype[i + 3], 2.5);
            }
            return fitness;

        }
        public override string ToString()
        {
            return "NDTT";
        }
    }

    public class Function5 : Function
    {
        Dictionary<int, int> mapping;
        public Function5(Dictionary<int, int> mapping) { this.mapping = mapping; }
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += BlockCalc(genotype[mapping[i]], genotype[mapping[i + 1]], genotype[mapping[i + 2]], genotype[mapping[i + 3]], 1);
            }
            return fitness;

        }
        public override string ToString()
        {
            return "DTR";
        }
    }

    public class Function6 : Function
    {
        Dictionary<int, int> mapping;
        public Function6(Dictionary<int,int> mapping) { this.mapping = mapping; }
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += BlockCalc(genotype[mapping[i]], genotype[mapping[i + 1]], genotype[mapping[i + 2]], genotype[mapping[i + 3]], 2.5);
            }
            return fitness;
        }
        public override string ToString()
        {
            return "NDTR";
        }
    }

}
