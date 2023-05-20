using CuMicroservice.Services.Discount.Models;
using CuMicroservice.Shared.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CuMicroservice.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;
        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var saveStatus = await _connection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });
            return saveStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 404);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where code=@Code and userid=@UserId", new { Code = code, UserId = userId })).FirstOrDefault();
            return discount != null ? Response<Models.Discount>.Success(discount, 200) : Response<Models.Discount>.Fail("discount not found", 404);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _connection.QueryAsync<Models.Discount>("select * from discount where Id = @Id", new { Id = id })).FirstOrDefault();
            return discount != null ? Response<Models.Discount>.Success(discount, 200) : Response<Models.Discount>.Fail("discount not found", 404);
        }

        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _connection.ExecuteAsync("insert into discount (userid,rate,code) values (@UserId,@Rate,@Code)", discount);
            return saveStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var saveStatus = await _connection.ExecuteAsync("update discount set userid = @UserId, code=@Code, rate=@Rate where id=@Id", discount);
            return saveStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 404);
        }
    }
}
