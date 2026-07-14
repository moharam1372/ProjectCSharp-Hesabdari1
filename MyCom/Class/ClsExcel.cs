using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCom.Class
{
    public static class ClsExcel
    {
        static string GetBaseDirectory(string path)
        {
            string[] parts = path.Split(Path.DirectorySeparatorChar);
            return parts.Length >= 3 ? $"{parts[0]}{Path.DirectorySeparatorChar}{parts[1]}{Path.DirectorySeparatorChar}{parts[2]}" : path;
        }
        public class ModelExcel
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
        }
        public static List<ModelExcel> OpenFile()
        {

            string filePath = @"M:\Project New Game Port\GamePort\مقالات\Game.xlsx";
            var dataList = new List<ModelExcel>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                int lastRow = worksheet.LastRowUsed().RowNumber();

                for (int row = 1; row <= lastRow; row++) // فرض می‌کنیم ردیف اول عنوان نیست
                {
                    var model = new ModelExcel
                    {
                        A = worksheet.Cell(row, 1).GetString(),
                        B = worksheet.Cell(row, 2).GetString(),
                        C = GetBaseDirectory(worksheet.Cell(row, 2).GetString())
                    };
                    dataList.Add(model);
                }
            }

            return dataList;
        }
    }
}
