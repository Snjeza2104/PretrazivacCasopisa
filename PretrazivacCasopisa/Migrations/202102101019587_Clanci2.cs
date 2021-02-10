namespace PretrazivacCasopisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clanci2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clancis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naslov = c.String(nullable: false, maxLength: 100),
                        CasopisID = c.Int(nullable: false),
                        broj = c.Int(nullable: false),
                        AutorID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Autoris", t => t.AutorID, cascadeDelete: true)
                .ForeignKey("dbo.Casopis", t => t.CasopisID, cascadeDelete: true)
                .Index(t => t.CasopisID)
                .Index(t => t.AutorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clancis", "CasopisID", "dbo.Casopis");
            DropForeignKey("dbo.Clancis", "AutorID", "dbo.Autoris");
            DropIndex("dbo.Clancis", new[] { "AutorID" });
            DropIndex("dbo.Clancis", new[] { "CasopisID" });
            DropTable("dbo.Clancis");
        }
    }
}
