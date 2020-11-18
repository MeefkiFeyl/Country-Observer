namespace Country_Observer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMessages : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Messages", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Type", c => c.Int());
        }
    }
}
