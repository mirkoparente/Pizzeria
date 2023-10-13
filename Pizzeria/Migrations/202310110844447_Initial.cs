namespace Pizzeria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articoli",
                c => new
                    {
                        IdArticoli = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 100),
                        Foto = c.String(),
                        Prezzo = c.Decimal(storeType: "money"),
                        TempoConsegna = c.Int(),
                        Ingredienti = c.String(),
                    })
                .PrimaryKey(t => t.IdArticoli);
            
            CreateTable(
                "dbo.DettagliOrdine",
                c => new
                    {
                        IdDettagli = c.Int(nullable: false, identity: true),
                        IdOrdini = c.Int(nullable: false),
                        IdArticoli = c.Int(nullable: false),
                        Quantita = c.Int(),
                    })
                .PrimaryKey(t => t.IdDettagli)
                .ForeignKey("dbo.Ordini", t => t.IdOrdini)
                .ForeignKey("dbo.Articoli", t => t.IdArticoli)
                .Index(t => t.IdOrdini)
                .Index(t => t.IdArticoli);
            
            CreateTable(
                "dbo.Ordini",
                c => new
                    {
                        IdOrdini = c.Int(nullable: false, identity: true),
                        DataOrdine = c.DateTime(),
                        Stato = c.String(maxLength: 50),
                        IndirizzoSpedizione = c.String(nullable: false),
                        Note = c.String(),
                        IdUtenti = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdOrdini)
                .ForeignKey("dbo.Utenti", t => t.IdUtenti)
                .Index(t => t.IdUtenti);
            
            CreateTable(
                "dbo.Utenti",
                c => new
                    {
                        IdUtenti = c.Int(nullable: false, identity: true),
                        Nome = c.String(maxLength: 50),
                        Cognome = c.String(maxLength: 50),
                        Indirizzo = c.String(),
                        Citta = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                        Ruolo = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUtenti);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DettagliOrdine", "IdArticoli", "dbo.Articoli");
            DropForeignKey("dbo.Ordini", "IdUtenti", "dbo.Utenti");
            DropForeignKey("dbo.DettagliOrdine", "IdOrdini", "dbo.Ordini");
            DropIndex("dbo.Ordini", new[] { "IdUtenti" });
            DropIndex("dbo.DettagliOrdine", new[] { "IdArticoli" });
            DropIndex("dbo.DettagliOrdine", new[] { "IdOrdini" });
            DropTable("dbo.Utenti");
            DropTable("dbo.Ordini");
            DropTable("dbo.DettagliOrdine");
            DropTable("dbo.Articoli");
        }
    }
}
