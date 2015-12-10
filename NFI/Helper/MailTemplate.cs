using NFI.Enums;

namespace NFI.Helper
{
    public static class MailTemplate
    {
        public static string GetMailBodyForApplicant(ApplicationType appType)
        {
            var mailBody = "";
            switch (appType)
            {
                case ApplicationType.Sorfond:

                    mailBody = @"Vi bekrefter med dette å ha mottatt din søknad.<br/> 
                                Normalt kan du forvente et svar på søknaden innen 5 til 7 uker.<br/> 
                                Behandlingstiden gjelder fra søknadsfrist.<br/><br/>  
                                Med vennelig hilsen<br/>
                                Norsk filminstitutt";
                    break;

                case ApplicationType.Insentivordning:

                    mailBody = @"Vi bekrefter med dette å ha mottatt din søknad.<br/><br/> 
                                Normalt kan du forvente svar på søknaden innen 6 uker.Behandlingstiden gjelder fra søknadsfrist.<br/><br/>
                                Minner om opplysningsplikten iht forskriften § 14:<br/>
                                Søker plikter å gi Norsk filminstitutt alle opplysninger som er nødvendige for å behandle søknaden og grunnlaget for tilskuddet.<br/>
                                Etter at tilsagn om tilskudd foreligger, plikter tilskuddsmottakeren av eget tiltak å gi melding om eventuelle endringer i forutsetningene for tilskuddet.<br/>
                                Tilskuddsmottakeren kan ikke foreta vesentlige endringer i den aktuelle produksjonen uten at dette er skriftlig forelagt for og skriftlig godkjent av Norsk filminstitutt.<br/><br/>

                                Spørsmål kan rettes til: <strong>insentiv@nfi.no</strong><br/>
                                Med vennelig hilsen<br/>
                                Norsk filminstitutt";
                    break;
                case ApplicationType.IncentiveScheme:
                    mailBody = @"We hereby confirm the submission of your application.<br/>
                                The evaluation period is six weeks from the date of the application deadline.<br/><br/>

                                Calling attention to the Regulations, section 14, Disclosure duty:<br/> 
                                The applicant has a duty to provide the Norwegian Film Institute with all information it requires to process the application and evaluate the basis for the grant.<br/> 
                                Once a decision to give a grant has been made, the grant recipient must on its own initiative notify any changes to the terms on which the grant is based.The grant recipient may not make material changes to the production or measure in question without submitting these to the Norwegian Film Institute in writing and obtaining its written approval.
                                <br/><br/>
                                For questions, please contact: incentive@nfi.no<br/><br/>

                                Best regards<br/>
                                Norwegian Film Institute";
                    break;

                case ApplicationType.UdsReisestotte:
                    mailBody = @"Hei<br/><br/>
                                Vi bekrefter med dette å ha mottatt din søknad. <br/>
                                Normalt kan du forvente en tilbakemelding på søknaden innen 5 uker. <br/>
                                For tilskuddsordninger med søknadsfrister, gjelder behandlingstiden fra frist.<br/><br/>

                                Med vennlig hilsen <br/>
                                Norsk filminstitutt<br/><br/>

                                Etter at tilsagn om tilskudd foreligger, plikter tilskuddsmottakeren av eget tiltak å gi melding om eventuelle endringer i forutsetningene for tilskuddet. <br/>
                                Tilskuddsmottakeren kan ikke foreta vesentlige endringer i den aktuelle produksjonen uten at dette er skriftlig forelagt for og skriftlig godkjent av Norsk filminstitutt.<br/><br/>

                                Spørsmål kan rettes til: insentiv@nfi.no<br/>
                                Med vennelig hilsen<br/>
                                Norsk filminstitutt";
                    break;

                case ApplicationType.Lansering:
                    mailBody = @"Hei,<br/> <br/> 
                                Vi bekrefter med dette å ha mottatt din søknad. <br/> 
                                Normalt kan du forvente en tilbakemelding på søknaden innen 5 uker. <br/> 
                                For tilskuddsordninger med søknadsfrister, gjelder behandlingstiden fra frist.<br/> <br/> 

                                Med vennlig hilsen <br/> 
                                Norsk filminstitutt";
                    break;

                case ApplicationType.Ordninger:
                    mailBody = @"Hei,<br/><br/>
Vi bekrefter med dette å ha mottatt din søknad.<br/> 
Normalt kan du forvente en tilbakemelding på søknaden innen 5 uker. <br/>
For tilskuddsordninger med søknadsfrister, gjelder behandlingstiden fra frist.<br/><br/>

Med vennlig hilsen <br/>
Norsk filminstitutt<br/><br/>

Etter at tilsagn om tilskudd foreligger, plikter tilskuddsmottakeren av eget tiltak å gi melding om eventuelle endringer i forutsetningene for tilskuddet. <br/>
Tilskuddsmottakeren kan ikke foreta vesentlige endringer i den aktuelle produksjonen uten at dette er skriftlig forelagt for og skriftlig godkjent av Norsk filminstitutt.<br/><br/>

Spørsmål kan rettes til: insentiv@nfi.no<br/>
Med vennelig hilsen<br/>
Norsk filminstitutt";
                    break;

            }


            return mailBody;

        }
    }
}