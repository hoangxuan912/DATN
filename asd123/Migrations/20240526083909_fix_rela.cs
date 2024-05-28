using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asd123.Migrations
{
    /// <inheritdoc />
    public partial class fix_rela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Subjects",
                newName: "MajorId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_DepartmentId",
                table: "Subjects",
                newName: "IX_Subjects_MajorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Majors_MajorId",
                table: "Subjects",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Majors_MajorId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "Subjects",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_MajorId",
                table: "Subjects",
                newName: "IX_Subjects_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Departments_DepartmentId",
                table: "Subjects",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
