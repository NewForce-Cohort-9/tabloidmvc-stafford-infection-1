﻿using System.Collections.Generic;

namespace TabloidMVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Post> RelatedPost { get; set; } = new List<Post>();
    }
}