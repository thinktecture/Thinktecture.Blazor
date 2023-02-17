namespace Thinktecture.Blazor.PwaUpdate.Services
{
    public interface IUpdateService
    {
        Action UpdateAvailable { get; set; }

        Task InitializeServiceWorkerUpdateAsync();
        void OnUpdateAvailable();
        Task ReloadAsync();
    }
}