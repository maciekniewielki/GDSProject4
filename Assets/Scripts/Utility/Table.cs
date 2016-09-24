public class Table
{
    public string[][] data;
    public string[] headers;

    private int height;
    private int width;

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }

    public Table(int width, int height)
    {
        this.height = height;
        this.width = width;
        data = new string[width][];
        for (int ii = 0; ii < width; ii++)
            data[ii] = new string[height];

        headers = new string[width];
    }

    public void SetHeader(string[] headerData)
    {
        SetRow(0, headerData);
    }

    public void SetRow(int rowNumber, string[] rowData)
    {
        data[rowNumber] = rowData;
    }

}
