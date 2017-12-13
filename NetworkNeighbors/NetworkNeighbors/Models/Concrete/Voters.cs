using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using NetworkNeighbors.DTOs.VAN;
using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Entities;

namespace NetworkNeighbors.Models.Concrete
{
    public partial class Repository : IDataRepository
    {
        #region Voters

        public IQueryable<Voter> Voters
        {
            get
            {
                return db.Voters.AsQueryable();
            }
        }

        private bool ValidateVoter(Voter entity)
        {
            bool valid = true;
            if(entity.voter_id == null || entity.voter_id.Trim().Length == 0)
            {
                HttpContext.Current.Trace.Warn("voter_id not found");
                valid = false;
            }
            if (entity.first_name == null || entity.first_name.Trim().Length == 0)
            {
                HttpContext.Current.Trace.Warn("first_name not found");
                valid = false;
            }
            if (entity.last_name == null || entity.last_name.Trim().Length == 0)
            {
                HttpContext.Current.Trace.Warn("last_name not found");
                valid = false;
            }
            return valid;
        }

        public string SaveVoter(Voter entity)
        {
            try
            {
                if (ValidateVoter(entity))
                {
                    var original = Voters.Where(n => n.voter_id == entity.voter_id).FirstOrDefault();
                    if(original != null)
                    {
                        // update existing
                        original = (Voter)Helpers.UpdateObject(original, entity, "voter_id");
                    }
                    else
                    {
                        // insert new voter
                        db.Voters.Add(entity);
                    }
                    return entity.voter_id;
                }
            }
            catch(Exception ex)
            {
                foreach (var e in db.GetValidationErrors())
                {
                    foreach(var e2 in e.ValidationErrors)
                    {
                        HttpContext.Current.Trace.Warn(e2.PropertyName + ": " + e2.ErrorMessage);
                    }
                }
                HttpContext.Current.Trace.Warn(ex.ToString());
            }
            return null;
        }

        public bool DeleteVoter(string id)
        {
            try
            {
                var entity = db.Voters.Where(n => n.voter_id == id).FirstOrDefault();
                db.Voters.Remove(entity);
                return db.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                HttpContext.Current.Trace.Warn(ex.ToString());
            }
            return false;
        }

        public async Task<Voter> CheckVoterAsync(Voter entity)
        {
            try
            {
                try
                {
                    // Requests to VAN have been failing - appears to be offline. So if requests doesn't work, for now let's treat as unregistered
                    var dto = new NetworkNeighbors.DTOs.VAN.PersonDTO
                    {
                        firstName = entity.first_name,
                        lastName = entity.last_name
                    };
                    if (!String.IsNullOrEmpty(entity.email))
                    {
                        dto.emails = new List<EmailDTO> { new EmailDTO { email = entity.email } };
                    }
                    if (!String.IsNullOrEmpty(entity.phone))
                    {
                        dto.phones = new List<PhoneDTO> { new PhoneDTO { phoneNumber = entity.phone } };
                    }
                    if (!String.IsNullOrEmpty(entity.address_1) && !String.IsNullOrEmpty(entity.city) && !String.IsNullOrEmpty(entity.state) && !String.IsNullOrEmpty(entity.zip_code))
                    {
                        dto.addresses = new List<AddressDTO> { new AddressDTO { addressLine1 = entity.address_1, addressLine2 = entity.address_2, city = entity.city, stateOrProvince = entity.state, zipOrPostalCode = entity.zip_code } };
                    }
                    if (entity.dob.HasValue)
                    {
                        dto.dateOfBirth = entity.dob.Value;
                    }                
                    var match = await FindPersonInVANAsync(dto);
                    if (match != null)
                    {
                        entity.van_id = match.vanId;
                        entity.registration_status = match.status;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Trace.Warn(ex.ToString());
                }
                entity.voter_id = SaveVoter(entity);
                if (!String.IsNullOrEmpty(entity.voter_id))
                {
                    return entity;
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Warn(ex.ToString());
            }
            return null;
        }

        #endregion 
    }
}