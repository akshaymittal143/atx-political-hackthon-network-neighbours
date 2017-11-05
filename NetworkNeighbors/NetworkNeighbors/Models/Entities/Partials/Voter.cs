using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkNeighbors.Models.Entities
{
    public partial class Voter
    {
        public string registration_status { get; set; }

        public string dobStr
        {
            get
            {
                if (dob.HasValue)
                {
                    return dob.Value.ToShortDateString();
                }
                else
                {
                    return String.Empty;
                }
            }
        }
    }
}