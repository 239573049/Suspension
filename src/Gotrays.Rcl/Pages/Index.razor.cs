using BlazorComponent;
using Gotrays.Contract.Dtos.Chats;

namespace Gotrays.Rcl.Pages;

public partial class Index
{
    private List<ChannelDto> _channels = new();

    private StringNumber _selectedItem;

    private StringNumber SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            if (_selectedItem != null)
            {
                SelectChannel = _channels.FirstOrDefault(x => x.Id.ToString() == value.ToString());
            }
        }
    }

    private ChannelDto SelectChannel;
    
    private string _search = string.Empty;

    private async Task SearchChanged(string search)
    {
        _search = search;
        await LoadChannel();
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadChannel();
        
        if (_channels.Count == 0)
        {
            var dto = new ChannelDto()
            {
                Id = Guid.NewGuid(),
                Title = "智能助手",
                Role = string.Empty,
                CreatedTime = DateTime.Now,
                Sort = 0,
                Description = "默认创建的频道",
                MaxHistory = 0,
            };
            await ChannelService.AddAsync(dto);
            _channels.Add(dto);
        }

        SelectedItem = _channels.FirstOrDefault()?.Id.ToString();
    }

    private void CreateAsync()
    {
        
    }
    
    private async Task LoadChannel()
    {
        _channels = await ChannelService.GetListAsync(_search);
        
        SelectedItem = _channels.FirstOrDefault()?.Id.ToString();
    }
}