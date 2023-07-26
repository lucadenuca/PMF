using Microsoft.VisualBasic.FileIO;
using PMF.Database.Entities;
using System.IO;

namespace PMF.Services
{
    public class CategoriesImporter
    {
        public List<CategoryEntity> ImportCategories()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string relativePath = "categories.csv";
            string filePath = Path.Combine(currentDirectory, relativePath);
            List<CategoryEntity> categories = new List<CategoryEntity>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.ReadLine(); // Skip the header row if present

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length >= 3)
                    {
                        string code = fields[0];
                        string parentCode = fields[1];
                        string name = fields[2];

                        // Create a new SpendingCategory object and add it to the list
                        categories.Add(new CategoryEntity
                        {
                            Code = code,
                            ParentCode = parentCode,
                            Name = name
                        });
                    }
                    // Skip the row if it does not have enough fields or does not match the expected format
                }
            }

            return categories;
        }
    }
}
