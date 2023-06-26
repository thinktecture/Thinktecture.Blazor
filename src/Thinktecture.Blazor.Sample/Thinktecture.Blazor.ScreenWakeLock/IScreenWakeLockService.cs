namespace Thinktecture.Blazor.ScreenWakeLock;

public interface IScreenWakeLockService
{
    Task<bool> IsSupportedAsync();
    Task EnterFullScreenAsync();
    Task ExitFullScreenAsync();    
    Task RequestWakeLockAsync();
    Task ReleaseWakeLockAsync();
}