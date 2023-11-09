using BlazorComponent;
using Gotrays.Contract.Dtos.Pays;
using Masa.Utils.Models;

namespace Gotrays.Rcl.Components;

public partial class PurchaseHistory
{
    private List<DataTableHeader<ProductListDto>> _headers = new List<DataTableHeader<ProductListDto>>
    {
        new ()
        {
            Text= "用户名",
            Value= nameof(ProductListDto.UserName)
        },
        new (){ Text= "支付平台", Value= nameof(ProductListDto.PayType)},
        new (){ Text= "产品名称", Value= nameof(ProductListDto.ProductName)},
        new (){ Text= "原价", Value= nameof(ProductListDto.OriginalPrice)},
        new (){ Text= "实际支付价格", Value= nameof(ProductListDto.ActualPayment)},
        new (){ Text= "支付状态", Value= nameof(ProductListDto.PayState)},
        new (){ Text= "支付时间", Value= nameof(ProductListDto.CreationTime)}
    };

    private PaginatedListBase<ProductListDto> Items = new();

    private int Page = 1;

    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        Items = await PayService.PayHistoryAsync(Page, 10);
    }

    private async Task ValueChanged(int page)
    {
        Page = page;
        await LoadAsync();
    }
}