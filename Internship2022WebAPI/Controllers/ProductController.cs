using Internship2022WebAPI.Data;
using Internship2022WebAPI.Data.DaoModels;
using Microsoft.AspNetCore.Mvc;

namespace Internship2022WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsRepository productsRepository;
        public ProductController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        [HttpGet("get")]
        public IActionResult Get()
        {
            return Ok(productsRepository.GetProducts());
        }

        [HttpGet("get/by/Id")]
        public IActionResult GetById(Guid id)
        {
            ProductDao product = productsRepository.GetById(id);
            if (product is null)
                return NotFound();

            return Ok(product);
        }


        [HttpGet("get/by/keyWord")]

        public IActionResult GetByKeyword(string keyword)
        {
            List<ProductDao> product = productsRepository.GetByKeyword(keyword);
            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("get/theMostRecentCreated")]

        public IActionResult GetTheMostRecentCreated()
        {
            ProductDao product = productsRepository.GetTheMostRecent();
            if (product is null)
                return NotFound();

            return Ok(product);
        }


        [HttpGet("get/GetByRatingsAscending")]
        public IActionResult GetByRatingsAsc()
        {
            List<ProductDao> product = productsRepository.GetByRatingsAvgAsc();

            return Ok(product);
        }

        [HttpGet("get/GetByRatingsDescending")]
        public IActionResult GetByRatingsDesc()
        {
            List<ProductDao> product = productsRepository.GetByRatingsAvgDesc();

            return Ok(product);
        }

        [HttpPost("create")]
        public IActionResult Create(ProductModel product)
        {
            return Ok(productsRepository.AddProduct(product));
        }

        

        [HttpPatch("update/updateProduct")]
        public IActionResult Update(Guid id , string name , string description , int[] ratings)
        {
            ProductDao product = productsRepository.GetById(id);

            return Ok(productsRepository.Modify(product,id,name,description,ratings));
        }
    }
}
