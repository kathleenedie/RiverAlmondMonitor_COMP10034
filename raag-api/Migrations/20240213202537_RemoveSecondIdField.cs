using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSecondIdField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSeriesId",
                table: "TimeSeries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSeriesId",
                table: "TimeSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
