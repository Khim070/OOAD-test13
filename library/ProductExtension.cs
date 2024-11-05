using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace productlib
{
    public static class ProductExtension
    {
        public static Product ToEntity(this ProductCreateReq req)
        {
            var category = Category.None;
            Category.TryParse(req.Category, out category);
            return new Product
            {
                Id = Guid.NewGuid().ToString(),
                Name = req.Name,
                Code = req.Code,
                Category = category,
                Created = DateTime.Now,
                LastUpdated = null,
            };
        }

        public static ProductResponse ToResponse(this Product prd)
        {
            return new ProductResponse
            {
                Id = prd.Id,
                Code = prd.Code,
                Name = prd.Name,
                Category = prd.Category.ToString(),
            };
        }

        public static void Copy(this Product prd, ProductUpdateReq req)
        {
            var category = Category.None;
            Category.TryParse(req.Category, out category);
            prd.Name = req.Name;
            prd.Category = category;
        }

        public static Product Clone(this Product prd)
        {
            return new Product
            {
                Id = prd.Id,
                Code = prd.Code,
                Name = prd.Name,
                Category = prd.Category,
                Created = prd.Created,
                LastUpdated = prd.LastUpdated,
            };
        }

        public static void Copy(this Product prd, Product other)
        {
            prd.Id = other.Id;
            prd.Code = other.Code;
            prd.Name = other.Name;
            prd.Category = other.Category;
            prd.Created = other.Created;
            prd.LastUpdated = other.LastUpdated;
        }
    }
}
