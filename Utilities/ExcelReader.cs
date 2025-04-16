using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MySpecFlowTests.Utilities
{
    public static class ExcelReader
    {
        public static List<Dictionary<string, string>> ReadExcel(string filePath)
        {
            var dataList = new List<Dictionary<string, string>>();

            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(file);
                ISheet sheet = workbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);

                for (int i = 1; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;

                    var rowData = new Dictionary<string, string>();
                    for (int j = 0; j < headerRow.LastCellNum; j++)
                    {
                        string key = headerRow.GetCell(j).ToString();
                        string value = row.GetCell(j)?.ToString() ?? string.Empty;
                        rowData[key] = value;
                    }

                    dataList.Add(rowData);
                }
            }

            return dataList;
        }
    }
}
