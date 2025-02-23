using Microsoft.AspNetCore.Components;

namespace BoardBard.Web.Client.Layout;

public partial class MainLayout 
{
    #region Properties

    private bool SideBarExpanded { get; set; }

    #endregion

    #region UI

    private async Task ToggleSideBar()
    {
        SideBarExpanded = !SideBarExpanded;
        await InvokeAsync(StateHasChanged);
    }

    #endregion
}