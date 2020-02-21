using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class CreateCheckoutDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<string>(nullable: false),
                    AddressLine1 = table.Column<string>(maxLength: 64, nullable: false),
                    AddressLine2 = table.Column<string>(maxLength: 256, nullable: true),
                    TownOrCity = table.Column<string>(maxLength: 64, nullable: false),
                    Postcode = table.Column<string>(maxLength: 64, nullable: false),
                    Country = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    CardId = table.Column<string>(nullable: false),
                    CardNumber = table.Column<string>(nullable: false),
                    NameOnCard = table.Column<string>(maxLength: 256, nullable: false),
                    Cvv = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    MerchantId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.MerchantId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(nullable: false),
                    Title = table.Column<int>(nullable: false),
                    FistName = table.Column<string>(maxLength: 256, nullable: false),
                    FamilyName = table.Column<string>(maxLength: 256, nullable: false),
                    AddressId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<string>(nullable: false),
                    PaymentMerchantMerchantId = table.Column<string>(nullable: false),
                    PaymentCustomerCustomerId = table.Column<string>(nullable: false),
                    PaymentCardCardId = table.Column<string>(nullable: false),
                    PaymentAmount = table.Column<double>(nullable: false),
                    PaymentCurrency = table.Column<int>(nullable: false),
                    PaymentStatus = table.Column<int>(nullable: false),
                    ExternalPaymentReference = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Card_PaymentCardCardId",
                        column: x => x.PaymentCardCardId,
                        principalTable: "Card",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_PaymentCustomerCustomerId",
                        column: x => x.PaymentCustomerCustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Merchant_PaymentMerchantMerchantId",
                        column: x => x.PaymentMerchantMerchantId,
                        principalTable: "Merchant",
                        principalColumn: "MerchantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentCardCardId",
                table: "Payments",
                column: "PaymentCardCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentCustomerCustomerId",
                table: "Payments",
                column: "PaymentCustomerCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMerchantMerchantId",
                table: "Payments",
                column: "PaymentMerchantMerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Merchant");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
