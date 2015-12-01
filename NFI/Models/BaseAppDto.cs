using System;

namespace NFI.Models
{
    public class BaseAppDto
    {
        public Guid AppId { get; set; }
        public string ZipFilePath { get; set; }
        public string PdfFilePath { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreateTime { get; set; }
    }
}