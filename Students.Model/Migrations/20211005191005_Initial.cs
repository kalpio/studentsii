using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Students.Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COURSE",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NUMBER = table.Column<long>(maxLength: 64, nullable: false),
                    NAME = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COURSE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "STUDENT",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIRST_NAME = table.Column<string>(maxLength: 64, nullable: false),
                    LAST_NAME = table.Column<string>(maxLength: 128, nullable: false),
                    DATE_OF_BIRTH = table.Column<DateTime>(nullable: false),
                    PERSONAL_ID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STUDENT", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ENROLLMENT",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STUDENT_ID = table.Column<long>(nullable: false),
                    COURSE_ID = table.Column<long>(nullable: false),
                    ENROLLMENT_DATE = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2021, 10, 5, 21, 10, 5, 229, DateTimeKind.Local).AddTicks(8846))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENROLLMENT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ENROLLMENT_COURSE_COURSE_ID",
                        column: x => x.COURSE_ID,
                        principalTable: "COURSE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ENROLLMENT_STUDENT_STUDENT_ID",
                        column: x => x.STUDENT_ID,
                        principalTable: "STUDENT",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ENROLLMENT_COURSE_ID",
                table: "ENROLLMENT",
                column: "COURSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ENROLLMENT_STUDENT_ID",
                table: "ENROLLMENT",
                column: "STUDENT_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_STUDENT_PERSONAL_ID",
                table: "STUDENT",
                column: "PERSONAL_ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENROLLMENT");

            migrationBuilder.DropTable(
                name: "COURSE");

            migrationBuilder.DropTable(
                name: "STUDENT");
        }
    }
}
