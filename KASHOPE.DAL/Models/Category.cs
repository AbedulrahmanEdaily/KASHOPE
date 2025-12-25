using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.Models
{
    public class Category : BaseModel
    {
        public List<CategoryTranslation>? CategoryTranslations { get; set; }
        public List<Product> Products { get; set; }
    }
}
