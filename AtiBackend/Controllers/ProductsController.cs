using Business.Abstract;
using Business.Validation;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        ProductValidator _validator = new ProductValidator();
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("getall")]
        
        public IActionResult GetList()
        {
            var result = _productService.GetList();
           return Ok(result);
        }

        [HttpGet("getbycatagory")]
        public IActionResult GetListByCatagory(int catagoryId)
        {
            var result = _productService.GetListByCategory(catagoryId);
            return Ok(result);
        }

        [HttpGet("get")]
        public IActionResult Get(int productId)
        {
            
            var result = _productService.GetByID(productId);
            return Ok(result);
        }



        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _validator.Validate(product);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                }

                return BadRequest(ModelState);
            }

            _productService.Add(product);
            return Ok();
        }

        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _validator.Validate(product);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }
            _productService.update(product);
            return Ok();
        }

        [HttpPost("delete")]
        public IActionResult Delete(Product product)
        {
            _productService.Delete(product);
            return Ok();
        }
    }
}
