using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class ads : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    BackgroundColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPromotion = table.Column<bool>(type: "bit", nullable: false),
                    EndPromotion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromoPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ads", x => x.Id);
                });

            var sql = "INSERT INTO Ads\r\n     VALUES\r\n           (0, 1, '#0c71c3', 1, \r\n           'https://geekon.edu.pl/wp-content/uploads/2023/06/2.jpg',\r\n           'Kurs SQL Master',\r\n           '14-tygodniowy kurs online, który od zupełnych podstaw poprowadzi Cię za rękę przez świat baz danych i języka SQL. Wystarczy, że umiesz używać myszki i klawiatury, całej reszty Cię nauczę. Kurs zawiera dodatkowe moduły przygotowujące do rozmowy kwalifikacyjnej, a także specjalne materiały dla analityków, testerów oraz programistów.',\r\n           'https://geekon.edu.pl/produkt/sql-master/',\r\n           497.00,\r\n           1,\r\n           '2026-03-01',\r\n           747.00)";
            migrationBuilder.Sql(sql);

            sql = "INSERT INTO Ads\r\n     VALUES\r\n           (1, 1, '#0c71c3', 1, \r\n           'https://geekon.edu.pl/wp-content/uploads/2023/06/2.jpg',\r\n           'Kurs SQL Master',\r\n           '14-tygodniowy kurs online, który od zupełnych podstaw poprowadzi Cię za rękę przez świat baz danych i języka SQL. Wystarczy, że umiesz używać myszki i klawiatury, całej reszty Cię nauczę. Kurs zawiera dodatkowe moduły przygotowujące do rozmowy kwalifikacyjnej, a także specjalne materiały dla analityków, testerów oraz programistów.',\r\n           'https://geekon.edu.pl/produkt/sql-master/',\r\n           497.00,\r\n           0,\r\n           '2026-03-01',\r\n           747.00)";
            migrationBuilder.Sql(sql);

            sql = "INSERT INTO Ads\r\n     VALUES\r\n           (1, 1, '#0c71c3', 1, \r\n           'https://geekon.edu.pl/wp-content/uploads/2023/06/2.jpg',\r\n           'Kurs SQL Master',\r\n           '14-tygodniowy kurs online, który od zupełnych podstaw poprowadzi Cię za rękę przez świat baz danych i języka SQL. Wystarczy, że umiesz używać myszki i klawiatury, całej reszty Cię nauczę. Kurs zawiera dodatkowe moduły przygotowujące do rozmowy kwalifikacyjnej, a także specjalne materiały dla analityków, testerów oraz programistów.',\r\n           'https://geekon.edu.pl/produkt/sql-master/',\r\n           497.00,\r\n           1,\r\n           '2026-03-01',\r\n           747.00)";
            migrationBuilder.Sql(sql);

            sql = "INSERT INTO Ads\r\n     VALUES\r\n           (1, 1, '#0c71c3', 1, \r\n           'https://geekon.edu.pl/wp-content/uploads/2023/06/2.jpg',\r\n           'Kurs SQL Master',\r\n           '14-tygodniowy kurs online, który od zupełnych podstaw poprowadzi Cię za rękę przez świat baz danych i języka SQL. Wystarczy, że umiesz używać myszki i klawiatury, całej reszty Cię nauczę. Kurs zawiera dodatkowe moduły przygotowujące do rozmowy kwalifikacyjnej, a także specjalne materiały dla analityków, testerów oraz programistów.',\r\n           'https://geekon.edu.pl/produkt/sql-master/',\r\n           497.00,\r\n           0,\r\n           '2026-03-01',\r\n           747.00)";
            migrationBuilder.Sql(sql);

            sql = "INSERT INTO Ads\r\n     VALUES\r\n           (1, 1, '#0c71c3', 1, \r\n           'https://geekon.edu.pl/wp-content/uploads/2023/06/2.jpg',\r\n           'Kurs SQL Master',\r\n           '14-tygodniowy kurs online, który od zupełnych podstaw poprowadzi Cię za rękę przez świat baz danych i języka SQL. Wystarczy, że umiesz używać myszki i klawiatury, całej reszty Cię nauczę. Kurs zawiera dodatkowe moduły przygotowujące do rozmowy kwalifikacyjnej, a także specjalne materiały dla analityków, testerów oraz programistów.',\r\n           'https://geekon.edu.pl/produkt/sql-master/',\r\n           497.00,\r\n           1,\r\n           '2026-03-01',\r\n           747.00)";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ads");
        }
    }
}
