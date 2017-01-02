namespace SOZA_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SafeAreaRadius : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SafeAreaRadius", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SafeAreaRadius");
        }
    }
}
