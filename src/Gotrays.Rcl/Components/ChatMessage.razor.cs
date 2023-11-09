using System.Diagnostics;
using BlazorComponent;
using Gotrays.Contract.Dtos.Chats;
using Gotrays.Contract.Dtos.Users;
using Gotrays.Shared;
using Masa.Blazor.Presets;
using Microsoft.AspNetCore.Components;

namespace Gotrays.Rcl.Components;

public partial class ChatMessage
{
    private ChannelDto _selectChannel;

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
            page = 1;
            _selectChannel = value;
            _ = LoadMessage(_selectChannel.Id);
        }
    }

    private List<ChatMessageDto> _chatMessages = new();

    private int page = 1;

    private int pageSize = 5;

    private string _value;

    private async Task LoadMessage(Guid channelId)
    {
        var result = await ChatMessageService.GetListAsync(channelId, page++, pageSize);
        foreach (var messageDto in result)
        {
            InitEvent(messageDto);
        }

        _chatMessages.AddRange(result);

        // 如果数据为空则不进行分页递增
        if (result.Count == 0)
        {
            page--;
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

        await ChatService.ChatAsync(input, message =>
        {
            chatAi.Message += message + Environment.NewLine;
            InvokeAsync(StateHasChanged);
        });

        await ChatMessageService.CreateAsync(chatAi);
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
        
        KeyLoadEventBus.Subscription(Constant.LoadEventBus.Notifications,(async o =>
        {
            if (o is string str)
            {
                await PopupService.EnqueueSnackbarAsync(new SnackbarOptions(str, AlertTypes.Info));
            }
        }));

    }

    public async Task ClearAsync()
    {
        page = 1;
        _chatMessages.Clear();
        await LoadMessage(_selectChannel.Id);
    }
}