namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdersNullableFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Consultant_Id", "dbo.Consultants");
            DropForeignKey("dbo.Orders", "PartnerId", "dbo.Partners");
            DropIndex("dbo.Orders", new[] { "PartnerId" });
            DropIndex("dbo.Orders", new[] { "Consultant_Id" });
            AlterColumn("dbo.Orders", "PartnerId", c => c.Int());
            AlterColumn("dbo.Orders", "Consultant_Id", c => c.Int());
            CreateIndex("dbo.Orders", "PartnerId");
            CreateIndex("dbo.Orders", "Consultant_Id");
            AddForeignKey("dbo.Orders", "Consultant_Id", "dbo.Consultants", "Consultant_Id");
            AddForeignKey("dbo.Orders", "PartnerId", "dbo.Partners", "PartnerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Orders", "Consultant_Id", "dbo.Consultants");
            DropIndex("dbo.Orders", new[] { "Consultant_Id" });
            DropIndex("dbo.Orders", new[] { "PartnerId" });
            AlterColumn("dbo.Orders", "Consultant_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "PartnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Consultant_Id");
            CreateIndex("dbo.Orders", "PartnerId");
            AddForeignKey("dbo.Orders", "PartnerId", "dbo.Partners", "PartnerId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Consultant_Id", "dbo.Consultants", "Consultant_Id", cascadeDelete: true);
        }
    }
}
