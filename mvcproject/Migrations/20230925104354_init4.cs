using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcproject.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Education_EducationId",
                table: "Feedback");

            migrationBuilder.RenameColumn(
                name: "Answers",
                table: "Tests",
                newName: "incorrect");

            migrationBuilder.RenameColumn(
                name: "EducationId",
                table: "Feedback",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_EducationId",
                table: "Feedback",
                newName: "IX_Feedback_UserId");

            migrationBuilder.AddColumn<string>(
                name: "correct",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Task",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Task",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Result",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Feedback",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Result_UserId",
                table: "Result",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_CourseId",
                table: "Feedback",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Course_CourseId",
                table: "Feedback",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_User_UserId",
                table: "Feedback",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Result_User_UserId",
                table: "Result",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_Course_CourseId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_User_UserId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Result_User_UserId",
                table: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Result_UserId",
                table: "Result");

            migrationBuilder.DropIndex(
                name: "IX_Feedback_CourseId",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "correct",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Result");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Feedback");

            migrationBuilder.RenameColumn(
                name: "incorrect",
                table: "Tests",
                newName: "Answers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Feedback",
                newName: "EducationId");

            migrationBuilder.RenameIndex(
                name: "IX_Feedback_UserId",
                table: "Feedback",
                newName: "IX_Feedback_EducationId");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "Task",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Description",
                table: "Task",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_Education_EducationId",
                table: "Feedback",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
