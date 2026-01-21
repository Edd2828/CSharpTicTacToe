using Asset;
using Enums;

namespace Player;

class Computer
{
    PlayerType computer = PlayerType.computer;
    Marker symbol = Marker.O;
    public Board Turn(Board board)
    {
        Console.WriteLine("**");
        List<int[]> availableSpaces = new List<int[]>();

        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                if (board.config[row,column] == Marker.Empty.GetHashCode())
                {
                    availableSpaces.Add([row, column]);
                }
            }
        }

        Random rnd = new Random();
        int numberOfAvailableSpaces = availableSpaces.Count;
        int randomIntRange = rnd.Next(0, numberOfAvailableSpaces);
        int rowInput = availableSpaces.ElementAt(randomIntRange)[0];
        int columnInput = availableSpaces.ElementAt(randomIntRange)[1];

        board.InputValue(computer, symbol, rowInput, columnInput);
        Console.WriteLine("Computer's turn, thinking...");
        Thread.Sleep(3000);
        board.Display();
        return board;
    }
    
    public Dictionary<string, object> CheckForEndScenerio(Board board)
    {
        int numberOfEndScenerios = 0;
        Dictionary<string, object> review = new Dictionary<string, object>();

        // horizontal trio
        for (int row = 0; row < 3; row++)
        {
            int[] trio = new int[3];
            for (int col = 0; col < 3; col++)
            {
                int spot = board.config[row,col];
                trio[col] = spot;
            }
            
            Dictionary<string, int> possibleEndScenerios = new Dictionary<string, int>();
            if (IsTrioPossible(trio, ref possibleEndScenerios))
            {
                numberOfEndScenerios++;
                
                review["position"] = possibleEndScenerios["position"];
                review["marker"] = possibleEndScenerios["marker"];
            }
        }

        // vertical trio
        for (int row = 0; row < 3; row++)
        {
            int[] trio = new int[3];
            for (int col = 0; col < 3; col++)
            {
                int spot = board.config[col, row];
                trio[col] = spot;
            }
            
            Dictionary<string, int> possibleEndScenerios = new Dictionary<string, int>();
            if (IsTrioPossible(trio, ref possibleEndScenerios))
            {
                numberOfEndScenerios++;
                review["position"] = possibleEndScenerios["position"];
                review["marker"] = possibleEndScenerios["marker"];
            }
        }

        // // diagonals
        // int[] diagonalDownRight = {board.config[0,0],board.config[1,1],board.config[2,2]};
        // int[] diagonalDownLeft = {board.config[0,2],board.config[1,1],board.config[2,0]};
        // if (CheckForMatch(diagonalDownRight) || CheckForMatch(diagonalDownLeft))
        // {
        //     return true;
        // }

        review["numberOfEndScenerios"] = numberOfEndScenerios;

        return review;
    }
    public bool IsTrioPossible(int[] trio, ref Dictionary<string, int> possibleEndScenerios)
    {
        int first = trio[0];
        int second = trio[1];
        int third = trio[2];

        int emptySpot = Marker.Empty.GetHashCode();

        if (first == emptySpot && second == third)
        {
            possibleEndScenerios["position"] = first;
            possibleEndScenerios["marker"] = second;
            return true;
        }
        else if (second == emptySpot && first == third)
        {
            possibleEndScenerios["position"] = second;
            possibleEndScenerios["marker"] = first;
            return true;
        }
        else if (third == emptySpot && second == third)
        {
            possibleEndScenerios["position"] = third;
            possibleEndScenerios["marker"] = second;
            return true;
        }
        return false;
    }
}