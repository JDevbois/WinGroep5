namespace BackendAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Promotions", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Promotions", "Company_Id1", "dbo.Companies");
            DropForeignKey("dbo.Promotions", "Company_Id2", "dbo.Companies");
            DropForeignKey("dbo.Companies", "UserId", "dbo.Users");
            DropIndex("dbo.Companies", new[] { "UserId" });
            DropIndex("dbo.Promotions", new[] { "Company_Id" });
            DropIndex("dbo.Promotions", new[] { "Company_Id1" });
            DropIndex("dbo.Promotions", new[] { "Company_Id2" });
            AlterColumn("dbo.Companies", "UserId", c => c.String());
            AlterColumn("dbo.Companies", "Name", c => c.String());
            AlterColumn("dbo.Companies", "City", c => c.String());
            AlterColumn("dbo.Companies", "Street", c => c.String());
            AlterColumn("dbo.Companies", "PostalCode", c => c.String());
            AlterColumn("dbo.Companies", "Number", c => c.String());
            AlterColumn("dbo.Companies", "OpeningHours", c => c.String());
            AlterColumn("dbo.Companies", "Type", c => c.String());
            AlterColumn("dbo.Companies", "Website", c => c.String());
            DropTable("dbo.Promotions");
            DropTable("dbo.Notifications");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        City = c.String(nullable: false),
                        PostalCode = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        Number = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CompanyId = c.String(nullable: false),
                        CompanyName = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        Logo = c.String(),
                        PromotionId = c.String(nullable: false),
                        PromotionTypeIdentifier = c.Int(nullable: false),
                        PromotionType = c.String(nullable: false),
                        NotificationType = c.Int(nullable: false),
                        Message = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        StartDate = c.DateTimeOffset(nullable: false, precision: 7),
                        EndDate = c.DateTimeOffset(nullable: false, precision: 7),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        CompanyId = c.String(nullable: false),
                        PDFUri = c.String(nullable: false),
                        Company_Id = c.String(maxLength: 128),
                        Company_Id1 = c.String(maxLength: 128),
                        Company_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Companies", "Website", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "OpeningHours", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Number", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "PostalCode", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Companies", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Promotions", "Company_Id2");
            CreateIndex("dbo.Promotions", "Company_Id1");
            CreateIndex("dbo.Promotions", "Company_Id");
            CreateIndex("dbo.Companies", "UserId");
            AddForeignKey("dbo.Companies", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Promotions", "Company_Id2", "dbo.Companies", "Id");
            AddForeignKey("dbo.Promotions", "Company_Id1", "dbo.Companies", "Id");
            AddForeignKey("dbo.Promotions", "Company_Id", "dbo.Companies", "Id");
        }
    }
}
