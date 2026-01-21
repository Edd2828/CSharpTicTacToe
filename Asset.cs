using Enums;


namespace Asset;

class Board
{
    public int[,] config;
    public Marker emptySlot = Marker.Empty;

    public void Initialise()
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

        config = emptyBoard;
    }

    public void Display()
    {
        for (int i = 0; i < 3; i++)
        {
            var col1 = SymbolSpacing((Marker)config[i,0]);
            var col2 = SymbolSpacing((Marker)config[i,1]);
            var col3 = SymbolSpacing((Marker)config[i,2]);

            Console.WriteLine($"{col1} | {col2} | {col3}");

        }
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
    public void InputValue(PlayerType player, Marker symbol, int row, int column)
    {
        if (player == PlayerType.computer)
        {
            config[row,column] = symbol.GetHashCode();
        }
        else if (player == PlayerType.human)
        {
            config[row,column] = symbol.GetHashCode();
        }
    }
    public bool CheckSpaceAvailable(int[] chosenPosition)
    {
        int row = chosenPosition[0];
        int column = chosenPosition[1];

        if (config[row, column] == emptySlot.GetHashCode())
        {
            return true;
        }

        return false;
    }
    public bool CheckForMatch(int[] rowOrColumn)
    {

        HashSet<int> removeDuplicates = [.. rowOrColumn];
        List<int> noDuplicatesRowOrColumn = [.. removeDuplicates];

        if (noDuplicatesRowOrColumn.Count == 1 && noDuplicatesRowOrColumn[0] != Marker.Empty.GetHashCode())
        {
            return true;
        }

        return false;
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
                    rowCollection[column] = config[row, column];
                }
                else if (boardElement == BoardElement.column)
                {
                    rowCollection[column] = config[column, row];
                }
            }
            if (CheckForMatch(rowCollection))
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
        int[] diagonalDownRight = {config[0,0],config[1,1],config[2,2]};
        int[] diagonalDownLeft = {config[0,2],config[1,1],config[2,0]};
        if (CheckForMatch(diagonalDownRight) || CheckForMatch(diagonalDownLeft))
        {
            return true;
        }

        int emptySpaceCount = 0;
        foreach (int item in config)
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