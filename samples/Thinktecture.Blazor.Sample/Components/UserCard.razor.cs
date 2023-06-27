using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.Sample.Models;

namespace Thinktecture.Blazor.Sample.Components;

public partial class UserCard
{
    [Parameter, EditorRequired] public User User { get; set; }
    [Parameter] public bool Active { get; set; }
    [Parameter] public string CssClass { get; set; } = string.Empty;
}