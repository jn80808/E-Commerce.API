namespace E_Commerce.API.Models.Domain
{
    public class ProductCategory
    // Junction Table for Many-to-Many Relationship between Product and Category
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int CategoryId { get; set; }
       
        public Category Category { get; set; }



    }

}
