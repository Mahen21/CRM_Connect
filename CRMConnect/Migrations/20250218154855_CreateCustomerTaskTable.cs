using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRMConnect.Migrations
{
    /// <inheritdoc />
    public partial class CreateCustomerTaskTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Customers_CustomerId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "CustomerTasks");

            migrationBuilder.RenameColumn(
                name: "LogId",
                table: "CommunicationLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "CustomerTasks",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_CustomerId",
                table: "CustomerTasks",
                newName: "IX_CustomerTasks_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerTasks",
                table: "CustomerTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTasks_Customers_CustomerId",
                table: "CustomerTasks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTasks_Customers_CustomerId",
                table: "CustomerTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerTasks",
                table: "CustomerTasks");

            migrationBuilder.RenameTable(
                name: "CustomerTasks",
                newName: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CommunicationLogs",
                newName: "LogId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tasks",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerTasks_CustomerId",
                table: "Tasks",
                newName: "IX_Tasks_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Customers_CustomerId",
                table: "Tasks",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
