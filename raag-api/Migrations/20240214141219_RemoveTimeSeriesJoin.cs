using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeSeriesJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSeriesId",
                table: "Measurements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements",
                column: "TimeSeriesId",
                principalTable: "TimeSeries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements");

            migrationBuilder.AlterColumn<int>(
                name: "TimeSeriesId",
                table: "Measurements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements",
                column: "TimeSeriesId",
                principalTable: "TimeSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
