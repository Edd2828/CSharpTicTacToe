using Enums;


namespace Builder;

class GameController
{
    public Player playerTurn;
    public int[,] board; 
    public Marker playerSymbol = Marker.X;
    public Marker computerSymbol = Marker.O;

    public Marker emptySlot = Marker.Empty;

    public void Game()
    {
        Console.WriteLine("Game Start...");
        InitialiseEmptyBoard();

        SelectStartPlayer();

        while(!CheckWinCondition())
        {
            
            if (playerTurn == Player.player)
            {
                PlayerTurn();
                playerTurn = Player.computer;
            }
            else if (playerTurn == Player.computer)
            {
                ComputerTurn();
                playerTurn = Player.player;
            }
        }
        Console.WriteLine($"{playerTurn} loses");
    }

    private void SelectStartPlayer()
    {
        Random rnd = new Random();
        int randomIntRange = rnd.Next(0, 1);
        playerTurn = (Player)randomIntRange;
    }
    public string SymbolSpacing(Marker symbol)
    {
        if (symbol == emptySlot)
        {
            return symbol.ToString();
        }
        else
        {
            return $"  {symbol}  ";
        }
    }
    public void DisplayBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            var col1 = SymbolSpacing((Marker)board[i,0]);
            var col2 = SymbolSpacing((Marker)board[i,1]);
            var col3 = SymbolSpacing((Marker)board[i,2]);

            Console.WriteLine($"{col1} | {col2} | {col3}");

        }
    }
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
    public bool CheckSpaceAvailable(int[] chosenPosition)
    {
        int row = chosenPosition[0];
        int column = chosenPosition[1];

        if (board[row, column] == emptySlot.GetHashCode())
        {
            return true;
        }

        return false;
    }
    public void InitialiseEmptyBoard()
    {
        int empty = emptySlot.GetHashCode();
        int[,] emptyBoard = new int[3,3];

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                emptyBoard[row,col] = empty;
            }
            
        }

        board = emptyBoard;
    }

    public void InputValue(int row, int column)
    {
        if (playerTurn == Player.computer)
        {
            board[row,column] = computerSymbol.GetHashCode();
        }
        else if (playerTurn == Player.player)
        {
            board[row,column] = playerSymbol.GetHashCode();
        }
    }

    public void PlayerTurn()
    {
        Console.WriteLine("**");
        int[] chosenPosition = PlayerChoice();
        bool spaceCheck = CheckSpaceAvailable(chosenPosition);
        if (spaceCheck)
        {
            InputValue(chosenPosition[0], chosenPosition[1]);
            DisplayBoard();
        }
        else
        {
            Console.WriteLine("Space not available, choose again..");
            PlayerTurn();
        }
    }

    public void ComputerTurn()
    {
        Console.WriteLine("**");
        List<int[]> availableSpaces = new List<int[]>();

        for (int row = 0; row < 3; row++)
        {
            for (int column = 0; column < 3; column++)
            {
                if (board[row,column] == emptySlot.GetHashCode())
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

        InputValue(rowInput, columnInput);
        Console.WriteLine("Computer's turn");
        DisplayBoard();
    }

    public bool RowColumnChecker(BoardElement boardElement)
    {
        for (int row = 0; row < 3; row++)
        {
            int[] rowCollection = new int[3];
            for (int column = 0; column < 3; column++)
            {
                if (boardElement == BoardElement.row)
                {
                    rowCollection[column] = board[row, column];
                }
                else if (boardElement == BoardElement.column)
                {
                    rowCollection[column] = board[column, row];
                }
            }
            if (CheckDistinctValues(rowCollection))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckWinCondition()
    {
        
        // column
        if (RowColumnChecker(BoardElement.column))
        {
            return true;
        }

        // row
        if (RowColumnChecker(BoardElement.row))
        {
            return true;
        }

        // diagonals
        int[] diagonalDownRight = {board[0,0],board[1,1],board[2,2]};
        int[] diagonalDownLeft = {board[0,2],board[1,1],board[2,0]};
        if (CheckDistinctValues(diagonalDownRight) || CheckDistinctValues(diagonalDownLeft))
        {
            return true;
        }

        int emptySpaceCount = 0;
        foreach (int item in board)
        {
            if (item == emptySlot.GetHashCode())
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

    public bool CheckDistinctValues(int[] rowOrColumn)
    {
        List<int> itemsToCheck = new List<int>();

        int iterationCount = 0;

        foreach (int item in rowOrColumn)
        {
            if (item == emptySlot.GetHashCode())
            {
                return false;
            }

            if (iterationCount == 0)
            {
                itemsToCheck.Add(item);
            }
            else
            {
                if (rowOrColumn[iterationCount - 1] != item)
                {
                    itemsToCheck.Add(item);
                }
            }

            iterationCount++;
        }

        if (itemsToCheck.Count == 1)
        {
            return true;
        }

        return false;
    }
}