namespace PatrickJahr.Blazor.PwaUpdate.Services
{
    public interface IUpdateService
    {
        Action UpdateAvailable { get; set; }

        Task InitializeServiceWorkerUpdateAsync();
        void OnUpdateAvailable();
        Task ReloadAsync();
    }
}