using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace MySpecFlowTests.Utilities
{
    public class CSVReader
    {
        public static IEnumerable<Dictionary<string, string>> ReadCSV(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<Dictionary<string, string>>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var row = new Dictionary<string, string>();
                    foreach (var header in csv.HeaderRecord)
                    {
                        row[header] = csv.GetField(header);
                    }
                    records.Add(row);
                }
                return records;
            }
        }
    }
}
