using System.Collections.Generic;
using NetworkNeighbors.DTOs.VAN;
using NetworkNeighbors.Models.Entities;

namespace NetworkNeighbors.Mappers
{
    /// <summary>
    /// Class for mapping one object to another. Particularly useful for mapping entity models to DTOs.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Map a given Voter model's Address to a list of Address DTOs.
        /// </summary>
        /// <param name="voter"></param>
        /// <returns>List of Address DTOs</returns>
        public static List<AddressDTO> MapAddressDTOs(Voter voter)
        {
            if (voter == null ||
                (
                    // If one of these fields is populated, we'll try to build an address from it
                    string.IsNullOrWhiteSpace(voter.address_1)
                    && string.IsNullOrWhiteSpace(voter.address_2)
                    && string.IsNullOrWhiteSpace(voter.city)
                    && string.IsNullOrWhiteSpace(voter.state)
                    && string.IsNullOrWhiteSpace(voter.state)
                    && string.IsNullOrWhiteSpace(voter.zip_code))
                )
            {
                return null;
            }
            else
            {
                return new List<AddressDTO>()
                {
                    new AddressDTO()
                    {
                        addressLine1 = voter.address_1,
                        addressLine2 = voter.address_2,
                        city = voter.city,
                        stateOrProvince = voter.state,
                        zipOrPostalCode = voter.zip_code
                    }
                };
            }
        }

        /// <summary>
        /// Maps a given Voter model's Email to a list of Email DTOs.
        /// </summary>
        /// <param name="voter">Voter to map Email from</param>
        /// <returns>List of Email DTOs</returns>
        public static List<EmailDTO> MapEmailDTOs(Voter voter)
        {
            if (voter == null || string.IsNullOrWhiteSpace(voter.email))
            {
                return null;
            }
            else
            {
                return new List<EmailDTO>()
                {
                    new EmailDTO()
                    {
                        email = voter.email
                    }
                };
            }
        }

        /// <summary>
        /// Map a given Voter model to a Person DTO.
        /// </summary>
        /// <param name="voter">Voter to map from</param>
        /// <returns>Person DTO</returns>
        public static PersonDTO MapPersonDTO(Voter voter)
        {
            if (voter == null)
            {
                return null;
            }
            else
            {
                PersonDTO person = new PersonDTO()
                {
                    firstName = voter.first_name,
                    lastName = voter.last_name,
                    dateOfBirth = voter.dob,
                    emails = MapEmailDTOs(voter),
                    phones = MapPhoneDTOs(voter),
                    addresses = MapAddressDTOs(voter)
                    // Should recordedAddresses be what we have stored also?
                };

                return person;
            }
        }

        /// <summary>
        /// Map a given Voter model's Phone to a list of Phone DTOs.
        /// </summary>
        /// <param name="voter">Voter to map Phone from</param>
        /// <returns>List of Phone DTOs</returns>
        public static List<PhoneDTO> MapPhoneDTOs(Voter voter)
        {
            if (voter == null || string.IsNullOrWhiteSpace(voter.phone))
            {
                return null;
            }
            else
            {
                return new List<PhoneDTO>()
            {
                new PhoneDTO()
                {
                    phoneNumber = voter.phone
                }
            };
            }
        }
    }
}