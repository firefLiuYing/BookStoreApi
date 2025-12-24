using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class CustomerReviseService(BookStoreContext  context)
{
    public async Task<ApiResponse<NewUserInfo>> Revise(NewUserInfo newUserInfo)
    {
        var response =new ApiResponse<NewUserInfo>();
        if ( newUserInfo?.Password == null || newUserInfo?.Name == null || newUserInfo?.Address == null)
        {
            response.Success = false;
            response.Message = "某一栏为空";
            response.Data=null;
            return response;
        }

        var customer = await context.Customer.FirstOrDefaultAsync(c => c.Id == newUserInfo.Id);
        if (customer == null)
        {
            response.Success = true;
            response.Message = "该用户不存在";
            response.Data = null;
            return response;
        }
        else
        {
            response.Success = true;
            response.Message = "用户名已存在,可以修改";
            response.Data = null;
            
            customer.Password = newUserInfo.Password;
            customer.Name = newUserInfo.Name;
            customer.Address = newUserInfo.Address;
            
            await context.SaveChangesAsync();
            return response;
        }
        
    }
    
    public class NewUserInfo 
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}


