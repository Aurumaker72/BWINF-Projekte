using BWINF.Wundertueten;

namespace BWINF.Nachricht.Tests
{
    public class Tests
    {

        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { new Simulation()
        {
            BagCount = 3,
            CardGameCount = 4,
            DexterityGameCount = 2,
            DiceGameCount = 4,
        }};
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Test_DistributionEqual(Simulation simulation)
        {
            var bags = Simulator.Simulate(simulation).ToArray();

            for (int i = 1; i < bags.Length; i++)
            {
                var previousBag = bags[i - 1];
                var bag = bags[i];

                Assert.True(Math.Abs(bag.DiceGameCount - previousBag.DiceGameCount) <= 1);
                Assert.True(Math.Abs(bag.CardGameCount - previousBag.CardGameCount) <= 1);
                Assert.True(Math.Abs(bag.DexterityGameCount - previousBag.DexterityGameCount) <= 1);
            }
        }
    }
}