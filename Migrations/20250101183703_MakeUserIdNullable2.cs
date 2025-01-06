using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce_Databaser_i_ett_sammanhang.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserIdNullable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid?>(
                name: "UserId",
                table: "Addresses",
                type: "uuid",
                nullable: true, // Allow NULL values
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Addresses",
                type: "uuid",
                nullable: false, // Revert to NOT NULL
                oldClrType: typeof(Guid?),
                oldType: "uuid");
        }

    }
}
