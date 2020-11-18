namespace Country_Observer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMessagesTable1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "Type", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Type", c => c.Int(nullable: false));
        }
    }
}
