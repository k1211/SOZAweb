namespace SOZA_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AndroidTokensTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AndroidClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(),
                        PhoneNumber = c.String(),
                        Guardian_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Guardian_Id)
                .Index(t => t.Guardian_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AndroidClients", "Guardian_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AndroidClients", new[] { "Guardian_Id" });
            DropTable("dbo.AndroidClients");
        }
    }
}
