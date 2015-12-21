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
        [Required("en")]
        [DisplayName("Name of production company")]
        public string NameProductionCompany { get; set; }

        [Required("en")]
        [DisplayName("Legal registration number")]
        public string LegalRegistrationNumber { get; set; }

        [Required("en")]
        [DisplayName("Postal address")]
        public string PostalAddress { get; set; }

        [Required("en")]
        [DisplayName("Postcode")]
        public string Postcode { get; set; }

        [Required("en")]
        [DisplayName("City/Town")]
        public string CityTown { get; set; }

        [Required("en")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required("en")]
        [DisplayName("Name of producer")]
        public string NameProducer { get; set; }

        [Required("en")]
        [DisplayName("Gender, Main producer")]
        public string GenderMainProducer { get; set; }

        [Required("en")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required("en")]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required("en")]
        [DisplayName("Cell number")]
        public string CellNumber { get; set; }

        [Required("en")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Production company webpage")]
        public string ProductionCompanyWebpage { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Certificate of Origin/Registration")]
        public HttpPostedFileBase CertificateOriginProductionCompany { get; set; }
        [DisplayName("Certificate of Origin/Registration")]
        public string CertificateOriginProductionCompanyPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Producer's CV")]
        public HttpPostedFileBase ProducersCv { get; set; }
        [DisplayName("Producer's CV")]
        public string ProducersCvPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Track record of the production company")]
        public HttpPostedFileBase TrackRecordTheProductionCompany { get; set; }
        [DisplayName("Track record of the production company")]
        public string TrackRecordTheProductionCompanyPath { get; set; }

        // 2. Contact information applicant: / if different from main producer
        [DisplayName("Name of applicant")]
        public string NameApplicant { get; set; }
        [DisplayName("Title")]
        public string TitleContactInfo { get; set; }
        [DisplayName("Phone number")]
        public string PhoneNumberContactInfo { get; set; }
        [DisplayName("Cell number")]
        public string CellNumberContactInfo { get; set; }

        [Required("en")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("E-mail: (This address will receive a confirmation mail)")]
        public string EmailContactInfo { get; set; }

        [DisplayName("Name of company")]
        public string NameCompanyContactInfo { get; set; }
        [DisplayName("Legal registration number")]
        public string LegalRegistrationNumberContactInfo { get; set; }
        [DisplayName("Postal address")]
        public string PostalAddressContactInfo { get; set; }
        [DisplayName("Postcode")]
        public string PostcodeContactInfo { get; set; }
        [DisplayName("City/Town")]
        public string CityTownContactInfo { get; set; }
        [DisplayName("Country")]
        public string CountryContactInfo { get; set; }
        [DisplayName("Production company webpage")]
        public string ProductionCompanyWebpageContactInfo { get; set; }

        [JsonIgnore, FileSize("en")]
        [DisplayName("Declaration from main producer allowing the applicant to apply to the incentive scheme on behalf of the main producer.")]
        public HttpPostedFileBase DeclarationFromMainProducer { get; set; }
        [DisplayName("Declaration from main producer allowing the applicant to apply to the incentive scheme on behalf of the main producer.")]
        public string DeclarationFromMainProducerPath { get; set; }

        // 3. Project information
        [Required("en")]
        [DisplayName("Project title")]
        public string ProjectTitle { get; set; }

        [Required("en")]
        [DisplayName("Is the project an original work?")]
        public string IsTheProjectOriginalWork { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Documentation of ownership of rights (held by main producer)")]
        public HttpPostedFileBase DocumentationOwnershipOfRights { get; set; }
        [DisplayName("Documentation of ownership of rights (held by main producer)")]
        public string DocumentationOwnershipOfRightsPath { get; set; }

        [Required("en")]
        [DisplayName("Project format")]
        public string ProjectFormat { get; set; }
        [Required("en")]
        [DisplayName("Genre")]
        public string Genre { get; set; }
        [Required("en")]
        [DisplayName("Duration in minutes")]
        public string DurationInMinutes { get; set; }

        [Required("en")]
        [DisplayName("Production language")]
        public string ProductionLanguage { get; set; }
        [Required("en")]
        [DisplayName("Start date for production in Norway")]
        public DateTime StartDateForProductionInNorway { get; set; }

        [Required("en")]
        [DisplayName("Final date for production in Norway")]
        public DateTime FinalDateForProductionInNorway { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Qualification test")]
        public HttpPostedFileBase QualificationTest { get; set; }
        [DisplayName("Qualification test")]
        public string QualificationTestPath { get; set; }

        [Required("en")]
        [DisplayName("Short summary of the story, max 200 characters")]
        public string ShortSummaryOfTheStory { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Screenplay/Script")]
        public HttpPostedFileBase ScreenplayScript { get; set; }
        [DisplayName("Screenplay/Script")]
        public string ScreenplayScriptPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Treatment/Season outline")]
        public HttpPostedFileBase TreatmentSeasonOutline { get; set; }
        [DisplayName("Treatment/Season outline")]
        public string TreatmentSeasonOutlinePath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Production schedule")]
        public HttpPostedFileBase ProductionSchedule { get; set; }
        [DisplayName("Production schedule")]
        public string ProductionSchedulePath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Cast & crew list")]
        public HttpPostedFileBase CastCrewList { get; set; }
        [DisplayName("Cast & crew list")]
        public string CastCrewListPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Location list")]
        public HttpPostedFileBase LocationList { get; set; }
        [DisplayName("Location list")]
        public string LocationListPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("List of organisations and subcontractors involved the production in Norway and the EEC")]
        public HttpPostedFileBase ListOfOrganisations { get; set; }
        [DisplayName("List of organisations and subcontractors involved the production in Norway and the EEC")]
        public string ListOfOrganisationsPath { get; set; }

        

        [Required("en")]
        [DisplayName("Describe how the production is suited to increase the capacity of the filmmakers involved to undertake ambitious and demanding productions of high quality and of cultural value")]
        public string DescribeHowTheProductionIsSuited { get; set; }

        [Required("en")]
        [DisplayName("Describe the production strategy for sustainable and green recording")]
        public string DescribeTheProductionStrategy { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Distribution strategy")]
        public HttpPostedFileBase DistributionStrategy { get; set; }
        [DisplayName("Distribution strategy")]
        public string DistributionStrategyPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("International distribution agreement")]
        public HttpPostedFileBase InternationalDistributionAgreement { get; set; }
        [DisplayName("International distribution agreement")]
        public string InternationalDistributionAgreementPath { get; set; }

        // 4. Financial information
        [Required("en")]
        [DisplayName("Total production budget in NOK")]
        public string TotalProductionBudget { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Attachment - Total production budget")]
        public HttpPostedFileBase AttachmentTotalProductionBudget { get; set; }
        [DisplayName("Attachment - Total production budget")]
        public string AttachmentTotalProductionBudgetPath { get; set; }

        [Required("en")]
        [DisplayName("Estimated costs spent in Norway in NOK")]
        public string EstimatedCostsSpentInNorway { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Budget for production in Norway in NOK (If more than 80 % of the total approved production costs for the production will arise in Norway, the grant amount will be calculated based on the total production costs in the EEA. The grant will under such circumstances be calculated based on the total approved production costs in Norway and the EEA.)")]
        public HttpPostedFileBase BudgetForProductionInNorway { get; set; }
        [DisplayName("Budget for production in Norway in NOK (If more than 80 % of the total approved production costs for the production will arise in Norway, the grant amount will be calculated based on the total production costs in the EEA. The grant will under such circumstances be calculated based on the total approved production costs in Norway and the EEA.)")]
        public string BudgetForProductionInNorwayPath { get; set; }

        [JsonIgnore, Required("en"), FileSize("en")]
        [DisplayName("Financing plan (specify private and public sources and confirmed/not confirmed funding)")]
        public HttpPostedFileBase FinancingPlan { get; set; }
        [DisplayName("Financing plan (specify private and public sources and confirmed/not confirmed funding)")]
        public string FinancingPlanPath { get; set; }

        [DisplayName("Percentage of confirmed financing")]
        public string PercentageConfirmedFinancing { get; set; }


        // 5. Eventuelle andre vedlegg
        [JsonIgnore, FileSize("en")]
        [DisplayName("Other relevant attachments")]
        public List<HttpPostedFileBase> OtherRelevantAttachments { get; set; }
        [DisplayName("Other relevant attachments")]
        public List<string> OtherRelevantAttachmentsPaths { get; set; }

        [DisplayName("Description of other attachments")]
        public string DescriptionOfOtherAttachments { get; set; }
    }
}