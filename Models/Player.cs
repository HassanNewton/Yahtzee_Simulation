using System.Collections.Immutable;

using Yahtzee_Simulation.Models;

namespace Playground.Projects.Yahtzee.Models;

public record Player (string Name, YahzeeCup YahzeeCup)
{
	public override string ToString() => $"Player: {Name}, Hand: {YahzeeCup}";
}
