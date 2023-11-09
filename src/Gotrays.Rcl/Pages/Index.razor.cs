using System.Reflection;
using BlazorComponent;
using Gotrays.Contract.Dtos.Chats;
using Gotrays.Contract.Dtos.Systems;
using Gotrays.Rcl.Components;

namespace Gotrays.Rcl.Pages;

public partial class Index
{
    private List<ChannelDto> _channels = new();

    public ChannelDto UpdateChannelDto { get; set; }

    public GiteeReleaseDto GiteeReleaseDto { get; set; }
    
    private StringNumber _selectedItem;

    private ChatMessage ChatMessage;

    private bool addChannel;
    private bool updateChannel;
    private bool updateVersion;
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

        _ = UpdateAsync();

        if (_channels.Count == 0)
        {
            var dto = new ChannelDto()
            {
                Id = Guid.NewGuid(),
                Title = "智能助手",
                Role = string.Empty,
                CreatedTime = DateTime.Now,
                Sort = 0,
                Model = "gpt-3.5-turbo",
                Description = "默认创建的频道",
                MaxHistory = 0,
            };
            await ChannelService.AddAsync(dto);
            _channels.Add(dto);
        }

        SelectedItem = _channels.FirstOrDefault()?.Id.ToString();
    }

    private async Task UpdateAsync()
    {
        GiteeReleaseDto = await AppService.GetApp();
        
        
        Assembly assembly = typeof(About).Assembly;
        if (GiteeReleaseDto.tag_name != assembly.GetName().Version.ToString())
        {
            updateVersion = true;
            await InvokeAsync(StateHasChanged);
        }
    }

    private void CreateAsync()
    {
        addChannel = true;
    }

    private async Task LoadChannel()
    {
        _channels = await ChannelService.GetListAsync(_search);

        SelectedItem = _channels.FirstOrDefault()?.Id.ToString();
    }

    private async Task DeleteChannel(Guid id)
    {
        await ChannelService.DeleteAsync(id);
        await LoadChannel();
    }

    private void UpdateChannel(ChannelDto item)
    {
        UpdateChannelDto = (ChannelDto)item.Clone();
        updateChannel = true;
    }

    private async Task ClearChannel(Guid id)
    {
        await ChatMessageService.DeleteAsync(id);
        await ChatMessage.ClearAsync();
    }
}