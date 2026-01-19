using Enums;


namespace Asset;

class Board
{
    public int[,] config;
    public Marker playerSymbol = Marker.X;
    public Marker computerSymbol = Marker.O;
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
    public void InputValue(PlayerType player, int row, int column)
    {
        if (player == PlayerType.computer)
        {
            config[row,column] = computerSymbol.GetHashCode();
        }
        else if (player == PlayerType.human)
        {
            config[row,column] = playerSymbol.GetHashCode();
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
            if (CheckDistinctValues(rowCollection))
            {
                return true;
            }
        }
        return false;
    }
}