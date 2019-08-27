using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpootLight.Controllers
{
    class Jobs
    {
        private Jobs(string value) { Value = value; }

        public string Value { get; set; }

        public static Jobs ChapitresComptable { get { return new Jobs("ACCOUNT_PR"); } }
        public static Jobs ChapitresComptableCrush { get { return new Jobs("ACCOUNT_PR_CR"); } }

        public static Jobs PortfeuilBalois { get { return new Jobs("SEGMENT_PR"); } }
        public static Jobs PortfeuilBaloisCrush { get { return new Jobs("SEGMENT_PR_CR"); } }


        public static Jobs DeviseTauxChange { get { return new Jobs("CURRENCY_PR"); } }
        public static Jobs DeviseTauxChangeCrush { get { return new Jobs("CURRENCY_PR_CR"); } }


        public static Jobs SegmentClientInterne { get { return new Jobs("INTERNAL_SEGMENT_PR"); } }
        public static Jobs SegmentClientInterneCrush { get { return new Jobs("INTERNAL_SEGMENT_PR_CR"); } }

        // ***************************************************************************
        public static Jobs CalculActifPondere { get { return new Jobs("ASSET_FT"); } }
        public static Jobs CalculTechRisk { get { return new Jobs("ARC_FT"); } }
        public static Jobs COREPCA { get { return new Jobs("COREP_CA_FT"); } }
        public static Jobs COREPCRSA { get { return new Jobs("COREP_CR_FT"); } }
        public static Jobs ClientElgCMDR { get { return new Jobs("CUSOMER_CMDR_FT"); } }
        public static Jobs ClientEngagement { get { return new Jobs("CUSOMER_COMMITMENT_FT"); } }
        // ***************************************************************************
        public static Jobs Compte { get { return new Jobs("AUX_ACCOUNT_DT"); } }
        public static Jobs Client { get { return new Jobs("CUSTOMER_DT"); } }
        public static Jobs Garantie { get { return new Jobs("GUARANTEE_DT"); } }
        public static Jobs Provision { get { return new Jobs("PROVISION_DT"); } }
        public static Jobs Default { get { return new Jobs("DEFAULT_DT"); } }

        // CRM Excel paths  ***************************************************************************
        public static Jobs CRMSVFile { get { return new Jobs("COREP_CRM_SV.xlsx"); } }
        public static Jobs CRMECFile { get { return new Jobs("COREP_CRM_EC.xlsx"); } }
        public static Jobs CRMCLFile { get { return new Jobs("COREP_CRM_CL.xlsx"); } }
        public static Jobs CRMENFile { get { return new Jobs("COREP_CRM_EN.xlsx"); } }

        // JOB CRM ********* 

        public static Jobs CRMJob { get { return new Jobs("CRM_JOB"); } }

        // CR SA Excel paths  ****************************************************************************

        public static Jobs CR_SASVFile { get { return new Jobs("COREP_CR_SA_SV.xlsx"); } }
        public static Jobs CR_SAECFile { get { return new Jobs("COREP_CR_SA_EC.xlsx"); } }
        public static Jobs CR_SACLFile { get { return new Jobs("COREP_CR_SA_CL.xlsx"); } }
        public static Jobs CR_SAENFile { get { return new Jobs("COREP_CR_SA_EN.xlsx"); } }

        // JOB SR SA ********* 

        public static Jobs CR_SAJob { get { return new Jobs("CRSA_JOB"); } }

        // CMDR JObs  ****************************************************************************
        public static Jobs CMDR335File { get { return new Jobs("CMDR_335.xlsx"); } }
        public static Jobs CMDR335Job { get { return new Jobs("CMDR335_JOB"); } }

        public static Jobs CMDR337File { get { return new Jobs("CMDR_337.xlsx"); } }
        public static Jobs CMDR337Job { get { return new Jobs("CMDR337_JOB"); } }

        public static Jobs CMDR339File { get { return new Jobs("CMDR_339.xlsx"); } }
        public static Jobs CMDR339Job { get { return new Jobs("CMDR339_JOB"); } }

    }
}
