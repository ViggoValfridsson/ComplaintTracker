namespace Data_Storage_Submission.Services.Menus;

internal class DisplayTableService<T> where T : class
{
    // List of the character width of each column. This is used so that every row is the same width
    private List<int> colWidths = new List<int>();

    public void DisplayTable(List<T> data)
    {
        CalculateColWidths(data);
        PrintHeaders(data[0]);
        PrintRows(data);
    }

    private void CalculateColWidths(List<T> data)
    {
        // Checks all values and property name string lengths and takes the longest value and sets it as the column width
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

        // Insert the width for the row number column
        colWidths.Insert(0, 5);
    }

    private void PrintHeaders(T row)
    {
        // Prints the property names as headers in the table.
        var properties = row!.GetType().GetProperties();

        // Adds header for row number column
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
        for (int i = 0; i < rows.Count; i++)
        {
            // Initial data in list is the row number
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
            // Pads the left of the string to make sure that the column is a consistent width
            Console.Write("|" + values[i].PadLeft(colWidths[i]));
        }

        Console.Write("|\n");

        PrintLine(values.Count);
    }

    private void PrintLine(int colAmount)
    {
        // Creates a line to break off every row
        Console.Write("+");
        for (int i = 0; i < colAmount; i++)
        {
            Console.Write(new String('-', colWidths[i]) + "+");
        }
        Console.WriteLine();
    }
}