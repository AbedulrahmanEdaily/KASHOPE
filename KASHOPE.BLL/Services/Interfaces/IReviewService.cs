using KASHOPE.DAL.DTO.Request.ReviewRequest;
using KASHOPE.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<BaseResponse> AddReviewAsync(string userId, int productId, ReviewRequest request);
    }
}
