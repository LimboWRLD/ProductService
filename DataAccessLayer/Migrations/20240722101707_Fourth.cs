using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiacPraksaP1.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add the OwnerId column without the foreign key constraint
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Products",
                type: "TEXT",
                nullable: true);

            // Step 2: Update existing records with a valid OwnerId
            // Replace 'default_owner_id' with a valid User Id from AspNetUsers table
            migrationBuilder.Sql("UPDATE Products SET OwnerId = 'default_owner_id' WHERE OwnerId IS NULL");

            // Step 3: Alter the column to make it not nullable
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Products",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            // Step 4: Create an index on OwnerId
            migrationBuilder.CreateIndex(
                name: "IX_Products_OwnerId",
                table: "Products",
                column: "OwnerId");

            // Step 5: Add the foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_OwnerId",
                table: "Products",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_OwnerId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OwnerId",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Products",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: false);

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Products");
        }

    }
}
