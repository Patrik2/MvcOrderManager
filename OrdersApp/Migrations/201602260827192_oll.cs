namespace OrdersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oll : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(maxLength: 128),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.Products", t => t.Product_ItemId)
                .ForeignKey("dbo.Orders", t => t.OrderNumber)
                .Index(t => t.OrderNumber)
                .Index(t => t.Product_ItemId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderNumber = c.String(nullable: false, maxLength: 128),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderNumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "OrderNumber", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "Product_ItemId", "dbo.Products");
            DropIndex("dbo.OrderItems", new[] { "Product_ItemId" });
            DropIndex("dbo.OrderItems", new[] { "OrderNumber" });
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.OrderItems");
        }
    }
}
