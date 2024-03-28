using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raag_api.Migrations
{
    /// <inheritdoc />
    public partial class AddConditionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    ImpactedCondition = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: true),
                    CurrentCondition = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    TargetCond2027 = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    TargetCondLongTerm = table.Column<string>(type: "nvarchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conditions");
        }
    }
}
