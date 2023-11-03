using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class base0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BannerNextBases",
                columns: table => new
                {
                    BannerNextBase_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BannerNextBase_Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BannerNextBase_ShowingType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BannerNextBase_IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerNextBases", x => x.BannerNextBase_Id);
                });

            migrationBuilder.CreateTable(
                name: "BestDeals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestDeals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogGroups",
                columns: table => new
                {
                    BlogGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogGroupTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogGroupEnTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogGroups", x => x.BlogGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CheckOut = table.Column<bool>(type: "bit", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DiscountCoupen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BuyerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuyerFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuyerCellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecipientIsBuyer = table.Column<bool>(type: "bit", nullable: false),
                    RecipientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipientFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipientPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentofFareDuringDelivery = table.Column<bool>(type: "bit", nullable: false),
                    ShippingByTipax = table.Column<bool>(type: "bit", nullable: false),
                    ShippingBySnapp = table.Column<bool>(type: "bit", nullable: false),
                    ShippingByPishtazPost = table.Column<bool>(type: "bit", nullable: false),
                    Freight = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CartTotal = table.Column<int>(type: "int", nullable: true),
                    DedicatedCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CellphonesBanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cellphone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellphonesBanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiscountCoupens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Percent = table.Column<float>(type: "real", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCoupens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageBanners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ViewOrder = table.Column<int>(type: "int", nullable: false),
                    BannerCount = table.Column<int>(type: "int", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId");
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiscountPercent = table.Column<int>(type: "int", nullable: true),
                    DiscountValue = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroups_ProductGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SiteInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkTime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MainPageMessage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AddressLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SiteCurrency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FreeShippingValue = table.Column<int>(type: "int", nullable: true),
                    InstaLink = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WhatsUpLink = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text1Class = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text1Dir = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Text1DataAnimation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text1DataDelay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text2Class = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text2Dir = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Text2DataAnimation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text2DataDelay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text3Class = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text3Dir = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Text3DataAnimation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text3DataDelay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkDataAnimation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkDataDelay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkClass = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsEndofProccess = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorePageBanners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Text1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Text2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Text3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LinkUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LinkText = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePageBanners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    File = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BannerNextSliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Link = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BannerNextBaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BannerNextSliders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BannerNextSliders_BannerNextBases_BannerNextBaseId",
                        column: x => x.BannerNextBaseId,
                        principalTable: "BannerNextBases",
                        principalColumn: "BannerNextBase_Id");
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BlogDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BlogImageInBlog = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlogImageInBlogDetails = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlogText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogPageTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogPageDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BlogSummary = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlogTags = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BlogIsActive = table.Column<bool>(type: "bit", nullable: false),
                    BlogGroupId = table.Column<int>(type: "int", nullable: false),
                    BlogShortKey = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogRefferalLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BlogLinkText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BlogViewsCount = table.Column<int>(type: "int", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.BlogId);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogGroups_BlogGroupId",
                        column: x => x.BlogGroupId,
                        principalTable: "BlogGroups",
                        principalColumn: "BlogGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageBannerItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Text2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Text3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UrlText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlLink = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ViewOrder = table.Column<int>(type: "int", nullable: false),
                    HasDiscount = table.Column<bool>(type: "bit", nullable: false),
                    PBId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageBannerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageBannerItems_PageBanners_PBId",
                        column: x => x.PBId,
                        principalTable: "PageBanners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    PermissionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId");
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    CountyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Freight = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.CountyId);
                    table.ForeignKey(
                        name: "FK_Counties_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateFreights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Freight = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateFreights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateFreights_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComment_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PageComment = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UnitofMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentDiscount = table.Column<int>(type: "int", nullable: true),
                    ValueDiscount = table.Column<int>(type: "int", nullable: true),
                    MinimumInventory = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BigImage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BigImageAlt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmallImage1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmallImage1Alt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmallImage2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SmallImage2Alt = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ProductType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AsBestSelling = table.Column<bool>(type: "bit", nullable: false),
                    BestSellRate = table.Column<int>(type: "int", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    HowToUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: true),
                    BlogId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "BlogId");
                    table.ForeignKey(
                        name: "FK_Products_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cellphone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "CountyId");
                });

            migrationBuilder.CreateTable(
                name: "ProductItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    PercentDiscount = table.Column<int>(type: "int", nullable: true),
                    ValueDiscount = table.Column<int>(type: "int", nullable: true),
                    MinimumInventory = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    AsBestSelling = table.Column<bool>(type: "bit", nullable: false),
                    BestSellRate = table.Column<int>(type: "int", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    UnitofMeasure = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ValueBaseOnUoM = table.Column<float>(type: "real", nullable: false),
                    ShowAsPrice = table.Column<bool>(type: "bit", nullable: false),
                    ShowInMainPage = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CountyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuyerFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BuyerCellphone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipientFamily = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RecipientPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiscountCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DiscountValue = table.Column<int>(type: "int", nullable: true),
                    DeliveryCost = table.Column<int>(type: "int", nullable: true),
                    DeliveryCostDiscount = table.Column<int>(type: "int", nullable: true),
                    OrderSum = table.Column<long>(type: "bigint", nullable: true),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsCanceled = table.Column<bool>(type: "bit", nullable: false),
                    UserBusinessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserBusinessPercent = table.Column<float>(type: "real", nullable: true),
                    DedicatedNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DeliveryType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    URId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.URId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    NetPrice = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OrderDetailPrice = table.Column<int>(type: "int", nullable: false),
                    OrderDetailNetPrice = table.Column<int>(type: "int", nullable: false),
                    OrderDetailDiscountCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OrderDetailDiscountValue = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Order_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStatuses_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderStatuses_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WareHouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Input = table.Column<float>(type: "real", nullable: true),
                    Export = table.Column<float>(type: "real", nullable: true),
                    ProductItemId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    OrderdetialId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    WareHouse_Comment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareHouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WareHouses_CartItems_OrderdetialId",
                        column: x => x.OrderdetialId,
                        principalTable: "CartItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WareHouses_ProductItems_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "ProductItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WareHouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsDeleted", "RoleName", "RoleTitle" },
                values: new object[,]
                {
                    { 1, false, "Admin", "مدیر سایت" },
                    { 2, false, "Customer", "مشتری" },
                    { 3, false, "Marketer", "همکار فروش" },
                    { 4, false, "Operator", "اپراتور" }
                });

            migrationBuilder.InsertData(
                table: "SiteInfos",
                columns: new[] { "Id", "Address", "AddressLink", "FreeShippingValue", "InstaLink", "MainPageMessage", "Mobile1", "Mobile2", "Phone1", "Phone2", "RegDate", "SiteCurrency", "WhatsUpLink", "WorkTime" },
                values: new object[] { 1, "کرج، مهرشهر،ابتدای بلوار ارم، خیابان رسولی، نبش کوچه پیچک یک", null, null, null, "", "09128027423", "09104319418", "02633302220", "02633311872", new DateTime(2023, 3, 18, 17, 17, 17, 207, DateTimeKind.Local).AddTicks(7435), "ریال", null, "هر روز هفته، از 10 صبح تا 10 شب" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "IsDeleted", "StateName" },
                values: new object[,]
                {
                    { 1, false, "آذربایجان شرقی" },
                    { 2, false, "آذربایجان غربی" },
                    { 3, false, "اردبیل" },
                    { 4, false, "اصفهان" },
                    { 5, false, "البرز" },
                    { 6, false, "ایلام" },
                    { 7, false, "بوشهر" },
                    { 8, false, "تهران" },
                    { 9, false, "چهار محال و بختیاری" },
                    { 10, false, "خراسان جنوبی" },
                    { 11, false, "خراسان رضوی" },
                    { 12, false, "خراسان شمالی" },
                    { 13, false, "خوزستان" },
                    { 14, false, "زنجان" },
                    { 15, false, "سمنان" },
                    { 16, false, "سیستان و بلوچستان" },
                    { 17, false, "فارس" },
                    { 18, false, "قزوین" },
                    { 19, false, "قم" },
                    { 20, false, "کردستان" },
                    { 21, false, "کرمان" },
                    { 22, false, "کرمانشاه" },
                    { 23, false, "کهکیلویه و بویراحمد" },
                    { 24, false, "گلستان" },
                    { 25, false, "گیلان" },
                    { 26, false, "لرستان" },
                    { 27, false, "مازندران" },
                    { 28, false, "مرکزی" },
                    { 29, false, "هرمزگان" },
                    { 30, false, "همدان" },
                    { 31, false, "یزد" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "IsDeleted", "IsEndofProccess", "Text" },
                values: new object[,]
                {
                    { 1, false, false, "تحویل به پست" },
                    { 2, false, true, "تحویل به مشتری" },
                    { 3, false, true, "انصراف" }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "CountyId", "CountyName", "Freight", "IsDeleted", "StateId" },
                values: new object[,]
                {
                    { 1, "کشکسرای", 200000, false, 1 },
                    { 2, "سهند", 200000, false, 1 },
                    { 3, "سیس", 200000, false, 1 },
                    { 4, "دوزدوزان", 200000, false, 1 },
                    { 5, "تیمورلو", 200000, false, 1 },
                    { 6, "صوفیان", 200000, false, 1 },
                    { 7, "سردرود", 200000, false, 1 },
                    { 8, "هادیشهر", 200000, false, 1 },
                    { 9, "هشترود", 200000, false, 1 },
                    { 10, "زرنق", 200000, false, 1 },
                    { 11, "ترکمانچای", 200000, false, 1 },
                    { 12, "ورزقان", 200000, false, 1 },
                    { 13, "تسوج", 200000, false, 1 },
                    { 14, "زنوز", 200000, false, 1 },
                    { 15, "ایخچی", 200000, false, 1 },
                    { 16, "شزفخانه", 200000, false, 1 },
                    { 17, "مهربان", 200000, false, 1 },
                    { 18, "مبارک شهر", 200000, false, 1 },
                    { 19, "تیکمه داش", 200000, false, 1 },
                    { 20, "باسمنج", 200000, false, 1 },
                    { 21, "سیه رود", 200000, false, 1 },
                    { 22, "میانه", 200000, false, 1 },
                    { 23, "خمارلو", 200000, false, 1 },
                    { 24, "خواجه", 200000, false, 1 },
                    { 25, "بناب مرند", 200000, false, 1 },
                    { 26, "قره آغاج", 200000, false, 1 },
                    { 27, "وایقان", 200000, false, 1 },
                    { 28, "مراغه", 200000, false, 1 },
                    { 29, "ممقان", 200000, false, 1 },
                    { 30, "خامنه", 200000, false, 1 },
                    { 31, "خسروشاه", 200000, false, 1 },
                    { 32, "لیلان", 200000, false, 1 },
                    { 33, "نظرکهریزی", 200000, false, 1 },
                    { 34, "اهر", 200000, false, 1 },
                    { 35, "بخشایش", 200000, false, 1 },
                    { 36, "آقکند", 200000, false, 1 },
                    { 37, "جوان قلعه", 200000, false, 1 },
                    { 38, "کلیبر", 200000, false, 1 },
                    { 39, "مرند", 200000, false, 1 },
                    { 40, "اسکو", 200000, false, 1 },
                    { 41, "شندآباد", 200000, false, 1 },
                    { 42, "شربیان", 200000, false, 1 },
                    { 43, "گوگان", 200000, false, 1 },
                    { 44, "بستان آباد", 200000, false, 1 },
                    { 45, "تبریز", 200000, false, 1 },
                    { 46, "جلفا", 200000, false, 1 },
                    { 47, "آچاچی", 200000, false, 1 },
                    { 48, "هریس", 200000, false, 1 },
                    { 49, "یامچی", 200000, false, 1 },
                    { 50, "خاروانا", 200000, false, 1 },
                    { 51, "کوزه کنان", 200000, false, 1 },
                    { 52, "خداجو", 200000, false, 1 },
                    { 53, "آذرشهر", 200000, false, 1 },
                    { 54, "شبستر", 200000, false, 1 },
                    { 55, "سراب", 200000, false, 1 },
                    { 56, "ملکان", 200000, false, 1 },
                    { 57, "بناب", 200000, false, 1 },
                    { 58, "هوراند", 200000, false, 1 },
                    { 59, "کلوانق", 200000, false, 1 },
                    { 60, "ترک", 200000, false, 1 },
                    { 61, "عجب شیر", 200000, false, 1 },
                    { 62, "آبش احمد", 200000, false, 1 },
                    { 63, "تازه شهر", 200000, false, 2 },
                    { 64, "نالوس", 200000, false, 2 },
                    { 65, "ایواوغلی", 200000, false, 2 },
                    { 66, "شاهین دژ", 200000, false, 2 },
                    { 67, "گردکشانه", 200000, false, 2 },
                    { 68, "باروق", 200000, false, 2 },
                    { 69, "سیلوانه", 200000, false, 2 },
                    { 70, "بازرگان", 200000, false, 2 },
                    { 71, "نازک علیا", 200000, false, 2 },
                    { 72, "ربط", 200000, false, 2 },
                    { 73, "تکاب", 200000, false, 2 },
                    { 74, "دیزج دیز", 200000, false, 2 },
                    { 75, "سیمینه", 200000, false, 2 },
                    { 76, "نوشین", 200000, false, 2 },
                    { 77, "میاندوآب", 200000, false, 2 },
                    { 78, "مرگنلر", 200000, false, 2 },
                    { 79, "سلماس", 200000, false, 2 },
                    { 80, "آواجیق", 200000, false, 2 },
                    { 81, "قطور", 200000, false, 2 },
                    { 82, "محمودآباد", 200000, false, 2 },
                    { 83, "خوی", 200000, false, 2 },
                    { 84, "نقده", 200000, false, 2 },
                    { 85, "سرو", 200000, false, 2 },
                    { 86, "خلیفان", 200000, false, 2 },
                    { 87, "پلدشت", 200000, false, 2 },
                    { 88, "میرآباد", 200000, false, 2 },
                    { 89, "اشنویه", 200000, false, 2 },
                    { 90, "زرآباد", 200000, false, 2 },
                    { 91, "بوکان", 200000, false, 2 },
                    { 92, "پیرانشهر", 200000, false, 2 },
                    { 93, "چهاربرج", 200000, false, 2 },
                    { 94, "قوشچی", 200000, false, 2 },
                    { 95, "شوط", 200000, false, 2 },
                    { 96, "ماکو", 200000, false, 2 },
                    { 97, "سیه چشمه", 200000, false, 2 },
                    { 98, "سردشت", 200000, false, 2 },
                    { 99, "کشاورز", 200000, false, 2 },
                    { 100, "فیرورق", 200000, false, 2 },
                    { 101, "محمدیار", 200000, false, 2 },
                    { 102, "ارومیه", 200000, false, 2 },
                    { 103, "مهاباد", 200000, false, 2 },
                    { 104, "قره ضیاءالدین", 200000, false, 2 },
                    { 105, "پارس آباد", 200000, false, 3 },
                    { 106, "فخر آباد", 200000, false, 3 },
                    { 107, "کلور", 200000, false, 3 },
                    { 108, "نیر", 200000, false, 3 },
                    { 109, "اردبیل", 200000, false, 3 },
                    { 110, "اسلام آباد", 200000, false, 3 },
                    { 111, "تازه کندانگوت", 200000, false, 3 },
                    { 112, "مشگین شهر", 200000, false, 3 },
                    { 113, "جعفر آباد", 200000, false, 3 },
                    { 114, "نمین", 200000, false, 3 },
                    { 115, "اصلاندوز", 200000, false, 3 },
                    { 116, "مرادلو", 200000, false, 3 },
                    { 117, "خلخال", 200000, false, 3 },
                    { 118, "کوراییم", 200000, false, 3 },
                    { 119, "هیر", 200000, false, 3 },
                    { 120, "گیوی", 200000, false, 3 },
                    { 121, "گرمی", 200000, false, 3 },
                    { 122, "لاهرود", 200000, false, 3 },
                    { 123, "هشتجین", 200000, false, 3 },
                    { 124, "عنبران", 200000, false, 3 },
                    { 125, "تازه کند", 200000, false, 3 },
                    { 126, "قصابه", 200000, false, 3 },
                    { 127, "رضی", 200000, false, 3 },
                    { 128, "سرعین", 200000, false, 3 },
                    { 129, "بیله سوار", 200000, false, 3 },
                    { 130, "آبی بیگلو", 200000, false, 3 },
                    { 131, "آبی بیگلو", 200000, false, 4 },
                    { 132, "زیار", 200000, false, 4 },
                    { 133, "زرین شهر", 200000, false, 4 },
                    { 134, "گلشن", 200000, false, 4 },
                    { 135, "پیربکران", 200000, false, 4 },
                    { 136, "خالدآباد", 200000, false, 4 },
                    { 137, "سجزی", 200000, false, 4 },
                    { 138, "گوگد", 200000, false, 4 },
                    { 139, "تیران", 200000, false, 4 },
                    { 140, "ونک", 200000, false, 4 },
                    { 141, "دهق", 200000, false, 4 },
                    { 142, "زواره", 200000, false, 4 },
                    { 143, "کاشان", 200000, false, 4 },
                    { 144, "ابوزید آباد", 200000, false, 4 },
                    { 145, "اصغر آباد", 200000, false, 4 },
                    { 146, "بافران", 200000, false, 4 },
                    { 147, "شهرضا", 200000, false, 4 },
                    { 148, "خور", 200000, false, 4 },
                    { 149, "مجلسی", 200000, false, 4 },
                    { 150, "هرند", 200000, false, 4 },
                    { 151, "فولادشهر", 200000, false, 4 },
                    { 152, "کمشچه", 200000, false, 4 },
                    { 153, "کلیشادوسودرجان", 200000, false, 4 },
                    { 154, "لای بید", 200000, false, 4 },
                    { 155, "قهجاورستان", 200000, false, 4 },
                    { 156, "چرمین", 200000, false, 4 },
                    { 157, "رزوه", 200000, false, 4 },
                    { 158, "فریدونشهر", 200000, false, 4 },
                    { 159, "طرق آباد", 200000, false, 4 },
                    { 160, "نصر آباد", 200000, false, 4 },
                    { 161, "برزک", 200000, false, 4 },
                    { 162, "سفید شهر", 200000, false, 4 },
                    { 163, "سمیرم", 200000, false, 4 },
                    { 164, "گلدشت", 200000, false, 4 },
                    { 165, "اردستان", 200000, false, 4 },
                    { 166, "جوشقان قالی", 200000, false, 4 },
                    { 167, "بویین و میاندشت", 200000, false, 4 },
                    { 168, "کرکوند", 200000, false, 4 },
                    { 169, "درچه", 200000, false, 4 },
                    { 170, "انارک", 200000, false, 4 },
                    { 171, "دولت آباد", 200000, false, 4 },
                    { 172, "ایمانشهر", 200000, false, 4 },
                    { 173, "گرگاب", 200000, false, 4 },
                    { 174, "حسن آباد", 200000, false, 4 },
                    { 175, "سده لنجان", 200000, false, 4 },
                    { 176, "حبیب آباد", 200000, false, 4 },
                    { 177, "بهاران", 200000, false, 4 },
                    { 178, "میمه", 200000, false, 4 },
                    { 179, "تودشک", 200000, false, 4 },
                    { 180, "گلشهر", 200000, false, 4 },
                    { 181, "رضوانشهر", 200000, false, 4 },
                    { 182, "داران", 200000, false, 4 },
                    { 183, "علویجه", 200000, false, 4 },
                    { 184, "نیک آباد", 200000, false, 4 },
                    { 185, "مشکات", 200000, false, 4 },
                    { 186, "آران و بیدگل", 200000, false, 4 },
                    { 187, "خوانسار", 200000, false, 4 },
                    { 188, "نجف آباد", 200000, false, 4 },
                    { 189, "منظریه", 200000, false, 4 },
                    { 190, "فرخی", 200000, false, 4 },
                    { 191, "دیزیچه", 200000, false, 4 },
                    { 192, "اژیه", 200000, false, 4 },
                    { 193, "زاینده رود", 200000, false, 4 },
                    { 194, "خورزوق", 200000, false, 4 },
                    { 195, "قهدریجان", 200000, false, 4 },
                    { 196, "شاهین شهر", 200000, false, 4 },
                    { 197, "بهارستان", 200000, false, 4 },
                    { 198, "چمگردان", 200000, false, 4 },
                    { 199, "دهاقان", 200000, false, 4 },
                    { 200, "برف انبار", 200000, false, 4 },
                    { 201, "بادرود", 200000, false, 4 },
                    { 202, "کوهپایه", 200000, false, 4 },
                    { 203, "گلپایگان", 200000, false, 4 },
                    { 204, "عسگران", 200000, false, 4 },
                    { 205, "حنا", 200000, false, 4 },
                    { 206, "کهریزسنگ", 200000, false, 4 },
                    { 207, "مهاباد", 200000, false, 4 },
                    { 208, "کامو و چوگان", 200000, false, 4 },
                    { 209, "افوس", 200000, false, 4 },
                    { 300, "زیباشهر", 200000, false, 4 },
                    { 301, "کوشک", 200000, false, 4 },
                    { 302, "نایین", 200000, false, 4 },
                    { 303, "سین", 200000, false, 4 },
                    { 304, "زازران", 200000, false, 4 },
                    { 305, "مبارکه", 200000, false, 4 },
                    { 306, "ورزنه", 200000, false, 4 },
                    { 307, "ورنامخواست", 200000, false, 4 },
                    { 308, "شاپور آباد", 200000, false, 4 },
                    { 309, "فلاورجان", 200000, false, 4 },
                    { 310, "وزوان", 200000, false, 4 },
                    { 311, "اصفهان", 200000, false, 4 },
                    { 312, "باغ بهادران", 200000, false, 4 },
                    { 313, "چادگان", 200000, false, 4 },
                    { 314, "دامنه", 200000, false, 4 },
                    { 315, "نطنز", 200000, false, 4 },
                    { 316, "محمد آباد", 200000, false, 4 },
                    { 317, "نیاسر", 200000, false, 4 },
                    { 318, "نوش آباد", 200000, false, 4 },
                    { 319, "کمه", 200000, false, 4 },
                    { 320, "جوزدان", 200000, false, 4 },
                    { 321, "قمصر", 200000, false, 4 },
                    { 322, "جندق", 200000, false, 4 },
                    { 323, "طالخونچه", 200000, false, 4 },
                    { 324, "خمینی شهر", 200000, false, 4 },
                    { 325, "باغشاد", 200000, false, 4 },
                    { 326, "دستگرد", 200000, false, 4 },
                    { 327, "ابریشم", 200000, false, 4 },
                    { 328, "چهارباغ", 200000, false, 5 },
                    { 329, "آسارا", 200000, false, 5 },
                    { 330, "کرج", 200000, false, 5 },
                    { 331, "طالقان", 200000, false, 5 },
                    { 332, "شهرجدید هشتگرد", 200000, false, 5 },
                    { 333, "محمدشهر", 200000, false, 5 },
                    { 334, "مشکین دشت", 200000, false, 5 },
                    { 335, "نظرآباد", 200000, false, 5 },
                    { 336, "هشتگرد", 200000, false, 5 },
                    { 337, "ماهدشت", 200000, false, 5 },
                    { 338, "اشتهارد", 200000, false, 5 },
                    { 339, "کوهسار", 200000, false, 5 },
                    { 340, "گرمدره", 200000, false, 5 },
                    { 341, "تنکمان", 200000, false, 5 },
                    { 342, "گلسار", 200000, false, 5 },
                    { 343, "کمال شهر", 200000, false, 5 },
                    { 344, "فردیس", 200000, false, 5 },
                    { 345, "آبدانان", 200000, false, 6 },
                    { 346, "شباب", 200000, false, 6 },
                    { 347, "موسیان", 200000, false, 6 },
                    { 348, "بدره", 200000, false, 6 },
                    { 349, "ایلام", 200000, false, 6 },
                    { 350, "ایوان", 200000, false, 6 },
                    { 351, "مهران", 200000, false, 6 },
                    { 352, "آسمان آباد", 200000, false, 6 },
                    { 353, "پهله", 200000, false, 6 },
                    { 354, "مهر", 200000, false, 6 },
                    { 355, "سراب باغ", 200000, false, 6 },
                    { 356, "بلاوه", 200000, false, 6 },
                    { 357, "میمه", 200000, false, 6 },
                    { 358, "دره شهر", 200000, false, 6 },
                    { 359, "ارکواز", 200000, false, 6 },
                    { 360, "مورموری", 200000, false, 6 },
                    { 361, "توحید", 200000, false, 6 },
                    { 362, "دهلران", 200000, false, 6 },
                    { 363, "لومار", 200000, false, 6 },
                    { 364, "چوار", 200000, false, 6 },
                    { 365, "زرنه", 200000, false, 6 },
                    { 366, "صالح آباد", 200000, false, 6 },
                    { 367, "سرابله", 200000, false, 6 },
                    { 368, "ماژین", 200000, false, 6 },
                    { 369, "دلگشا", 200000, false, 6 },
                    { 370, "ریز", 200000, false, 7 },
                    { 371, "برازجان", 200000, false, 7 },
                    { 372, "بندر ریگ", 200000, false, 7 },
                    { 373, "اهرم", 200000, false, 7 },
                    { 374, "دوراهک", 200000, false, 7 },
                    { 375, "خورموج", 200000, false, 7 },
                    { 376, "نخل تقی", 200000, false, 7 },
                    { 377, "کلمه", 200000, false, 7 },
                    { 378, "بندر دیلم", 200000, false, 7 },
                    { 379, "وحدتیه", 200000, false, 7 },
                    { 380, "بنک", 200000, false, 7 },
                    { 381, "چغادک", 200000, false, 7 },
                    { 382, "بندر دیر", 200000, false, 7 },
                    { 383, "کاکی", 200000, false, 7 },
                    { 384, "جم", 200000, false, 7 },
                    { 385, "دالکی", 200000, false, 7 },
                    { 386, "بندر گناوه", 200000, false, 7 },
                    { 387, "آباد", 200000, false, 7 },
                    { 388, "آبدان", 200000, false, 7 },
                    { 389, "خارک", 200000, false, 7 },
                    { 390, "شنبه", 200000, false, 7 },
                    { 391, "بوشکان", 200000, false, 7 },
                    { 392, "انارستان", 200000, false, 7 },
                    { 393, "شبانکاره", 200000, false, 7 },
                    { 394, "سیراف", 200000, false, 7 },
                    { 395, "دلوار", 200000, false, 7 },
                    { 396, "بردستان", 200000, false, 7 },
                    { 397, "بادوله", 200000, false, 7 },
                    { 398, "عسلویه", 200000, false, 7 },
                    { 399, "تنگ ارم", 200000, false, 7 },
                    { 400, "امام حسن", 200000, false, 7 },
                    { 401, "سعد آباد", 200000, false, 7 },
                    { 402, "بندر کنگان", 200000, false, 7 },
                    { 403, "بوشهر", 200000, false, 7 },
                    { 404, "بردخون", 200000, false, 7 },
                    { 405, "آب پخش", 200000, false, 7 },
                    { 406, "شاهدشهر", 200000, false, 8 },
                    { 407, "پیشوا", 200000, false, 8 },
                    { 408, "جوادآباد", 200000, false, 8 },
                    { 409, "ارجمند", 200000, false, 8 },
                    { 410, "ری", 200000, false, 8 },
                    { 411, "نصیر شهر", 200000, false, 8 },
                    { 412, "رودهن", 200000, false, 8 },
                    { 413, "اندیشه", 200000, false, 8 },
                    { 414, "نسیم شهر", 200000, false, 8 },
                    { 415, "صبا شهر", 200000, false, 8 },
                    { 416, "ملارد", 200000, false, 8 },
                    { 417, "شمشک", 200000, false, 8 },
                    { 418, "پاکدشت", 200000, false, 8 },
                    { 419, "باقرشهر", 200000, false, 8 },
                    { 420, "احمدآباد مستوفی", 200000, false, 8 },
                    { 421, "کیلان", 200000, false, 8 },
                    { 422, "قرچک", 200000, false, 8 },
                    { 423, "فردوسیه", 200000, false, 8 },
                    { 424, "گلستان", 200000, false, 8 },
                    { 425, "ورامین", 200000, false, 8 },
                    { 426, "فیروزکوه", 200000, false, 8 },
                    { 427, "فشم", 200000, false, 8 },
                    { 428, "پرند", 200000, false, 8 },
                    { 429, "آبعلی", 200000, false, 8 },
                    { 430, "چهاردانگه", 200000, false, 8 },
                    { 431, "تهران", 200000, false, 8 },
                    { 432, "بومهن", 200000, false, 8 },
                    { 433, "وحیدیه", 200000, false, 8 },
                    { 434, "صفادشت", 200000, false, 8 },
                    { 435, "لواسان", 200000, false, 8 },
                    { 436, "فرون آباد", 200000, false, 8 },
                    { 437, "کهریزک", 200000, false, 8 },
                    { 438, "رباط کریم", 200000, false, 8 },
                    { 439, "آبسرد", 200000, false, 8 },
                    { 440, "باغستان", 200000, false, 8 },
                    { 441, "صالحیه", 200000, false, 8 },
                    { 442, "شهریار", 200000, false, 8 },
                    { 443, "قدس", 200000, false, 8 },
                    { 444, "تجریش", 200000, false, 8 },
                    { 445, "شریف آباد", 200000, false, 8 },
                    { 446, "حسن آباد", 200000, false, 8 },
                    { 447, "اسلامشهر", 200000, false, 8 },
                    { 448, "دماوند", 200000, false, 8 },
                    { 449, "پردیس", 200000, false, 8 },
                    { 450, "وردنجان", 200000, false, 9 },
                    { 451, "گوجان", 200000, false, 9 },
                    { 452, "گهرو", 200000, false, 9 },
                    { 453, "سورشجان", 200000, false, 9 },
                    { 454, "سرخون", 200000, false, 9 },
                    { 455, "شهرکرد", 200000, false, 9 },
                    { 456, "منج", 200000, false, 9 },
                    { 457, "بروجن", 200000, false, 9 },
                    { 458, "پردنجان", 200000, false, 9 },
                    { 459, "سامان", 200000, false, 9 },
                    { 460, "فرخ شهر", 200000, false, 9 },
                    { 461, "صمصامی", 200000, false, 9 },
                    { 462, "طاقانک", 200000, false, 9 },
                    { 463, "کاج", 200000, false, 9 },
                    { 464, "نقنه", 200000, false, 9 },
                    { 465, "لردگان", 200000, false, 9 },
                    { 466, "باباحیدر", 200000, false, 9 },
                    { 467, "دستنا", 200000, false, 9 },
                    { 468, "سودجان", 200000, false, 9 },
                    { 469, "بازفت", 200000, false, 9 },
                    { 470, "هفشجان", 200000, false, 9 },
                    { 471, "سردشت", 200000, false, 9 },
                    { 472, "فرادنبه", 200000, false, 9 },
                    { 473, "چلیچه", 200000, false, 9 },
                    { 474, "بن", 200000, false, 9 },
                    { 475, "فارسان", 200000, false, 9 },
                    { 476, "شلمزار", 200000, false, 9 },
                    { 477, "نافچ", 200000, false, 9 },
                    { 478, "دشتک", 200000, false, 9 },
                    { 479, "بلداجی", 200000, false, 9 },
                    { 480, "آلونی", 200000, false, 9 },
                    { 481, "گندمان", 200000, false, 9 },
                    { 482, "جونقان", 200000, false, 9 },
                    { 483, "ناغان", 200000, false, 9 },
                    { 484, "هارونی", 200000, false, 9 },
                    { 485, "چلگرد", 200000, false, 9 },
                    { 486, "کیان", 200000, false, 9 },
                    { 487, "اردل", 200000, false, 9 },
                    { 488, "سفیددشت", 200000, false, 9 },
                    { 489, "مال خلیفه", 200000, false, 9 },
                    { 490, "اسلامیه", 200000, false, 10 },
                    { 491, "شوسف", 200000, false, 10 },
                    { 492, "قاین", 200000, false, 10 },
                    { 493, "عشق آباد", 200000, false, 10 },
                    { 494, "طبس مسینا", 200000, false, 10 },
                    { 495, "ارسک", 200000, false, 10 },
                    { 496, "آیسک", 200000, false, 10 },
                    { 497, "نیمبلوک", 200000, false, 10 },
                    { 498, "دیهوک", 200000, false, 10 },
                    { 499, "سر بیشه", 200000, false, 10 },
                    { 500, "محمدشهر", 200000, false, 10 },
                    { 501, "بیرجند", 200000, false, 10 },
                    { 502, "فردوس", 200000, false, 10 },
                    { 503, "نهبندان", 200000, false, 10 },
                    { 504, "اسفدن", 200000, false, 10 },
                    { 505, "گزیک", 200000, false, 10 },
                    { 506, "حاجی آباد", 200000, false, 10 },
                    { 507, "سه قلعه", 200000, false, 10 },
                    { 508, "آرین شهر", 200000, false, 10 },
                    { 509, "مود", 200000, false, 10 },
                    { 510, "خوسف", 200000, false, 10 },
                    { 511, "قهستان", 200000, false, 10 },
                    { 512, "بشرویه", 200000, false, 10 },
                    { 513, "سرایان", 200000, false, 10 },
                    { 514, "خضری دشت بیاض", 200000, false, 10 },
                    { 515, "طبس", 200000, false, 10 },
                    { 516, "اسدیه", 200000, false, 10 },
                    { 517, "زهان", 200000, false, 10 },
                    { 518, "بار", 200000, false, 11 },
                    { 519, "نیل شهر", 200000, false, 11 },
                    { 520, "جنگل", 200000, false, 11 },
                    { 521, "درود", 200000, false, 11 },
                    { 522, "رباط سنگ", 200000, false, 11 },
                    { 523, "سلطان آباد", 200000, false, 11 },
                    { 524, "فریمان", 200000, false, 11 },
                    { 525, "گناباد", 200000, false, 11 },
                    { 526, "کاریز", 200000, false, 11 },
                    { 527, "همت آباد", 200000, false, 11 },
                    { 528, "سلامی", 200000, false, 11 },
                    { 529, "باجگیران", 200000, false, 11 },
                    { 530, "بجستان", 200000, false, 11 },
                    { 531, "چناران", 200000, false, 11 },
                    { 532, "درگز", 200000, false, 11 },
                    { 533, "کلات", 200000, false, 11 },
                    { 534, "چکنه", 200000, false, 11 },
                    { 535, "نصرآباد", 200000, false, 11 },
                    { 536, "بردسکن", 200000, false, 11 },
                    { 537, "مشهد", 200000, false, 11 },
                    { 538, "کدکن", 200000, false, 11 },
                    { 539, "نقاب", 200000, false, 11 },
                    { 540, "قلندرآباد", 200000, false, 11 },
                    { 541, "کاشمر", 200000, false, 11 },
                    { 542, "شاندیز", 200000, false, 11 },
                    { 543, "نشتیفان", 200000, false, 11 },
                    { 544, "ششتمد", 200000, false, 11 },
                    { 545, "شادمهر", 200000, false, 11 },
                    { 546, "عشق آباد", 200000, false, 11 },
                    { 547, "چاپشلو", 200000, false, 11 },
                    { 548, "رشتخوار", 200000, false, 11 },
                    { 549, "قدمگاه", 200000, false, 11 },
                    { 550, "صالح آباد", 200000, false, 11 },
                    { 551, "داورزن", 200000, false, 11 },
                    { 552, "فرهادگاه", 200000, false, 11 },
                    { 553, "کاخک", 200000, false, 11 },
                    { 554, "مشهدریزه", 200000, false, 11 },
                    { 555, "جغتای", 200000, false, 11 },
                    { 556, "مزدآوند", 200000, false, 11 },
                    { 557, "قوچان", 200000, false, 11 },
                    { 558, "یونسی", 200000, false, 11 },
                    { 559, "سنگان", 200000, false, 11 },
                    { 560, "نوخندان", 200000, false, 11 },
                    { 561, "کندر", 200000, false, 11 },
                    { 562, "نیشابور", 200000, false, 11 },
                    { 563, "احمدآباد صولت", 200000, false, 11 },
                    { 564, "شهرآباد", 200000, false, 11 },
                    { 565, "رضویه", 200000, false, 11 },
                    { 566, "تربت حیدریه", 200000, false, 11 },
                    { 567, "باخرز", 200000, false, 11 },
                    { 568, "سفید سنگ", 200000, false, 11 },
                    { 569, "بیدخت", 200000, false, 11 },
                    { 570, "تایباد", 200000, false, 11 },
                    { 571, "فیروزه", 200000, false, 11 },
                    { 572, "قاسم آباد", 200000, false, 11 },
                    { 573, "سبزوار", 200000, false, 11 },
                    { 574, "فیض آباد", 200000, false, 11 },
                    { 575, "گلمکان", 200000, false, 11 },
                    { 576, "لطف آباد", 200000, false, 11 },
                    { 577, "شهرزو", 200000, false, 11 },
                    { 578, "خرو", 200000, false, 11 },
                    { 579, "تربت جام", 200000, false, 11 },
                    { 580, "انابد", 200000, false, 11 },
                    { 581, "ملک آباد", 200000, false, 11 },
                    { 582, "بایک", 200000, false, 11 },
                    { 583, "دولت آباد", 200000, false, 11 },
                    { 584, "سرخس", 200000, false, 11 },
                    { 585, "ریوش", 200000, false, 11 },
                    { 586, "طرقبه", 200000, false, 11 },
                    { 587, "خواف", 200000, false, 11 },
                    { 588, "روداب", 200000, false, 11 },
                    { 589, "خلیل آباد", 200000, false, 11 },
                    { 590, "چناران شهر", 200000, false, 12 },
                    { 591, "راز", 200000, false, 12 },
                    { 592, "پیش قلعه", 200000, false, 12 },
                    { 593, "قوشخانه", 200000, false, 12 },
                    { 594, "شوقان", 200000, false, 12 },
                    { 595, "اسفراین", 200000, false, 12 },
                    { 596, "گرمه", 200000, false, 12 },
                    { 597, "قاضی", 200000, false, 12 },
                    { 598, "شیروان", 200000, false, 12 },
                    { 599, "خصار گرمخان", 200000, false, 12 },
                    { 600, "آشخانه", 200000, false, 12 },
                    { 601, "تیتکانلو", 200000, false, 12 },
                    { 602, "جاجرم", 200000, false, 12 },
                    { 603, "بجنورد", 200000, false, 12 },
                    { 604, "درق", 200000, false, 12 },
                    { 605, "آوا", 200000, false, 12 },
                    { 606, "زیارت", 200000, false, 12 },
                    { 607, "سنخواست", 200000, false, 12 },
                    { 608, "صفی آباد", 200000, false, 12 },
                    { 609, "ایور", 200000, false, 12 },
                    { 610, "فاروج", 200000, false, 12 },
                    { 611, "لوجلی", 200000, false, 12 },
                    { 612, "هفتگل", 200000, false, 13 },
                    { 613, "بیدروبه", 200000, false, 13 },
                    { 614, "شاوور", 200000, false, 13 },
                    { 615, "حمزه", 200000, false, 13 },
                    { 616, "گتوند", 200000, false, 13 },
                    { 617, "شرافت", 200000, false, 13 },
                    { 618, "منصوریه", 200000, false, 13 },
                    { 619, "زهره", 200000, false, 13 },
                    { 620, "رامهرمز", 200000, false, 13 },
                    { 621, "بندر امام خمینی", 200000, false, 13 },
                    { 622, "کوت عبداله", 200000, false, 13 },
                    { 623, "میداود", 200000, false, 13 },
                    { 624, "چغامیش", 200000, false, 13 },
                    { 625, "ملاثانی", 200000, false, 13 },
                    { 626, "چم گلک", 200000, false, 13 },
                    { 627, "حر", 200000, false, 13 },
                    { 628, "شمس آباد", 200000, false, 13 },
                    { 629, "آبژدان", 200000, false, 13 },
                    { 630, "چوبیده", 200000, false, 13 },
                    { 631, "مسجد سلیمان", 200000, false, 13 },
                    { 632, "مقاومت", 200000, false, 13 },
                    { 633, "ترکالکی", 200000, false, 13 },
                    { 634, "دارخوین", 200000, false, 13 },
                    { 635, "سردشت", 200000, false, 13 },
                    { 636, "لالی", 200000, false, 13 },
                    { 637, "کوت سید نعیم", 200000, false, 13 },
                    { 638, "حمیدیه", 200000, false, 13 },
                    { 639, "دهدز", 200000, false, 13 },
                    { 640, "قلعه تل", 200000, false, 13 },
                    { 641, "میانرود", 200000, false, 13 },
                    { 642, "رفیع", 200000, false, 13 },
                    { 643, "اندیمشک", 200000, false, 13 },
                    { 644, "الوان", 200000, false, 13 },
                    { 645, "سالند", 200000, false, 13 },
                    { 646, "صالح شهر", 200000, false, 13 },
                    { 647, "اروندکنار", 200000, false, 13 },
                    { 648, "سرداران", 200000, false, 13 },
                    { 649, "تشان", 200000, false, 13 },
                    { 650, "رامشیر", 200000, false, 13 },
                    { 651, "شادگان", 200000, false, 13 },
                    { 652, "بندر ماهشهر", 200000, false, 13 },
                    { 653, "جایزان", 200000, false, 13 },
                    { 654, "بستان", 200000, false, 13 },
                    { 655, "ویس", 200000, false, 13 },
                    { 656, "اهواز", 200000, false, 13 },
                    { 657, "فتح المبین", 200000, false, 13 },
                    { 658, "شهر امام", 200000, false, 13 },
                    { 659, "قلعه خواجه", 200000, false, 13 },
                    { 660, "حسینیه", 200000, false, 13 },
                    { 661, "گلگیر", 200000, false, 13 },
                    { 662, "مینوشهر", 200000, false, 13 },
                    { 663, "سماله", 200000, false, 13 },
                    { 664, "شوشتر", 200000, false, 13 },
                    { 665, "بهبهان", 200000, false, 13 },
                    { 666, "هندیجان", 200000, false, 13 },
                    { 667, "ابوحمیظه", 200000, false, 13 },
                    { 668, "آغاجاری", 200000, false, 13 },
                    { 669, "ایذه", 200000, false, 13 },
                    { 670, "صیدون", 200000, false, 13 },
                    { 671, "سیاه منصور", 200000, false, 13 },
                    { 672, "هویزه", 200000, false, 13 },
                    { 673, "آزادی", 200000, false, 13 },
                    { 674, "شوش", 200000, false, 13 },
                    { 675, "دزفول", 200000, false, 13 },
                    { 676, "جنت مکان", 200000, false, 13 },
                    { 677, "آبادان", 200000, false, 13 },
                    { 678, "گوریه", 200000, false, 13 },
                    { 679, "خرمشهر", 200000, false, 13 },
                    { 680, "مشراگه", 200000, false, 13 },
                    { 681, "خنافره", 200000, false, 13 },
                    { 682, "چمران", 200000, false, 13 },
                    { 683, "امیدیه", 200000, false, 13 },
                    { 684, "سوسنگرد", 200000, false, 13 },
                    { 685, "شیبان", 200000, false, 13 },
                    { 686, "الهایی", 200000, false, 13 },
                    { 687, "باغ ملک", 200000, false, 13 },
                    { 688, "صفی آباد", 200000, false, 13 },
                    { 689, "سجاس", 200000, false, 14 },
                    { 690, "زرین رود", 200000, false, 14 },
                    { 691, "آب بر", 200000, false, 14 },
                    { 692, "ارمغانخانه", 200000, false, 14 },
                    { 693, "کرسف", 200000, false, 14 },
                    { 694, "هیدج", 200000, false, 14 },
                    { 695, "سلطانیه", 200000, false, 14 },
                    { 696, "خرمدره", 200000, false, 14 },
                    { 697, "نیک پی", 200000, false, 14 },
                    { 698, "قیدار", 200000, false, 14 },
                    { 699, "ابهر", 200000, false, 14 },
                    { 700, "دندی", 200000, false, 14 },
                    { 701, "حلب", 200000, false, 14 },
                    { 702, "نور بهار", 200000, false, 14 },
                    { 703, "گرماب", 200000, false, 14 },
                    { 704, "چورزق", 200000, false, 14 },
                    { 705, "زنجان", 200000, false, 14 },
                    { 706, "سهرود", 200000, false, 14 },
                    { 707, "صایین قلعه", 200000, false, 14 },
                    { 708, "ماه نشان", 200000, false, 14 },
                    { 709, "زرین آباد", 200000, false, 14 },
                    { 710, "ایوانکی", 200000, false, 15 },
                    { 711, "مجن", 200000, false, 15 },
                    { 712, "دامغان", 200000, false, 15 },
                    { 713, "سرخه", 200000, false, 15 },
                    { 714, "مهدی شهر", 200000, false, 15 },
                    { 715, "شاهرود", 200000, false, 15 },
                    { 716, "سمنان", 200000, false, 15 },
                    { 717, "کهن آباد", 200000, false, 15 },
                    { 718, "گرمسار", 200000, false, 15 },
                    { 719, "کلاته خیج", 200000, false, 15 },
                    { 720, "دیباج", 200000, false, 15 },
                    { 721, "درجزین", 200000, false, 15 },
                    { 722, "رودیان", 200000, false, 15 },
                    { 723, "بسطام", 200000, false, 15 },
                    { 724, "امیریه", 200000, false, 15 },
                    { 725, "میامی", 200000, false, 15 },
                    { 726, "شهمیرزاد", 200000, false, 15 },
                    { 727, "بیارجمند", 200000, false, 15 },
                    { 728, "کلاته", 200000, false, 15 },
                    { 729, "آرادان", 200000, false, 15 },
                    { 730, "محمدی", 200000, false, 16 },
                    { 731, "شهرک علی اکبر", 200000, false, 16 },
                    { 732, "بنجار", 200000, false, 16 },
                    { 733, "گلمورتی", 200000, false, 16 },
                    { 734, "نگور", 200000, false, 16 },
                    { 735, "راسک", 200000, false, 16 },
                    { 736, "بنت", 200000, false, 16 },
                    { 737, "قصرقند", 200000, false, 16 },
                    { 738, "جالق", 200000, false, 16 },
                    { 739, "هیدوچ", 200000, false, 16 },
                    { 740, "نوک آباد", 200000, false, 16 },
                    { 741, "زهک", 200000, false, 16 },
                    { 742, "بمبپور", 200000, false, 16 },
                    { 743, "پیشین", 200000, false, 16 },
                    { 744, "گشت", 200000, false, 16 },
                    { 745, "محمدآباد", 200000, false, 16 },
                    { 746, "زاهدان", 200000, false, 16 },
                    { 747, "زابلی", 200000, false, 16 },
                    { 748, "چاه بهار", 200000, false, 16 },
                    { 749, "زرآباد", 200000, false, 16 },
                    { 750, "بزمان", 200000, false, 16 },
                    { 751, "اسپکه", 200000, false, 16 },
                    { 752, "فنوج", 200000, false, 16 },
                    { 753, "سراوان", 200000, false, 16 },
                    { 754, "ادیمی", 200000, false, 16 },
                    { 755, "زابل", 200000, false, 16 },
                    { 756, "دوست محمد", 200000, false, 16 },
                    { 757, "ایرانشهر", 200000, false, 16 },
                    { 758, "سرباز", 200000, false, 16 },
                    { 759, "سیرکان", 200000, false, 16 },
                    { 760, "میرجاوه", 200000, false, 16 },
                    { 761, "نصرت آباد", 200000, false, 16 },
                    { 762, "سوران", 200000, false, 16 },
                    { 763, "خاش", 200000, false, 16 },
                    { 764, "کنارک", 200000, false, 16 },
                    { 765, "محمدان", 200000, false, 16 },
                    { 766, "نیک شهر", 200000, false, 16 },
                    { 767, "کازرون", 200000, false, 17 },
                    { 768, "کارزین", 200000, false, 17 },
                    { 769, "فدامی", 200000, false, 17 },
                    { 770, "خومه زار", 200000, false, 17 },
                    { 771, "سلطان آباد", 200000, false, 17 },
                    { 772, "فیروزآباد", 200000, false, 17 },
                    { 773, "دبیران", 200000, false, 17 },
                    { 774, "باب انار", 200000, false, 17 },
                    { 775, "رامجرد", 200000, false, 17 },
                    { 776, "سروستان", 200000, false, 17 },
                    { 777, "قره بلاغ", 200000, false, 17 },
                    { 778, "ارسنجان", 200000, false, 17 },
                    { 779, "دژکرد", 200000, false, 17 },
                    { 780, "بیرم", 200000, false, 17 },
                    { 781, "دهرم", 200000, false, 17 },
                    { 782, "شیراز", 200000, false, 17 },
                    { 783, "ایزدخواست", 200000, false, 17 },
                    { 784, "علامرودشت", 200000, false, 17 },
                    { 785, "اوز", 200000, false, 17 },
                    { 786, "وراوی", 200000, false, 17 },
                    { 787, "بیضا", 200000, false, 17 },
                    { 788, "نی ریز", 200000, false, 17 },
                    { 789, "کنار تخته", 200000, false, 17 },
                    { 790, "امام شهر", 200000, false, 17 },
                    { 791, "جهرم", 200000, false, 17 },
                    { 792, "بابامنیر", 200000, false, 17 },
                    { 793, "گراش", 200000, false, 17 },
                    { 794, "فسا", 200000, false, 17 },
                    { 795, "شهرپیر", 200000, false, 17 },
                    { 796, "حسن آباد", 200000, false, 17 },
                    { 797, "کامفیروز", 200000, false, 17 },
                    { 798, "خنچ", 200000, false, 17 },
                    { 799, "خانه زنیان", 200000, false, 17 },
                    { 800, "استهبان", 200000, false, 17 },
                    { 801, "بوانات", 200000, false, 17 },
                    { 802, "لطیفی", 200000, false, 17 },
                    { 803, "فراشبند", 200000, false, 17 },
                    { 804, "زرقان", 200000, false, 17 },
                    { 805, "صغاد", 200000, false, 17 },
                    { 806, "اشکنان", 200000, false, 17 },
                    { 807, "قائمیه", 200000, false, 17 },
                    { 808, "گله دار", 200000, false, 17 },
                    { 809, "دوبرجی", 200000, false, 17 },
                    { 810, "آباده طشک", 200000, false, 17 },
                    { 811, "خرامه", 200000, false, 17 },
                    { 812, "میمند", 200000, false, 17 },
                    { 813, "افزر", 200000, false, 17 },
                    { 814, "دوزه", 200000, false, 17 },
                    { 815, "سیدان", 200000, false, 17 },
                    { 816, "کوپن", 200000, false, 17 },
                    { 817, "زاهدشهر", 200000, false, 17 },
                    { 818, "قادرآباد", 200000, false, 17 },
                    { 819, "سده", 200000, false, 17 },
                    { 820, "بنارویه", 200000, false, 17 },
                    { 821, "سعادت شهر", 200000, false, 17 },
                    { 822, "شهر صدرا", 200000, false, 17 },
                    { 823, "سورمق", 200000, false, 17 },
                    { 824, "حسامی", 200000, false, 17 },
                    { 825, "جویم", 200000, false, 17 },
                    { 826, "خوزی", 200000, false, 17 },
                    { 827, "اردکان", 200000, false, 17 },
                    { 828, "فطرویه", 200000, false, 17 },
                    { 829, "نودان", 200000, false, 17 },
                    { 830, "مبارک آباددیز", 200000, false, 17 },
                    { 831, "داراب", 200000, false, 17 },
                    { 832, "نورآباد", 200000, false, 17 },
                    { 833, "کوار", 200000, false, 17 },
                    { 834, "نوبندگان", 200000, false, 17 },
                    { 835, "حاجی آباد", 200000, false, 17 },
                    { 836, "خاوران", 200000, false, 17 },
                    { 837, "مرودشت", 200000, false, 17 },
                    { 838, "کوهنجان", 200000, false, 17 },
                    { 839, "ششده", 200000, false, 17 },
                    { 840, "مزایجان", 200000, false, 17 },
                    { 841, "ایج", 200000, false, 17 },
                    { 842, "خور", 200000, false, 17 },
                    { 843, "نوجین", 200000, false, 17 },
                    { 844, "لپویی", 200000, false, 17 },
                    { 845, "بهمن", 200000, false, 17 },
                    { 846, "اهل", 200000, false, 17 },
                    { 847, "خشت", 200000, false, 17 },
                    { 848, "مهر", 200000, false, 17 },
                    { 849, "جنت شهر", 200000, false, 17 },
                    { 850, "مشکان", 200000, false, 17 },
                    { 851, "بالاده", 200000, false, 17 },
                    { 852, "قیر", 200000, false, 17 },
                    { 853, "قطب آباد", 200000, false, 17 },
                    { 854, "خانمین", 200000, false, 17 },
                    { 855, "مصیری", 200000, false, 17 },
                    { 856, "میانشهر", 200000, false, 17 },
                    { 857, "صفاشهر", 200000, false, 17 },
                    { 858, "اقلید", 200000, false, 17 },
                    { 859, "عمادده", 200000, false, 17 },
                    { 860, "مادر سلیمان", 200000, false, 17 },
                    { 861, "داریان", 200000, false, 17 },
                    { 862, "رونیز", 200000, false, 17 },
                    { 863, "کره ای", 200000, false, 17 },
                    { 864, "لار", 200000, false, 17 },
                    { 865, "اسیر", 200000, false, 17 },
                    { 866, "هماشهر", 200000, false, 17 },
                    { 867, "آباده", 200000, false, 17 },
                    { 868, "لامرد", 200000, false, 17 },
                    { 869, "سگزآباد", 200000, false, 18 },
                    { 870, "بیدستان", 200000, false, 18 },
                    { 871, "کوهین", 200000, false, 18 },
                    { 872, "رازمیان", 200000, false, 18 },
                    { 873, "خرمدشت", 200000, false, 18 },
                    { 874, "آبگرم", 200000, false, 18 },
                    { 875, "شال", 200000, false, 18 },
                    { 876, "شریفیه", 200000, false, 18 },
                    { 877, "اقبالیه", 200000, false, 18 },
                    { 878, "نرجه", 200000, false, 18 },
                    { 879, "ارداق", 200000, false, 18 },
                    { 880, "الوند", 200000, false, 18 },
                    { 881, "خاکعلی", 200000, false, 18 },
                    { 882, "سیردان", 200000, false, 18 },
                    { 883, "ضیاد آباد", 200000, false, 18 },
                    { 884, "بوئین زهرا", 200000, false, 18 },
                    { 885, "محمدیه", 200000, false, 18 },
                    { 886, "محمود آباد نمونه", 200000, false, 18 },
                    { 887, "معلم کلایه", 200000, false, 18 },
                    { 888, "اسفرورین", 200000, false, 18 },
                    { 889, "آوج", 200000, false, 18 },
                    { 890, "دانسفهان", 200000, false, 18 },
                    { 891, "آبیک", 200000, false, 18 },
                    { 892, "قزوین", 200000, false, 18 },
                    { 893, "تاکستان", 200000, false, 18 },
                    { 894, "کهک", 200000, false, 19 },
                    { 895, "قم", 200000, false, 19 },
                    { 896, "سلفچگان", 200000, false, 19 },
                    { 897, "جعفریه", 200000, false, 19 },
                    { 898, "قنوات", 200000, false, 19 },
                    { 899, "دستجرد", 200000, false, 19 },
                    { 900, "قروه", 200000, false, 20 },
                    { 901, "توپ آغاج", 200000, false, 20 },
                    { 902, "سروآباد", 200000, false, 20 },
                    { 903, "بوئین سفلی", 200000, false, 20 },
                    { 904, "زرینه", 200000, false, 20 },
                    { 905, "دلبران", 200000, false, 20 },
                    { 906, "سنندج", 200000, false, 20 },
                    { 907, "یاسوکند", 200000, false, 20 },
                    { 908, "موچش", 200000, false, 20 },
                    { 909, "بانه", 200000, false, 20 },
                    { 910, "مریوان", 200000, false, 20 },
                    { 911, "سریش آباد", 200000, false, 20 },
                    { 912, "صاحب", 200000, false, 20 },
                    { 913, "دهگلان", 200000, false, 20 },
                    { 914, "بابارشانی", 200000, false, 20 },
                    { 915, "دیواندره", 200000, false, 20 },
                    { 916, "برده رشه", 200000, false, 20 },
                    { 917, "شویشه", 200000, false, 20 },
                    { 918, "بیجار", 200000, false, 20 },
                    { 919, "اورامان تخت", 200000, false, 20 },
                    { 920, "کانی سور", 200000, false, 20 },
                    { 921, "کانی دینار", 200000, false, 20 },
                    { 922, "دزج", 200000, false, 20 },
                    { 923, "سقز", 200000, false, 20 },
                    { 924, "بلبان آباد", 200000, false, 20 },
                    { 925, "پیرتاج", 200000, false, 20 },
                    { 926, "کامیاران", 200000, false, 20 },
                    { 927, "آرمرده", 200000, false, 20 },
                    { 928, "چناره", 200000, false, 20 },
                    { 929, "کهنوج", 200000, false, 21 },
                    { 930, "بلوک", 200000, false, 21 },
                    { 931, "پاریز", 200000, false, 21 },
                    { 932, "گنبکی", 200000, false, 21 },
                    { 933, "زنگی آباد", 200000, false, 21 },
                    { 934, "بم", 200000, false, 21 },
                    { 935, "خانوک", 200000, false, 21 },
                    { 936, "کیانشهر", 200000, false, 21 },
                    { 937, "جوپار", 200000, false, 21 },
                    { 938, "عنبر آباد", 200000, false, 21 },
                    { 939, "جوزم", 200000, false, 21 },
                    { 940, "نظام شهر", 200000, false, 21 },
                    { 941, "لاله زار", 200000, false, 21 },
                    { 942, "کشکوئیه", 200000, false, 21 },
                    { 943, "زیدآباد", 200000, false, 21 },
                    { 944, "هنزا", 200000, false, 21 },
                    { 945, "چترود", 200000, false, 21 },
                    { 946, "جبالبارز", 200000, false, 21 },
                    { 947, "سیرجان", 200000, false, 21 },
                    { 948, "رودبار", 200000, false, 21 },
                    { 949, "کرمان", 200000, false, 21 },
                    { 950, "بافت", 200000, false, 21 },
                    { 951, "صفائیه", 200000, false, 21 },
                    { 952, "منوجان", 200000, false, 21 },
                    { 953, "اندوهجرد", 200000, false, 21 },
                    { 954, "هجدک", 200000, false, 21 },
                    { 955, "خورسند", 200000, false, 21 },
                    { 956, "امین شهر", 200000, false, 21 },
                    { 957, "بردسیر", 200000, false, 21 },
                    { 958, "رفسنجان", 200000, false, 21 },
                    { 959, "هماشهر", 200000, false, 21 },
                    { 960, "محمد آباد", 200000, false, 21 },
                    { 961, "اختیار آباد", 200000, false, 21 },
                    { 962, "بروات", 200000, false, 21 },
                    { 963, "ریحان", 200000, false, 21 },
                    { 964, "کوهبنان", 200000, false, 21 },
                    { 965, "ماهان", 200000, false, 21 },
                    { 966, "دوساری", 200000, false, 21 },
                    { 967, "دهج", 200000, false, 21 },
                    { 968, "فاریاب", 200000, false, 21 },
                    { 969, "گلزار", 200000, false, 21 },
                    { 970, "بهرمان", 200000, false, 21 },
                    { 971, "بلورد", 200000, false, 21 },
                    { 972, "فهرج", 200000, false, 21 },
                    { 973, "کاظم آباد", 200000, false, 21 },
                    { 974, "جیرفت", 200000, false, 21 },
                    { 975, "نجف شهر", 200000, false, 21 },
                    { 976, "قلعه گنج", 200000, false, 21 },
                    { 977, "باغین", 200000, false, 21 },
                    { 978, "بزنجان", 200000, false, 21 },
                    { 979, "زرند", 200000, false, 21 },
                    { 980, "نودژ", 200000, false, 21 },
                    { 981, "گلباف", 200000, false, 21 },
                    { 982, "راور", 200000, false, 21 },
                    { 983, "خاتون آباد", 200000, false, 21 },
                    { 984, "نرمالشیر", 200000, false, 21 },
                    { 985, "دشتکار", 200000, false, 21 },
                    { 986, "مس سرچسمه", 200000, false, 21 },
                    { 987, "خواجو شهر", 200000, false, 21 },
                    { 989, "رابر", 200000, false, 21 },
                    { 990, "راین", 200000, false, 21 },
                    { 991, "درب بهشت", 200000, false, 21 },
                    { 992, "یزدان شهر", 200000, false, 21 },
                    { 993, "زهکلوت", 200000, false, 21 },
                    { 994, "محی آباد", 200000, false, 21 },
                    { 995, "مردهک", 200000, false, 21 },
                    { 996, "شهداد", 200000, false, 21 },
                    { 997, "ارزوئیه", 200000, false, 21 },
                    { 998, "نگار", 200000, false, 21 },
                    { 999, "شهربابک", 200000, false, 21 },
                    { 1000, "انار", 200000, false, 21 },
                    { 1001, "سنقر", 200000, false, 22 },
                    { 1002, "شاهو", 200000, false, 22 },
                    { 1003, "بانوره", 200000, false, 22 },
                    { 1004, "تازه آباد", 200000, false, 22 },
                    { 1005, "هلشی", 200000, false, 22 },
                    { 1006, "جوانرود", 200000, false, 22 },
                    { 1007, "قصر شیرین", 200000, false, 22 },
                    { 1008, "نوسود", 200000, false, 22 },
                    { 1009, "کرند", 200000, false, 22 },
                    { 1010, "کوزران", 200000, false, 22 },
                    { 1011, "بیستون", 200000, false, 22 },
                    { 1012, "حمیل", 200000, false, 22 },
                    { 1013, "گیلانغرب", 200000, false, 22 },
                    { 1014, "سطر", 200000, false, 22 },
                    { 1015, "روانسر", 200000, false, 22 },
                    { 1016, "پاوه", 200000, false, 22 },
                    { 1017, "ازگله", 200000, false, 22 },
                    { 1018, "کرمانشاه", 200000, false, 22 },
                    { 1019, "میان راهان", 200000, false, 22 },
                    { 1020, "کنگاور", 200000, false, 22 },
                    { 1021, "سرپل ذهاب", 200000, false, 22 },
                    { 1022, "ریجاب", 200000, false, 22 },
                    { 1023, "باینگان", 200000, false, 22 },
                    { 1024, "هرسین", 200000, false, 22 },
                    { 1025, "اسلام آباد غرب", 200000, false, 22 },
                    { 1026, "سرمست", 200000, false, 22 },
                    { 1027, "سومار", 200000, false, 22 },
                    { 1028, "نودشه", 200000, false, 22 },
                    { 1029, "گهواره", 200000, false, 22 },
                    { 1030, "رباط", 200000, false, 22 },
                    { 1031, "صحنه", 200000, false, 22 },
                    { 1032, "گودین", 200000, false, 22 },
                    { 1033, "گراب سفلی", 200000, false, 23 },
                    { 1034, "لنده", 200000, false, 23 },
                    { 1035, "سی سخت", 200000, false, 23 },
                    { 1036, "دهدشت", 200000, false, 23 },
                    { 1037, "یاسوج", 200000, false, 23 },
                    { 1038, "سرفاریاب", 200000, false, 23 },
                    { 1039, "دوگنبدان", 200000, false, 23 },
                    { 1040, "چیتاب", 200000, false, 23 },
                    { 1041, "لیکک", 200000, false, 23 },
                    { 1042, "دیشموک", 200000, false, 23 },
                    { 1043, "مادوان", 200000, false, 23 },
                    { 1044, "باشت", 200000, false, 23 },
                    { 1045, "پاتاوه", 200000, false, 23 },
                    { 1046, "قلعه رئیسی", 200000, false, 23 },
                    { 1047, "مارگون", 200000, false, 23 },
                    { 1048, "چرام", 200000, false, 23 },
                    { 1049, "سوق", 200000, false, 23 },
                    { 1050, "سیمین شهر", 200000, false, 24 },
                    { 1051, "مزرعه", 200000, false, 24 },
                    { 1052, "رامیان", 200000, false, 24 },
                    { 1053, "فراغی", 200000, false, 24 },
                    { 1054, "گنبد کاووس", 200000, false, 24 },
                    { 1055, "کردکوی", 200000, false, 24 },
                    { 1056, "مراوه", 200000, false, 24 },
                    { 1057, "بندر ترکمن", 200000, false, 24 },
                    { 1058, "نگین شهر", 200000, false, 24 },
                    { 1059, "آق قلا", 200000, false, 24 },
                    { 1060, "سرخنکلاته", 200000, false, 24 },
                    { 1061, "گالیکش", 200000, false, 24 },
                    { 1062, "سنگدوین", 200000, false, 24 },
                    { 1063, "دلند", 200000, false, 24 },
                    { 1064, "بندر گز", 200000, false, 24 },
                    { 1065, "نوده خاندوز", 200000, false, 24 },
                    { 1066, "مینو دشت", 200000, false, 24 },
                    { 1067, "گرگان", 200000, false, 24 },
                    { 1068, "گمیش تپه", 200000, false, 24 },
                    { 1069, "علی آباد", 200000, false, 24 },
                    { 1070, "خان ببین", 200000, false, 24 },
                    { 1071, "کلاله", 200000, false, 24 },
                    { 1072, "اینچه برون", 200000, false, 24 },
                    { 1073, "فاضل آباد", 200000, false, 24 },
                    { 1074, "تاتار علیا", 200000, false, 24 },
                    { 1075, "نوکنده", 200000, false, 24 },
                    { 1076, "آزاد شهر", 200000, false, 24 },
                    { 1077, "انبار آلوم", 200000, false, 24 },
                    { 1078, "جلین", 200000, false, 24 },
                    { 1079, "منجیل", 200000, false, 25 },
                    { 1080, "شلمان", 200000, false, 25 },
                    { 1081, "خشکبیجار", 200000, false, 25 },
                    { 1082, "ماکلوان", 200000, false, 25 },
                    { 1083, "سنگر", 200000, false, 25 },
                    { 1084, "مرجقل", 200000, false, 25 },
                    { 1085, "لیسار", 200000, false, 25 },
                    { 1086, "رضوانشهر", 200000, false, 25 },
                    { 1087, "رحیم آباد", 200000, false, 25 },
                    { 1088, "لوندویل", 200000, false, 25 },
                    { 1089, "احمد سرگوراب", 200000, false, 25 },
                    { 1090, "لوشان", 200000, false, 25 },
                    { 1091, "اطاقوار", 200000, false, 25 },
                    { 1092, "لشت نشاء", 200000, false, 25 },
                    { 1093, "فومن", 200000, false, 25 },
                    { 1094, "چوبر", 200000, false, 25 },
                    { 1095, "بازار جمعه", 200000, false, 25 },
                    { 1096, "کلاچای", 200000, false, 25 },
                    { 1097, "بندر انزلی", 200000, false, 25 },
                    { 1098, "املش", 200000, false, 25 },
                    { 1099, "رستم آباد", 200000, false, 25 },
                    { 1100, "لاهیجان", 200000, false, 25 },
                    { 1101, "توتکابن", 200000, false, 25 },
                    { 1102, "لنگرود", 200000, false, 25 },
                    { 1103, "کوچصفهان", 200000, false, 25 },
                    { 1104, "صومعه سرا", 200000, false, 25 },
                    { 1105, "اسالم", 200000, false, 25 },
                    { 1106, "دیلمان", 200000, false, 25 },
                    { 1107, "رودسر", 200000, false, 25 },
                    { 1108, "کیاشهر", 200000, false, 25 },
                    { 1109, "شفت", 200000, false, 25 },
                    { 1110, "رودبار", 200000, false, 25 },
                    { 1111, "کومله", 200000, false, 25 },
                    { 1112, "رشت", 200000, false, 25 },
                    { 1113, "ماسوله", 200000, false, 25 },
                    { 1114, "خمام", 200000, false, 25 },
                    { 1115, "ماسال", 200000, false, 25 },
                    { 1116, "واجارگاه", 200000, false, 25 },
                    { 1117, "هشتپر (تالش)", 200000, false, 25 },
                    { 1118, "پره سر", 200000, false, 25 },
                    { 1119, "بره سر", 200000, false, 25 },
                    { 1120, "آستارا", 200000, false, 25 },
                    { 1121, "رودبنه", 200000, false, 25 },
                    { 1122, "جیرنده", 200000, false, 25 },
                    { 1123, "چاف و چمخاله", 200000, false, 25 },
                    { 1124, "لولمان", 200000, false, 25 },
                    { 1125, "گوراب زرمیخ", 200000, false, 25 },
                    { 1126, "حویق", 200000, false, 25 },
                    { 1127, "سیاهکل", 200000, false, 25 },
                    { 1128, "چابکسر", 200000, false, 25 },
                    { 1129, "آستانه اشرفیه", 200000, false, 25 },
                    { 1130, "رانکوه", 200000, false, 25 },
                    { 1131, "چالانچولان", 200000, false, 26 },
                    { 1132, "بیران شهر", 200000, false, 26 },
                    { 1133, "ویسیان", 200000, false, 26 },
                    { 1134, "شول آباد", 200000, false, 26 },
                    { 1135, "پلدختر", 200000, false, 26 },
                    { 1136, "کوهدشت", 200000, false, 26 },
                    { 1137, "هفت چشمه", 200000, false, 26 },
                    { 1138, "بروجرد", 200000, false, 26 },
                    { 1139, "الشتر", 200000, false, 26 },
                    { 1140, "مومن آباد", 200000, false, 26 },
                    { 1141, "دورود", 200000, false, 26 },
                    { 1142, "زاغه", 200000, false, 26 },
                    { 1143, "چقابل", 200000, false, 26 },
                    { 1144, "الیگودرز", 200000, false, 26 },
                    { 1145, "معمولان", 200000, false, 26 },
                    { 1146, "کوهنانی", 200000, false, 26 },
                    { 1147, "نورآباد", 200000, false, 26 },
                    { 1148, "سپیددشت", 200000, false, 26 },
                    { 1149, "سراب دوره", 200000, false, 26 },
                    { 1150, "ازنا", 200000, false, 26 },
                    { 1151, "گراب", 200000, false, 26 },
                    { 1152, "خرم آباد", 200000, false, 26 },
                    { 1153, "اشترینان", 200000, false, 26 },
                    { 1154, "فیروز آباد", 200000, false, 26 },
                    { 1155, "درب گنبد", 200000, false, 26 },
                    { 1156, "گلوگاه", 200000, false, 27 },
                    { 1157, "پل سفید", 200000, false, 27 },
                    { 1158, "دابودشت", 200000, false, 27 },
                    { 1159, "چالوس", 200000, false, 27 },
                    { 1160, "کیاسر", 200000, false, 27 },
                    { 1161, "بهمنمیر", 200000, false, 27 },
                    { 1162, "تنکابن", 200000, false, 27 },
                    { 1163, "کلاردشت", 200000, false, 27 },
                    { 1164, "ایزدشهر", 200000, false, 27 },
                    { 1165, "گتاب", 200000, false, 27 },
                    { 1166, "سلمان شهر", 200000, false, 27 },
                    { 1167, "ارطه", 200000, false, 27 },
                    { 1168, "امیرکلا", 200000, false, 27 },
                    { 1169, "کوهی خیل", 200000, false, 27 },
                    { 1170, "پایین هولار", 200000, false, 27 },
                    { 1171, "گزنک", 200000, false, 27 },
                    { 1172, "محمود آباد", 200000, false, 27 },
                    { 1173, "رامسر", 200000, false, 27 },
                    { 1174, "نوشهر", 200000, false, 27 },
                    { 1175, "خلیل آباد", 200000, false, 27 },
                    { 1176, "کیاکلا", 200000, false, 27 },
                    { 1177, "نور", 200000, false, 27 },
                    { 1178, "مرزیکلا", 200000, false, 27 },
                    { 1179, "فریدونکنار", 200000, false, 27 },
                    { 1180, "زیرآب", 200000, false, 27 },
                    { 1181, "امامزاده عبدالله", 200000, false, 27 },
                    { 1182, "هچیرود", 200000, false, 27 },
                    { 1183, "فریم", 200000, false, 27 },
                    { 1184, "هادی شهر", 200000, false, 27 },
                    { 1185, "نشتارود", 200000, false, 27 },
                    { 1186, "پول", 200000, false, 27 },
                    { 1187, "بهشهر", 200000, false, 27 },
                    { 1188, "کلارآباد", 200000, false, 27 },
                    { 1189, "بلده", 200000, false, 27 },
                    { 1190, "بابل", 200000, false, 27 },
                    { 1191, "جویبار", 200000, false, 27 },
                    { 1192, "آلاشت", 200000, false, 27 },
                    { 1193, "آمل", 200000, false, 27 },
                    { 1194, "نکا", 200000, false, 27 },
                    { 1195, "کتالم و سادات شهر", 200000, false, 27 },
                    { 1196, "بابلسر", 200000, false, 27 },
                    { 1197, "شیرود", 200000, false, 27 },
                    { 1198, "شیرگاه", 200000, false, 27 },
                    { 1199, "رویان", 200000, false, 27 },
                    { 1200, "زرگر محله", 200000, false, 27 },
                    { 1201, "عباس آباد", 200000, false, 27 },
                    { 1202, "قائم شهر", 200000, false, 27 },
                    { 1203, "خوش رودپی", 200000, false, 27 },
                    { 1204, "مرزن آباد", 200000, false, 27 },
                    { 1205, "ساری", 200000, false, 27 },
                    { 1206, "رینه", 200000, false, 27 },
                    { 1207, "سرخرود", 200000, false, 27 },
                    { 1208, "خرم آباد", 200000, false, 27 },
                    { 1209, "کجور", 200000, false, 27 },
                    { 1210, "رستمکلا", 200000, false, 27 },
                    { 1211, "سورک", 200000, false, 27 },
                    { 1212, "چمستان", 200000, false, 27 },
                    { 1213, "آستانه", 200000, false, 28 },
                    { 1214, "خنجین", 200000, false, 28 },
                    { 1215, "نراق", 200000, false, 28 },
                    { 1216, "کمیجان", 200000, false, 28 },
                    { 1217, "آشتیان", 200000, false, 28 },
                    { 1218, "رازقان", 200000, false, 28 },
                    { 1219, "مهاجران", 200000, false, 28 },
                    { 1220, "غرق آباد", 200000, false, 28 },
                    { 1221, "خنداب", 200000, false, 28 },
                    { 1222, "قورچی باشی", 200000, false, 28 },
                    { 1223, "خشکرود", 200000, false, 28 },
                    { 1224, "ساروق", 200000, false, 28 },
                    { 1225, "محلات", 200000, false, 28 },
                    { 1226, "شازند", 200000, false, 28 },
                    { 1227, "ساوه", 200000, false, 28 },
                    { 1228, "میلاجرد", 200000, false, 28 },
                    { 1229, "تفرش", 200000, false, 28 },
                    { 1230, "زاویه", 200000, false, 28 },
                    { 1231, "اراک", 200000, false, 28 },
                    { 1232, "توره", 200000, false, 28 },
                    { 1233, "نوبران", 200000, false, 28 },
                    { 1234, "فرمهین", 200000, false, 28 },
                    { 1235, "دلیجان", 200000, false, 28 },
                    { 1236, "پرندک", 200000, false, 28 },
                    { 1237, "کارچان", 200000, false, 28 },
                    { 1238, "نیمور", 200000, false, 28 },
                    { 1239, "هندودر", 200000, false, 28 },
                    { 1240, "آوه", 200000, false, 28 },
                    { 1241, "جاورسیان", 200000, false, 28 },
                    { 1242, "خمین", 200000, false, 28 },
                    { 1243, "مامونیه", 200000, false, 28 },
                    { 1244, "داودآباد", 200000, false, 28 },
                    { 1245, "شهباز", 200000, false, 28 },
                    { 1246, "بیکاء", 200000, false, 29 },
                    { 1247, "تیرور", 200000, false, 29 },
                    { 1248, "گروک", 200000, false, 29 },
                    { 1249, "قشم", 200000, false, 29 },
                    { 1250, "کوشکنار", 200000, false, 29 },
                    { 1251, "کیش", 200000, false, 29 },
                    { 1252, "سرگز", 200000, false, 29 },
                    { 1253, "بندرعباس", 200000, false, 29 },
                    { 1254, "زیارتعلی", 200000, false, 29 },
                    { 1255, "سندرک", 200000, false, 29 },
                    { 1256, "کوهستک", 200000, false, 29 },
                    { 1257, "لمزان", 200000, false, 29 },
                    { 1258, "رویدر", 200000, false, 29 },
                    { 1259, "قلعه قاضی", 200000, false, 29 },
                    { 1260, "فارغان", 200000, false, 29 },
                    { 1261, "ابوموسی", 200000, false, 29 },
                    { 1262, "هشتبندی", 200000, false, 29 },
                    { 1263, "سردشت", 200000, false, 29 },
                    { 1264, "درگهان", 200000, false, 29 },
                    { 1265, "پارسیان", 200000, false, 29 },
                    { 1266, "کنگ", 200000, false, 29 },
                    { 1267, "جناح", 200000, false, 29 },
                    { 1268, "تازیان پایین", 200000, false, 29 },
                    { 1269, "دهبازر", 200000, false, 29 },
                    { 1270, "میناب", 200000, false, 29 },
                    { 1271, "سیریک", 200000, false, 29 },
                    { 1272, "سوزا", 200000, false, 29 },
                    { 1273, "خمیر", 200000, false, 29 },
                    { 1274, "چارک", 200000, false, 29 },
                    { 1275, "حاجی آباد", 200000, false, 29 },
                    { 1276, "فین", 200000, false, 29 },
                    { 1277, "بندر جاسک", 200000, false, 29 },
                    { 1278, "گوهران", 200000, false, 29 },
                    { 1279, "هرمز", 200000, false, 29 },
                    { 1280, "دشتی", 200000, false, 29 },
                    { 1281, "بندر لنگه", 200000, false, 29 },
                    { 1282, "بستک", 200000, false, 29 },
                    { 1283, "تخت", 200000, false, 29 },
                    { 1284, "زنگنه", 200000, false, 30 },
                    { 1285, "دمق", 200000, false, 30 },
                    { 1286, "سرکان", 200000, false, 30 },
                    { 1287, "آجین", 200000, false, 30 },
                    { 1288, "جورقان", 200000, false, 30 },
                    { 1289, "برزول", 200000, false, 30 },
                    { 1290, "فامنین", 200000, false, 30 },
                    { 1291, "سامن", 200000, false, 30 },
                    { 1292, "بهار", 200000, false, 30 },
                    { 1293, "فرسنج", 200000, false, 30 },
                    { 1294, "شیرین سو", 200000, false, 30 },
                    { 1295, "مریانج", 200000, false, 30 },
                    { 1296, "فیروزان", 200000, false, 30 },
                    { 1297, "قروه درجزین", 200000, false, 30 },
                    { 1298, "ازندریان", 200000, false, 30 },
                    { 1299, "لالجین", 200000, false, 30 },
                    { 1300, "گل تپه", 200000, false, 30 },
                    { 1301, "گیان", 200000, false, 30 },
                    { 1302, "ملایر", 200000, false, 30 },
                    { 1303, "صالح آباد", 200000, false, 30 },
                    { 1304, "تویسرکان", 200000, false, 30 },
                    { 1305, "اسدآباد", 200000, false, 30 },
                    { 1306, "همدان", 200000, false, 30 },
                    { 1307, "نهاوند", 200000, false, 30 },
                    { 1308, "رزن", 200000, false, 30 },
                    { 1309, "جوکار", 200000, false, 30 },
                    { 1310, "مهاجران", 200000, false, 30 },
                    { 1311, "کبودرآهنگ", 200000, false, 30 },
                    { 1312, "قهاوند", 200000, false, 30 },
                    { 1313, "مرودست", 200000, false, 31 },
                    { 1314, "مهردشت", 200000, false, 31 },
                    { 1315, "حمیدیا", 200000, false, 31 },
                    { 1316, "تفت", 200000, false, 31 },
                    { 1317, "اشکذر", 200000, false, 31 },
                    { 1318, "ندوشن", 200000, false, 31 },
                    { 1319, "یزد", 200000, false, 31 },
                    { 1320, "عقدا", 200000, false, 31 },
                    { 1321, "بهاباد", 200000, false, 31 },
                    { 1322, "ابرکوه", 200000, false, 31 },
                    { 1323, "زارچ", 200000, false, 31 },
                    { 1324, "نیر", 200000, false, 31 },
                    { 1325, "اردکان", 200000, false, 31 },
                    { 1326, "هرات", 200000, false, 31 },
                    { 1327, "بفروییه", 200000, false, 31 },
                    { 1328, "شاهدیه", 200000, false, 31 },
                    { 1329, "بافق", 200000, false, 31 },
                    { 1330, "خضرآباد", 200000, false, 31 },
                    { 1331, "میبد", 200000, false, 31 },
                    { 1332, "مهریز", 200000, false, 31 },
                    { 1333, "احمدآباد", 200000, false, 31 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "BirthDate", "Cellphone", "CountyId", "Email", "Family", "IsActive", "Name", "Password", "PostalCode", "RegDate", "UserName" },
                values: new object[] { 1, null, null, "09126617096", 330, null, "سایت", true, "مدیر", "osare1358t", null, new DateTime(2023, 3, 18, 17, 17, 17, 207, DateTimeKind.Local).AddTicks(7361), "osareTB" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "URId", "IsActive", "IsDeleted", "RegisterDate", "RoleId", "UserId" },
                values: new object[] { 1, true, false, new DateTime(2023, 3, 18, 17, 17, 17, 207, DateTimeKind.Local).AddTicks(7399), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_BannerNextSliders_BannerNextBaseId",
                table: "BannerNextSliders",
                column: "BannerNextBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComment_BlogId",
                table: "BlogComment",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogGroupId",
                table: "Blogs",
                column: "BlogGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductItemId",
                table: "CartItems",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_StateId",
                table: "Counties",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_Order_Id",
                table: "OrderDetails",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_OrderId",
                table: "OrderStatuses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatuses_StatusId",
                table: "OrderStatuses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PageBannerItems_PBId",
                table: "PageBannerItems",
                column: "PBId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentId",
                table: "Permissions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_ParentId",
                table: "ProductGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductId",
                table: "ProductItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BlogId",
                table: "Products",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductGroupId",
                table: "Products",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StateFreights_StateId",
                table: "StateFreights",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountyId",
                table: "Users",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouses_OrderdetialId",
                table: "WareHouses",
                column: "OrderdetialId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouses_ProductId",
                table: "WareHouses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WareHouses_ProductItemId",
                table: "WareHouses",
                column: "ProductItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BannerNextSliders");

            migrationBuilder.DropTable(
                name: "BestDeals");

            migrationBuilder.DropTable(
                name: "BlogComment");

            migrationBuilder.DropTable(
                name: "CellphonesBanks");

            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "DiscountCoupens");

            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "PageBannerItems");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SiteInfos");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "StateFreights");

            migrationBuilder.DropTable(
                name: "StorePageBanners");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "UploadCenters");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "WareHouses");

            migrationBuilder.DropTable(
                name: "BannerNextBases");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "PageBanners");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductItems");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "BlogGroups");
        }
    }
}
