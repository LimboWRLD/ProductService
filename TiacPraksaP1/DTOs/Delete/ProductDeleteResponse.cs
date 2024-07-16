namespace TiacPraksaP1.DTOs.Delete
{
    public class ProductDeleteResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; } = 0;
    }
}
