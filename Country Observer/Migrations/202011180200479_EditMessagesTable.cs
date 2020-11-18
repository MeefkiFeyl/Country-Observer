namespace Country_Observer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMessagesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Type");
        }
    }
}
