namespace Gighub1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedArtistId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Gigs", new[] { "artistId" });
            CreateIndex("dbo.Gigs", "ArtistId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Gigs", new[] { "ArtistId" });
            CreateIndex("dbo.Gigs", "artistId");
        }
    }
}
