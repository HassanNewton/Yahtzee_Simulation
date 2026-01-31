# Yahtzee Simulation

En komplett 13-round Yahtzee-simulering implementerad i C# med .NET 10 med fokus på funktionell programmering.

## Installation & Kör

**Krav:** .NET 10 eller senare

```bash
git clone <repository-url>
cd Yahtzee_Simulation
dotnet build
dotnet run
```

## Vad gör programmet

Simulationen kör ett fullständigt Yahtzee-spel med tre spelare (Hassan, Martin, Henrik) under 13 rundor. Varje spelare har en poängkort med alla 13 kategorier (Ettor-Sexor, Tretal, Fyrtal, Kåk, Liten Stege, Stor Stege, Yahtzee, Chans). Bonuspoäng beräknas automatiskt.

Utgången visar varje spelares slutpoäng och poängfördelning per kategori.

## Kod

Projektet använder funktionell programmering med:

- Immutable data structures (`ImmutableList`, `ImmutableDictionary`)
- LINQ och `Aggregate` för spellogik
- Monadic extensions (`Tap()`) för kedjade operationer
- Tydlig separation mellan Models, Extensions, Game Logic och State

**Mapp-struktur:**

- `Models/` - Die, CupOfDice, YahtzeeCup, Player
- `Extensions/` - CupOfDiceExtension med monadic patterns
- `YahtzeeGame.cs` - Huvudspelsimuleraren
- `YahtzeeScoreCard.cs` - Poängkort med alla kategorier
- `GameState.cs` & `GameLogic.cs` - Speltillstånd och regler
