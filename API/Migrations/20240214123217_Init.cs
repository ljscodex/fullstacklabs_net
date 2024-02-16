using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Attack = table.Column<int>(type: "INTEGER", nullable: false),
                    Defense = table.Column<int>(type: "INTEGER", nullable: false),
                    Hp = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Battle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MonsterA = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterB = table.Column<int>(type: "INTEGER", nullable: false),
                    Winner = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battle_Monster_MonsterA",
                        column: x => x.MonsterA,
                        principalTable: "Monster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battle_Monster_MonsterB",
                        column: x => x.MonsterB,
                        principalTable: "Monster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battle_Monster_Winner",
                        column: x => x.Winner,
                        principalTable: "Monster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Monster",
                columns: new[] { "Id", "Attack", "Defense", "Hp", "ImageUrl", "Name", "Speed" },
                values: new object[,]
                {
                    { 1, 60, 40, 10, "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/dead-unicorn.png", "Dead Unicorn", 80 },
                    { 2, 50, 20, 80, "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/old-shark.png", "Old Shark", 90 },
                    { 3, 90, 80, 90, "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/red-dragon.png", "Red Dragon", 70 },
                    { 4, 50, 40, 80, "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/robot-bear.png", "Robot Bear", 60 },
                    { 5, 80, 20, 70, "https://fsl-assessment-public-files.s3.amazonaws.com/assessment-cc-01/angry-snake.png", "Angry Snake", 80 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battle_MonsterA",
                table: "Battle",
                column: "MonsterA");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_MonsterB",
                table: "Battle",
                column: "MonsterB");

            migrationBuilder.CreateIndex(
                name: "IX_Battle_Winner",
                table: "Battle",
                column: "Winner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battle");

            migrationBuilder.DropTable(
                name: "Monster");
        }
    }
}
