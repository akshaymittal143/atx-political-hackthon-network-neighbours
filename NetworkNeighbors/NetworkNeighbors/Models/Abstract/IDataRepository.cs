using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using NetworkNeighbors.Models.Entities;

namespace NetworkNeighbors.Models.Abstract
{
    public interface IDataRepository
    {
        #region VAN
        string VANEcho(string str);
        #endregion

        #region Voters
        IQueryable<Voter> Voters { get; }
        string SaveVoter(Voter entity);
        bool DeleteVoter(string id);
        #endregion
    }
}