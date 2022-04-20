using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

class Program
{
    //1. tic tac toe x 8
    //board, där smallerboard och biggerboard ärver ifrån. använder det för att sätta ihop flera små brädor till ett större
    public static char playerSignature = ' ';
    //static Dictionary<string, dynamic> map = new Dictionary<string, dynamic>();
    static int turns = 0; //Will count each turn.  Once == 10 then the game is a draw.
  
    //Skapa upp alla bräden här
    static string[] ArrBoard =
    {
        "NW", "NC", "NE", "CW", "CC", "CE", "SW", "SC", "SE"
    }; //Global char array variable to store the players input.

    //Inklistrad metoder

    //CompositeElement bigBoard = new CompositeElement("Picture");
    //root.Add(new PrimitiveElement("Red Line"));
    //        root.Add(new PrimitiveElement("Blue Circle"));
    //        root.Add(new PrimitiveElement("Green Box"));
    //        // Create a branch
    //        CompositeElement comp = new CompositeElement("Two Circles");
    //comp.Add(new PrimitiveElement("Black Circle"));
    //        comp.Add(new PrimitiveElement("White Circle"));
    //        root.Add(comp);
    //        // Add and remove a PrimitiveElement
    //        PrimitiveElement pe = new PrimitiveElement("Yellow Line");
    //root.Add(pe);
    //        root.Remove(pe);
    //        // Recursively display nodes
    //        root.Display(1);
    //        // Wait for user
    //        Console.ReadKey();


    //Interface
    //public interface IBoard
    //{
    //    void DisplayBoard();
    //}


    ////Leaf

    //public class smallBoard : IBoard
    //{



    //public string[] arrayBoard { get; set; }

    //public smallBoard(string[] arrayBoard)
    //{  //string NW, string NC, string NE, string CW, string CC, string CE, string SW, string SC, string SE

    //    this.arrayBoard = new string[] { "NW", "NC", "NE", "CW", "CC", "CE", "SW", "SC", "SE" };
    //}



    //displayar bara
    //    public void DisplayBoard()
    //    {
    //        foreach (var item in arrayBoard)
    //        {
    //            Console.WriteLine(item);
    //        }
    //    }
    //}

    ////Composite aKa BigBoard
    //public class bigBoard : IBoard
    //{


    //    List<IBoard> smallerBoards = new List<IBoard>();



    //    public bigBoard(List<IBoard> smallerBoards)
    //    {
    //        this.smallerBoards = smallerBoards;
    //    }

    //    public void addBoard(IBoard board)
    //    {
    //        smallerBoards.Add(board);
    //    }

    //    public void DisplayBoard()
    //    {

    //        foreach (var item in smallerBoards)
    //        {
    //            item.DisplayBoard();
    //        }
    //    }



    //}






    public static void BoardReset() //If this method is called then the game resets.  
    {
        string[] ArrBoardInitialize =
        {
        "NW", "NC", "NE", "CW", "CC", "CE", "SW", "SC", "SE"
    };

        ArrBoard = ArrBoardInitialize;
        DrawBoard();

    }

    public static void DrawBoard()
    {
        Console.Clear();
        Console.WriteLine("  -------------------------");
        Console.WriteLine("  |       |       |       |");
        Console.WriteLine("  |   {0}   |   {1}   |   {2}   |", ArrBoard[0], ArrBoard[1], ArrBoard[2]);
        Console.WriteLine("  |       |       |       |");
        Console.WriteLine("  -------------------------");
        Console.WriteLine("  |       |       |       |");
        Console.WriteLine("  |   {0}   |   {1}   |   {2}   |", ArrBoard[3], ArrBoard[4], ArrBoard[5]);
        Console.WriteLine("  |       |       |       |");
        Console.WriteLine("  -------------------------");
        Console.WriteLine("  |       |       |       |");
        Console.WriteLine("  |   {0}   |   {1}   |   {2}   |", ArrBoard[6], ArrBoard[7], ArrBoard[8]);
        Console.WriteLine("  |       |       |       |");
        Console.WriteLine("  -------------------------");
    } //Draws the player board to terminal.  



    static void Main(string[] args)
    {

        //string userInput = args[0];
        //string userInput = "NW.CC, NW.SE, NW.CE, NW.SW, NW.NE, NW.CE, NW.SE ,NW.SW, NW.NW, NW.NC, NW.CW, NW.SC";
        string userInput = "CE,SC, NE, CC, SW,   NC";
        //string userInput = " NW.CC, NC.CC, NW.NW, NE.CC, NW.SE, CE.CC, CW.CC, SE.CC, CW.NW, CC.CC, CW.SE, CC.NW, CC.SE, CE.NW, SW.CC, CE.SE, SW.NW, SE.SE, SW.SE";
        userInput = string.Concat(userInput.Where(c => !char.IsWhiteSpace(c)));
        string[] moves = userInput.Split(',');


        //Skapa Leaf-objects aKa smallBoard

        //Toppen aKa Norr
        //IBoard NW = new smallBoard();
        //IBoard NC = new smallBoard();
        //IBoard NE = new smallBoard();

        ////Mitten aka Center
        //IBoard CW = new smallBoard();
        //IBoard CC = new smallBoard();
        //IBoard CE = new smallBoard();

        ////Botten aKa Syd
        //IBoard SW = new smallBoard();
        //IBoard SC = new smallBoard();
        //IBoard SE = new smallBoard();




        //Skapa composite-object aKa bigBoard

        //bigBoard Board = new bigBoard(<List> small);

        //Board.addBoard(NW);
        //Board.addBoard(NC);
        //Board.addBoard(NE);
        //Board.addBoard(CW);
        //Board.addBoard(CC);
        //Board.addBoard(CE);
        //Board.addBoard(SW);
        //Board.addBoard(SC);
        //Board.addBoard(SE);

        ////Detta displayar alla smallboards
        //Board.DisplayBoard();



        //map.Add("NW", NW);
        //map.Add("NC", NC);
        //map.Add("NE", NE);
        //map.Add("CW", CC);


        //int player = 2; // Player 1 Starts
       

        do //Alternates player turns.
        {
         
            eligebleTile(moves);
            DrawBoard();
            HorizontalWin();
            VerticalWin();
            DiagonalWin();
            Console.WriteLine(HorizontalWin());
            Console.WriteLine(VerticalWin());
            Console.WriteLine(DiagonalWin());
            if (turns == 9)
            {
                Draw();
            }


        } while (turns <= 9);
    }

    //Gameplay loop.  Controls player turns & overrides the array with players input.


    public static void eligebleTile(string[] moves)
    {
        bool playerOTurn = true;
        int countO = 0;
        int countX = 0;
        int player;
        List<string> movesX = new List<string>();
        List<string> movesO = new List<string>();
        string input = "";
        //delar upp strängen som matas in för vardera spelare
        //problemet är att den sätter allt i stringen till en egen index



        // Kollar så att ingen av spelarna har spelat det draget som läggs till nu och om ingen har spelat det så lägger den till i antingen playero eller playerx
        for (var index = 0; index < moves.Length; index++)
        {
            if (!movesO.Contains(moves[index]) && !movesX.Contains(moves[index])) //Metod från eligebleTile
            {
                if (playerOTurn)
                {
                    player = 1;
                    movesO.Add(moves[index]);
                    input = movesO[countO];
                    countO++;


                    Console.WriteLine(input + countO + "Player1");
                    turns++;
                    OorX(player, input);

                }
                else
                {
                    player = 2;
                    movesX.Add(moves[index]);
                    input = movesX[countX];
                    countX++;
                    Console.WriteLine(input + countX + "Player2");
                    turns++;
                    OorX(player, input);

                }

                playerOTurn = !playerOTurn;
            }
        }
    }


   
    //code smell?

    public static char getPlayerSignature(int player)
    {

        if (player == 1)
        {
            playerSignature = 'X';

        }
        else if (player == 2)
        {
            playerSignature = 'O';

        }
        return playerSignature;
    }

  


    //bryt ut i polymorfism, objekten kan sen kalla på respektive metod t ex nw.getPlayerSignature som innehåller en metod för att lägga till i arrayen
    public static void OorX(int player, string input)
    {

        //object value;
        //var tempValue = input.Substring(0, 2);
        //var tempString = input.Substring(3);
        //Console.WriteLine(tempString);
        //Console.WriteLine(tempValue);
        //foreach (var item in map)
        //{
        //    if (tempValue.Equals(item.Key))
        //        {
        //        map.TryGetValue(item.Key, out value);
        //        Console.WriteLine(value);

        //    }

        //if (tempValue.Equals(item.Key)) { 


        //switch (tempValue, tempString)
        //{
        //    case var _ when tempValue.Contains(item.Key):

        //        ArrBoard[1] = getPlayerSignature(player).ToString();
        //        break;
        //}
        //    }
        //}


        //måste kolla värdet i lilla brädet efter koordinaterna
        //if (tempString.Contains())
        //{


        //    ArrBoard[1] = getPlayerSignature(player).ToString();
        //}

        //}



        switch (input)
        {
            
            case var _ when input.Contains("NW"):
                ArrBoard[0] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("NC"):
                ArrBoard[1] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("NE"):
                ArrBoard[2] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("CW"):
                ArrBoard[3] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("CC"):
                ArrBoard[4] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("CE"):
                ArrBoard[5] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("SW"):
                ArrBoard[6] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("SC"):
                ArrBoard[7] = getPlayerSignature(player).ToString();
                break;
            case var _ when input.Contains("SE"):
                ArrBoard[8] = getPlayerSignature(player).ToString();
                break;
        }
    }

    //Controls if the player is X or O.
  


    public static string HorizontalWin() //Dictionary<string, dynamic> map
    {
        string boardWin = "";
        char[] playerSignatures = { 'X', 'O' };
        foreach (var playerSignature in playerSignatures)
        {
            if (((ArrBoard[0].Contains(playerSignature)) && (ArrBoard[1].Contains(playerSignature)) && (ArrBoard[2].Contains(playerSignature)))
                || ((ArrBoard[3].Contains(playerSignature)) && (ArrBoard[4].Contains(playerSignature)) &&
                    (ArrBoard[5].Contains(playerSignature)))
                || ((ArrBoard[6].Contains(playerSignature)) && (ArrBoard[7].Contains(playerSignature)) &&
                    (ArrBoard[8].Contains(playerSignature))))
            {
                Console.Clear();
                if (playerSignature == 'X')
                {
                    boardWin = "spelare 1 vann horisontalt";

                    DrawBoard();

                }
                else if (playerSignature == 'O')
                {


                    boardWin = "spelare 2 vann horisontalt";
                 DrawBoard();
                }
              

                //WinArt();
                //Console.WriteLine("Please press any key to reset the game");
                //Console.ReadKey();
                //BoardReset();

                break;
            }
        }
        return boardWin;
    } //Method is called to check for a horizontal win.  

    //public static void HorizontalWin()
    //{
    //    char[] playerSignatures = { 'X', 'O' };
    //    foreach (var playerSignature in playerSignatures)
    //    {
    //        if (((ArrBoard[0].Contains(playerSignature)) && (ArrBoard[1].Contains(playerSignature)) && (ArrBoard[2].Contains(playerSignature)))
    //            || ((ArrBoard[3].Contains(playerSignature)) && (ArrBoard[4].Contains(playerSignature)) &&
    //                (ArrBoard[5].Contains(playerSignature)))
    //            || ((ArrBoard[6].Contains(playerSignature)) && (ArrBoard[7].Contains(playerSignature)) &&
    //                (ArrBoard[8].Contains(playerSignature))))
    //        {
    //            Console.Clear();
    //            if (playerSignature == 'X')
    //            {
    //                Console.WriteLine("Congratulations Player 1.\nYou have a achieved a horizontal win! " +
    //                                  "\nYou're the Tic Tac Toe Master!\n" +
    //                                  "\nTurns taken{0}", turns);
    //                DrawBoard();
    //            }
    //            else if (playerSignature == 'O')
    //            {
    //                Console.WriteLine("Congratulations Player 2.\nYou have a achieved a horizontal win! " +
    //                                  "\nYou're the Tic Tac Toe Master!\n" +
    //                                  "\nTurns taken{0}", turns);
    //                DrawBoard();
    //            }


    //            //WinArt();
    //            Console.WriteLine("Please press any key to reset the game");
    //            Console.ReadKey();
    //            //BoardReset();

    //            break;
    //        }
    //    }
    //}


    public static string VerticalWin()
    {
        string boardWin = "";
        char[] playerSignatures = { 'X', 'O' };
        foreach (char playerSignature in playerSignatures)
        {
            if (((ArrBoard[0].Contains(playerSignature)) && (ArrBoard[3].Contains(playerSignature)) && (ArrBoard[6].Contains(playerSignature)))
                || ((ArrBoard[1].Contains(playerSignature)) && (ArrBoard[4].Contains(playerSignature)) &&
                    (ArrBoard[7].Contains(playerSignature)))
                || ((ArrBoard[2].Contains(playerSignature)) && (ArrBoard[5].Contains(playerSignature)) &&
                    (ArrBoard[8].Contains(playerSignature))))
            {
                Console.Clear();
                if (playerSignature == 'X')
                {
                    boardWin = "spelare 1 vann vertikalt";

                    DrawBoard();

                }
                else if (playerSignature == 'O')
                {


                    boardWin = "spelare 2 vann vertikalt";
                    DrawBoard();
                }

                //Console.Clear();
                //if (playerSignature == 'X')
                //{
                //    Console.WriteLine(
                //        "Player 1, that was Fantastic.\nA vertical win!\nYou're the Tic Tac Toe Master!\n");
                //    DrawBoard();
                //}
                //else
                //{
                //    Console.WriteLine(
                //        "Player 2, that was Fantastic.\nA vertical win!\nYou're the Tic Tac Toe Master!\n");
                //    DrawBoard();
                //}

                ////WinArt();
                //Console.WriteLine("Please press any key to reset the game");
                //Console.ReadKey();
                ////BoardReset();

                break;
            }
        }
        return boardWin;
    } //Method is called to check for a vertical win.  

    public static string DiagonalWin()
    {
        string boardWin = "";
        char[] playerSignatures = { 'X', 'O' };
        foreach (char playerSignature in playerSignatures)
        {
            if ((ArrBoard[0].Contains(playerSignature)) && (ArrBoard[4].Contains(playerSignature)) && (ArrBoard[8].Contains(playerSignature))
                || (ArrBoard[6].Contains(playerSignature)) && (ArrBoard[4].Contains(playerSignature)) &&
                    (ArrBoard[2].Contains(playerSignature)))
            {
                Console.Clear();
                if (playerSignature == 'X')
                {
                    boardWin = "spelare 1 vann diagonalt";

                    DrawBoard();

                }
                else if (playerSignature == 'O')
                {


                    boardWin = "spelare 2 vann diagonalt";
                    DrawBoard();
                }

                //Console.Clear();
                //if (playerSignature == 'X')
                //{

                //    Console.WriteLine("WOW!, player 1 that's a diagonal win! " +
                //                      "\nExcellently played, it's one for the ages! " +
                //                      "\nYou're the Tic Tac Toe Legend!\n \n \n");
                //    DrawBoard();
                //}
                //else
                //{
                //    Console.WriteLine("WOW!, player 2 that's a diagonal win! " +
                //                      "\nExcellently played, it's one for the ages! " +
                //                      "\nYou're the Tic Tac Toe Legend!\n \n \n");
                //    DrawBoard();
                //}

                ////WinArt();
                //Console.WriteLine("Please press any key to reset the game");
                //Console.ReadKey();
                ////BoardReset();

                break;
            }
        }
        return boardWin;
    } //Method is called to check for a diagonal win.


    public static void Draw()
    {

        {
            Console.WriteLine("Aw gosh... it's a draw." +
                              "\nPlease press any key to reset the game and try again!");
            DrawBoard();
            Console.ReadKey();
            //BoardReset();

        }
    } //Method is called to check if the game is a draw.

}