ZINDAGI GAMES: MOBILE PROGRAMMER C# TEST

GETTING STARTED:

This test was designed as a C# console project in Visual Studio 2013.
To ensure your program compiles when you send it back we suggest you
use Visual Studio Express 2013 or Visual Studio Community 2013.

After opening the solution file in VS you should verify the project builds and
runs.  You should see an empty console window. Pressing Esc on your keyboard
should terminate the test.

The project contains a number of auto-generated files that can be ignored
and 2 C# source files of note: game.cs and Program.cs.  

Program.cs 
- contains the main loop
- you should not modify this file (except to test different input to Game() ctor)

game.cs
- contains the Game class 
- you implement the public interfaces
- you may add other members, classes and functions


SUMMARY:

Your task is to implement the Game class given the rules outlined below.

We're interested in seeing good software design, appropriate choice of 
data structures and performance-minded algorithms. We want to see how
you'd write code if you worked here, so show us your best stuff!

Please make use of system collections where appropriate (i.e. you don't have to 
rewrite data structures such as List<> if they're available in system collections).


GAME DETAILS:

This is a matching game that takes place on a 2d grid. 

Game.Game(int[][] layout, PieceInfo[] pieces)
----------------------------------------------

'layout' is an array defining valid and invalid board spots, 

e.g. 

{0,1,1},
{1,1,1,1},
{1,1,1,1},
{0,1,1},

0 == invalid 
1 == valid

'pieces' contains the definition of the game pieces.

To initialize your game board randomly assign a
piece type to each valid board spot.

Note: When we test your program we will...

1. Pass in different board layouts
2. Pass in different piece sets


Contiguous Blocks of Matching Pieces
------------------------------------

Your primary task is to write an algorithm that finds contiguous
blocks of matching game pieces. 

e.g. 

  S A
S A D D
A A A F
  A D

In this example there is a contiguous block of A, size = 5. 

Pieces must be adjacent horizontally or vertically to be considered 
neighbors. No diagonals.

The minimum block size is 3.


Game.FindBlocks()
-----------------

Find the largest contiguous block for each piece type.


Game.Print()
------------

Print the board to the console window. Highlight the largest contiguous
block for each piece type by changing the background color to the 
'highlightColor' specified by its PieceInfo.

Invalid board spots can be represented by a space (' ')


Game.ProcessInput(char c)
-------------------

After the board is printed, the program waits for the user to input
a character corresponding to a block they want to destroy.

Destroy the largest contiguous block of that type by replacing
all members of the block with new randomly generated pieces.


This program loops forever until you quit (ESC).
