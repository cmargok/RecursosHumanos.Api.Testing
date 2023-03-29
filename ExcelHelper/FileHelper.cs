using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Linq;
using System.Data;

namespace ExcelHelper
{
    public class FileHelper
    {
        public static List<List<string>> GetExcelData(IFormFile file) 
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            IExcelDataReader reader = null;

            DataSet dataSet = new();

            using (var stream = file.OpenReadStream())
            {

                if (file.FileName.EndsWith(".xls"))
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
                }
                else if (file.FileName.EndsWith(".xlsx"))
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                }
                if (reader == null)
                {
                    return null!;
                }


                dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = false
                    }
                });

            }

                int row_no = 1;

                List<List<string>> list = new List<List<string>>();

            List<string> arr;

                while (row_no < dataSet.Tables[0].Rows.Count)
                {                                                            
                    arr = new List<string>();

                    for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                    {
                        arr.Add(dataSet.Tables[0].Rows[row_no][i].ToString()); 
                    }

                    list.Add(arr);                   
                    row_no++;
            }

            return list;
        }

    


    }
}
