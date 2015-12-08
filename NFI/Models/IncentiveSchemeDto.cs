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
        public string CityTown { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Country")]
        public string Country { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Name of producer")]
        public string NameProducer { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Cell number")]
        public string CellNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Production company webpage")]
        public string ProductionCompanyWebpage { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Certificate of origin - production company")]
        public HttpPostedFileBase CertificateOriginProductionCompany { get; set; }
        [DisplayName("Certificate of origin - production company")]
        public string CertificateOriginProductionCompanyPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Producer's CV")]
        public HttpPostedFileBase ProducersCv { get; set; }
        [DisplayName("Producer's CV")]
        public string ProducersCvPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
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

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DisplayName("E-mail: (E-postadressen som skal motta bekreftelse på at søknaden er sendt inn)")]
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

        [JsonIgnore, FileSize]
        [DisplayName("Declaration from main producer allowing the applicant to apply to the incentive scheme on behalf of the main producer.")]
        public HttpPostedFileBase DeclarationFromMainProducer { get; set; }
        [DisplayName("Declaration from main producer allowing the applicant to apply to the incentive scheme on behalf of the main producer.")]
        public string DeclarationFromMainProducerPath { get; set; }

        // 3. Project information
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Project title")]
        public string ProjectTitle { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Is the project an original work?")]
        public string IsTheProjectOriginalWork { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Documentation of ownership of rights (held by main producer)")]
        public HttpPostedFileBase DocumentationOwnershipOfRights { get; set; }
        [DisplayName("Documentation of ownership of rights (held by main producer)")]
        public string DocumentationOwnershipOfRightsPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Project format")]
        public string ProjectFormat { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Genre")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Duration in minutes")]
        public string DurationInMinutes { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Production language")]
        public string ProductionLanguage { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Start date for production in Norway")]
        public DateTime StartDateForProductionInNorway { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Final date for production in Norway")]
        public DateTime FinalDateForProductionInNorway { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Qualification test")]
        public HttpPostedFileBase QualificationTest { get; set; }
        [DisplayName("Qualification test")]
        public string QualificationTestPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Short summary of the story, max 200 characters")]
        public string ShortSummaryOfTheStory { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Screenplay/Script")]
        public HttpPostedFileBase ScreenplayScript { get; set; }
        [DisplayName("Screenplay/Script")]
        public string ScreenplayScriptPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Treatment/Season outline")]
        public HttpPostedFileBase TreatmentSeasonOutline { get; set; }
        [DisplayName("Treatment/Season outline")]
        public string TreatmentSeasonOutlinePath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Production schedule")]
        public HttpPostedFileBase ProductionSchedule { get; set; }
        [DisplayName("Production schedule")]
        public string ProductionSchedulePath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Cast & crew list")]
        public HttpPostedFileBase CastCrewList { get; set; }
        [DisplayName("Cast & crew list")]
        public string CastCrewListPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Location list")]
        public HttpPostedFileBase LocationList { get; set; }
        [DisplayName("Location list")]
        public string LocationListPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("List of organisations and subcontractors involved the production in Norway and the EEC")]
        public HttpPostedFileBase ListOfOrganisations { get; set; }
        [DisplayName("List of organisations and subcontractors involved the production in Norway and the EEC")]
        public string ListOfOrganisationsPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Distribution strategy")]
        public HttpPostedFileBase DistributionStrategy { get; set; }
        [DisplayName("Distribution strategy")]
        public string DistributionStrategyPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Describe how the production is suited to increase the capacity of the filmmakers involved to undertake ambitious and demanding productions of high quality and of cultural value")]
        public string DescribeHowTheProductionIsSuited { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Describe the production strategy for sustainable and green recording")]
        public string DescribeTheProductionStrategy { get; set; }


        // 4. Visual materials
        public List<VisualMaterialDto> VisualMaterialList { get; set; }

        // 5. Financial information
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Total production budget in NOK")]
        public string TotalProductionBudget { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Attachment - Total production budget")]
        public HttpPostedFileBase AttachmentTotalProductionBudget { get; set; }
        [DisplayName("Attachment - Total production budget")]
        public string AttachmentTotalProductionBudgetPath { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Estimated costs spent in Norway in NOK")]
        public string EstimatedCostsSpentInNorway { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Budget for production in Norway in NOK (If more than 80 % of the total approved production costs for the production will arise in Norway, the grant amount will be calculated based on the total production costs in the EEA. The grant will under such circumstances be")]
        public HttpPostedFileBase BudgetForProductionInNorway { get; set; }
        [DisplayName("Budget for production in Norway in NOK (If more than 80 % of the total approved production costs for the production will arise in Norway, the grant amount will be calculated based on the total production costs in the EEA. The grant will under such circumstances be")]
        public string BudgetForProductionInNorwayPath { get; set; }

        [JsonIgnore, Required(ErrorMessage = "This field is required"), FileSize]
        [DisplayName("Financing plan (specify private and public sources and confirmed/not confirmed funding)")]
        public HttpPostedFileBase FinancingPlan { get; set; }
        [DisplayName("Financing plan (specify private and public sources and confirmed/not confirmed funding)")]
        public string FinancingPlanPath { get; set; }

        [DisplayName("Percentage of confirmed financing")]
        public string PercentageConfirmedFinancing { get; set; }


        // 6. Eventuelle andre vedlegg
        [JsonIgnore, FileSize]
        [DisplayName("Other relevant attachments")]
        public List<HttpPostedFileBase> OtherRelevantAttachments { get; set; }
        [DisplayName("Other relevant attachments")]
        public List<string> OtherRelevantAttachmentPaths { get; set; }

        [DisplayName("Description of other attachments")]
        public string DescriptionOfOtherAttachments { get; set; }
    }

    public class VisualMaterialDto
    {
        [DisplayName("Visual materials (website, Vimeo, etc)")]
        public string VisualMaterials { get; set; }
        [DisplayName("Password for website with visual materials")]
        public string PasswordForWebsite { get; set; }
    }
}