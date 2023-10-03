using Maui.Blazor.CompositeUi;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBlazor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ModulesController : ControllerBase
{
    private readonly ILogger<ModulesController> _logger;

    public ModulesController(ILogger<ModulesController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetFeatures")]
    public IEnumerable<Module> Get()
    {
        var features = new List<Module>
        {
            new()
            {
                ModuleName = "TestFeature1",
                DownloadUrl = "https://blazorhostedassembly.blob.core.windows.net/testing/TestingModule.dll",
                ModuleRootPage = "/Counter3"
            }
        };
        return features;
    }
}