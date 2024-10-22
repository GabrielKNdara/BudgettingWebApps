namespace BudgettingWebApps.Models
{
    public class AttachmentDto
    {
        public byte[]ByteArray { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
