using System.Text.Json.Serialization;

namespace Maui.Blazor.CompositeUi;

public class Module
{   
    [JsonPropertyName("featureName")]
    public required string ModuleName { get; set; }
    [JsonPropertyName("downloadUrl")]
    public required string DownloadUrl { get; set; }
    [JsonPropertyName("featureMainPage")]
    public required string ModuleRootPage { get; set; }
}