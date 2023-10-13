namespace Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIndirizzoSpedizione : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ordini", "IndirizzoSpedizione");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordini", "IndirizzoSpedizione", c => c.String(nullable: false));
        }
    }
}
