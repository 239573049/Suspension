﻿using Gotrays.Contract.Modules;

namespace Gotrays.Shread.Pages;

public partial class Product
{
    private List<ChatProductDto> _products = new();

    private bool ShowCode;

    protected override async Task OnInitializedAsync()
    {
        _products = await ChatProductsService.GetListAsync();
    }

    private async Task Acquisition(ChatProductDto dto)
    {
        var result = await PaysService.StartPayAsync(new StartPayInput(dto.id));

        ShowCode = true;

        _ = Task.Run(async () =>
        {
            await Task.Delay(200);
            await MainInterop.QrCode("qr-code", result.qr);
        });
    }
    
    
}