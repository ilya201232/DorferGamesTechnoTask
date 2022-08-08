
namespace Garden
{
    public class CutterCommonData
    {
        public int NeededAmountOfCuts { get; }

        public int AmountOfCutsLeft { get; set; }

        public CutterCommonData(int neededAmountOfCuts)
        {
            NeededAmountOfCuts = neededAmountOfCuts;
            AmountOfCutsLeft = NeededAmountOfCuts;
        }
    }
}