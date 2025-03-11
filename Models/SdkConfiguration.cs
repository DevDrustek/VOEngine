namespace VBEngine.Models
{
    public class SdkConfiguration
    {
        public string BaseUri { get; set; } = "https://coreengine.com/api/";
        public string ApiKey { get; set; } = string.Empty;
        public string SdkVersion { get; set; } = "1.0.0";
        public Dictionary<string, string> Endpoints { get; set; } = new()
    {
        { "SendRequest", "requester/{apiKey}" },
        { "ReceiveResponse", "provider/{apiKey}" }
    };
    }

}
