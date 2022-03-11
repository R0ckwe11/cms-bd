using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_bd.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Config_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ImageMetadata",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageMetadata", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ImageMetadata_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsArchived = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tags_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVisible = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Coupons_ImageMetadata_ImageID",
                        column: x => x.ImageID,
                        principalTable: "ImageMetadata",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Coupons_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVisible = table.Column<int>(type: "int", nullable: false),
                    IsInMenu = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Posts_ImageMetadata_ImageID",
                        column: x => x.ImageID,
                        principalTable: "ImageMetadata",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Posts_Users_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TagCouponPivot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCouponPivot", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TagCouponPivot_Coupons_CouponID",
                        column: x => x.CouponID,
                        principalTable: "Coupons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TagCouponPivot_Tags_TagID",
                        column: x => x.TagID,
                        principalTable: "Tags",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UsedCoupons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    UsedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCoupons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsedCoupons_Coupons_CouponID",
                        column: x => x.CouponID,
                        principalTable: "Coupons",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UsedCoupons_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Config_UpdatedBy",
                table: "Config",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_ImageID",
                table: "Coupons",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_UpdatedBy",
                table: "Coupons",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ImageMetadata_CreatedBy",
                table: "ImageMetadata",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImageID",
                table: "Posts",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UpdatedBy",
                table: "Posts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TagCouponPivot_CouponID",
                table: "TagCouponPivot",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_TagCouponPivot_TagID",
                table: "TagCouponPivot",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UpdatedBy",
                table: "Tags",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCoupons_CouponID",
                table: "UsedCoupons",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCoupons_UserID",
                table: "UsedCoupons",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "TagCouponPivot");

            migrationBuilder.DropTable(
                name: "UsedCoupons");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "ImageMetadata");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
