using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_View_AdminEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW AdminEmails AS SELECT Email FROM ApplicationUsers WHERE Role = 'admin'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS AdminEmails");
        }
    }
}
