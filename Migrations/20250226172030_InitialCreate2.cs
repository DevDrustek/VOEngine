using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace VBEngine.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:order_status_type", "Pending,Prepareing,Delivering,Completed");

            migrationBuilder.CreateTable(
                name: "OfferStatus",
                columns: table => new
                {
                    OfferStatusId = table.Column<int>(type: "integer", nullable: false),
                    OfferStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OfferStatus_pkey", x => x.OfferStatusId);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    OrderStatusId = table.Column<int>(type: "integer", nullable: false),
                    OrderStatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderStatus_pkey", x => x.OrderStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('providers_providerid_seq'::regclass)"),
                    FullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PhoneNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("providers_pkey", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "ProviderService",
                columns: table => new
                {
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    ServiceId = table.Column<int>(type: "integer", nullable: false),
                    AddedDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProviderId", x => new { x.ProviderId, x.ServiceId });
                });

            migrationBuilder.CreateTable(
                name: "RequestStatus",
                columns: table => new
                {
                    RequsetStatusId = table.Column<int>(type: "integer", nullable: false),
                    RequestStatusName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("RequestStatus_pkey", x => x.RequsetStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ServiceId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('services_serviceid_seq'::regclass)"),
                    ServiceName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("services_pkey", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreatedDate = table.Column<List<NpgsqlRange<DateOnly>>>(type: "datemultirange", nullable: false),
                    OfferStatusId = table.Column<int>(type: "integer", nullable: false),
                    RequestedDate = table.Column<DateTimeOffset>(type: "time with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Offer_pkey", x => x.OfferId);
                    table.ForeignKey(
                        name: "OfferStatusKey",
                        column: x => x.OfferStatusId,
                        principalTable: "OfferStatus",
                        principalColumn: "OfferStatusId");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RequestedDate = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true),
                    DeliveredDate = table.Column<DateTimeOffset>(type: "time with time zone", nullable: true),
                    OrderStatus = table.Column<int>(type: "integer", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_pkey", x => x.OrderId);
                    table.ForeignKey(
                        name: "Order_OrderStatus",
                        column: x => x.OrderStatus,
                        principalTable: "OrderStatus",
                        principalColumn: "OrderStatusId");
                });

            migrationBuilder.CreateTable(
                name: "QoS",
                columns: table => new
                {
                    QoSId = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('qos_qosid_seq'::regclass)"),
                    ProviderId = table.Column<int>(type: "integer", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    ResponseTime = table.Column<decimal>(type: "numeric", nullable: false),
                    Reliability = table.Column<decimal>(type: "numeric", nullable: false),
                    Availability = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("qos_pkey", x => x.QoSId);
                    table.ForeignKey(
                        name: "qos_providerid_fkey",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RequsetDetail = table.Column<string>(type: "json", nullable: false),
                    RequsetServices = table.Column<string>(type: "json", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequsetStatus = table.Column<int>(type: "integer", nullable: false),
                    RequesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("requests_pkey", x => x.RequestId);
                    table.ForeignKey(
                        name: "Request_RequestStatus",
                        column: x => x.RequsetStatus,
                        principalTable: "RequestStatus",
                        principalColumn: "RequsetStatusId");
                });

            migrationBuilder.CreateTable(
                name: "RequsetDetail",
                columns: table => new
                {
                    RequestDetailId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequsetDetail", x => x.RequestDetailId);
                    table.ForeignKey(
                        name: "FK_RequsetDetail_Request_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Request",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OfferStatusId",
                table: "Offer",
                column: "OfferStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatus",
                table: "Order",
                column: "OrderStatus");

            migrationBuilder.CreateIndex(
                name: "IX_QoS_ProviderId",
                table: "QoS",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_RequsetStatus",
                table: "Request",
                column: "RequsetStatus");

            migrationBuilder.CreateIndex(
                name: "IX_RequsetDetail_RequestId",
                table: "RequsetDetail",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProviderService");

            migrationBuilder.DropTable(
                name: "QoS");

            migrationBuilder.DropTable(
                name: "RequsetDetail");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "OfferStatus");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "RequestStatus");
        }
    }
}
