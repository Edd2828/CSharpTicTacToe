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

        while(!board.CheckWinCondition())
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
}