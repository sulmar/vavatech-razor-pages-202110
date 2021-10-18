﻿using System;

namespace Vavatech.RazorPages.Models
{
    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    
}
