using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class JoinStationAndTimeSeries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "TimeSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSeries_StationId",
                table: "TimeSeries",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSeries_Stations_StationId",
                table: "TimeSeries",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSeries_Stations_StationId",
                table: "TimeSeries");

            migrationBuilder.DropIndex(
                name: "IX_TimeSeries_StationId",
                table: "TimeSeries");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "TimeSeries");
        }
    }
}
