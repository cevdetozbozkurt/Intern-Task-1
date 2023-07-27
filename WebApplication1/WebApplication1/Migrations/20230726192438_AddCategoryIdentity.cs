using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AlterColumn<int>(
			name: "Id",
			table: "Categories",
			type: "int",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "int")
			/*.Annotation("SqlServer:Identity", "1, 1")*/;
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AlterColumn<int>(
			name: "Id",
			table: "Categories",
			type: "int",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "int")
			.OldAnnotation("SqlServer:Identity", "1, 1");
		}
    }
}
