﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiacPraksaP1.Models;

namespace DataAccessLayer.Entities
{
    public class UserProduct
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int ProductId {  get; set; }

        public Product Product { get; set; }
    }
}