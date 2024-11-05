using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace productlib
{
    public class FileContext
    {
        private readonly string _filename;

        public List<Product> Products { get; set; }

        public FileContext(string filename)
        {
            _filename = filename;
            if(!File.Exists(filename))
            {
                File.WriteAllText(_filename, "[]");
            }
            string jsonData = File.ReadAllText(_filename) ?? "[]";
            Products = JsonSerializer.Deserialize<List<Product>>(jsonData) ?? [];
        }

        public int SaveChanges()
        {
            File.WriteAllText(_filename, JsonSerializer.Serialize<List<Product>>(Products));
            return Products.Count;
        }
    }
}
