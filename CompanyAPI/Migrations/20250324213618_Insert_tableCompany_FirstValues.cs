using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyAPI.Migrations
{
    /// <inheritdoc />
    public partial class Insert_tableCompany_FirstValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [CompanyDb].[dbo].[Companies]
                (
                [Name], [StockTicker], [Exchange], [Isin], [WebsiteUrl]
                )
                VALUES
                (
                'Apple INC.', 'AAPL', 'NASDAQ', 'US0378331005', 'https://www.apple.com'
                )");
            migrationBuilder.Sql(@"INSERT INTO [CompanyDb].[dbo].[Companies]
                (
                [Name], [StockTicker], [Exchange], [Isin], [WebsiteUrl]
                )
                VALUES
                (
                'British Airways Plc', 'BAIRY', 'Pink Sheets', 'US1104193065', ''
                )");
            migrationBuilder.Sql(@"INSERT INTO [CompanyDb].[dbo].[Companies]
                (
                [Name], [StockTicker], [Exchange], [Isin], [WebsiteUrl]
                )
                VALUES
                (
                'Heineken NV', 'HEIA', 'Euronext Amsterdam', 'NL0000009165', ''
                )");
            migrationBuilder.Sql(@"INSERT INTO [CompanyDb].[dbo].[Companies]
                (
                [Name], [StockTicker], [Exchange], [Isin], [WebsiteUrl]
                )
                VALUES
                (
                'Panasonic Corp', '6752', 'Tokyo Stock Exchange', 'JP3866800000', 'http://www.panasonic.co.jp'
                )");
            migrationBuilder.Sql(@"INSERT INTO [CompanyDb].[dbo].[Companies]
                (
                [Name], [StockTicker], [Exchange], [Isin], [WebsiteUrl]
                )
                VALUES
                (
                'Porsche Automobil', 'PAH3', 'Deutsche Börse', 'DE000PAH0038', 'https://www.porsche.com/'
                )");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
