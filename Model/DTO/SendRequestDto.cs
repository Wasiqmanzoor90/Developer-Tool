namespace MyApiProject.Model
{
    public class SendRequestDto
    {
        public string Method { get; set; } = "GET"; // GET, POST, PUT, DELETE, PATCH
        public string Url { get; set; } = string.Empty;
        public Dictionary<string, string>? Headers { get; set; }
        public Dictionary<string, string>? QueryParams { get; set; }
        public string? Body { get; set; }
        public string BodyType { get; set; } = "none"; // json, xml, form, raw, none
        public string AuthType { get; set; } = "none"; // none, bearer, basic, apikey
        public string? AuthValue { get; set; }
    }
}