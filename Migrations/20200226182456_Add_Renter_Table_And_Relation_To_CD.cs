using Microsoft.EntityFrameworkCore.Migrations;

namespace CompactDiscProject.Migrations
{
    public partial class Add_Renter_Table_And_Relation_To_CD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Renter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    CompactDiscId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renter_CompactDisc_CompactDiscId",
                        column: x => x.CompactDiscId,
                        principalTable: "CompactDisc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Renter_CompactDiscId",
                table: "Renter",
                column: "CompactDiscId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Renter");
        }
    }
}
