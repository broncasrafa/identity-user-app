using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserToDatabaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_ROLE",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_ROLE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_USER",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(1)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true),                    
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),                    
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),                    
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),                    
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_ROLE_CLAIMS",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_ROLE_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_IDENTITY_ROLE_CLAIMS_TB_IDENTITY_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "TB_IDENTITY_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_USER_CLAIMS",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_USER_CLAIMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_IDENTITY_USER_CLAIMS_TB_IDENTITY_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "TB_IDENTITY_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_USER_LOGINS",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_USER_LOGINS", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TB_IDENTITY_USER_LOGINS_TB_IDENTITY_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "TB_IDENTITY_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_USER_ROLES",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_USER_ROLES", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TB_IDENTITY_USER_ROLES_TB_IDENTITY_ROLE_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "TB_IDENTITY_ROLE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_IDENTITY_USER_ROLES_TB_IDENTITY_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "TB_IDENTITY_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_IDENTITY_USER_TOKENS",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_IDENTITY_USER_TOKENS", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TB_IDENTITY_USER_TOKENS_TB_IDENTITY_USER_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "TB_IDENTITY_USER",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "TB_IDENTITY_ROLE",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_IDENTITY_ROLE_CLAIMS_RoleId",
                schema: "dbo",
                table: "TB_IDENTITY_ROLE_CLAIMS",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "TB_IDENTITY_USER",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "TB_IDENTITY_USER",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_IDENTITY_USER_CLAIMS_UserId",
                schema: "dbo",
                table: "TB_IDENTITY_USER_CLAIMS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_IDENTITY_USER_LOGINS_UserId",
                schema: "dbo",
                table: "TB_IDENTITY_USER_LOGINS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_IDENTITY_USER_ROLES_RoleId",
                schema: "dbo",
                table: "TB_IDENTITY_USER_ROLES",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_IDENTITY_ROLE_CLAIMS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_IDENTITY_USER_CLAIMS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_IDENTITY_USER_LOGINS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_IDENTITY_USER_ROLES",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_IDENTITY_USER_TOKENS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_IDENTITY_ROLE",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TB_IDENTITY_USER",
                schema: "dbo");
        }
    }
}
