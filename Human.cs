using Asset;
using Enums;

namespace Player;


class Human
{
    PlayerType human = PlayerType.human;
    Marker symbol = Marker.X;
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
            board.InputValue(human, symbol, chosenPosition[0], chosenPosition[1]);
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