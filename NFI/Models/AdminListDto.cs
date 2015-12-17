using System;
using NFI.Enums;

namespace NFI.Models
{
    public class AdminListDto
    {
        public Guid AppId { get; set; }
        public string AppType { get; set; }
        public ApplicationType AppTypeId { get; set; }
        public string ApplicantName { get; set; }
        public string Email { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsArchived { get; set; }
    }
}