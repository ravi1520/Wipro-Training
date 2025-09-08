namespace SecureShop.Models
{
    public class Product
    {

    public int Id { get; set; }

    public required string Name { get; set; }        // required property
    public required string Description { get; set; } // required property

    public decimal Price { get; set; }
}

    }

