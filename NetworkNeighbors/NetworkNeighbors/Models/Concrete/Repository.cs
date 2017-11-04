using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NetworkNeighbors.Models.Abstract;
using NetworkNeighbors.Models.Entities;

namespace NetworkNeighbors.Models.Concrete
{
    public partial class Repository: IDataRepository
    {
        #region Repository

        private NetworkNeighborsEntities db;

        public Repository()
        {
            db = new NetworkNeighborsEntities();
        }

        #endregion 
    }
}