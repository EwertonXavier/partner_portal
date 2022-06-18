namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class consultant : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consultants",
                c => new
                    {
                        Consultant_Id = c.Int(nullable: false, identity: true),
                        First_Name = c.String(),
                        Last_Name = c.String(),
                        Address = c.String(),
                        Mobile_Number = c.String(),
                        Email = c.String(),
                        Wage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Hire_date = c.DateTime(nullable: false),
                        Status = c.String(),
                        PartnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Consultant_Id)
                .ForeignKey("dbo.Partners", t => t.PartnerId, cascadeDelete: true)
                .Index(t => t.PartnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consultants", "PartnerId", "dbo.Partners");
            DropIndex("dbo.Consultants", new[] { "PartnerId" });
            DropTable("dbo.Consultants");
        }
    }
}
