using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetworkNeighbors.DTOs.VAN
{
    public class PersonDTO
    {
        public int vanId { get; set; }

        [MaxLength(20)]
        public string firstName { get; set; }

        [MaxLength(20)]
        public string middleName { get; set; }

        [MaxLength(25)]
        public string lastName { get; set; }

        public DateTime? dateOfBirth { get; set; }

        [MaxLength(1)]
        public string party { get; set; }

        [MaxLength(1)]
        public string sex { get; set; }

        [MaxLength(20)]
        public string salutation { get; set; }

        [MaxLength(60)]
        public string envelopeName { get; set; }

        [MaxLength(10)]
        public string title { get; set; }

        [MaxLength(50)]
        public string suffix { get; set; }

        [MaxLength(50)]
        public string nickname { get; set; }

        [MaxLength(50)]
        public string website { get; set; }

        [MaxLength(1)]
        public string contactMethodPreferenceCode { get; set; }

        [MaxLength(50)]
        public string employer { get; set; }

        [MaxLength(50)]
        public string occupation { get; set; }

        public IEnumerable<EmailDTO> emails { get; set; }

        public IEnumerable<PhoneDTO> phones { get; set; }

        public IEnumerable<AddressDTO> addresses { get; set; }

        public IEnumerable<AddressDTO> recordedAddresses { get; set; }

        public IEnumerable<IdentifierDTO> identifiers { get; set; }

        public IEnumerable<CustomFieldValueDTO> customFieldValues { get; set; }

        public SelfReportedRaceDTO selfReportedRace { get; set; }

        public SelfReportedEthnicityDTO selfReportedEthnicity { get; set; }

        public SelfReportedLanguagePreferenceDTO selfReportedLanguagePreference { get; set; }

        public IEnumerable<SuppressionDTO> suppressions { get; set; }
    }
}