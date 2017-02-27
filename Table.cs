using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace EA
{
    public class Table
    {
        public Function F;
        List<Row> rows = new List<Row>();
        public Table(Function f)
        {
            this.F = f;
            CrossOver TX = new TwoPoint();
            CrossOver UX = new UniPoint();
            foreach (CrossOver cs in new CrossOver[] { TX,UX })
                foreach (int popSize in new int[] { 50,100,250,500})
                {
                    Row row = new Row(f,cs,popSize);
                    row.run();
                    rows.Add(row);
                }
        }

        public void ExportCSV() {
            List<string> allLines = new List<string>();
            allLines.Add("CrossOver , PopSize , Successes , Gen.(First Hit) , Gen. (Converge) , Fct Evals , CPU Time");
            foreach (Row row in rows) {
                allLines.Add(row.convertToRealRow());

            }
            File.WriteAllLines("C:\\Users\\niels\\Desktop\\Niels\\Table.csv", allLines.ToArray());
        }

    }

    public class Row
    {
        Function F;
        CrossOver Cs;
        int PopSize;
        Random rng = new Random();

        int CountSucces;
        List<int> FirstHits;
        List<int> GenConverges;
        List<int> FctEvals;
        List<int> CPUTime;

        public Row(Function f, CrossOver cs, int popSize)
        {
            F = f;
            Cs = cs;
            PopSize = popSize;
            FirstHits = new List<int>();
            GenConverges = new List<int>();
            FctEvals = new List<int>();
            CPUTime = new List<int>();
        }


        public void run()
        {
            for (int run = 0; run < 25; run++)
            {
                Population pop = new Population(PopSize, rng);
                bool converged = false;
                int gen = 0;
                int firstHit = -1 ;
                bool firstHitted = false;
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                while (!converged)
                {
                    gen++;
                    pop.createOffspring(rng, F, Cs);
                    if (!firstHitted &&
                        pop.checkGlobalOptimum(F))
                    {
                        firstHit = gen;
                        firstHitted = true;
                    }
                    converged = pop.convergecheck();
                }
                int genConverge = gen;
                bool Succes = pop.checkGlobalOptimum(F);
                int fctEval = gen * PopSize * 2;
                int totalTime = (int)stopWatch.ElapsedMilliseconds;
                //pop.print();
                //Console.WriteLine("Function {0} with Crossover {1}", F, Cs);


                if (Succes) CountSucces++;
                if (firstHitted) FirstHits.Add(firstHit);
                GenConverges.Add(genConverge);
                FctEvals.Add(fctEval);
                CPUTime.Add(totalTime);

            }
        }

        public void print()
        {
            Console.WriteLine(PopSize);
            Console.WriteLine(CountSucces);
            Console.WriteLine((double)FirstHits.Sum(x => x) / FirstHits.Count);
            Console.WriteLine((double)GenConverges.Sum(x => x) / GenConverges.Count);
            Console.WriteLine((double)FctEvals.Sum(x => x) / FctEvals.Count);
            Console.WriteLine((double)CPUTime.Sum(x => x) / CPUTime.Count);
        }

        public string convertToRealRow() {
            List<string> s = new List<string>();
            s.Add(Cs.ToString());
            s.Add(PopSize.ToString());
            s.Add(CountSucces + "/25");
            s.Add(meanAndStdev(FirstHits));
            s.Add(meanAndStdev(GenConverges));
            s.Add(meanAndStdev(FctEvals));
            s.Add(meanAndStdev(CPUTime));
            return String.Join(",", s);
        }

        public string meanAndStdev(List<int> list) {
            double Mean = mean(list);
            double Std = stdev(list, Mean);
            return Mean + "(" + Std+ ")";
        }

        public double mean(List<int> list)
        {
            if (list.Count != 0) {
                return (double)list.Sum(x => x) / list.Count;
            }
            else
            {
                return -1;
            }
        } 

        public double stdev(List<int> list, double mean)
        {
            double ssd = 0;
            foreach(int value in list)
            {
                ssd += Math.Pow((mean - value),2);
            }
            if (list.Count != 0)
            {
                return Math.Sqrt(ssd / list.Count);
            }else
            {
                return -1;
            }
        }

    }
}
