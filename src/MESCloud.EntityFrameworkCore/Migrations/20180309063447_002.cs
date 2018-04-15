using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MESCloud.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MesWMSReel_MesWMSMPN_PartNoId",
                table: "MesWMSReel");

            migrationBuilder.AlterColumn<string>(
                name: "PartNoId",
                table: "MesWMSReel",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MesWMSReel_MesWMSMPN_PartNoId",
                table: "MesWMSReel",
                column: "PartNoId",
                principalTable: "MesWMSMPN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MesWMSReel_MesWMSMPN_PartNoId",
                table: "MesWMSReel");

            migrationBuilder.AlterColumn<string>(
                name: "PartNoId",
                table: "MesWMSReel",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AddForeignKey(
                name: "FK_MesWMSReel_MesWMSMPN_PartNoId",
                table: "MesWMSReel",
                column: "PartNoId",
                principalTable: "MesWMSMPN",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
