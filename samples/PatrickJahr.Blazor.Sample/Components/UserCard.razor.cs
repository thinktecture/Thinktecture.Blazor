using Microsoft.AspNetCore.Components;
using PatrickJahr.Blazor.Sample.Models;

namespace PatrickJahr.Blazor.Sample.Components;

public partial class UserCard
{
    [Parameter, EditorRequired] public User User { get; set; }
    [Parameter] public bool Active { get; set; }
    [Parameter] public string CssClass { get; set; } = string.Empty;
}