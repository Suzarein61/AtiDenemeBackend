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
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _catagoryDal;
        public CategoryManager(ICategoryDal catagoryDal)
        {
            _catagoryDal = catagoryDal;
        }
        public List<Category> GetList()
        {
            return _catagoryDal.GetList().ToList();
        }
    }
}
