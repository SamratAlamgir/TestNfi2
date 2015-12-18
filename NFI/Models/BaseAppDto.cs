using System;
using System.ComponentModel;

namespace NFI.Models
{
    public class BaseAppDto
    {
        public Guid AppId { get; set; }
        public string ZipFilePath { get; set; }
        public string PdfFilePath { get; set; }
        public bool IsArchived { get; set; }
        [DisplayName("Create Time")]
        public DateTime CreateTime { get; set; }
    }
}