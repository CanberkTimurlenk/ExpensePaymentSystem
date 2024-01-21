using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCase.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Procedure_CompletePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE CompletePayment                                        
                    @Id INT
                AS
                BEGIN                                
                    UPDATE Payments
                    SET IsCompleted = 1
                    WHERE Id = @Id;
                END;"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CompletePayment");
        }
    }
}
