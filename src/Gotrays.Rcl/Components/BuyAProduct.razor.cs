using Gotrays.Contract.Dtos.Products;
using GotraysService.Contracts.Dtos.Products;

namespace Gotrays.Rcl.Components;

public partial class BuyAProduct
{
    private bool payment;

    private List<GetProductListDto> Values = new();

    protected override async Task OnInitializedAsync()
    {
        Values = await ChatProductService.GetListAsync();
    }

    private async Task BuyAsync(GetProductListDto item)
    {
        payment = true;

        var result = await PayService.StartPayAsync(new StartPayPayload()
        {
            ProductId = item.Id,
        });

        await GotraysInterop.QrCode("qr-code", result.qr);
    }
}