﻿namespace TiacPraksaP1.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; } = 0;

        public User Owner { get; set; } 

        public string OwnerId {  get; set; }
    }
}
