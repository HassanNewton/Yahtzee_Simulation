using System.Collections.Immutable;
using Yahtzee_Simulation.Models;
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


// Kontrollerar om rutan på scorekortet som hör till denna Yahtzee-kombination
// fortfarande är tom och kan fyllas.

// Vi skickar in själva YahzeeCup-objektet, inte en sträng.
// Det gör att spellogiken styrs av kombinationen som hittades,
// och inte av strängmatchning
        public bool IsBoxFree(YahzeeCup combo)
        {
            var key = combo.GetType().Name;
            return Boxes.ContainsKey(key) && Boxes[key] == null;
        }


// Fyller en ruta på scorekortet med poängen och returnerar
// ett NYTT scorekort (immutability).


// Denna metod räknar inte ut någon poäng och vet inget om tärningar.
// Den ansvarar bara för regeln:
//   "En ruta får bara fyllas en gång."
    public YahtzeeScoreCard FillBox(string boxName, int score)
    {
        if (!Boxes.ContainsKey(boxName) || Boxes[boxName] != null)
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

