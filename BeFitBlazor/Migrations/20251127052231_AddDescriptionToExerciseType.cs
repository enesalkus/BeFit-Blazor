using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFitBlazor.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToExerciseType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ExerciseTypes",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ExerciseTypes");
        }
    }
}
