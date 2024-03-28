using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveParameterJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSeries_Parameters_ParameterId",
                table: "TimeSeries");

            migrationBuilder.DropIndex(
                name: "IX_TimeSeries_ParameterId",
                table: "TimeSeries");

            migrationBuilder.DropColumn(
                name: "ParameterId",
                table: "TimeSeries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParameterId",
                table: "TimeSeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSeries_ParameterId",
                table: "TimeSeries",
                column: "ParameterId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSeries_Parameters_ParameterId",
                table: "TimeSeries",
                column: "ParameterId",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
