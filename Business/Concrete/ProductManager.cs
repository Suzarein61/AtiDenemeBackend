using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal= productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
            
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public void update(Product product)
        {
            _productDal.Update(product);
        }


        public Product GetByID(int productId)
        {
              return _productDal.Get(p => p.ProductID == productId);
            
        }

        public List<Product> GetList()
        {
            return _productDal.GetList().ToList();
        }

        public List<Product> GetListByCategory(int catagoryId)
        {
            return _productDal.GetList(p => p.CategoryID == catagoryId).ToList();
        }

    }




 
}
