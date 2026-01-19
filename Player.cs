using Asset;
using Enums;

namespace Player;

class Computer
{
    PlayerType computer = PlayerType.computer;
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

        board.InputValue(computer, rowInput, columnInput);
        Console.WriteLine("Computer's turn");
        board.Display();
        return board;
    }
}

class Human
{
    PlayerType human = PlayerType.human;
    public int[] PlayerChoice()
    {
        // int[] chosenPosition = new int[2];
        Console.WriteLine("Choose row (options are 0,1,2): ");
        int row = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Choose column (options are 0,1,2): ");
        int column = Convert.ToInt32(Console.ReadLine());

        if (row > 2 || row < 0 || column > 2 || column < 0)
        {
            Console.WriteLine("Must choose row and column using 0,1,2");
            PlayerChoice();
        }

        int[] chosenPosition = [row, column];

        return chosenPosition;
    }

    public Board Turn(Board board)
    {
        Console.WriteLine("**");
        int[] chosenPosition = PlayerChoice();
        bool spaceCheck = board.CheckSpaceAvailable(chosenPosition);
        if (spaceCheck)
        {
            board.InputValue(human, chosenPosition[0], chosenPosition[1]);
            board.Display();
        }
        else
        {
            Console.WriteLine("Space not available, choose again..");
            Turn(board);
        }

        return board;
    }
}