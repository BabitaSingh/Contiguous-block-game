using System;
using System.Collections.Generic;
using ZindagiMobileTest;

class Game
{
    //-------------------------------------------------------------------------
    // Initialize internal data
    public PieceInfo[][] layout; //Use of global layout grid to access game board across all functions
    public Dictionary<char,ConsoleColor> pieces = new Dictionary<char,ConsoleColor>();  //Use of global storage DataStructure to store pieces to get an easy access during different function calls
    public class mapKey //structure to store element position on board in form of x-position and y-position
    {
        //default constructor used for initialization
        public int xPosition;
        public int yPosition;
    };
    public class blockInfo //structure to store all four existing adjacent element indices
    {
        //default constructor used for initialization
        public mapKey above;
        public mapKey below;
        public mapKey left;
        public mapKey right;
    };
    Dictionary<char, List<List<mapKey>>> storage;  ////DataStructure to store blocks of same adjacent characters
    public Game(int[][] layout, PieceInfo[] pieces)
    {
        /* IMPLEMENT ME -- See README-Instructions */
       
        PieceInfo blankSpace = new PieceInfo(' ', ConsoleColor.Black); //Object for handling space character
        Random r = new Random(); //use of Random() class to randomly generate indices for all valid spots on board
        int randomNo;
        this.layout = new PieceInfo[layout.Length][]; //public board which would be accessible across all classes

        //loop to initialize new layout public board 
        for (int i = 0; i < layout.Length; i++)
        {
            this.layout[i] = new PieceInfo[layout[i].Length];
            for (int j = 0; j < layout[i].Length; j++)
            {
                if (layout[i][j] != 0)
                {
                    randomNo = r.Next(pieces.Length);
                    PieceInfo temp = new PieceInfo(pieces[randomNo].type, pieces[randomNo].highlightColor);
                    this.layout[i][j] = temp; 
                }
                else
                    this.layout[i][j] = blankSpace;
            }
        }
        
        for (int i = 0; i < pieces.Length;i++ )
        {
            this.pieces.Add(pieces[i].type,pieces[i].highlightColor);
        }
    }

    //-------------------------------------------------------------------------
    // Find adjacent blocks information like indices for above below left right
    public blockInfo GetBlocks(int row, int col)
    {
        blockInfo blocks; int n;
        blocks = new blockInfo();
        if (row == 2 && col == 2)
            Console.WriteLine("");
        //get adjacent elements for left right above below
        if (row == 0)
        {
            n = layout[row].Length;
            if (col != 0)
            {
                //left
                blocks.left = new mapKey();
                blocks.left.xPosition = row;
                blocks.left.yPosition = col - 1;
            }
            if (col != n - 1)
            {
                //right
                blocks.right = new mapKey();
                blocks.right.xPosition = row;
                blocks.right.yPosition = col + 1;
            }
            if (row != layout.Length - 1)
            {
                //below
                if (layout[row + 1].Length > col)
                {
                    blocks.below = new mapKey();
                    blocks.below.xPosition = row + 1;
                    blocks.below.yPosition = col;
                }
            }
        }
        else if (row == layout.Length - 1)
        {
            if (col != 0)
            {
                //left
                blocks.left = new mapKey();
                blocks.left.xPosition = row;
                blocks.left.yPosition = col - 1;
            }
            if (col != layout[row].Length - 1)
            {
                //right
                blocks.right = new mapKey();
                blocks.right.xPosition = row;
                blocks.right.yPosition = col + 1;
            }
            if (row != 0)
            {
                //above
                if (layout[row - 1].Length > col)
                {
                    blocks.above = new mapKey();
                    blocks.above.xPosition = row - 1;
                    blocks.above.yPosition = col;
                }
            }
        }
        else
        {
            if (col != 0)
            {
                //left
                blocks.left = new mapKey();
                blocks.left.xPosition = row;
                blocks.left.yPosition = col - 1;
            }
            if (col != layout[row].Length - 1)
            {
                //right
                blocks.right = new mapKey();
                blocks.right.xPosition = row;
                blocks.right.yPosition = col + 1;
            }
            if (row != 0)
            {
                //above
                if (layout[row - 1].Length > col)
                {
                    blocks.above = new mapKey();
                    blocks.above.xPosition = row - 1;
                    blocks.above.yPosition = col;
                }
            }
            if (row != layout.Length)
            {
                //below
                if (layout[row + 1].Length > col)
                {
                    blocks.below = new mapKey();
                    blocks.below.xPosition = row + 1;
                    blocks.below.yPosition = col;
                }
            }
        }
        return blocks;
    }
  
    //-------------------------------------------------------------------------
    // Find largest contiguous blocks
    public void FindBlocks()
    {
        /* IMPLEMENT ME -- See README-Instructions */
    /*********** Algorithm to get blocks following the BFS approach *******************/
        storage = new Dictionary<char, List<List<mapKey>>>();
        Queue<mapKey> Qpieces;
        bool[][] visited = new bool[layout.Length][]; //keep track of visited pieces to avoid duplicates and infinite loops
        for (int i = 0; i < visited.Length; i++) // loop to initialize all visited pieces false
        {
            visited[i] = new bool[layout[i].Length];
            for(int j=0;j<layout[i].Length;j++)
            {
                visited[i][j] = false;
            }
        }       

            for (int i = 0; i < layout.Length; i++)
            {
                for (int j = 0; j < layout[i].Length; j++)
                {
                    if (visited[i][j] == false && layout[i][j].type != ' ') //if piece is not space and not visisted
                    {
                        if (!storage.ContainsKey(layout[i][j].type)) //check for valid entry ie if the entry exists
                        {
                            List<List<mapKey>> positionList = new List<List<mapKey>>(); // a new list structure for each entry
                            storage.Add(layout[i][j].type, positionList);
                        }
                        mapKey key = new mapKey();
                        key.xPosition = i;
                        key.yPosition = j; // make key object for current entry's position indices
                        Qpieces = new Queue<mapKey>();
                        Qpieces.Enqueue(key);
                        List<mapKey> mapList = new List<mapKey>();
                        //loop to get all adjacent pieces of the current key; left; right; above; below;
                        while (Qpieces.Count != 0) // enqueue all adjacent pieces when the type is same; and dequeue untill adjacent pieces are not visited
                        {
                            mapKey mkey = new mapKey();
                            mkey = Qpieces.Dequeue();
                            if (visited[mkey.xPosition][mkey.yPosition] == false)
                            {
                                mapList.Add(mkey);
                                blockInfo blocks = GetBlocks(mkey.xPosition, mkey.yPosition);
                                if (blocks.left != null && layout[blocks.left.xPosition][blocks.left.yPosition].type == layout[mkey.xPosition][mkey.yPosition].type)
                                    Qpieces.Enqueue(blocks.left);
                                if (blocks.right != null && layout[blocks.right.xPosition][blocks.right.yPosition].type == layout[mkey.xPosition][mkey.yPosition].type)
                                    Qpieces.Enqueue(blocks.right);
                                if (blocks.above != null && layout[blocks.above.xPosition][blocks.above.yPosition].type == layout[mkey.xPosition][mkey.yPosition].type)
                                    Qpieces.Enqueue(blocks.above);
                                if (blocks.below != null && layout[blocks.below.xPosition][blocks.below.yPosition].type == layout[mkey.xPosition][mkey.yPosition].type)
                                    Qpieces.Enqueue(blocks.below);
                                visited[mkey.xPosition][mkey.yPosition] = true;
                            }
                        }
                        storage[layout[i][j].type].Add(mapList);
                    }
                }
            }
    }

    //-------------------------------------------------------------------------
    // Print the game board to the console

    public void Print()
    {
        /* IMPLEMENT ME -- See README-Instructions */
     //   printLayout();
        PieceInfo[][] pieces = new PieceInfo[layout.Length][];
        Console.WriteLine();
        Console.WriteLine();
        for (int i = 0; i < layout.Length; i++)
        {
            pieces[i] = new PieceInfo[layout[i].Length]; //to get length of each row
        }

        int[] max = new int[storage.Count];
        int index = 0;
        foreach (KeyValuePair<char, List<List<mapKey>>> entry in storage) //to get the maximum size for blocks
        {
            foreach (List<mapKey> list in entry.Value)
            {
                if (list.Count > max[index])
                    max[index] = list.Count;
            }
            index++;
        }

        index = 0;
        foreach (KeyValuePair<char, List<List<mapKey>>> entry in storage) //loop to iterate through all block size counts and see if the count is not more than 2 then add white color else default ConsoleColor of the piece
        {
            foreach (List<mapKey> list in entry.Value)
            {
                if (max[index] > 2 && list.Count == max[index])
                {
                    foreach (mapKey element in list)
                    {
                        pieces[element.xPosition][element.yPosition] = new PieceInfo(entry.Key, this.pieces[entry.Key]);
                                       
                    }
                }
                else
                {
                    foreach (mapKey element in list)
                    {
                        pieces[element.xPosition][element.yPosition] = new PieceInfo(entry.Key, ConsoleColor.White);
                    }
                }
            }
            index++;
        }

        for (int i = 0; i < pieces.Length; i++) //loop which iterates through the piecesList and prints the pieces with color
        {
            for (int j = 0; j < pieces[i].Length; j++)
            {
                if (pieces[i][j] == null)
                {
                    Console.Write("  ");
                }
                else
                {
                    Console.ForegroundColor = pieces[i][j].highlightColor;
                    Console.Write(pieces[i][j].type + " ");
                }
            }
            Console.WriteLine();
        }

    }

    //-------------------------------------------------------------------------
    // Process user keyboard input

    public void ProcessInput(char c)
    {
        /* IMPLEMENT ME -- See README-Instructions */
        List<mapKey> tempList = null;
        if (!pieces.ContainsKey(c)) //check for the piece entered is from the board and highlighted one
            Console.WriteLine("Please enter a valid character");
        else
        {
            List<List<mapKey>> positionList = storage[c];
            int max = 0;
            foreach (List<mapKey> list in positionList) //find the maximum size of block for entered character and put it in a templist
            {
                if (list.Count > max)
                {
                    max = list.Count;
                    tempList = list;
                }
            }
            Random r = new Random();
            foreach (mapKey pos in tempList) //for all those x and y positions in templist a random piece is generated and being assigned to main layout
            {
                int randomNo = r.Next(pieces.Count);
                List<char> charList = new List<char>(pieces.Keys);
                PieceInfo temp = new PieceInfo(charList[randomNo], pieces[charList[randomNo]]);
                layout[pos.xPosition][pos.yPosition] = temp;
            }

        }
    }
}