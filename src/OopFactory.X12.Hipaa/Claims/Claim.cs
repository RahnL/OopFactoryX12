﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using OopFactory.X12.Hipaa.Common;

namespace OopFactory.X12.Hipaa.Claims
{
    public enum ClaimTypeEnum
    {
        Professional,
        Institutional,
        Dental
    }

    public class Claim
    {
        public Claim()
        {
            if (Providers == null) Providers = new List<Provider>();
            if (ServiceLines == null) ServiceLines = new List<ServiceLine>();
        }

        [XmlAttribute]
        public ClaimTypeEnum Type { get; set; }
        [XmlAttribute]
        public string PatientControlNumber { get; set; }
        [XmlAttribute]
        public decimal TotalClaimChargeAmount { get; set; }

        public Entity Submitter { get; set; }
        public Entity Receiver { get; set; }
        public BillingInformation BillingInfo { get; set; }
        public ClaimMember Subscriber { get; set; }
        public Entity Payer { get; set; }
        public ClaimMember Patient { get; set; }

        #region Institional Claim Properties

        [XmlAttribute]
        public string MedicalRecordNumber { get; set; }

        [XmlElement(ElementName="Condition")]
        public List<Lookup> Conditions { get; set; }

        [XmlElement(ElementName="Occurrence")]
        public List<CodedDate> Occurrences { get; set; }

        [XmlElement(ElementName="OccurrenceSpan")]
        public List<CodedDateRange> OccurrenceSpans { get; set; }

        [XmlElement(ElementName="Value")]
        public List<CodedAmount> Values { get; set; }

        [XmlElement(ElementName="Procedure")]
        public List<CodedDate> Procedures { get; set; }

        [XmlElement(ElementName="Provider")]
        public List<Provider> Providers { get; set; }
        
        #endregion

        [XmlElement(ElementName="ServiceLine")]
        public List<ServiceLine> ServiceLines { get; set; }

        #region Calculated Fields
        public Provider ServiceLocation
        {
            get
            {
                var serviceFacilityLocation = Providers.FirstOrDefault(p => p.Name.Identifier == "77");
                if (serviceFacilityLocation != null)
                    return serviceFacilityLocation;
                else
                {
                    if (BillingInfo != null)
                        return BillingInfo.Providers.FirstOrDefault(p => p.Name.Identifier == "85");
                    else
                        return null;
                }
            }
        }

        public Provider PayToProvider
        {
            get
            {
                if (BillingInfo != null)
                {
                    var payToProvider = BillingInfo.Providers.FirstOrDefault(p => p.Name.Identifier == "87");
                    if (payToProvider != null)
                        return payToProvider;
                    else // the billing provider is the pay to provider when the pay-to provider is not present
                        return BillingInfo.Providers.FirstOrDefault(p => p.Name.Identifier == "85");
                }
                else
                    return null;
            }
        }

        public Provider PayToPlan
        {
            get
            {
                if (BillingInfo != null)
                    return BillingInfo.Providers.FirstOrDefault(p => p.Name.Identifier == "PE");
                else
                    return null;
            }
        }
        #endregion
    }
}