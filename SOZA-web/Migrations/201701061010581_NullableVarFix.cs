namespace SOZA_web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableVarFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "SafeLatLng_lat", c => c.Double(nullable: false, defaultValue: 54.3715175));
            AlterColumn("dbo.AspNetUsers", "SafeLatLng_lng", c => c.Double(nullable: false, defaultValue: 18.6126851));
            AlterColumn("dbo.AspNetUsers", "SafeAreaRadius", c => c.Int(nullable: false, defaultValue: 10));
        }

        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "SafeAreaRadius", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "SafeLatLng_lng", c => c.Double());
            AlterColumn("dbo.AspNetUsers", "SafeLatLng_lat", c => c.Double());
        }
    }
}
