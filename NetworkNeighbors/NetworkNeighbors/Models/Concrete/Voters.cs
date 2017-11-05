using System;
using System.Linq;
using System.Data;
using System.Web;
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
                    if(db.SaveChanges() > 0)
                    {
                        return entity.voter_id;
                    }
                }
            }
            catch(Exception ex)
            {
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

        #endregion 
    }
}