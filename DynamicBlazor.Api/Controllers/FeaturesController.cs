using Microsoft.AspNetCore.Mvc;

namespace DynamicBlazor.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FeaturesController : ControllerBase
{
    private readonly ILogger<FeaturesController> _logger;

    public FeaturesController(ILogger<FeaturesController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetFeatures")]
    public IEnumerable<Feature> Get()
    {
        var features = new List<Feature>()
        {
            new()
            {
                FeatureName = "TestFeature1",
                DownloadUrl = "https://blazorhostedassembly.blob.core.windows.net/testing/TestingModule.dll",
                FeatureMainPage = "/Counter3"
            }
        };
        return features;
    }
}