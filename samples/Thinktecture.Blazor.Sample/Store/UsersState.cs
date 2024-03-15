using System.Net.Http.Json;
using Fluxor;
using Thinktecture.Blazor.Sample.Models;

namespace Thinktecture.Blazor.Sample.Store
{
    [FeatureState]
    public record ImageState
    {
        public bool Loading { get; set; } = false;
        public bool ShowDialog { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
        public List<User> Users { get; set; } = new();
        public User? SelectedUser { get; set; }
    }

    public record LoadUsersAction();
    public record LoadUsersActionSuccess(List<User> Users);
    public record LoadUsersActionFailed(string ErrorMessage);
    public record SelectUserAction(User? User);
    public record ToggleViewModeAction();

    public partial class UserReducers
    {
        [ReducerMethod]
        public static ImageState LoadImages(ImageState state, LoadUsersAction _)
            => state with { Loading = true };

        [ReducerMethod]
        public static ImageState LoadImages(ImageState state, LoadUsersActionSuccess action) =>
            state with { Loading = false, Users = action.Users };

        [ReducerMethod]
        public static ImageState LoadImages(ImageState state, LoadUsersActionFailed action)
            => state with { Loading = false, ErrorMessage = action.ErrorMessage };

        [ReducerMethod]
        public static ImageState SelectImage(ImageState state, SelectUserAction action)
            => state with { Loading = false, SelectedUser = action.User };
        
        [ReducerMethod]
        public static ImageState SelectImage(ImageState state, ToggleViewModeAction action)
            => state with { Loading = false, ShowDialog = !state.ShowDialog };
    }

    public class LoadUserEffect : Effect<LoadUsersAction>
    {
        private readonly HttpClient _httpClient;

        public LoadUserEffect(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public override async Task HandleAsync(LoadUsersAction action, IDispatcher dispatcher)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<IEnumerable<User>>("https://random-data-api.com/api/v2/users?size=50");
                dispatcher.Dispatch(new LoadUsersActionSuccess(result?.ToList() ?? new List<User>()));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new LoadUsersActionFailed(ex.Message));
            }
        }
    }
}
