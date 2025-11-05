namespace MyApiProject.Model.DTO
{
    public class HttpResponseDto
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; set; } = new();
        public string Body { get; set; } = string.Empty;
        public long ResponseTimeMs { get; set; }
        public long ContentLength { get; set; }
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}