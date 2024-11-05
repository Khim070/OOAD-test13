using Microsoft.EntityFrameworkCore;
using productlib;
using System.Text.Json;

namespace ProductAPI
{
    public class FileDbContext : DbContext
    {
        private readonly string _filePath;

        public FileDbContext(DbContextOptions<FileDbContext> options, string filePath) : base(options)
        {
            _filePath = filePath;
        }

        // override onModelCreating if needed
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Product>(new ProductConfig());
        }

        // Load data from JSON when context is initialized
        public async Task LoadDataAsync()
        {
            if(File.Exists(_filePath))
            {
                var jsonData = await File.ReadAllTextAsync(_filePath);
                var Products = JsonSerializer.Deserialize<List<Product>>(jsonData);
                if (Products != null)
                {
                    Set<Product>().AddRange(Products);
                    await SaveChangesAsync();
                }
            }
        }

        // Save data to JSON file upon changes
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            var Products = await Set<Product>().ToListAsync();
            var jsonData = JsonSerializer.Serialize(Products);
            await File.WriteAllTextAsync(_filePath, jsonData);
            return result;
        }
    }
}
