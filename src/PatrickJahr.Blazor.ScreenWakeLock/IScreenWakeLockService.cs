namespace PatrickJahr.Blazor.ScreenWakeLock;

public interface IScreenWakeLockService
{
    Task<bool> IsSupportedAsync();
    Task RequestWakeLockAsync();
    Task ReleaseWakeLockAsync();
    Action WakeLockReleased { get; set; }
}