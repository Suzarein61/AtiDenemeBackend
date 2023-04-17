using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatagoriesController : ControllerBase
    {
        private ICategoryService _catagoryService;  
        public CatagoriesController(ICategoryService catagoryService)
        {
            _catagoryService = catagoryService;
        }

        [HttpGet("getall")]
        public IActionResult GetList()
        {
            var data = _catagoryService.GetList();
            return Ok(data);


        }

       
    }
}
