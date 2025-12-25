using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Mapster
{
    public static class MapsterConfiguration
    {
        public static void MapConfigCategory()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig().Map(ds => ds.CreatedBy, sr => sr.User.UserName);
            TypeAdapterConfig<Category, CategoryUserResponse>.NewConfig().Map(ds => ds.Name, sr => sr.CategoryTranslations.Where(t=>t.Language == MapContext.Current.Parameters["lang"].ToString()).Select(t=>t.Name).FirstOrDefault());
            
        }
    }
}
