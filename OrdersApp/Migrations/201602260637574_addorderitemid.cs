namespace OrdersApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addorderitemid : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.OrderItems", new[] { "Order_OrderNumber" });
            DropColumn("dbo.OrderItems", "OrderNumber");
            RenameColumn(table: "dbo.OrderItems", name: "Order_OrderNumber", newName: "OrderNumber");
            DropPrimaryKey("dbo.OrderItems");
            AddColumn("dbo.OrderItems", "OrderItemID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.OrderItems", "OrderNumber", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.OrderItems", "OrderItemID");
            CreateIndex("dbo.OrderItems", "OrderNumber");
            DropTable("dbo.CartItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        OrderNumber = c.String(nullable: false, maxLength: 128),
                        Quantity = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderNumber);
            
            DropIndex("dbo.OrderItems", new[] { "OrderNumber" });
            DropPrimaryKey("dbo.OrderItems");
            AlterColumn("dbo.OrderItems", "OrderNumber", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.OrderItems", "OrderItemID");
            AddPrimaryKey("dbo.OrderItems", "OrderNumber");
            RenameColumn(table: "dbo.OrderItems", name: "OrderNumber", newName: "Order_OrderNumber");
            AddColumn("dbo.OrderItems", "OrderNumber", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.OrderItems", "Order_OrderNumber");
        }
    }
}
