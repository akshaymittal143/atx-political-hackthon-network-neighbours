﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetworkNeighbors.Models.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NetworkNeighborsEntities : DbContext
    {
        public NetworkNeighborsEntities()
            : base("name=NetworkNeighborsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Referral> Referrals { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Voter> Voters { get; set; }
    }
}
