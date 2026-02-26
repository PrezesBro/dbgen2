using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBGenerator.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Databases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Databases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatabaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Databases_DatabaseId",
                        column: x => x.DatabaseId,
                        principalTable: "Databases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataType = table.Column<int>(type: "int", nullable: false),
                    Precision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Columns_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Datas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Datas_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ForeignKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnFkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TablePkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForeignKey_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Columns_TableId",
                table: "Columns",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Datas_TableId",
                table: "Datas",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_ForeignKey_TableId",
                table: "ForeignKey",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_DatabaseId",
                table: "Tables",
                column: "DatabaseId");

            var sql = "INSERT INTO Databases(Name, Version, Description, CreateDate)\r\nVALUES('Pracownicy', 1, 'Prosta baza danych zawierająca informacje o pracownikach i projektach, w których biorą udział', GETDATE())\r\n\r\ndeclare @db_id int = (SELECT SCOPE_IDENTITY())\r\n\r\n\r\nINSERT INTO Tables(Name, DatabaseId)\r\nVALUES('Dzial', @db_id)\r\n\r\ndeclare @tbl_id int = (SELECT SCOPE_IDENTITY())\r\n\r\nINSERT INTO Columns(Name, DataType, Precision, TableId)\r\nVALUES('NazwaDzialu', 0, '30', @tbl_id),\r\n('NrPietra', 1, '', @tbl_id),\r\n('IdSzefaDzialu', 1, '', @tbl_id)\r\n\r\nINSERT INTO Datas(TableId, Value)\r\nVALUES(@tbl_id, 'Promocja;1;2'),\r\n(@tbl_id, 'Współpraca z partnerami;2;8'),\r\n(@tbl_id, 'Dział techniczny;1;10')\r\n\r\nINSERT INTO ForeignKey(TableId, TablePkName, ColumnFkName)\r\nVALUES(@tbl_id, 'Pracownik', 'IdSzefaDzialu')\r\n\r\n\r\n\r\nINSERT INTO Tables(Name, DatabaseId)\r\nVALUES('Pracownik', @db_id)\r\n\r\nSELECT @tbl_id = SCOPE_IDENTITY()\r\n\r\n\r\nINSERT INTO Columns(Name, DataType, Precision, TableId)\r\nVALUES('Imie', 0, '50', @tbl_id),\r\n('Nazwisko', 0, '50', @tbl_id),\r\n('PESEL', 0, '11', @tbl_id),\r\n('DataZatrudnienia', 3, '', @tbl_id),\r\n('Pensja', 2, '10,2', @tbl_id),\r\n('IdDzialu', 1, '', @tbl_id)\r\n\r\nINSERT INTO Datas(TableId, Value)\r\nVALUES(@tbl_id, 'Andrzej;Kowalski;83060206996;2015-05-12;2000,00;1'),\r\n(@tbl_id, 'Krzysztof;Jasiński;88010711814;1990-01-24;1800;1'),\r\n(@tbl_id, 'Anna;Nowak;78041116947;2000-11-08;1750;2'),\r\n(@tbl_id, 'Zenon;Krzysztofik;80120615533;2001-07-01;2100;1'),\r\n(@tbl_id, 'Stefan;Garczarek;95062317353;2009-10-01;1903;1'),\r\n(@tbl_id, 'Elżbieta;Nowicka;88092207824;1998-01-20;2213;2'),\r\n(@tbl_id, 'Katarzyna;Piekarska;87030717423;2004-12-01;1599;2'),\r\n(@tbl_id, 'Natalia;Ostrowska;'';2003-05-20;1842;2'),\r\n(@tbl_id, 'Maciej;Zieliński;84100601235;2006-02-09;3000;2'),\r\n(@tbl_id, 'Paweł;Kwaśnicki;'';2011-08-10;2300;3'),\r\n(@tbl_id, 'Anna;Piecińska;78052413343;2008-06-26;1978;3')\r\n\r\nINSERT INTO ForeignKey(TableId, TablePkName, ColumnFkName)\r\nVALUES(@tbl_id, 'Dzial', 'IdDzialu')\r\n\r\n\r\n\r\n\r\nINSERT INTO Tables(Name, DatabaseId)\r\nVALUES('PracownikWydarzenie', @db_id)\r\n\r\nSELECT @tbl_id = SCOPE_IDENTITY()\r\n\r\n\r\nINSERT INTO Columns(Name, DataType, Precision, TableId)\r\nVALUES('IdPracownika', 1, '', @tbl_id),\r\n('IdWydarzenia', 1, '', @tbl_id)\r\n\r\nINSERT INTO Datas(TableId, Value)\r\nVALUES(@tbl_id, '1;1'),\r\n(@tbl_id, '1;2'),\r\n(@tbl_id, '1;5'),\r\n(@tbl_id, '2;1'),\r\n(@tbl_id, '2;3'),\r\n(@tbl_id, '2;6'),\r\n(@tbl_id, '3;1'),\r\n(@tbl_id, '3;5'),\r\n(@tbl_id, '4;2'),\r\n(@tbl_id, '5;4'),\r\n(@tbl_id, '6;2'),\r\n(@tbl_id, '6;4'),\r\n(@tbl_id, '7;2'),\r\n(@tbl_id, '7;4'),\r\n(@tbl_id, '8;4'),\r\n(@tbl_id, '8;6'),\r\n(@tbl_id, '9;3'),\r\n(@tbl_id, '9;6'),\r\n(@tbl_id, '10;1'),\r\n(@tbl_id, '10;2'),\r\n(@tbl_id, '10;5'),\r\n(@tbl_id, '10;6'),\r\n(@tbl_id, '11;3'),\r\n(@tbl_id, '11;4'),\r\n(@tbl_id, '11;6')\r\n\r\nINSERT INTO ForeignKey(TableId, TablePkName, ColumnFkName)\r\nVALUES(@tbl_id, 'Pracownik', 'IdPracownika'),\r\n(@tbl_id, 'Wydarzenia', 'IdWydarzenia')\r\n\r\n\r\n\r\nINSERT INTO Tables(Name, DatabaseId)\r\nVALUES('Wydarzenia', @db_id)\r\n\r\nSELECT @tbl_id = SCOPE_IDENTITY()\r\n\r\nINSERT INTO Columns(Name, DataType, Precision, TableId)\r\nVALUES('Nazwa', 0, '50', @tbl_id),\r\n('DataWydarzenia', 3, '', @tbl_id),\r\n('Miejsce', 0, '50', @tbl_id)\r\n\r\nINSERT INTO Datas(TableId, Value)\r\nVALUES(@tbl_id, 'Dzień dziecka;2015-06-01;Wrocław, Rynek'),\r\n(@tbl_id, 'Dzień matki;2015-05-26;Wrocław, Galeria Dominikańska'),\r\n(@tbl_id, 'Kino plenerowe;2015-07-14;Warszawa, Lotnisko Bemowo'),\r\n(@tbl_id, 'Muzyczna wyspa;2015-08-15;Wrocław, Wyspa Słodowa'),\r\n(@tbl_id, 'Noc kabaretowa;2015-08-04;Koszalin, Amfiteatr'),\r\n(@tbl_id, 'Wakacyjna piosenka dla dzieci;2015-07-26;Koszalin, Amfiteatr')";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Datas");

            migrationBuilder.DropTable(
                name: "ForeignKey");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Databases");
        }
    }
}
