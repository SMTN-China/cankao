using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MESCloud.Migrations
{
    public partial class ini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpAuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    CustomData = table.Column<string>(maxLength: 2000, nullable: true),
                    Exception = table.Column<string>(maxLength: 2000, nullable: true),
                    ExecutionDuration = table.Column<int>(nullable: false),
                    ExecutionTime = table.Column<DateTime>(nullable: false),
                    ImpersonatorTenantId = table.Column<int>(nullable: true),
                    ImpersonatorUserId = table.Column<long>(nullable: true),
                    MethodName = table.Column<string>(maxLength: 256, nullable: true),
                    Parameters = table.Column<string>(maxLength: 1024, nullable: true),
                    ServiceName = table.Column<string>(maxLength: 256, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpAuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpBackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsAbandoned = table.Column<bool>(nullable: false),
                    JobArgs = table.Column<string>(maxLength: 1048576, nullable: false),
                    JobType = table.Column<string>(maxLength: 512, nullable: false),
                    LastTryTime = table.Column<DateTime>(nullable: true),
                    NextTryTime = table.Column<DateTime>(nullable: false),
                    Priority = table.Column<byte>(nullable: false),
                    TryCount = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpBackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpEditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityChangeSets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    ExtensionData = table.Column<string>(nullable: true),
                    ImpersonatorTenantId = table.Column<int>(nullable: true),
                    ImpersonatorUserId = table.Column<long>(nullable: true),
                    Reason = table.Column<string>(maxLength: 256, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityChangeSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    Icon = table.Column<string>(maxLength: 128, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpLanguageTexts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Key = table.Column<string>(maxLength: 256, nullable: false),
                    LanguageName = table.Column<string>(maxLength: 10, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Source = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(maxLength: 67108864, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpLanguageTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Data = table.Column<string>(maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    ExcludedUserIds = table.Column<string>(maxLength: 131072, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: false),
                    Severity = table.Column<byte>(nullable: false),
                    TenantIds = table.Column<string>(maxLength: 131072, nullable: true),
                    UserIds = table.Column<string>(maxLength: 131072, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpNotificationSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpNotificationSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 95, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 128, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpOrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenantNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Data = table.Column<string>(maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(maxLength: 96, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    NotificationName = table.Column<string>(maxLength: 96, nullable: false),
                    Severity = table.Column<byte>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenantNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UserLinkId = table.Column<long>(nullable: true),
                    UserName = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLoginAttempts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrowserInfo = table.Column<string>(maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Result = table.Column<byte>(nullable: false),
                    TenancyName = table.Column<string>(maxLength: 64, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    UserNameOrEmailAddress = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLoginAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    TenantNotificationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserOrganizationUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AuthenticationSource = table.Column<string>(maxLength: 64, nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 256, nullable: false),
                    EmailConfirmationCode = table.Column<string>(maxLength: 328, nullable: true),
                    HomeAddress = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEmailConfirmed = table.Column<bool>(nullable: false),
                    IsLockoutEnabled = table.Column<bool>(nullable: false),
                    IsPhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    IsTwoFactorEnabled = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LockoutEndDateUtc = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    NormalizedEmailAddress = table.Column<string>(maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(maxLength: 32, nullable: false),
                    Password = table.Column<string>(maxLength: 128, nullable: false),
                    PasswordResetCode = table.Column<string>(maxLength: 328, nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    Sex = table.Column<bool>(nullable: false),
                    Surname = table.Column<string>(maxLength: 32, nullable: false),
                    Telephone = table.Column<string>(maxLength: 15, nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUsers_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpUsers_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpUsers_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesSysI18N",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassName = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 30, nullable: false),
                    I18NKey = table.Column<string>(maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PropertyName = table.Column<string>(maxLength: 30, nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesSysI18N", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MesSysMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ExternalLink = table.Column<string>(maxLength: 200, nullable: false),
                    Group = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(maxLength: 50, nullable: false),
                    Index = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Link = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Target = table.Column<string>(maxLength: 10, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Translate = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesSysMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesSysMenu_MesSysMenu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MesSysMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesSysOrg",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    ParentId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesSysOrg", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesSysOrg_MesSysOrg_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MesSysOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSBarCodeAnalysis",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ClassName = table.Column<string>(maxLength: 30, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsReplace = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    PropertyName = table.Column<string>(maxLength: 30, nullable: true),
                    RegEX = table.Column<string>(maxLength: 2000, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Test = table.Column<string>(maxLength: 500, nullable: true),
                    TestValue = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSBarCodeAnalysis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSCustomer",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSStorageArea",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSStorageArea", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSStorageLocationType",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MoreMateriel = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSStorageLocationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AbpFeatures",
                columns: table => new
                {
                    EditionId = table.Column<int>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpFeatures_AbpEditions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "AbpEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityChanges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChangeTime = table.Column<DateTime>(nullable: false),
                    ChangeType = table.Column<byte>(nullable: false),
                    EntityChangeSetId = table.Column<long>(nullable: false),
                    EntityId = table.Column<string>(maxLength: 48, nullable: true),
                    EntityTypeFullName = table.Column<string>(maxLength: 192, nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityChanges_AbpEntityChangeSets_EntityChangeSetId",
                        column: x => x.EntityChangeSetId,
                        principalTable: "AbpEntityChangeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true),
                    Value = table.Column<string>(maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpSettings_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpTenants",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConnectionString = table.Column<string>(maxLength: 1024, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    EditionId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TenancyName = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpTenants_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpTenants_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpTenants_AbpEditions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "AbpEditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpTenants_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserClaims",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserClaims_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserLogins",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 256, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserLogins_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserRoles_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpUserTokens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpUserTokens_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSStorage",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    AboutUserId = table.Column<int>(nullable: true),
                    AboutUserId1 = table.Column<long>(nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IncomingMethod = table.Column<int>(nullable: false),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSStorage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSStorage_AbpUsers_AboutUserId1",
                        column: x => x.AboutUserId1,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 64, nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    Info = table.Column<string>(maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsStatic = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 32, nullable: false),
                    NormalizedName = table.Column<string>(maxLength: 32, nullable: false),
                    OrgId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(maxLength: 2000, nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpRoles_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpRoles_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpRoles_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbpRoles_MesSysOrg_OrgId",
                        column: x => x.OrgId,
                        principalTable: "MesSysOrg",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpEntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EntityChangeId = table.Column<long>(nullable: false),
                    NewValue = table.Column<string>(maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(maxLength: 96, nullable: true),
                    PropertyTypeFullName = table.Column<string>(maxLength: 192, nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpEntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "AbpEntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSLine",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ForCustomerMStorageId = table.Column<string>(maxLength: 30, nullable: true),
                    ForSelfMStorageId = table.Column<string>(maxLength: 30, nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSLine_MesWMSStorage_ForCustomerMStorageId",
                        column: x => x.ForCustomerMStorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSLine_MesWMSStorage_ForSelfMStorageId",
                        column: x => x.ForSelfMStorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSMPN",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CustomerId = table.Column<string>(maxLength: 30, nullable: true),
                    IncomingMethod = table.Column<int>(nullable: false),
                    Info = table.Column<string>(maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MPNHierarchy = table.Column<int>(nullable: false),
                    MPNLevel = table.Column<int>(nullable: false),
                    MPNType = table.Column<int>(nullable: false),
                    MPQ1 = table.Column<int>(nullable: true),
                    MPQ2 = table.Column<int>(nullable: true),
                    MPQ3 = table.Column<int>(nullable: true),
                    MPQ4 = table.Column<int>(nullable: true),
                    MPQ5 = table.Column<int>(nullable: true),
                    MSDLevel = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    RegisterStorageId = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    ShelfLife = table.Column<double>(nullable: false),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSMPN", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSMPN_MesWMSCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "MesWMSCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSMPN_MesWMSStorage_RegisterStorageId",
                        column: x => x.RegisterStorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReelMoveMethod",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    AllocationTypesStr = table.Column<string>(maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    InStorageId = table.Column<string>(maxLength: 30, nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReelMoveMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethod_MesWMSStorage_InStorageId",
                        column: x => x.InStorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSStorageLocation",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    Code = table.Column<string>(maxLength: 30, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsBright = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MainBoardId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    PositionId = table.Column<int>(nullable: false),
                    ReelId = table.Column<string>(maxLength: 60, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    StorageAreaId = table.Column<string>(maxLength: 30, nullable: true),
                    StorageId = table.Column<string>(maxLength: 30, nullable: true),
                    StorageLocationTypeId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSStorageLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSStorageLocation_MesWMSStorageArea_StorageAreaId",
                        column: x => x.StorageAreaId,
                        principalTable: "MesWMSStorageArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSStorageLocation_MesWMSStorage_StorageId",
                        column: x => x.StorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSStorageLocation_MesWMSStorageLocationType_StorageLocationTypeId",
                        column: x => x.StorageLocationTypeId,
                        principalTable: "MesWMSStorageLocationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AbpPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    IsGranted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    TenantId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpPermissions_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbpPermissions_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbpRoleClaims",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MesSysMenuRoleMap",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    MenuId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesSysMenuRoleMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesSysMenuRoleMap_MesSysMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "MesSysMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MesSysMenuRoleMap_AbpRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AbpRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSBOM",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    AllowableMoreSend = table.Column<bool>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MoreSendPercentage = table.Column<double>(nullable: false),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    ProductId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSBOM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSBOM_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSBOM_MesWMSMPN_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSMPNStorageAreaMap",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    MPNId = table.Column<string>(maxLength: 30, nullable: true),
                    StorageAreaId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSMPNStorageAreaMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSMPNStorageAreaMap_MesWMSMPN_MPNId",
                        column: x => x.MPNId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSMPNStorageAreaMap_MesWMSStorageArea_StorageAreaId",
                        column: x => x.StorageAreaId,
                        principalTable: "MesWMSStorageArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReceivedReelBill",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IQCCheckId = table.Column<string>(maxLength: 30, nullable: true),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    PoId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReceivedId = table.Column<string>(maxLength: 30, nullable: true),
                    ReceivedQty = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReceivedReelBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReceivedReelBill_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSSlot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BoardSide = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Feeder = table.Column<string>(maxLength: 50, nullable: true),
                    Index = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LineId = table.Column<string>(maxLength: 30, nullable: true),
                    LineSide = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 1000, nullable: true),
                    Machine = table.Column<string>(maxLength: 30, nullable: true),
                    MachineType = table.Column<string>(maxLength: 30, nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    ProductId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    Side = table.Column<int>(nullable: false),
                    SlotName = table.Column<string>(maxLength: 30, nullable: true),
                    Table = table.Column<string>(maxLength: 10, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSSlot_MesWMSLine_LineId",
                        column: x => x.LineId,
                        principalTable: "MesWMSLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSSlot_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSSlot_MesWMSMPN_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSUPH",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LineId = table.Column<string>(maxLength: 30, nullable: true),
                    Meter = table.Column<int>(nullable: false),
                    Pin = table.Column<int>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSUPH", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSUPH_MesWMSLine_LineId",
                        column: x => x.LineId,
                        principalTable: "MesWMSLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSUPH_MesWMSMPN_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSWorkBill",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LineId = table.Column<string>(maxLength: 30, nullable: true),
                    PlanEndTime = table.Column<DateTime>(nullable: false),
                    PlanStartTime = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<string>(maxLength: 30, nullable: true),
                    ProductionQty = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    ReadyMQty = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    TenantId = table.Column<int>(nullable: false),
                    WorkBillStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSWorkBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSWorkBill_MesWMSLine_LineId",
                        column: x => x.LineId,
                        principalTable: "MesWMSLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSWorkBill_MesWMSMPN_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReadyMBill",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeliverTime = table.Column<DateTime>(nullable: false),
                    Info = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsUrgent = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Linestr = table.Column<string>(maxLength: 50, nullable: true),
                    MakeDetailsType = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    Productstr = table.Column<string>(maxLength: 100, nullable: true),
                    ReReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReadyMTime = table.Column<int>(nullable: false),
                    ReadyMType = table.Column<int>(nullable: false),
                    ReelMoveMethodId = table.Column<string>(maxLength: 30, nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    WorkBilQtys = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReadyMBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBill_MesWMSReadyMBill_ReReadyMBillId",
                        column: x => x.ReReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBill_MesWMSReelMoveMethod_ReelMoveMethodId",
                        column: x => x.ReelMoveMethodId,
                        principalTable: "MesWMSReelMoveMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSRMMStorageMap",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ReelMoveMethodId = table.Column<string>(maxLength: 30, nullable: true),
                    StorageId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSRMMStorageMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSRMMStorageMap_MesWMSReelMoveMethod_ReelMoveMethodId",
                        column: x => x.ReelMoveMethodId,
                        principalTable: "MesWMSReelMoveMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSRMMStorageMap_MesWMSStorage_StorageId",
                        column: x => x.StorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSWorkBillDetailed",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BOMId = table.Column<string>(maxLength: 36, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReturnQty = table.Column<int>(nullable: false),
                    SendQty = table.Column<int>(nullable: false),
                    SlotId = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    WorkBillId = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSWorkBillDetailed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSWorkBillDetailed_MesWMSBOM_BOMId",
                        column: x => x.BOMId,
                        principalTable: "MesWMSBOM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSWorkBillDetailed_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSWorkBillDetailed_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSWorkBillDetailed_MesWMSWorkBill_WorkBillId",
                        column: x => x.WorkBillId,
                        principalTable: "MesWMSWorkBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReadyMBillDetailed",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BOMId = table.Column<string>(maxLength: 36, nullable: true),
                    BatchCodes = table.Column<string>(maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DemandQty = table.Column<int>(nullable: false),
                    FollowQty = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCut = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    PriorityReplacePN = table.Column<bool>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    ReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReelMoveMethodId = table.Column<string>(maxLength: 30, nullable: true),
                    ReplacePNs = table.Column<string>(maxLength: 50, nullable: true),
                    ReturnQty = table.Column<int>(nullable: false),
                    SendQty = table.Column<int>(nullable: false),
                    Suppliers = table.Column<string>(maxLength: 50, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReadyMBillDetailed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBillDetailed_MesWMSBOM_BOMId",
                        column: x => x.BOMId,
                        principalTable: "MesWMSBOM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBillDetailed_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBillDetailed_MesWMSReadyMBill_ReadyMBillId",
                        column: x => x.ReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBillDetailed_MesWMSReelMoveMethod_ReelMoveMethodId",
                        column: x => x.ReelMoveMethodId,
                        principalTable: "MesWMSReelMoveMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReadyMBillWorkBillMap",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    WorkBillId = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReadyMBillWorkBillMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBillWorkBillMap_MesWMSReadyMBill_ReadyMBillId",
                        column: x => x.ReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadyMBillWorkBillMap_MesWMSWorkBill_WorkBillId",
                        column: x => x.WorkBillId,
                        principalTable: "MesWMSWorkBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReadySlot",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BoardSide = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DemandQty = table.Column<int>(nullable: false),
                    Feeder = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LineId = table.Column<string>(maxLength: 30, nullable: true),
                    LineSide = table.Column<int>(nullable: false),
                    Location = table.Column<string>(maxLength: 1000, nullable: true),
                    Machine = table.Column<string>(maxLength: 30, nullable: true),
                    MachineType = table.Column<string>(maxLength: 30, nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    ProductId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReadyMBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    SendPartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    SendQty = table.Column<int>(nullable: false),
                    Side = table.Column<int>(nullable: false),
                    SlotId = table.Column<int>(nullable: true),
                    SlotName = table.Column<string>(maxLength: 30, nullable: true),
                    Table = table.Column<string>(maxLength: 10, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    Version = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReadySlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSLine_LineId",
                        column: x => x.LineId,
                        principalTable: "MesWMSLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSMPN_ProductId",
                        column: x => x.ProductId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSReadyMBill_ReReadyMBillId",
                        column: x => x.ReReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSReadyMBillDetailed_ReadyMBillDetailedId",
                        column: x => x.ReadyMBillDetailedId,
                        principalTable: "MesWMSReadyMBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSMPN_SendPartNoId",
                        column: x => x.SendPartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReadySlot_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReel",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 60, nullable: false),
                    BatchCode = table.Column<string>(maxLength: 30, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DateCode = table.Column<string>(maxLength: 10, nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    ExtendShelfLife = table.Column<double>(nullable: false),
                    IQCCheckId = table.Column<string>(maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFirstSelected = table.Column<bool>(nullable: false),
                    IsUseed = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LotCode = table.Column<string>(maxLength: 50, nullable: true),
                    MakeDate = table.Column<DateTime>(nullable: false),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    PoId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReadyMBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    ReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReceivedId = table.Column<string>(maxLength: 36, nullable: true),
                    SlotId = table.Column<int>(nullable: true),
                    StorageId = table.Column<string>(maxLength: 30, nullable: true),
                    StorageLocationId = table.Column<string>(maxLength: 30, nullable: true),
                    Supplier = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    WorkBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    WorkBillId = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSReadyMBillDetailed_ReadyMBillDetailedId",
                        column: x => x.ReadyMBillDetailedId,
                        principalTable: "MesWMSReadyMBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSReadyMBill_ReadyMBillId",
                        column: x => x.ReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSStorage_StorageId",
                        column: x => x.StorageId,
                        principalTable: "MesWMSStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSWorkBillDetailed_WorkBillDetailedId",
                        column: x => x.WorkBillDetailedId,
                        principalTable: "MesWMSWorkBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReel_MesWMSWorkBill_WorkBillId",
                        column: x => x.WorkBillId,
                        principalTable: "MesWMSWorkBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReelSendTemp",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 60, nullable: false),
                    BOMId = table.Column<string>(maxLength: 36, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DemandQty = table.Column<int>(nullable: false),
                    DemandSendQty = table.Column<int>(nullable: false),
                    FisrtStorageLocationId = table.Column<string>(maxLength: 30, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCut = table.Column<bool>(nullable: false),
                    IsSend = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReadyMBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    ReelMoveMethodId = table.Column<string>(maxLength: 30, nullable: true),
                    SelectQty = table.Column<int>(nullable: false),
                    SendQty = table.Column<int>(nullable: false),
                    SlotId = table.Column<int>(nullable: true),
                    StorageLocationId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReelSendTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSBOM_BOMId",
                        column: x => x.BOMId,
                        principalTable: "MesWMSBOM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSReadyMBill_ReReadyMBillId",
                        column: x => x.ReReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSReadyMBillDetailed_ReadyMBillDetailedId",
                        column: x => x.ReadyMBillDetailedId,
                        principalTable: "MesWMSReadyMBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSReelMoveMethod_ReelMoveMethodId",
                        column: x => x.ReelMoveMethodId,
                        principalTable: "MesWMSReelMoveMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSendTemp_MesWMSStorageLocation_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "MesWMSStorageLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReelShortTemp",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BOMId = table.Column<string>(maxLength: 36, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DemandQty = table.Column<int>(nullable: false),
                    DemandSendQty = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    ReReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReadyMBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    SelectQty = table.Column<int>(nullable: false),
                    ShortQty = table.Column<int>(nullable: false),
                    SlotId = table.Column<int>(nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReelShortTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReelShortTemp_MesWMSBOM_BOMId",
                        column: x => x.BOMId,
                        principalTable: "MesWMSBOM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelShortTemp_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelShortTemp_MesWMSReadyMBill_ReReadyMBillId",
                        column: x => x.ReReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelShortTemp_MesWMSReadyMBillDetailed_ReadyMBillDetailedId",
                        column: x => x.ReadyMBillDetailedId,
                        principalTable: "MesWMSReadyMBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelShortTemp_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReelSupplyTemp",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 60, nullable: false),
                    BOMId = table.Column<string>(maxLength: 36, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DemandQty = table.Column<int>(nullable: false),
                    DemandSendQty = table.Column<int>(nullable: false),
                    FisrtStorageLocationId = table.Column<string>(maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCut = table.Column<bool>(nullable: false),
                    IsSend = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReadyMBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    ReelMoveMethodId = table.Column<string>(maxLength: 30, nullable: true),
                    SelectQty = table.Column<int>(nullable: false),
                    SendQty = table.Column<int>(nullable: false),
                    SlotId = table.Column<int>(nullable: true),
                    StorageLocationId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReelSupplyTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSBOM_BOMId",
                        column: x => x.BOMId,
                        principalTable: "MesWMSBOM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSMPN_PartNoId",
                        column: x => x.PartNoId,
                        principalTable: "MesWMSMPN",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSReadyMBill_ReReadyMBillId",
                        column: x => x.ReReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSReadyMBillDetailed_ReadyMBillDetailedId",
                        column: x => x.ReadyMBillDetailedId,
                        principalTable: "MesWMSReadyMBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSReelMoveMethod_ReelMoveMethodId",
                        column: x => x.ReelMoveMethodId,
                        principalTable: "MesWMSReelMoveMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelSupplyTemp_MesWMSStorageLocation_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "MesWMSStorageLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MesWMSReelMoveMethodLog",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PartNoId = table.Column<string>(maxLength: 30, nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    ReadyMBillDetailedId = table.Column<string>(maxLength: 36, nullable: true),
                    ReadyMBillId = table.Column<string>(maxLength: 30, nullable: true),
                    ReceivedReelBillId = table.Column<string>(maxLength: 36, nullable: true),
                    ReelId = table.Column<string>(maxLength: 60, nullable: true),
                    ReelMoveMethodId = table.Column<string>(maxLength: 30, nullable: true),
                    SlotId = table.Column<int>(nullable: true),
                    StorageLocationId = table.Column<string>(maxLength: 30, nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    WorkBillDetailedId = table.Column<string>(nullable: true),
                    WorkBillId = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MesWMSReelMoveMethodLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSReadyMBillDetailed_ReadyMBillDetailedId",
                        column: x => x.ReadyMBillDetailedId,
                        principalTable: "MesWMSReadyMBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSReadyMBill_ReadyMBillId",
                        column: x => x.ReadyMBillId,
                        principalTable: "MesWMSReadyMBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSReceivedReelBill_ReceivedReelBillId",
                        column: x => x.ReceivedReelBillId,
                        principalTable: "MesWMSReceivedReelBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSReel_ReelId",
                        column: x => x.ReelId,
                        principalTable: "MesWMSReel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSReelMoveMethod_ReelMoveMethodId",
                        column: x => x.ReelMoveMethodId,
                        principalTable: "MesWMSReelMoveMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSSlot_SlotId",
                        column: x => x.SlotId,
                        principalTable: "MesWMSSlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSStorageLocation_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "MesWMSStorageLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSWorkBillDetailed_WorkBillDetailedId",
                        column: x => x.WorkBillDetailedId,
                        principalTable: "MesWMSWorkBillDetailed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MesWMSReelMoveMethodLog_MesWMSWorkBill_WorkBillId",
                        column: x => x.WorkBillId,
                        principalTable: "MesWMSWorkBill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionDuration",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "ExecutionDuration" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionTime",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpAuditLogs_TenantId_UserId",
                table: "AbpAuditLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_EntityChangeSetId",
                table: "AbpEntityChanges",
                column: "EntityChangeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChanges_EntityTypeFullName_EntityId",
                table: "AbpEntityChanges",
                columns: new[] { "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChangeSets_TenantId_CreationTime",
                table: "AbpEntityChangeSets",
                columns: new[] { "TenantId", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChangeSets_TenantId_Reason",
                table: "AbpEntityChangeSets",
                columns: new[] { "TenantId", "Reason" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityChangeSets_TenantId_UserId",
                table: "AbpEntityChangeSets",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpEntityPropertyChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_EditionId_Name",
                table: "AbpFeatures",
                columns: new[] { "EditionId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpFeatures_TenantId_Name",
                table: "AbpFeatures",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpLanguages_TenantId_Name",
                table: "AbpLanguages",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpLanguageTexts_TenantId_Source_LanguageName_Key",
                table: "AbpLanguageTexts",
                columns: new[] { "TenantId", "Source", "LanguageName", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId",
                table: "AbpNotificationSubscriptions",
                columns: new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId",
                table: "AbpNotificationSubscriptions",
                columns: new[] { "TenantId", "NotificationName", "EntityTypeName", "EntityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpOrganizationUnits_TenantId_Code",
                table: "AbpOrganizationUnits",
                columns: new[] { "TenantId", "Code" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_TenantId_Name",
                table: "AbpPermissions",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_RoleId",
                table: "AbpPermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpPermissions_UserId",
                table: "AbpPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoleClaims_RoleId",
                table: "AbpRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoleClaims_TenantId_ClaimType",
                table: "AbpRoleClaims",
                columns: new[] { "TenantId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_CreatorUserId",
                table: "AbpRoles",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_DeleterUserId",
                table: "AbpRoles",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_LastModifierUserId",
                table: "AbpRoles",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_OrgId",
                table: "AbpRoles",
                column: "OrgId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpRoles_TenantId_NormalizedName",
                table: "AbpRoles",
                columns: new[] { "TenantId", "NormalizedName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettings_UserId",
                table: "AbpSettings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpSettings_TenantId_Name",
                table: "AbpSettings",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenantNotifications_TenantId",
                table: "AbpTenantNotifications",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_CreatorUserId",
                table: "AbpTenants",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_DeleterUserId",
                table: "AbpTenants",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_EditionId",
                table: "AbpTenants",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_LastModifierUserId",
                table: "AbpTenants",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_TenancyName",
                table: "AbpTenants",
                column: "TenancyName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_EmailAddress",
                table: "AbpUserAccounts",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_UserName",
                table: "AbpUserAccounts",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_TenantId_EmailAddress",
                table: "AbpUserAccounts",
                columns: new[] { "TenantId", "EmailAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_TenantId_UserId",
                table: "AbpUserAccounts",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserAccounts_TenantId_UserName",
                table: "AbpUserAccounts",
                columns: new[] { "TenantId", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserClaims_UserId",
                table: "AbpUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserClaims_TenantId_ClaimType",
                table: "AbpUserClaims",
                columns: new[] { "TenantId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLoginAttempts_UserId_TenantId",
                table: "AbpUserLoginAttempts",
                columns: new[] { "UserId", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result",
                table: "AbpUserLoginAttempts",
                columns: new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_UserId",
                table: "AbpUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_TenantId_UserId",
                table: "AbpUserLogins",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserLogins_TenantId_LoginProvider_ProviderKey",
                table: "AbpUserLogins",
                columns: new[] { "TenantId", "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserNotifications_UserId_State_CreationTime",
                table: "AbpUserNotifications",
                columns: new[] { "UserId", "State", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserOrganizationUnits_TenantId_OrganizationUnitId",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "TenantId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserOrganizationUnits_TenantId_UserId",
                table: "AbpUserOrganizationUnits",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_UserId",
                table: "AbpUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_TenantId_RoleId",
                table: "AbpUserRoles",
                columns: new[] { "TenantId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserRoles_TenantId_UserId",
                table: "AbpUserRoles",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_CreatorUserId",
                table: "AbpUsers",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_DeleterUserId",
                table: "AbpUsers",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_LastModifierUserId",
                table: "AbpUsers",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_TenantId_NormalizedEmailAddress",
                table: "AbpUsers",
                columns: new[] { "TenantId", "NormalizedEmailAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_TenantId_NormalizedUserName",
                table: "AbpUsers",
                columns: new[] { "TenantId", "NormalizedUserName" });

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserTokens_UserId",
                table: "AbpUserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AbpUserTokens_TenantId_UserId",
                table: "AbpUserTokens",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_MesSysMenu_ParentId",
                table: "MesSysMenu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MesSysMenuRoleMap_MenuId",
                table: "MesSysMenuRoleMap",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MesSysMenuRoleMap_RoleId",
                table: "MesSysMenuRoleMap",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MesSysOrg_ParentId",
                table: "MesSysOrg",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSBOM_PartNoId",
                table: "MesWMSBOM",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSBOM_ProductId",
                table: "MesWMSBOM",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSLine_ForCustomerMStorageId",
                table: "MesWMSLine",
                column: "ForCustomerMStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSLine_ForSelfMStorageId",
                table: "MesWMSLine",
                column: "ForSelfMStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSMPN_CustomerId",
                table: "MesWMSMPN",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSMPN_RegisterStorageId",
                table: "MesWMSMPN",
                column: "RegisterStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSMPNStorageAreaMap_MPNId",
                table: "MesWMSMPNStorageAreaMap",
                column: "MPNId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSMPNStorageAreaMap_StorageAreaId",
                table: "MesWMSMPNStorageAreaMap",
                column: "StorageAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBill_ReReadyMBillId",
                table: "MesWMSReadyMBill",
                column: "ReReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBill_ReelMoveMethodId",
                table: "MesWMSReadyMBill",
                column: "ReelMoveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBillDetailed_BOMId",
                table: "MesWMSReadyMBillDetailed",
                column: "BOMId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBillDetailed_PartNoId",
                table: "MesWMSReadyMBillDetailed",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBillDetailed_ReadyMBillId",
                table: "MesWMSReadyMBillDetailed",
                column: "ReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBillDetailed_ReelMoveMethodId",
                table: "MesWMSReadyMBillDetailed",
                column: "ReelMoveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBillWorkBillMap_ReadyMBillId",
                table: "MesWMSReadyMBillWorkBillMap",
                column: "ReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadyMBillWorkBillMap_WorkBillId",
                table: "MesWMSReadyMBillWorkBillMap",
                column: "WorkBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_LineId",
                table: "MesWMSReadySlot",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_PartNoId",
                table: "MesWMSReadySlot",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_ProductId",
                table: "MesWMSReadySlot",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_ReReadyMBillId",
                table: "MesWMSReadySlot",
                column: "ReReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_ReadyMBillDetailedId",
                table: "MesWMSReadySlot",
                column: "ReadyMBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_SendPartNoId",
                table: "MesWMSReadySlot",
                column: "SendPartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReadySlot_SlotId",
                table: "MesWMSReadySlot",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReceivedReelBill_PartNoId",
                table: "MesWMSReceivedReelBill",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_PartNoId",
                table: "MesWMSReel",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_ReadyMBillDetailedId",
                table: "MesWMSReel",
                column: "ReadyMBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_ReadyMBillId",
                table: "MesWMSReel",
                column: "ReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_SlotId",
                table: "MesWMSReel",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_StorageId",
                table: "MesWMSReel",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_StorageLocationId",
                table: "MesWMSReel",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_WorkBillDetailedId",
                table: "MesWMSReel",
                column: "WorkBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReel_WorkBillId",
                table: "MesWMSReel",
                column: "WorkBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethod_InStorageId",
                table: "MesWMSReelMoveMethod",
                column: "InStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_CreatorUserId",
                table: "MesWMSReelMoveMethodLog",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_ReadyMBillDetailedId",
                table: "MesWMSReelMoveMethodLog",
                column: "ReadyMBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_ReadyMBillId",
                table: "MesWMSReelMoveMethodLog",
                column: "ReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_ReceivedReelBillId",
                table: "MesWMSReelMoveMethodLog",
                column: "ReceivedReelBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_ReelId",
                table: "MesWMSReelMoveMethodLog",
                column: "ReelId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_ReelMoveMethodId",
                table: "MesWMSReelMoveMethodLog",
                column: "ReelMoveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_SlotId",
                table: "MesWMSReelMoveMethodLog",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_StorageLocationId",
                table: "MesWMSReelMoveMethodLog",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_WorkBillDetailedId",
                table: "MesWMSReelMoveMethodLog",
                column: "WorkBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelMoveMethodLog_WorkBillId",
                table: "MesWMSReelMoveMethodLog",
                column: "WorkBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_BOMId",
                table: "MesWMSReelSendTemp",
                column: "BOMId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_PartNoId",
                table: "MesWMSReelSendTemp",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_ReReadyMBillId",
                table: "MesWMSReelSendTemp",
                column: "ReReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_ReadyMBillDetailedId",
                table: "MesWMSReelSendTemp",
                column: "ReadyMBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_ReelMoveMethodId",
                table: "MesWMSReelSendTemp",
                column: "ReelMoveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_SlotId",
                table: "MesWMSReelSendTemp",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSendTemp_StorageLocationId",
                table: "MesWMSReelSendTemp",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelShortTemp_BOMId",
                table: "MesWMSReelShortTemp",
                column: "BOMId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelShortTemp_PartNoId",
                table: "MesWMSReelShortTemp",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelShortTemp_ReReadyMBillId",
                table: "MesWMSReelShortTemp",
                column: "ReReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelShortTemp_ReadyMBillDetailedId",
                table: "MesWMSReelShortTemp",
                column: "ReadyMBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelShortTemp_SlotId",
                table: "MesWMSReelShortTemp",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_BOMId",
                table: "MesWMSReelSupplyTemp",
                column: "BOMId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_PartNoId",
                table: "MesWMSReelSupplyTemp",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_ReReadyMBillId",
                table: "MesWMSReelSupplyTemp",
                column: "ReReadyMBillId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_ReadyMBillDetailedId",
                table: "MesWMSReelSupplyTemp",
                column: "ReadyMBillDetailedId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_ReelMoveMethodId",
                table: "MesWMSReelSupplyTemp",
                column: "ReelMoveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_SlotId",
                table: "MesWMSReelSupplyTemp",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSReelSupplyTemp_StorageLocationId",
                table: "MesWMSReelSupplyTemp",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSRMMStorageMap_ReelMoveMethodId",
                table: "MesWMSRMMStorageMap",
                column: "ReelMoveMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSRMMStorageMap_StorageId",
                table: "MesWMSRMMStorageMap",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSSlot_LineId",
                table: "MesWMSSlot",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSSlot_PartNoId",
                table: "MesWMSSlot",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSSlot_ProductId",
                table: "MesWMSSlot",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSStorage_AboutUserId1",
                table: "MesWMSStorage",
                column: "AboutUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSStorageLocation_ReelId",
                table: "MesWMSStorageLocation",
                column: "ReelId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSStorageLocation_StorageAreaId",
                table: "MesWMSStorageLocation",
                column: "StorageAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSStorageLocation_StorageId",
                table: "MesWMSStorageLocation",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSStorageLocation_StorageLocationTypeId",
                table: "MesWMSStorageLocation",
                column: "StorageLocationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSUPH_LineId",
                table: "MesWMSUPH",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSUPH_ProductId",
                table: "MesWMSUPH",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSWorkBill_LineId",
                table: "MesWMSWorkBill",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSWorkBill_ProductId",
                table: "MesWMSWorkBill",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSWorkBillDetailed_BOMId",
                table: "MesWMSWorkBillDetailed",
                column: "BOMId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSWorkBillDetailed_PartNoId",
                table: "MesWMSWorkBillDetailed",
                column: "PartNoId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSWorkBillDetailed_SlotId",
                table: "MesWMSWorkBillDetailed",
                column: "SlotId");

            migrationBuilder.CreateIndex(
                name: "IX_MesWMSWorkBillDetailed_WorkBillId",
                table: "MesWMSWorkBillDetailed",
                column: "WorkBillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpAuditLogs");

            migrationBuilder.DropTable(
                name: "AbpBackgroundJobs");

            migrationBuilder.DropTable(
                name: "AbpEntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "AbpFeatures");

            migrationBuilder.DropTable(
                name: "AbpLanguages");

            migrationBuilder.DropTable(
                name: "AbpLanguageTexts");

            migrationBuilder.DropTable(
                name: "AbpNotifications");

            migrationBuilder.DropTable(
                name: "AbpNotificationSubscriptions");

            migrationBuilder.DropTable(
                name: "AbpOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpPermissions");

            migrationBuilder.DropTable(
                name: "AbpRoleClaims");

            migrationBuilder.DropTable(
                name: "AbpSettings");

            migrationBuilder.DropTable(
                name: "AbpTenantNotifications");

            migrationBuilder.DropTable(
                name: "AbpTenants");

            migrationBuilder.DropTable(
                name: "AbpUserAccounts");

            migrationBuilder.DropTable(
                name: "AbpUserClaims");

            migrationBuilder.DropTable(
                name: "AbpUserLoginAttempts");

            migrationBuilder.DropTable(
                name: "AbpUserLogins");

            migrationBuilder.DropTable(
                name: "AbpUserNotifications");

            migrationBuilder.DropTable(
                name: "AbpUserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "AbpUserRoles");

            migrationBuilder.DropTable(
                name: "AbpUserTokens");

            migrationBuilder.DropTable(
                name: "MesSysI18N");

            migrationBuilder.DropTable(
                name: "MesSysMenuRoleMap");

            migrationBuilder.DropTable(
                name: "MesWMSBarCodeAnalysis");

            migrationBuilder.DropTable(
                name: "MesWMSMPNStorageAreaMap");

            migrationBuilder.DropTable(
                name: "MesWMSReadyMBillWorkBillMap");

            migrationBuilder.DropTable(
                name: "MesWMSReadySlot");

            migrationBuilder.DropTable(
                name: "MesWMSReelMoveMethodLog");

            migrationBuilder.DropTable(
                name: "MesWMSReelSendTemp");

            migrationBuilder.DropTable(
                name: "MesWMSReelShortTemp");

            migrationBuilder.DropTable(
                name: "MesWMSReelSupplyTemp");

            migrationBuilder.DropTable(
                name: "MesWMSRMMStorageMap");

            migrationBuilder.DropTable(
                name: "MesWMSUPH");

            migrationBuilder.DropTable(
                name: "AbpEntityChanges");

            migrationBuilder.DropTable(
                name: "AbpEditions");

            migrationBuilder.DropTable(
                name: "MesSysMenu");

            migrationBuilder.DropTable(
                name: "AbpRoles");

            migrationBuilder.DropTable(
                name: "MesWMSReceivedReelBill");

            migrationBuilder.DropTable(
                name: "MesWMSReel");

            migrationBuilder.DropTable(
                name: "MesWMSStorageLocation");

            migrationBuilder.DropTable(
                name: "AbpEntityChangeSets");

            migrationBuilder.DropTable(
                name: "MesSysOrg");

            migrationBuilder.DropTable(
                name: "MesWMSReadyMBillDetailed");

            migrationBuilder.DropTable(
                name: "MesWMSWorkBillDetailed");

            migrationBuilder.DropTable(
                name: "MesWMSStorageArea");

            migrationBuilder.DropTable(
                name: "MesWMSStorageLocationType");

            migrationBuilder.DropTable(
                name: "MesWMSReadyMBill");

            migrationBuilder.DropTable(
                name: "MesWMSBOM");

            migrationBuilder.DropTable(
                name: "MesWMSSlot");

            migrationBuilder.DropTable(
                name: "MesWMSWorkBill");

            migrationBuilder.DropTable(
                name: "MesWMSReelMoveMethod");

            migrationBuilder.DropTable(
                name: "MesWMSLine");

            migrationBuilder.DropTable(
                name: "MesWMSMPN");

            migrationBuilder.DropTable(
                name: "MesWMSCustomer");

            migrationBuilder.DropTable(
                name: "MesWMSStorage");

            migrationBuilder.DropTable(
                name: "AbpUsers");
        }
    }
}
