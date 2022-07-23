﻿using KT.Services.Catalog.Dtos.CategoryDtos;
using KT.Services.Catalog.Dtos.FeatureDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Services.Catalog.Dtos.CourseDtos
{
    public class CourseDto
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
      
        public decimal Price { get; set; }

        // Bir Kurs bir kullanıcıya ait olacaktır. Oluşturan Kişi
        public string UserId { get; set; }

        public string Picture { get; set; }
 
        public DateTime CreatedTime { get; set; }

        public FeatureDto Feature { get; set; }

      
        public string CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}
