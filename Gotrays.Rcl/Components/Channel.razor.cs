using Gotrays.Shread.Modules;
using Microsoft.AspNetCore.Components;

namespace Gotrays.Rcl.Components;

public partial class Channel
{
    private Guid selectId;

    private Guid SelectId
    {
        get => selectId;
        set
        {
            if (value == selectId)
            {
                return;
            }

            if (value == null)
            {
                selectId = Values.First().Id;
                ValueChanged.InvokeAsync(Values[0]);
                return;
            }

            ValueChanged.InvokeAsync(Values.First(x => x.Id == value));
            selectId = value;
        }
    }

    [Parameter] public ShreadChannelDto Value { get; set; }

    [Parameter] public EventCallback<ShreadChannelDto> ValueChanged { get; set; }

    public List<ShreadChannelDto> Values { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await Load();
    }

    private void Select(ShreadChannelDto dto)
    {
        SelectId = dto.Id;
    }

    public async Task Load()
    {
        Values = await Free.Select<ShreadChannelDto>()
            .OrderByDescending(x => x.CreatedTime)
            .ToListAsync();

        if (Values.Count == 0)
        {
            var channel = new ShreadChannelDto("默认助手", "/favicon.png");

            await ValueChanged.InvokeAsync(channel);

            await Free.Insert(new ShreadChannelDto(channel.Title, channel.Avatar)).ExecuteAffrowsAsync();

            Values.Add(channel);
        }
        else
        {
            var v = Values.First();
            SelectId = v.Id;
        }
    }
}