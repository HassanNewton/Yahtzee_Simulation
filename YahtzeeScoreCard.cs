using System.Collections.Immutable;
namespace Yahtzee_Simulation;

public record YahtzeeScoreCard
{
    // Alla Yahtzee-rutor
    public ImmutableDictionary<string, int?> Boxes { get; init; }

    public YahtzeeScoreCard()
    {
        Boxes = new Dictionary<string, int?>
        {
            // Upper section
            { "Ones", null },
            { "Twos", null },
            { "Threes", null },
            { "Fours", null },
            { "Fives", null },
            { "Sixes", null },

            // Lower section
            { "ThreeOfAKind", null },
            { "FourOfAKind", null },
            { "FullHouse", null },
            { "SmallStraight", null },
            { "LargeStraight", null },
            { "Yahtzee", null },
            { "Chance", null }
        }.ToImmutableDictionary();
    }

        public bool IsBoxFree(string boxName) =>
        Boxes.ContainsKey(boxName) && Boxes[boxName] == null;


    public YahtzeeScoreCard FillBox(string boxName, int score)
    {
        if (!IsBoxFree(boxName))
            return this;

        var updatedBoxes = Boxes.SetItem(boxName, score);

        return this with { Boxes = updatedBoxes };
    }

        public int UpperSectionScore =>
        Boxes
            .Where(kvp =>
                kvp.Key is "Ones" or "Twos" or "Threes" or
                "Fours" or "Fives" or "Sixes")
            .Sum(kvp => kvp.Value ?? 0);

    public int Bonus =>
        UpperSectionScore >= 63 ? 35 : 0;

    public int TotalScore =>
        Boxes.Sum(kvp => kvp.Value ?? 0) + Bonus;
}

