using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA
{
    class Function
    {
        virtual public double Evaluate(BitArray genotype) { return 0; }
    }

    class Function1 : Function
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

    }
    class Function2 : Function
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

    }
    class Function3 : Function
    {
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += Program.BlockCalc(genotype[i], genotype[i + 1], genotype[i + 2], genotype[i + 3], 1);
            }
            return fitness;

        }

    }
    class Function4 : Function
    {
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += Program.BlockCalc(genotype[i], genotype[i + 1], genotype[i + 2], genotype[i + 3], 2.5);
            }
            return fitness;

        }

    }
    
    class Function5 : Function
    {
        Dictionary<int, int> mapping;
        public Function5(Dictionary<int, int> mapping) { this.mapping = mapping; }
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += Program.BlockCalc(genotype[mapping[i]], genotype[mapping[i + 1]], genotype[mapping[i + 2]], genotype[mapping[i + 3]], 1);
            }
            return fitness;

        }

    }

    class Function6 : Function
    {
        Dictionary<int, int> mapping;
        public Function6(Dictionary<int,int> mapping) { this.mapping = mapping; }
        public override double Evaluate(BitArray genotype)
        {
            double fitness = 0;
            for (int i = 0; i < genotype.Count; i = i + 4)
            {
                fitness += Program.BlockCalc(genotype[mapping[i]], genotype[mapping[i + 1]], genotype[mapping[i + 2]], genotype[mapping[i + 3]], 2.5);
            }
            return fitness;
        }

    }

}
