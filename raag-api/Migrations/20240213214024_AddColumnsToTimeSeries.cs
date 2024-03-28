using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsToTimeSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParameterTypeId",
                table: "TimeSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ParameterTypeName",
                table: "TimeSeries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParameterTypeId",
                table: "TimeSeries");

            migrationBuilder.DropColumn(
                name: "ParameterTypeName",
                table: "TimeSeries");
        }
    }
}
