using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class ListTablesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSeries_Stations_StationId",
                table: "TimeSeries");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSeries",
                table: "TimeSeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stations",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TimeSeries");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Stations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSeries",
                table: "TimeSeries",
                column: "TimeSeriesid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stations",
                table: "Stations",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements",
                column: "TimeSeriesId",
                principalTable: "TimeSeries",
                principalColumn: "TimeSeriesid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSeries_Stations_StationId",
                table: "TimeSeries",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSeries_Stations_StationId",
                table: "TimeSeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeSeries",
                table: "TimeSeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stations",
                table: "Stations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TimeSeries",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Stations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeSeries",
                table: "TimeSeries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stations",
                table: "Stations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    ParameterTypeId = table.Column<int>(type: "int", nullable: false),
                    ParameterTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationParameterName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parameters_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_StationId",
                table: "Parameters",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurements_TimeSeries_TimeSeriesId",
                table: "Measurements",
                column: "TimeSeriesId",
                principalTable: "TimeSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSeries_Stations_StationId",
                table: "TimeSeries",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
