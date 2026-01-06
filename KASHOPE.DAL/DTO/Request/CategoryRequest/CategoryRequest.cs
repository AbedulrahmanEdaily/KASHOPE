using KASHOPE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Request.CategoryRequest
{
    public class CategoryRequest
    {
        public Status Status { get; set; }
        public List<CategoryTranslationRequest> CategoryTranslations { get; set; } = new();
    }
}
