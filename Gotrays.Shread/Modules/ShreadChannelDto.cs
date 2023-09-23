using Gotrays.Contract.Modules;
using Microsoft.AspNetCore.Components.Web;

namespace Gotrays.Shread.Modules;

public class ShreadChannelDto : ChannelDto
{

    public ShreadChannelDto()
    {
    }

    public ShreadChannelDto(string title, string avatar)
    {
        Title = title;
        Avatar = avatar;
        MaxTokens = 500;
        CreatedTime = DateTime.Now;
    }
}