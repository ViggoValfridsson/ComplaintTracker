namespace Data_Storage_Submission.Services.Menus;

internal class DisplayTableService<T> where T : class
{
    private List<int> colWidths = new List<int>();
    public void DisplayTable(List<T> data)
    {
        CalculateColWidths(data);
        PrintHeaders(data[0]);
        PrintRows(data);
    }

    private void CalculateColWidths(List<T> data)
    {
        foreach (var row in data)
        {
            var properties = row!.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var propValue = (properties[i].GetValue(row) ?? "").ToString();
                int colWidth;

                if (propValue!.Length > properties[i].Name.Length)
                {
                    colWidth = propValue!.Length;
                }
                else
                {
                    colWidth = properties[i].Name.Length;
                }

                if (i >= colWidths.Count)
                {
                    colWidths.Add(colWidth);
                }
                else if (colWidth > colWidths[i])
                {
                    colWidths[i] = colWidth;
                }
            }
        }
    }

    private void PrintHeaders(T row)
    {
        var properties = row!.GetType().GetProperties();
        var propList = new List<string>();

        PrintLine(properties.Length);

        foreach (var property in properties)
        {
            propList.Add(property.Name);
        }

        PrintRow(propList);
    }

    private void PrintRows(List<T> rows)
    {
        foreach (var row in rows)
        {
            var propValues = new List<string>();
            var properties = row!.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propValue = (property.GetValue(row) ?? "").ToString();
                propValues.Add(propValue!);
            }

            PrintRow(propValues);
        }
    }

    private void PrintRow(List<string> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            Console.Write("|" + values[i].PadLeft(colWidths[i]));
        }

        Console.Write("|\n");

        PrintLine(values.Count);
    }

    private void PrintLine(int colAmount)
    {
        Console.Write("+");
        for (int i = 0; i < colAmount; i++)
        {
            Console.Write(new String('-', colWidths[i]) + "+");
        }
        Console.WriteLine();
    }
}