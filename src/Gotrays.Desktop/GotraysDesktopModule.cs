using CoreFlex.Module;
using Gotrays.Application;
using Gotrays.Rcl;

namespace Gotrays.Desktop;

[DependOns(typeof(GotraysRclModule), typeof(GotraysApplicationModule))]
public class GotraysDesktopModule : CoreFlexModule
{
}