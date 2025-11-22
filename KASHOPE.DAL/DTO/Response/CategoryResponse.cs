using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public List<CategoryTranslationResponse> CategoryTranslations { get; set; } = new();
    }
}
