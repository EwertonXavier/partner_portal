namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class artifact : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artifacts",
                c => new
                    {
                        Artifact_Id = c.Int(nullable: false, identity: true),
                        Create_Date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Content = c.String(),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Artifact_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artifacts", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Artifacts", new[] { "Customer_Id" });
            DropTable("dbo.Artifacts");
        }
    }
}
