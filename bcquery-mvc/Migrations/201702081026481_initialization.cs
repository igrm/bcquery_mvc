namespace bcquery_mvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Translation.Language",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        LanguageCode = c.String(nullable: false, maxLength: 3),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "Translation.TranslationItem",
                c => new
                    {
                        TranslationItemID = c.Int(nullable: false, identity: true),
                        TranslationItemCode = c.String(nullable: false, maxLength: 255),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TranslationItemID);
            
            CreateTable(
                "Translation.TranslationText",
                c => new
                    {
                        TranslationTextID = c.Int(nullable: false, identity: true),
                        TranslationTextValue = c.String(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        TranslationItemID = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.TranslationTextID)
                .ForeignKey("Translation.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("Translation.TranslationItem", t => t.TranslationItemID, cascadeDelete: true)
                .Index(t => t.LanguageID)
                .Index(t => t.TranslationItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Translation.TranslationText", "TranslationItemID", "Translation.TranslationItem");
            DropForeignKey("Translation.TranslationText", "LanguageID", "Translation.Language");
            DropIndex("Translation.TranslationText", new[] { "TranslationItemID" });
            DropIndex("Translation.TranslationText", new[] { "LanguageID" });
            DropTable("Translation.TranslationText");
            DropTable("Translation.TranslationItem");
            DropTable("Translation.Language");
        }
    }
}
