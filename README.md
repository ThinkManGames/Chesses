# Chesses
Welcome to Chesses! The multiplayer chess game that introduces you to 20+ new types of chess, each with their own wacky and wild rules. Play push chess, where the only way to win is to push the opponents king off the board, or connect 4 chess where you have to get a line of 4 pieces in the center of the board to win! 

## Design
Chesses is designed with modulatrity in mind. The goal of this design is to allow a sort of "port" like game logic where different aspects of the game can be swapped out. 

There are 6 main pieces of logic that are required for any given game of chess. They are listed below.

### The World
The world is the core logic of the game. It will never change regardless of the gamemode. The world handles logic like stopping/starting a game, getting the possible moves for any given piece, handling the actions to take after a piece is moved, and the logic to update who's turn it is.

### Board
The board sets up and keeps track of what pieces are where. In a generic 8x8 game, the board will be loaded into a 2d array that looks like below:

    "WR1", "WN1", "WB1", "WQ1", "WK1", "WB2", "WN2", "WR2"

    "WP1", "WP2", "WP3", "WP4", "WP5", "WP6", "WP7", "WP8"

    "E", "E", "E", "E", "E", "E", "E", "E"

    "E", "E", "E", "E", "E", "E", "E", "E"

    "E", "E", "E", "E", "E", "E", "E", "E"

    "E", "E", "E", "E", "E", "E", "E", "E"

    "BP1", "BP2", "BP3", "BP4", "BP5", "BP6", "BP7", "BP8"

    "BR1", "BN1", "BB1", "BQ1", "BK1", "BB2", "BN2", "BR2"

Game modes that utilize a custom board are games like mini chess, socially distanced chess, donut chess, hockey chess, etc. It will also handle anything that should be on the board that isnt a piece like mines in landmine chess and graves in F chess.

### Capture
Capture defines what happens when a piece is captured. It is used to check if a move is valid (meaning the move won't result in the king being in check) and is used to actually make a move. It will handle updating the board and removing any pieces that were captured. 

Game modes that utilize a custom capture are games like upgrade chess, downgrade chess, push chess, nuke chess, etc. Capture will also define when someone's turn has ended so games like snake draft chess and our chess require a custom capture in order to mess with the turn numbers.

### Moves
Moves will define how a piece can move. Anytime a piece is not going to move in a generic chess way, the game mode needs a custom moveset. Anytime a player selects a piece, the world will ask moves what spots are available for this piece and then will light up the spots available.

Game modes that utilize custom moves are games like piercing chess, horse chess, Teleport chess and F chess. Becuase pawns have really weird and stupid moves, gamemodes that start pawns in strange places also need custom moves to handle a pawn moving 2 spaces (like hockey chess).

### Win
Win will define what conditions need to be met for a player to win or draw the game. 

Game modes that utilize custom Win conditions are games like connect 4 chess, tnt run chess, blackout chess, etc. Additionally, any gamemode that changes how a piece can attack the king will also modify the win condition (like nuke chess, horse chess and piercing chess)

### Pregame
Any game that requires some setup needs a custom pregame. By default, there is no pregame, but if anything needs to happen before the normal game, pregame is required. 

Currently, only spy chess requires a custom pregame, but the functionality is available for any future gamemodes as well.

## Other stuff

### Spot Behavior
Spots light up if a piece can move there. Pretty basic.

### Square Behavior
Squares will glow green if the most recent move caused a piece to land in that square.

Squares will glow yellow if teh most recent move caused a piece to leave that square.

Squares will glow red if the king is in check on that square.

### AI
Bad AI will use minmax search to think a few moves ahead. Takes a while, but it works so whatever.
