using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productlib
{
    public class ProductRepo
    {
        private readonly DbContext _context;

        public ProductRepo(DbContext context)
        {
            _context = context;
        }

        public void Create(Product entity)
        {
            _context.Set<Product>().Add(entity.Clone());
            _context.SaveChangesAsync();
        }

        public void Create(List<Product> products)
        {
            _context.Set<Product>().AddRange(products);
            _context.SaveChangesAsync();
        }

        public IQueryable<Product> GetQueryable() => _context.Set<Product>().AsQueryable();

        public bool Update(Product entity)
        {
            var found = GetQueryable().FirstOrDefault(x => x.Id == entity.Id);
            if (found != null)
            {
                found.Copy(entity);
                _context.SaveChangesAsync();
            }
            return found != null;
        }

        public bool Delete(string id)
        {
            var found = _context.Set<Product>().FirstOrDefault(x => x.Id == id);
            if(found == null)
            {
                return false;
            }

            var result = _context.Set<Product>().Remove(found);
            var task = _context.SaveChangesAsync();
            task.Wait();
            return task.Result > 0;
        }
    }
}
