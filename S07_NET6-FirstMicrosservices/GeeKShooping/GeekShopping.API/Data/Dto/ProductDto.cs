namespace GeekShopping.API.Data.Dto
{
    public class ProductDto //Espelho da entidade, para que a entidade não seja exposta ao client
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageURL { get; set; }
    }
}
