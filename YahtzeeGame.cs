using System.Collections.Immutable;
using Playground.Projects.Yahtzee.Extensions;
using Playground.Projects.Yahtzee.Models;
using Yahtzee_Simulation.Models;
using Yahtzee_Simulation;

namespace Playground.Projects.Yahtzee;

public static class YahtzeeGame
{
    public static void RunSimulation()
    {
        Console.WriteLine("Testing the Cup of Dice.");

        var cupOfDice = new CupOfDice(10);
        Console.WriteLine($"Cup with 10 dice: {cupOfDice}\n");   

        cupOfDice = new CupOfDice(2);
        Console.WriteLine($"Cup with 2 dice: {cupOfDice}\n"); 


        var yahzeeCup = new YahzeeCup();
        Console.WriteLine($"Yahzee Cup with 5 dice: {yahzeeCup}\n");  

        Enumerable.Range(1, 10)
            .Aggregate(yahzeeCup, (currentCup, i) => 
            {
                var newCup = currentCup
                .ShakeAndRoll();

                newCup.Tap(cup => Console.WriteLine($"Yahzee Cup with 5 dice: {cup}"))
                .GetYahtzeeCombination()
                .Tap(ycombo =>  Console.WriteLine($"Yahzee Combination: {ycombo.GetType().Name}, Score: {ycombo.Score}\n"));
                
                return newCup;
            });  

        ImmutableList<Player> players = ImmutableList.Create(
            new Player("Hassan", new YahzeeCup()),   
            new Player("Martin", new YahzeeCup()),
            new Player("Henrik", new YahzeeCup()))

            .Tap(p => Console.WriteLine(string.Join("\n", p.Select(pl => $"{pl.Name} has Yahtzee cup: {pl.YahzeeCup}"))));


        System.Console.WriteLine("\nYahtzee Round Simulation:");
        System.Console.WriteLine("Your code should implement the Yahtzee round simulation below.");
        System.Console.WriteLine("========================");

        // Implement a Yahtzee round simulation here using functional patterns
        var initialState = new GameState(players);
        var finalState = Enumerable.Range(1, 13)
            .Aggregate(initialState, (state, round) =>
                GameLogic.PlayRound(state)
                        .NextRound());
        Console.WriteLine("\nFinal Results");
        Console.WriteLine("=============");

        foreach (var player in finalState.Players)
        {
            var card = finalState.GetScoreCard(player.Name);

            Console.WriteLine($"\nPlayer: {player.Name}");
            Console.WriteLine($"Total Score: {card.TotalScore}");
            Console.WriteLine("Score breakdown:");

            foreach (var box in card.Boxes)
            {
                Console.WriteLine($"{box.Key}: {box.Value}");
            }

            Console.WriteLine($"Bonus: {card.Bonus}");
        }

        // use existing monadic extensions and functional patterns
        // minimize imperative code, maximize declative code using LINQ and extension methods

    }
}