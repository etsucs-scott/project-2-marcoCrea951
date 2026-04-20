
Overview

This project implements a modified version of the card game "War" using a clean architecture:

WarGame.Core → Contains all game logic, rules, and state
WarGame.Console → Handles input/output only

All required rules, data structures, and gameplay behavior are implemented strictly in the Core project.

---

Build Instructions

Prerequisites

 .NET SDK 

Build the Solution

From the root directory:

```bash
dotnet build
```
---

Run Instructions

Option 1 — Command-Line Argument

Run the game and specify number of players (2–4):

```bash
dotnet run --project src/WarGame.Console
```
---

Option 2 — User Prompt

If no argument is provided, the program will prompt:

```text
Enter number of players (2–4):
```

The user inputs the desired number of players.

---

Player Configuration

Supported players: 2–4
Default names:

  Player 1
  Player 2
  Player 3
  Player 4

---

Game Rules Summary

 A standard 52-card deck is shuffled and dealt in round-robin order
 Each round:

   All active players reveal one card
   Highest rank wins (2 → Ace)
  Ties:

   Only tied players continue in a tiebreaker
   Each tiebreaker is one face-up card
   Cards accumulate in a shared pot
  Eliminations:

   Players with 0 cards at round start are eliminated
   Players unable to continue a tie are eliminated
  Winner:

   Player who collects all cards OR
   Player with most cards after 10,000 rounds
   If tied → draw

---

 Pot Rules

 Implemented using `List<Card>`
 All played cards are immediately added
 Winner receives pot in order played

---

 Example Output

```text
Round 12
Player 1: K
Player 2: 5
Player 3: K
Tie between Player 1 and Player 3!
Pot includes: K, 5, K
Tiebreaker: Player 1: 9 | Player 3: 2
Winner: Player 1 (Cards: P1=26, P2=12, P3=14)
```

---

 Code Documentation

All code includes clear comments following these rules:

Class-Level Comments

Each class explains:

* Its purpose
* What it represents in the game

Example:

```csharp
// Represents a single playing card with a suit and rank.
// Implements comparison logic based on rank (Ace high).
```

---

Each method describes:

 What it does
 Inputs/outputs (if applicable)

Example:

```csharp
// Plays (removes) the top card from the player's hand.
public Card PlayCard()
```
---

 Submission Note

This project was completed and submitted via GitHub Classroom.

Repository:

 (https://github.com/marcoCrea951/project-2-marcoCrea951.git)

---

