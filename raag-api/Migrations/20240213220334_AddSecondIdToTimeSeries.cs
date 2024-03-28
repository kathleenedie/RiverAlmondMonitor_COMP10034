using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondIdToTimeSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSeriesid",
                table: "TimeSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSeriesid",
                table: "TimeSeries");
        }
    }
}
