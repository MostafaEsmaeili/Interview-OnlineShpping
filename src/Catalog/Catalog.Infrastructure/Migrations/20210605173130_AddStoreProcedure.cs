using Microsoft.EntityFrameworkCore.Migrations;

namespace Catalog.Infrastructure.Migrations
{
    public partial class AddStoreProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE dbo.GetProductById
                                            @Id AS UNIQUEIDENTIFIER
                                        AS
                                        BEGIN
                                            SELECT Id,
                                                   CategoryId,
                                                   Name,
                                                   Price,
                                                   Description,
                                                   Tags,
                                                   [Order],
                                                   Enabled,
                                                   CreatedBy,
                                                   ModifiedBy,
                                                   ModifiedAt,
                                                   CreatedAt FROM dbo.Product WHERE Id = @Id
                                        END
                                        GO
                                        ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.GetProductById");
        }
    }
}
