namespace Data_Storage_Submission.Services.Menus;

internal class DisplayTableService<T> where T : class
{
    private List<int> colWidths = new List<int> ();

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

        colWidths.Insert(0, 3);
    }

    private void PrintHeaders(T row)
    {
        var properties = row!.GetType().GetProperties();
        var propList = new List<string>
        {
            "#"
        };

        foreach (var property in properties)
        {
            propList.Add(property.Name);
        }
        PrintLine(propList.Count);

        PrintRow(propList);
    }

    private void PrintRows(List<T> rows)
    {
        for (int i =0; i<rows.Count; i++) 
        {
            var propValues = new List<string>() { (i + 1).ToString() };
            var properties = rows[i].GetType().GetProperties();

            foreach (var property in properties)
            {
                var propValue = (property.GetValue(rows[i]) ?? "").ToString();
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