namespace SetareSazBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Banks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Buttons",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        Text = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                        ViewType = c.Int(nullable: false),
                        ImageUrl = c.String(maxLength: 250),
                        Row = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        Data = c.String(maxLength: 500),
                        BehaviourType = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Level = c.Int(nullable: false),
                        ProvinceId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId)
                .Index(t => t.Name)
                .Index(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LastMessageId = c.String(maxLength: 200),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChatId = c.String(maxLength: 250),
                        Title = c.String(maxLength: 250),
                        Exception = c.String(),
                        WebException = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserData",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChatId = c.String(maxLength: 250),
                        StateId = c.Long(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ChatId, unique: true);
            
            CreateTable(
                "dbo.UserInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChatId = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 100),
                        Mobile = c.String(maxLength: 11),
                        BirthDate = c.String(maxLength: 100),
                        NationalCode = c.String(maxLength: 10),
                        ProvinceId = c.Long(),
                        Province = c.String(maxLength: 100),
                        CityId = c.Long(),
                        City = c.String(maxLength: 100),
                        PopulationStatus = c.Int(),
                        Resident = c.String(),
                        Address = c.String(maxLength: 1000),
                        PostalCode = c.String(maxLength: 10),
                        PositionType = c.Int(),
                        FavoritePost = c.String(),
                        VideoSrc = c.String(),
                        Submitted = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ChatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.UserInfo", new[] { "ChatId" });
            DropIndex("dbo.UserData", new[] { "ChatId" });
            DropIndex("dbo.Provinces", new[] { "Name" });
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.Cities", new[] { "Name" });
            DropIndex("dbo.Buttons", new[] { "StateId" });
            DropTable("dbo.UserInfo");
            DropTable("dbo.UserData");
            DropTable("dbo.ExceptionLogs");
            DropTable("dbo.Configs");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.Buttons");
            DropTable("dbo.Banks");
        }
    }
}
