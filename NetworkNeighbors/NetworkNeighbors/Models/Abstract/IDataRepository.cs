using System.Linq;
using System.Threading.Tasks;
using NetworkNeighbors.DTOs.VAN;
using NetworkNeighbors.Models.Entities;

namespace NetworkNeighbors.Models.Abstract
{
    public interface IDataRepository
    {
        #region VAN
        Task<string> VANEchoAsync(string str);
        Task<MatchResponse> FindOrCreatePersonInVANAsync(PersonDTO person);
        Task<MatchResponse> FindPersonInVANAsync(PersonDTO person);
        Task<Voter> GetPersonInVANAsync(string vanID);
        #endregion

        #region Voters
        IQueryable<Voter> Voters { get; }
        string SaveVoter(Voter entity);
        bool DeleteVoter(string id);
        Task<Voter> CheckVoterAsync(Voter entity);
        #endregion
    }
}