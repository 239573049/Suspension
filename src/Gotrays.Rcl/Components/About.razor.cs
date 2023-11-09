using System.Reflection;

namespace Gotrays.Rcl.Components;

public partial class About
{
    public string Versions { get; set; }

    protected override void OnInitialized()
    {
        Assembly assembly = typeof(About).Assembly;
        Versions = assembly.GetName().Version.ToString();
    }
}