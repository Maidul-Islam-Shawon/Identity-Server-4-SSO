using Microsoft.AspNetCore.Components;


namespace ClientApp.Shared
{
    public partial class RedirectToLogin
    {
        [Inject]public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            NavigationManager.NavigateTo(
                $"/login?redirectUri={Uri.EscapeDataString(NavigationManager.Uri)}", true);
        }


    }
}
