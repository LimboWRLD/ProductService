namespace TiacPraksaP1.DTOs.Post
{
    public class ProductPostResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; } = 0;
    }
}
