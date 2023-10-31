using System.Text.Json;
using Gotrays.Contract.Modules;
using Gotrays.Rcl.Components;
using Gotrays.Shread.Modules;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Gotrays.Rcl.Pages;

public partial class Home : IAsyncDisposable
{
    private string _value = string.Empty;

    private static readonly string TitleId = Guid.NewGuid().ToString("N");

    private static readonly string ContentId = Guid.NewGuid().ToString("N");

    private Channel _channel;

    private readonly List<MessageDto> _message = new();

    private ShreadChannelDto value;

    private DotNetObjectReference<Home> _reference;

    private bool setting;

    private bool addChannel;

    private ShreadChannelDto _channelDto = new();

    [CascadingParameter(Name = nameof(UserInfoDto))]
    public UserInfoDto? _userInfoDto { get; set; }

    private ShreadChannelDto Value
    {
        get => value;
        set
        {
            if (value == this.value || value == null)
            {
                return;
            }

            _message.Clear();
            page = 1;
            _ = Load(value.Id);

            this.value = value;
        }
    }

    private int page = 1;

    private static readonly MarkdownItAnchorOptions s_anchorOptions = new()
    {
        Level = 1,
        PermalinkClass = "",
        PermalinkSymbol = ""
    };

    /// <summary>
    /// 得到Style
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    private string GetMessageStyle(bool v)
    {
        // 根据状态返回style
        if (v)
        {
            return "float: left;";
        }

        return "float: right;";
    }

    /// <summary>
    /// 发送内容。
    /// </summary>
    private async Task SendMessage()
    {
        try
        {
            var user = new MessageDto(Guid.NewGuid(), false, _value, Value.Id);
            _message.Add(user);

            await Free.Insert(user).ExecuteAffrowsAsync();

            List<object> chatMessage = new List<object>();

            chatMessage.AddRange(_message.Skip(Math.Max(0, _message.Count - _channelDto.History)).Select(x => new
            {
                role = x.Chat ? "assistant" : "user",
                content = x.Message
            }));

            chatMessage.Add(new
            {
                role = "user",
                content = _value
            });

            _value = string.Empty;


            var message = new MessageDto(Guid.NewGuid(), true, string.Empty, Value.Id);

            _message.Add(message);

            // 首次发送需要将滚动条置底
            await DocumentInterop.ScrollBottom(ContentId);

            var response = await ChatService.HttpRequestRaw("Chats/SendMessage", new
            {
                max_tokens = 1000,
                temperature = 0,
                stream = true,
                messages = chatMessage
            });
            await using var json = await response.Content.ReadAsStreamAsync();
            var strings = JsonSerializer.DeserializeAsyncEnumerable<string>(json);
            try
            {
                await foreach (var str in strings)
                {
                    message.Message += str;
                    _ = InvokeAsync(StateHasChanged);
                    await DocumentInterop.ScrollToBottom(ContentId);
                }
            }
            catch
            {
            }

            await Free.Insert(message).ExecuteAffrowsAsync();
        }
        catch (Exception e)
        {
            await PopupService.EnqueueSnackbarAsync(e);
        }
    }

    [JSInvokable]
    public async Task OnScroll(double scrollTop)
    {
        if (scrollTop == 0)
        {
            await Load(Value.Id);
        }
    }

    protected override void OnInitialized()
    {
        _reference = DotNetObjectReference.Create(this);
    }

    private async Task Load(Guid id)
    {
        var value = (await Free.Select<MessageDto>()
                .Where(x => x.ChannelId == id)
                .OrderByDescending(x => x.CreatedTime)
                .Page(page, 10)
                .ToListAsync())
            .OrderBy(x => x.CreatedTime)
            .ToList();

        page++;

        if (value.Count == 0)
        {
            page--;
            return;
        }

        _message.InsertRange(0, value);
        _ = InvokeAsync(StateHasChanged);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _ = Task.Factory.StartNew((Action)(async () =>
            {
                await Task.Delay(200);
                await MainInterop.Init(TitleId);
                await DocumentInterop.OnScroll(ContentId, _reference, nameof(OnScroll));
                await DocumentInterop.ScrollBottom(ContentId);
            }));
        }
    }

    public async ValueTask DisposeAsync()
    {
        await CastAndDispose(_reference);

        return;

        static async ValueTask CastAndDispose(IDisposable resource)
        {
            if (resource is IAsyncDisposable resourceAsyncDisposable)
                await resourceAsyncDisposable.DisposeAsync();
            else
                resource.Dispose();
        }
    }

    private async Task UpdateOnSave()
    {
        await Free.Update<ShreadChannelDto>(value).ExecuteAffrowsAsync();
        setting = false;
    }

    private async Task AddOnSave()
    {
        await Free.Insert(_channelDto).ExecuteAffrowsAsync();
        await _channel.Load();
    }

    private async Task DeleteChannel()
    {
        await Free.Delete<ShreadChannelDto>()
            .Where(x => x.Id == value.Id)
            .ExecuteAffrowsAsync();

        await Free.Delete<MessageDto>()
            .Where(x => x.ChannelId == value.Id)
            .ExecuteAffrowsAsync();

        setting = false;

        await _channel.Load();
    }
}