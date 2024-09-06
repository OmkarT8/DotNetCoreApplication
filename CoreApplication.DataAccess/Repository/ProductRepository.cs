using CoreApplication.DataAccess.Data;
using CoreApplication.DataAccess.Repository.IRepository;
using CoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApplication.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
