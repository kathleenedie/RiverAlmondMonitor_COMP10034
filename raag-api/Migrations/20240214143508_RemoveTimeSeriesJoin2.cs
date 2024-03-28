using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeSeriesJoin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements");

            migrationBuilder.DropIndex(
                name: "IX_Measurements_TimeSeriesId",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "TimeSeriesId",
                table: "Measurements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeSeriesId",
                table: "Measurements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_TimeSeriesId",
                table: "Measurements",
                column: "TimeSeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements",
                column: "TimeSeriesId",
                principalTable: "TimeSeries",
                principalColumn: "Id");
        }
    }
}
