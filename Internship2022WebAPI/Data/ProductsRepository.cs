using Internship2022WebAPI.Data.DaoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;

namespace Internship2022WebAPI.Data
{
    public interface IProductsRepository
    {
        List<ProductModel> GetProducts();
        ProductModel AddProduct(ProductModel product);
        
        ProductDao GetById(Guid id);
        ProductDao GetTheMostRecent();
        
        List<ProductDao> GetByKeyword(string keyword);
        List<ProductDao> GetByRatingsAvgAsc();
        List<ProductDao> GetByRatingsAvgDesc();

        ProductDao Modify(ProductDao prodcut , Guid id ,string name, string descirption, int[] ratings);


    }
    public class ProductsRepository : IProductsRepository
    {
        private readonly RetailDbContext retailDbContext;
        public ProductsRepository(RetailDbContext retailDbContext)
        {
            this.retailDbContext = retailDbContext;
        }

        public List<ProductModel> GetProducts()
        {
            return retailDbContext.Products.Select(product => new ProductModel
            {
                Id = product.Id,
                Name = product.Name

            }).ToList();
        }

        public List<ProductDao> GetByKeyword(string keyword)
        {
            return retailDbContext.Products.Where(s => s.Name.Contains(keyword) ||
              s.Id.ToString().Contains(keyword) || s.Description.Contains(keyword)).ToList();



        }
        public ProductModel AddProduct(ProductModel product)
        {
            retailDbContext.Products.Add(new DaoModels.ProductDao
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CreatedOn = DateTime.Now,
                Ratings = System.Text.Json.JsonSerializer.Serialize(product.Ratings)
            });
            retailDbContext.SaveChanges();

            return product;
        }

        public ProductDao GetById(Guid id)
        {

            return retailDbContext.Products.FirstOrDefault(x => x.Id == id);

        }


        public ProductDao GetTheMostRecent()
        {
            List<ProductDao> myList = (List<ProductDao>)retailDbContext.Products.OrderBy(x => x.CreatedOn).ToList();

            return myList[myList.Count - 1];
        }

        public List<ProductDao> GetByRatingsAvgAsc()
        {

            return (List<ProductDao>)retailDbContext.Products.OrderByDescending(product => product.Ratings).ToList();
        }

        public List<ProductDao> GetByRatingsAvgDesc()
        {

            return (List<ProductDao>)retailDbContext.Products.OrderBy(product => product.Ratings).ToList();
        }

        public ProductDao Modify(ProductDao product, Guid id, string name, string descirption, int[] ratings)
        {

            product.Name = name;
            product.Description = descirption;
            product.Ratings = System.Text.Json.JsonSerializer.Serialize(ratings);
            retailDbContext.SaveChanges();
            return product;

        }



    }

}
