using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace NFI.Models
{
    public class IncentiveSchemeDto : BaseAppDto
    {

        // 1. Contact information, Main producer
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Name of production company")]
        public string NameProductionCompany { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Legal registration number")]
        public string LegalRegistrationNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postal address")]
        public string PostalAddress { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Postcode")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("City/Town")]
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Name of producer")]
        public string NameOfProducer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Cell number")]
        public string CellNumber{ get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Production company webpage")]
        public string ProductionCompanyWebpage { get; set; }

        [JsonIgnore]
        [DisplayName("Certificate of origin - production company")]
        public HttpPostedFileBase CertificateOfOrigin { get; set; }
        public string CertificateOfOriginPath { get; set; }

        [JsonIgnore]
        [DisplayName("Producer's CV")]
        public HttpPostedFileBase ProducersCv { get; set; }
        public string ProducersCvPath { get; set; }

        [JsonIgnore]
        [DisplayName("Track record of the production company")]
        public HttpPostedFileBase TrackRecordProductionCompany { get; set; }
        public string TrackRecordProductionCompanyPath { get; set; }

        // 2. Contact information applicant: / if different from main producer


    }
}