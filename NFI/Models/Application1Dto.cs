using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NFI.Enums;

namespace NFI.Models
{
    public class Application1Dto
    {
        public string AppId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string Company { get; set; }
        public bool IsArchived { get; set; }
        public string ZipFilePath { get; set; }
        public DateTime CreateDate { get; set; }
    }
}