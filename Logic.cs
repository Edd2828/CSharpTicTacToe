using Enums;
using Asset;
using Player;


namespace Builder;

class GameController
{
    public PlayerType playerTurn;
   

    public void Game()
    {
        Console.WriteLine("Game Start...");
        Board board = new Board();
        Human human = new Human();
        Computer computer = new Computer();

        board.Initialise();

        SelectStartPlayer();

        while(!CheckWinCondition(board))
        {
            
            if (playerTurn == PlayerType.human)
            {
                board = human.Turn(board);
                playerTurn = PlayerType.computer;
            }
            else if (playerTurn == PlayerType.computer)
            {
                board = computer.Turn(board);
                playerTurn = PlayerType.human;
            }
        }
        Console.WriteLine($"{playerTurn} loses");
    }

    private void SelectStartPlayer()
    {
        Random rnd = new Random();
        int randomIntRange = rnd.Next(0, 1);
        playerTurn = (PlayerType)randomIntRange;
    }

    
    public bool CheckWinCondition(Board board)
    {
        
        // column
        if (board.RowColumnChecker(BoardElement.column))
        {
            return true;
        }

        // row
        if (board.RowColumnChecker(BoardElement.row))
        {
            return true;
        }

        // diagonals
        int[] diagonalDownRight = {board.config[0,0],board.config[1,1],board.config[2,2]};
        int[] diagonalDownLeft = {board.config[0,2],board.config[1,1],board.config[2,0]};
        if (board.CheckDistinctValues(diagonalDownRight) || board.CheckDistinctValues(diagonalDownLeft))
        {
            return true;
        }

        int emptySpaceCount = 0;
        foreach (int item in board.config)
        {
            if (item == Marker.Empty.GetHashCode())
            {
                emptySpaceCount++;
            }
        }
        if (emptySpaceCount == 0)
        {
            Console.WriteLine("It is a draw");
            return true;
        }
        
        return false;
    }

    
}