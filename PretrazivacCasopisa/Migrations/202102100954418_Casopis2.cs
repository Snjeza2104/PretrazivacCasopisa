namespace PretrazivacCasopisa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Casopis2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Casopis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Casopis");
        }
    }
}
