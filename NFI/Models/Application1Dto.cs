using System;
using System.ComponentModel;

namespace NFI.Models
{
    public class Application1Dto
    {
        public string AppId { get; set; }
        [NotVisible]
        public string Name { get; set; }
        [NotVisible]
        [EmailLink]
        public string Email { get; set; }
        [NotVisible]
        public string Sex { get; set; }
        public string Company { get; set; }
        public bool IsArchived { get; set; }

        [DisplayName("Attachment Link")]
        public string ZipFilePath { get; set; }
        public DateTime CreateDate { get; set; }
    }
}