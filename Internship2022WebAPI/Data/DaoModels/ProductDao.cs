namespace Internship2022WebAPI.Data.DaoModels
{
    public class ProductDao
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ratings { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
