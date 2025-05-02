using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VotingFrontEnd.Data.Migrations
{
    /// <inheritdoc />
    public partial class VotingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoteCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Name", "PhotoUrl", "VoteCount" },
                values: new object[,]
                {
                    { 3, "Allen Iverson", "ai.jpg", 0 },
                    { 6, "Bill Russell", "billrussell.jpg", 0 },
                    { 21, "Tim Duncan", "timduncan.jpg", 0 },
                    { 30, "Stephen Curry", "curry.jpg", 0 },
                    { 34, "Hakeem Olajuwon", "thedream.jpg", 0 },
                    { 41, "Dirk Nowitzki", "dirk.jpg", 0 },
                    { 824, "Kobe Bryant", "kobe.jpg", 0 },
                    { 2306, "LeBron James", "lebron.jpg", 0 },
                    { 2345, "Michael Jordan", "mj.jpg", 0 },
                    { 3201, "Magic Johnson", "magic.jpg", 0 },
                    { 3202, "Karl Malone", "mailman.jpg", 0 },
                    { 3301, "Kareem Abdul-Jabbar", "kareem.jpg", 0 },
                    { 3302, "Larry Bird", "bird.jpg", 0 },
                    { 3432, "Shaquille O'Neal", "shaq.jpg", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
