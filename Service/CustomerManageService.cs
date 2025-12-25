using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class CustomerManageService(BookStoreContext context)
{
    public async Task<ApiResponse<List<CustomerInformation>>> Query()
    {
        var response=new ApiResponse<List<CustomerInformation>>();
        try
        {
            var infos = await context.Customer.ToListAsync();
            response.Success = true;
            response.Message = $"查询成功，查询到{infos.Count}个结果";
            response.Data = infos.Select(c => new CustomerInformation(c)).ToList();
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }

    public async Task<ApiResponse<VoidResponse>> Update(CustomerInformation info)
    {
        var response = new ApiResponse<VoidResponse>();
        var customer=await context.Customer.FirstOrDefaultAsync(c => c.Id == info.Id);
        if (customer == null)
        {
            response.Success = false;
            response.Message = "未查找到指定Id用户";
            return response;
        }
        info.ToEntity(customer);
        context.Customer.Update(customer);
        await context.SaveChangesAsync();
        response.Success = true;
        response.Message = "更新成功";
        return response;
    }
    public class CustomerInformation
    {
        public int Id { get; set; } 
        public string NickName { get; set; } 
        public string Password { get; set; } 
        public string Name { get; set; } 
        public string? Address { get; set; } 
        public decimal Balance { get; set; } 
        public int Credit { get; set; } 
        public string? Email { get; set; } 
        public string? Phone { get; set; } 

        public Customer ToEntity(Customer? customer=null)
        {
            customer ??= new Customer();
            customer.Id = Id;
            customer.NickName = NickName;
            customer.Password = Password;
            customer.Name = Name;
            customer.Address = Address;
            customer.Balance = Balance;
            customer.Credit = Credit;
            customer.Email = Email;
            customer.Phone = Phone;
            return customer;
        }

        public CustomerInformation()
        {
            NickName = "";
            Password = "";
            Name = "";
        }
        public CustomerInformation(Customer entity) 
        {
            Id = entity.Id;
            NickName = entity.NickName;
            Password = entity.Password;
            Name = entity.Name;
            Address = entity.Address;
            Balance = entity.Balance;
            Credit = entity.Credit;
            Email = entity.Email;
            Phone = entity.Phone;
        }
    }
}