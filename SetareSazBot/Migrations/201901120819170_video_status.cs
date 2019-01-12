namespace SetareSazBot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class video_status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfo", "VideoStatus", c => c.String(maxLength: 1000));
            AlterColumn("dbo.UserInfo", "Resident", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserInfo", "FavoritePost", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfo", "FavoritePost", c => c.String());
            AlterColumn("dbo.UserInfo", "Resident", c => c.String());
            DropColumn("dbo.UserInfo", "VideoStatus");
        }
    }
}
