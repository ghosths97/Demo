using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services
{
    public class InMemoryProductService : IProductService
    {
        public InMemoryProductService()
        {
            Products = new List<Product>();
        }

        public List<Product> Products { get; set; }

        public Product GetProduct(int id)
        {
            return Products.Where(p => p.id == id).FirstOrDefault();
        }

        public void AddProduct(Product p)
        {
            Products.Add(p);
        }

        public IEnumerable<Product> GetAll()
        {
            return Products;
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
