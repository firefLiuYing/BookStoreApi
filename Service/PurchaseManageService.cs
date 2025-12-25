using BookStoreApi.Models;
using BookStoreApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Service;

public class PurchaseManageService(BookStoreContext context)
{
    public async Task<ApiResponse<VoidResponse>> Update(PurchaseUpdateRequest request)
    {
        var response = new ApiResponse<VoidResponse>();
        var purchase=await context.Purchase.FirstOrDefaultAsync(p=>p.Id == request.Id);
        if (purchase == null)
        {
            response.Success = false;
            response.Message = "未找到指定Id采购单";
            return response;
        }
        purchase.Arrived = request.Arrived;
        await context.SaveChangesAsync();
        response.Success = true;
        response.Message = "更新成功";
        return response;
    }

    public async Task<ApiResponse<PurchaseQueryResponse>> Query()
    {
        var response = new ApiResponse<PurchaseQueryResponse>();
        var purchase = await context.Purchase.ToListAsync();
        var details= purchase.Select(p =>
        {
            var detail = new PurchaseDetail();
            detail.Id = p.Id;
            detail.Arrived = p.Arrived;
            detail.CreatedTime = p.CreatedTime;
            detail.Details = context.PurchaseBookSupplier.Where(pbs=>pbs.PurchaseId==p.Id).ToList();
            return detail;
        }).ToList();
        response.Success = true;
        response.Message = "查询完成";
        response.Data = new PurchaseQueryResponse() { Datas = details };
        return response;
    }
    public class PurchaseUpdateRequest
    {
        public int Id { get; set; }
        public bool Arrived { get; set; }
    }

    public class PurchaseQueryResponse
    {
        public List<PurchaseDetail> Datas { get; set; }
    }

    public class PurchaseDetail
    {
        public int Id { get; set; }
        public bool Arrived { get; set; }
        public DateTime CreatedTime { get; set; }
        public List<PurchaseBookSupplier> Details { get; set; }
    }
}