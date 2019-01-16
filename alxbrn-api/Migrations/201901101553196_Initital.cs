namespace alxbrn_api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initital : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.AuthorizedApps",
            //    c => new
            //    {
            //        AuthorizedAppId = c.Int(nullable: false, identity: true),
            //        Name = c.String(nullable: false),
            //        AppToken = c.String(nullable: false, maxLength: 32),
            //        AppSecret = c.String(nullable: false, maxLength: 32),
            //        TokenExpiration = c.DateTime(nullable: false),
            //    })
            //    .PrimaryKey(t => t.AuthorizedAppId);

            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Name = c.String(nullable: false, maxLength: 32),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.CategoryItems",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Name = c.String(nullable: false, maxLength: 32),
            //        CategoryId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Comments",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Title = c.String(nullable: false, maxLength: 32),
            //        Description = c.String(nullable: false),
            //        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        ProductId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Coupons",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Name = c.String(nullable: false, maxLength: 12),
            //        Amount = c.Int(nullable: false),
            //        Percentage = c.Int(nullable: false),
            //        UsagesCount = c.Int(nullable: false),
            //        UsagesAllowed = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Employees",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Email = c.String(nullable: false),
            //        Password = c.String(nullable: false),
            //        Firstname = c.String(nullable: false),
            //        Lastname = c.String(nullable: false),
            //        HireDate = c.DateTime(nullable: false),
            //        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        RoleId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Histories",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Title = c.String(nullable: false, maxLength: 256),
            //        Date = c.DateTime(nullable: false),
            //        EmployeeId = c.Int(nullable: false),
            //        HistoryActionId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.HistoryActions",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Name = c.String(nullable: false, maxLength: 16),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.OrderLines",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        UnitId = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        UnitAmount = c.Int(nullable: false),
            //        UnitWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        TotalWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        OrderId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Orders",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        TotalWCoupon = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        TotalWOCoupon = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        TrackingNumber = c.String(maxLength: 64),
            //        CouponId = c.Int(nullable: false),
            //        Country = c.String(nullable: false, maxLength: 1024),
            //        Region = c.String(nullable: false, maxLength: 1024),
            //        Address = c.String(nullable: false, maxLength: 1024),
            //        OrderedDate = c.DateTime(nullable: false),
            //        EstimatedDate = c.DateTime(nullable: false),
            //        ShippedDate = c.DateTime(nullable: false),
            //        StatusId = c.Int(nullable: false),
            //        UserId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.OrderStatus",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Name = c.String(maxLength: 16),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Products",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Image = c.String(),
            //        Title = c.String(nullable: false, maxLength: 64),
            //        Description = c.String(nullable: false),
            //        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
            //        Amount = c.Int(nullable: false),
            //        CategoryItemId = c.Int(nullable: false),
            //        EmployeeId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.Id);

            //CreateTable(
            //    "dbo.Roles",
            //    c => new
            //    {
            //        Id = c.Int(nullable: false, identity: true),
            //        Name = c.String(nullable: false, maxLength: 16),
            //    })
            //    .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(maxLength: 128),
                    Username = c.String(nullable: false, maxLength: 32),
                    Password = c.String(nullable: false, maxLength: 64),
                    Firstname = c.String(maxLength: 256),
                    Lastname = c.String(),
                    Address1 = c.String(nullable: false, maxLength: 256),
                    Address2 = c.String(maxLength: 256),
                    Phone = c.String(maxLength: 16),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Products");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderLines");
            DropTable("dbo.HistoryActions");
            DropTable("dbo.Histories");
            DropTable("dbo.Employees");
            DropTable("dbo.Coupons");
            DropTable("dbo.Comments");
            DropTable("dbo.CategoryItems");
            DropTable("dbo.Categories");
            DropTable("dbo.AuthorizedApps");
        }
    }
}
