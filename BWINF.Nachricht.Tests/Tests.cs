using BWINF.Wundertueten;

namespace BWINF.Nachricht.Tests
{
    public class Tests
    {

        private const int simulationCount = 6;

        Simulation FromFile(string filename)
        {
            Simulation simulation = new();

            var lines = File.ReadAllLines(filename).Where(x => x.Length > 0).Select(x => int.Parse(x)).ToArray();

            simulation.BagCount = lines[0];
            simulation.ObjectCounts = new int[lines[1]];
            for (int i = 2; i < lines.Length; i++)
            {
                simulation.ObjectCounts[i - 2] = lines[i];
            }

            return simulation;
        }

        [Fact]
        public void Test_DistributionEqual()
        {
            List<Simulation> simulations = new();

            for (int i = 0; i < simulationCount; i++)
            {
                simulations.Add(FromFile($"Assets/wundertuete{i}.txt"));
            }

            foreach (var simulation in simulations)
            {
                var bags = Simulator.Simulate(simulation).ToArray();

                for (int i = 1; i < bags.Length; i++)
                {
                    var previousBag = bags[i - 1];
                    var bag = bags[i];

                    for (int j = 0; j < bag.ObjectCounts.Length; j++)
                    {
                        Assert.True(Math.Abs(bag.ObjectCounts[j] - previousBag.ObjectCounts[j]) <= 1);
                    }
                }
            }
        }
    }
}