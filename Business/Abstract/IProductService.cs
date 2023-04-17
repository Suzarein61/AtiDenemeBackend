using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        Product GetByID(int productId);
        List<Product> GetList();
        List<Product> GetListByCategory(int catagoryId);

        void Add(Product product);
        void Delete(Product product);
        void update(Product product);

        //IDataResult<Product> GetByID(int productId);
        //IDataResult<List<Product>> GetList();
        //IDataResult<List<Product>> GetListByCatagory(int catagoryId);

        //IResult Add(Product product);
        //IResult Delete(Product product);
        //IResult update(Product product);



    }
}
