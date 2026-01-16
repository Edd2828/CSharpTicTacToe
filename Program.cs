using Builder;

namespace TicTacToe;

class Program
{
    static void Main(string[] args)
    {

        GameController controller = new GameController();
        controller.Game();
    }
}