namespace BackendAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        EndDate = c.DateTimeOffset(nullable: false, precision: 7),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        CompanyId = c.String(maxLength: 128),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Company_Id = c.String(maxLength: 128),
                        Company_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id1)
                .ForeignKey("dbo.Companies", t => t.CompanyId)
                .Index(t => t.CompanyId)
                .Index(t => t.Company_Id)
                .Index(t => t.Company_Id1);
            
            DropColumn("dbo.Companies", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "Logo", c => c.String());
            DropForeignKey("dbo.Promotions", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Promotions", "Company_Id1", "dbo.Companies");
            DropForeignKey("dbo.Promotions", "Company_Id", "dbo.Companies");
            DropIndex("dbo.Promotions", new[] { "Company_Id1" });
            DropIndex("dbo.Promotions", new[] { "Company_Id" });
            DropIndex("dbo.Promotions", new[] { "CompanyId" });
            DropTable("dbo.Promotions");
        }
    }
}
