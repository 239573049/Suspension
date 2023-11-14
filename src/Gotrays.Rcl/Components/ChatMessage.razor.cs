using BlazorComponent;
using Gotrays.Contract.Dtos.Chats;
using Gotrays.Contract.Dtos.Users;
using Gotrays.Shared;
using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Gotrays.Rcl.Components;

public partial class ChatMessage : IDisposable
{
    private ChannelDto _selectChannel;

    private string Id;

    /// <summary>
    /// 当前token数量
    /// </summary>
    private int token;

    [CascadingParameter(Name = nameof(GetUserDto))]
    public GetUserDto userDto { get; set; }

    [CascadingParameter(Name = nameof(SelectChannel))]
    public ChannelDto SelectChannel
    {
        get => _selectChannel;
        set
        {
            if (value == null)
            {
                _selectChannel = null;
                return;
            }

            if (_selectChannel == value)
            {
                return;
            }

            _chatMessages.Clear();
            _page = 1;
            _selectChannel = value;
            _ = LoadMessage(_selectChannel.Id);
        }
    }

    private List<ChatMessageDto> _chatMessages = new();

    private DotNetObjectReference<ChatMessage> _dotNet;

    private int _page = 1;

    private int _pageSize = 6;

    private string _value;

    private async Task LoadMessage(Guid channelId)
    {
        var result = await ChatMessageService.GetListAsync(channelId, _page++, _pageSize);
        foreach (var messageDto in result)
        {
            InitEvent(messageDto);
        }

        _chatMessages.InsertRange(0, result);

        // 如果数据为空则不进行分页递增
        if (result.Count == 0)
        {
            _page--;
        }
    }

    private void InitEvent(ChatMessageDto messageDto)
    {
        messageDto.OnCopy += async () => { await Copy(messageDto); };

        messageDto.OnDelete += async () => { await Delete(messageDto); };

        messageDto.OnUpdate += async () => { await Update(messageDto); };
    }

    private async Task Update(ChatMessageDto messageDto)
    {
    }

    private async Task Delete(ChatMessageDto messageDto)
    {
        await ChatMessageService.DeleteAsync(messageDto.Id);

        _chatMessages.Remove(messageDto);
        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("删除成功！", AlertTypes.Success));
    }

    private async Task Copy(ChatMessageDto messageDto)
    {
        await GotraysInterop.CopyText(messageDto.Message);
        await PopupService.EnqueueSnackbarAsync(new SnackbarOptions("复制成功！", AlertTypes.Success));
    }

    private async Task OnMousedown(KeyboardEventArgs e)
    {
        if (e is { ShiftKey: false, Key: "Enter" })
        {
            await OnSendAsync();
        }
    }

    private async Task OnSendAsync()
    {
        if (_value.IsNullOrWhiteSpace())
        {
            return;
        }

        var curren = new ChatMessageDto()
        {
            Id = Guid.NewGuid(),
            Message = _value,
            Curren = true,
            CreatedTime = DateTime.Now,
            ChannelId = SelectChannel.Id,
            Type = ChatMessageType.Text,
        };

        InitEvent(curren);

        _chatMessages.Add(curren);

        await ChatMessageService.CreateAsync(curren);

        _value = string.Empty;

        var chatAi = new ChatMessageDto()
        {
            Id = Guid.NewGuid(),
            Message = string.Empty,
            CreatedTime = DateTime.Now,
            ChannelId = curren.ChannelId,
            Type = curren.Type,
            Curren = false,
        };

        InitEvent(chatAi);

        _chatMessages.Add(chatAi);

        var input = new ChatCompletionDto()
        {
            model = SelectChannel.Model,
            temperature = 0,
            top_p = 0,
        };

        // 如果角色设定不为空则添加到消息
        if (!SelectChannel.Role.IsNullOrWhiteSpace())
        {
            input.messages.Add(new ChatCompletionRequestMessage()
            {
                name = "system",
                content = SelectChannel.Role,
                role = "system"
            });
        }

        if (SelectChannel.MaxHistory > 0)
        {
            input.messages.AddRange(_chatMessages.Skip(Math.Max(0, _chatMessages.Count - SelectChannel.MaxHistory))
                .Select(x => new ChatCompletionRequestMessage
                {
                    role = x.Curren ? "user" : "assistant",
                    name = x.Curren ? "user" : "assistant",
                    content = x.Message
                }));
        }

        // 将自己发送内容添加到消息
        input.messages.Add(new ChatCompletionRequestMessage()
        {
            name = "user",
            content = curren.Message,
            role = "user"
        });

        await GotraysInterop.ScrollBottom(Id);

        await ChatService.ChatAsync(input, async message =>
        {
            chatAi.Message += message + Environment.NewLine;
            _ = InvokeAsync(StateHasChanged);
            await GotraysInterop.ScrollBottom(Id);
        });

        await ChatMessageService.CreateAsync(chatAi);

        await GetToken();
    }

    private async Task GetToken()
    {
        var result = await UserService.GetDayDosageAsync();

        token = result.free + result.money;
        _ = InvokeAsync(StateHasChanged);
    }

    private string GetChatMessageClass(ChatMessageDto chatMessage)
    {
        if (chatMessage.Curren)
        {
            return "self";
        }

        return "";
    }

    protected override void OnInitialized()
    {
        _dotNet = DotNetObjectReference.Create(this);
        Id = Guid.NewGuid().ToString("N");
        KeyLoadEventBus.Subscription(Constant.LoadEventBus.Notifications, (async o =>
        {
            if (o is string str)
            {
                await PopupService.EnqueueSnackbarAsync(new SnackbarOptions(str, AlertTypes.Info));
            }
        }));
    }

    [JSInvokable("OnScroll")]
    public async Task OnScroll(int scrollTop)
    {
        if (scrollTop == 0)
        {
            await LoadMessage(_selectChannel.Id);
            _ = InvokeAsync(StateHasChanged);
        }
    }

    public async Task ClearAsync()
    {
        _page = 1;
        _chatMessages.Clear();
        await LoadMessage(_selectChannel.Id);
    }


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(500);

                await GotraysInterop.OnScroll(Id, _dotNet, nameof(OnScroll));

                await GotraysInterop.ScrollBottom(Id);
            });
            await GetToken();
        }
    }

    public void Dispose()
    {
        _dotNet.Dispose();
        KeyLoadEventBus.Remove(Constant.LoadEventBus.Notifications);
    }
}