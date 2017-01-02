namespace SOZA_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SafeLatLng : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SafeLatLng_lat", c => c.Double(nullable: false));
            AddColumn("dbo.AspNetUsers", "SafeLatLng_lng", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "SafeLatLng_lng");
            DropColumn("dbo.AspNetUsers", "SafeLatLng_lat");
        }
    }
}
