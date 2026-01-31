using System.Linq;
using Yahtzee_Simulation.Models;

using Playground.Projects.Yahtzee;
using Playground.Projects.Yahtzee.Models;
using Playground.Projects.Yahtzee.Extensions;

namespace Yahtzee_Simulation;

public static class GameLogic
{
    // Spelar en hel runda: alla spelare får en tur
    public static GameState PlayRound(GameState gameState)
    {
        // Skicka GameState genom varje spelare och låt det transformeras
        return gameState.Players
            .Aggregate(gameState, (state, player) =>
                PlayTurn(state, player));
    }

    // En spelares tur
    private static GameState PlayTurn(GameState state, Player player)
    {
        // 1. Rulla tärningarna
        var rolledCup = player.YahzeeCup.ShakeAndRoll();

        // 2. Hämta alla möjliga kombinationer sorterade på poäng
        var combinations = rolledCup.GetAllCombinationsSorted();

        // 3. Hämta spelarens scorecard
        var scoreCard = state.GetScoreCard(player.Name);

        // 4. Välj den bästa kombinationen som är ledig på scorecard
        var bestFree = combinations
            .FirstOrDefault(c => scoreCard.IsBoxFree(c.GetType().Name));

        // Om inget fack är ledigt (ska i praktiken inte hända efter fixen)
        if (bestFree == null)
            return state;

        // 5. Fyll rätt ruta
        var updatedCard = scoreCard.FillBox(
            bestFree.GetType().Name,
            bestFree.Score);

        // 6. Returnera nytt GameState med uppdaterat scorecard
        return state.UpdateScoreCard(player.Name, updatedCard);
    }
}
