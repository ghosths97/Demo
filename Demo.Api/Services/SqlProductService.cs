using Demo.Models;
using Demo.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services
{

    // SqlRepo : IRepo
    public class SqlProductService : IProductService
    {

        private DemoDbContext _dbContext;

        public SqlProductService(DemoDbContext context)
        {
            _dbContext = context;
        }

       
        public void AddProduct(Product p)
        {
            _dbContext.Products.Add(p);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var p = _dbContext.Products.Where(p => p.id == id).FirstOrDefault();
            if(p!= null)
                _dbContext.Products.Remove(p);


            _dbContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
           return  _dbContext.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return _dbContext.Products.Where(p => p.id == id).FirstOrDefault();
        }

        public void Update(Product product)
        {
            var p = _dbContext.Products.Where(p => p.id == product.id).FirstOrDefault();
            p.name = product.name;
            p.production = product.production;

            _dbContext.SaveChanges();
        }
    }
}
