namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Status = c.String(),
                        Create_Date = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PartnerId = c.Int(nullable: false),
                        Consultant_Id = c.Int(nullable: true),
                        Artifact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Order_Id)
                .ForeignKey("dbo.Artifacts", t => t.Artifact_Id, cascadeDelete: true)
                .ForeignKey("dbo.Consultants", t => t.Consultant_Id, cascadeDelete: false)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: false)
                .Index(t => t.PartnerId)
                .Index(t => t.Consultant_Id)
                .Index(t => t.Artifact_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "PartnerId", "dbo.Partners");
            DropForeignKey("dbo.Orders", "Consultant_Id", "dbo.Consultants");
            DropForeignKey("dbo.Orders", "Artifact_Id", "dbo.Artifacts");
            DropIndex("dbo.Orders", new[] { "Artifact_Id" });
            DropIndex("dbo.Orders", new[] { "Consultant_Id" });
            DropIndex("dbo.Orders", new[] { "PartnerId" });
            DropTable("dbo.Orders");
        }
    }
}
