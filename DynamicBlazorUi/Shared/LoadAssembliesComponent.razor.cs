namespace DynamicBlazorUi.Shared;

public partial class LoadAssembliesComponent : ComponentBase
{
    // [Inject] private IModuleAssemblyService ModuleAssemblyService { get; set; }
    // [Inject] private NavigationManager NavigationManager { get; set; }
    //
    // protected override async Task OnInitializedAsync()
    // {
    //     await base.OnInitializedAsync();
    //     using var client = new HttpClient();
    //     // var res = await client.GetAsync("https://localhost:7192/Features");
    //     // var content = await res.Content.ReadAsStringAsync();
    //     // var features = JsonSerializer.Deserialize<List<Feature>>(content);
    //     var features = new List<Module>()
    //     {
    //         new()
    //         {
    //             ModuleName = "TestFeature1",
    //             DownloadUrl = "https://blazorhostedassembly.blob.core.windows.net/testing/TestingModule.dll",
    //             ModuleRootPage = "/Counter3"
    //         }
    //     };
    //     if (await ModuleAssemblyService.GetAssemblies(features))
    //     {
    //         NavigationManager.NavigateTo(NavigationManager.Uri, forceLoad: true);
    //     }
    // }
}