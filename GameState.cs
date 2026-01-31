using System.Collections.Immutable;
using Playground.Projects.Yahtzee.Models;
using Yahtzee_Simulation;
using Yahtzee_Simulation.Models;
namespace Playground.Projects.Yahtzee;



public record GameState
{
    public ImmutableList<Player> Players { get; init; }
    public ImmutableDictionary<string, YahtzeeScoreCard> ScoreCards { get; init; }
    public int CurrentRound { get; init; }

        public GameState(ImmutableList<Player> players)
    {
        Players = players;
        CurrentRound = 1;

        ScoreCards = players.ToImmutableDictionary(
            p => p.Name,
            _ => new YahtzeeScoreCard()
        );
    }

    public YahtzeeScoreCard GetScoreCard(string playerName) =>
        ScoreCards[playerName];

    public GameState UpdateScoreCard(string playerName, YahtzeeScoreCard newCard)
    {
        var updated = ScoreCards.SetItem(playerName, newCard);

        return this with { ScoreCards = updated };
    }

    public GameState NextRound() =>
        this with { CurrentRound = CurrentRound + 1 };
}
