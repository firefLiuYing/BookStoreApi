using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class AuthService(BookStoreContext  context)
{
    public async Task<ApiResponse<UserInfo>> Register(RegisterRequest request)
    {
        var response=new ApiResponse<UserInfo>();
        if (request?.Nickname == null || request?.Password == null)
        {
            response.Success = false;
            response.Message = "用户名或密码为空";
            response.Data=null;
            return response;
        }

        var customer = await context.Customer.FirstOrDefaultAsync(c => c.NickName == request.Nickname);
        if (customer != null)
        {
            response.Success = false;
            response.Message = "用户名已存在";
            response.Data = null;
            return response;
        }

        customer = new()
        {
            NickName = request.Nickname,
            Password = request.Password,
            Name = request.Nickname,
        };
        context.Customer.Add(customer);
        await context.SaveChangesAsync();
        response.Success = true;
        response.Message = "注册成功";
        response.Data = new ()
        {
            Id = customer.Id,
            Name = customer.Name,
        };
        return response;
    }

    public async Task<ApiResponse<UserInfo>> Login(LoginRequest request)
    {
        var response = new ApiResponse<UserInfo>();
        if (request?.Nickname == null || request?.Password == null)
        {
            response.Success = false;
            response.Message = "用户名或密码为空";
            response.Data=null;
            return response;
        }
        
        var customer = await context.Customer.FirstOrDefaultAsync(c => c.NickName == request.Nickname);
        if (customer == null||customer.Password!=request.Password)
        {
            response.Success = false;
            response.Message = "账号或密码错误";
            response.Data = null;
            return response;
        }
        
        response.Success = true;
        response.Message = "登录成功";
        response.Data = new()
        {
            Id = customer.Id,
            Name = customer.Name,
        };
        return response;
    }
    
    public class LoginRequest
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
