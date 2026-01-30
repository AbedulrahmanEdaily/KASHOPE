using KASHOPE.BLL.Services.Interfaces;
using KASHOPE.DAL.DTO.Request.ReviewRequest;
using KASHOPE.DAL.DTO.Response;
using KASHOPE.DAL.Models;
using KASHOPE.DAL.Repository.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository,IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<BaseResponse> AddReviewAsync(string userId, int productId, ReviewRequest request)
        {
            if (!await _orderRepository.HasUserDeliveredOrderForProductAsync(userId, productId))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You can only review products you have purchased."
                };
            }

            if (await _reviewRepository.HasUserReviewProductAsync(userId, productId))
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "You have already reviewed this product"
                };
            }
            var review = request.Adapt<Review>();
            review.UserId = userId;
            review.ProductId = productId;
            await _reviewRepository.CreateReviewAsync(review);
            return new BaseResponse
            {
                Success = true,
                Message = "Review Add"
            };
        }

      
    }
}
