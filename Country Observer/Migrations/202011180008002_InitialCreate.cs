namespace Country_Observer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CountryDbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Region = c.Int(),
                        Capital = c.Int(),
                        Name = c.String(),
                        Alpha3Code = c.String(),
                        Area = c.Double(),
                        Population = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Regions");
            DropTable("dbo.CountryDbs");
            DropTable("dbo.Cities");
        }
    }
}
