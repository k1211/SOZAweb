namespace SOZA_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GPSTraces : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GPSTraces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        AndroidClient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AndroidClients", t => t.AndroidClient_Id)
                .Index(t => t.AndroidClient_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GPSTraces", "AndroidClient_Id", "dbo.AndroidClients");
            DropIndex("dbo.GPSTraces", new[] { "AndroidClient_Id" });
            DropTable("dbo.GPSTraces");
        }
    }
}
