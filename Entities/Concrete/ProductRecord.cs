﻿using Core.Entity;

namespace Entities.Concrete
{
    public class ProductRecord:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int LanguageId { get; set; }
        public int ProductId { get; set; }
        //public virtual Product Product { get; set; } = null!;
        public virtual Language Language { get; set; }

        public bool IsDeleted { get; set; }

    }
}