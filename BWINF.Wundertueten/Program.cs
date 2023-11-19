using System.Text.Json;

namespace BWINF.Wundertueten
{
    public struct Simulation
    {
        public int DiceGameCount { get; set; }
        public int CardGameCount { get; set; }
        public int DexterityGameCount { get; set; }
        public int BagCount { get; set; }
    }

    public struct Bag
    {
        public int DiceGameCount { get; set; }
        public int CardGameCount { get; set; }
        public int DexterityGameCount { get; set; }
    }

    public static class Simulator
    {
        public static ICollection<Bag> Simulate(Simulation simulation)
        {
            Bag[] bags = new Bag[simulation.BagCount];

            var diceGameCount = (double)(simulation.DiceGameCount / (double)bags.Length);
            var cardGameCount = (double)(simulation.CardGameCount / (double)bags.Length);
            var dexterityGameCount = (double)(simulation.DexterityGameCount / (double)bags.Length);

            for (int i = 0; i < bags.Length; i++)
            {
                bags[i].DiceGameCount = (int)(diceGameCount < 1 ? Math.Ceiling(diceGameCount) : Math.Floor(diceGameCount));
                bags[i].CardGameCount = (int)(cardGameCount < 1 ? Math.Ceiling(cardGameCount) : Math.Floor(cardGameCount));
                bags[i].DexterityGameCount = (int)(dexterityGameCount < 1 ? Math.Ceiling(dexterityGameCount) : Math.Floor(dexterityGameCount));
            }

            // If we get a result below 1, we subtract one, for anything above one we add one 
            bags[^1].DiceGameCount += diceGameCount < 1 ? -1 : diceGameCount == 1 ? 0 : 1;
            bags[^1].CardGameCount += cardGameCount < 1 ? -1 : cardGameCount == 1 ? 0 : 1;
            bags[^1].DexterityGameCount += dexterityGameCount < 1 ? -1 : dexterityGameCount == 1 ? 0 : 1;

            return bags;
        }
    }
}
