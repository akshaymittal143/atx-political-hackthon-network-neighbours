namespace NetworkNeighbors.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Voters",
                c => new
                    {
                        voter_id = c.String(nullable: false, maxLength: 128),
                        first_name = c.String(),
                        last_name = c.String(),
                        dob = c.DateTime(),
                        email = c.String(),
                        phone = c.String(),
                        address_1 = c.String(),
                        address_2 = c.String(),
                        city = c.String(),
                        state = c.String(),
                        zip_code = c.String(),
                        van_id = c.String(),
                    })
                .PrimaryKey(t => t.voter_id);
            
            CreateTable(
                "dbo.Referrals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        referrer_voter_id = c.String(),
                        referred_voter_id = c.String(),
                        status_id = c.Int(nullable: false),
                        Voter_voter_id = c.String(maxLength: 128),
                        Voter1_voter_id = c.String(maxLength: 128),
                        Voter_voter_id1 = c.String(maxLength: 128),
                        Voter_voter_id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Status", t => t.status_id, cascadeDelete: true)
                .ForeignKey("dbo.Voters", t => t.Voter_voter_id)
                .ForeignKey("dbo.Voters", t => t.Voter1_voter_id)
                .ForeignKey("dbo.Voters", t => t.Voter_voter_id1)
                .ForeignKey("dbo.Voters", t => t.Voter_voter_id2)
                .Index(t => t.status_id)
                .Index(t => t.Voter_voter_id)
                .Index(t => t.Voter1_voter_id)
                .Index(t => t.Voter_voter_id1)
                .Index(t => t.Voter_voter_id2);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        status_id = c.Int(nullable: false, identity: true),
                        status1 = c.String(),
                    })
                .PrimaryKey(t => t.status_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referrals", "Voter_voter_id2", "dbo.Voters");
            DropForeignKey("dbo.Referrals", "Voter_voter_id1", "dbo.Voters");
            DropForeignKey("dbo.Referrals", "Voter1_voter_id", "dbo.Voters");
            DropForeignKey("dbo.Referrals", "Voter_voter_id", "dbo.Voters");
            DropForeignKey("dbo.Referrals", "status_id", "dbo.Status");
            DropIndex("dbo.Referrals", new[] { "Voter_voter_id2" });
            DropIndex("dbo.Referrals", new[] { "Voter_voter_id1" });
            DropIndex("dbo.Referrals", new[] { "Voter1_voter_id" });
            DropIndex("dbo.Referrals", new[] { "Voter_voter_id" });
            DropIndex("dbo.Referrals", new[] { "status_id" });
            DropTable("dbo.Status");
            DropTable("dbo.Referrals");
            DropTable("dbo.Voters");
        }
    }
}
