using System.Text.Json.Serialization;

namespace DynamicBlazor.Api.Controllers;

public class Feature
{   
    [JsonPropertyName("featureName")]
    public string FeatureName { get; set; }
    [JsonPropertyName("downloadUrl")]
    public string DownloadUrl { get; set; }
    [JsonPropertyName("featureMainPage")]
    public string FeatureMainPage { get; set; }
}