using Microsoft.VisualBasic.FileIO;
using PMF.Database.Entities;
namespace PMF.Services
{
    public class ImportCsv
    {
        public List<TransactionEntity> Import()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string relativePath = "transactions.csv";
            string filePath = Path.Combine(currentDirectory, relativePath);
            List<TransactionEntity> records = new List<TransactionEntity>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length >= 9)
                    {
                        TransactionEntity record = new TransactionEntity
                        {
                            Id = int.Parse(fields[0]),
                            BeneficiaryName = fields[1],
                            Date = DateTime.Parse(fields[2]),
                            Direction = fields[3],
                            Amount = decimal.Parse(fields[4]),
                            Description = fields[5],
                            Currency = fields[6],
                            Mcc = fields[7],
                            Kind = fields[8]
                        };

                        records.Add(record);
                    }
                }
            }

            return records;
        }
    }
}
