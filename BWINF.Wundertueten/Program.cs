using System.Diagnostics.Metrics;
using System.Text.Json;

namespace BWINF.Wundertueten
{
    public struct Simulation
    {
        public int[] ObjectCounts { get; set; }
        public int BagCount { get; set; }
    }

    public struct Bag
    {
        internal double[] ObjectCountFractions { get; set; }
        public int[] ObjectCounts { get; set; }
    }

    public static class Simulator
    {
        public static ICollection<Bag> Simulate(Simulation simulation)
        {
            Bag[] bags = new Bag[simulation.BagCount];

            for (int i = 0; i < bags.Length; i++)
            {
                ref Bag bag = ref bags[i];

                bag.ObjectCounts = new int[simulation.ObjectCounts.Length];
                bag.ObjectCountFractions = new double[bag.ObjectCounts.Length];

                for (int j = 0; j < bag.ObjectCounts.Length; j++)
                {
                    var count = (double)(simulation.ObjectCounts[j] / (double)bags.Length);

                    bag.ObjectCounts[j] = (int)(count < 1 ? Math.Ceiling(count) : Math.Floor(count));
                    bag.ObjectCountFractions[j] = count;
                }
            }

            // Die Werte der letzten Tüte müssen mithilfe der gepufferten Anteile angepasst werden, da wir durch Runden verlorene Objekte wieder hinzufügen müssen
            for (int i = 0; i < bags[^1].ObjectCounts.Length; i++)
            {
                bags[^1].ObjectCounts[i] += bags[^1].ObjectCountFractions[i] < 1 ? -1 : bags[^1].ObjectCountFractions[i] == 1 ? 0 : 1;
            }

            return bags;
        }
    }
}
