using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    SSN = table.Column<string>(type: "TEXT", nullable: false),
                    DOB = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Residence",
                columns: table => new
                {
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResidencePicture = table.Column<string>(type: "TEXT", nullable: false),
                    GuestNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BedroomNumber = table.Column<decimal>(type: "TEXT", nullable: false),
                    BathroomNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BuiltYear = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PricePerNight = table.Column<string>(type: "TEXT", nullable: false),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residence", x => x.ResidenceId);
                    table.ForeignKey(
                        name: "FK_Residence_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Residence_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReservationEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ResidenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Residence_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residence",
                        principalColumn: "ResidenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "ClientId", "DOB", "Email", "Name", "PhoneNumber", "SSN", "UserType" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.Doe@airbb.com", "John Doe", "555-000-0001", "232-24-2421", "Owner" },
                    { 2, new DateTime(2001, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "emy@airbb.com", "Emy", "555-000-0002", "123-45-6789", "Client" },
                    { 3, new DateTime(2002, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "anasmith@airbb.com", "Ana Smith", "555-000-0003", "235-74-7456", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 1, "Chicago" },
                    { 2, "New York" },
                    { 3, "Miami" },
                    { 4, "Atlanta" }
                });

            migrationBuilder.InsertData(
                table: "Residence",
                columns: new[] { "ResidenceId", "BathroomNumber", "BedroomNumber", "BuiltYear", "ClientId", "GuestNumber", "LocationId", "Name", "PricePerNight", "ResidencePicture" },
                values: new object[,]
                {
                    { 1, 1, 2m, new DateTime(2000, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 1, "Chicago Loop Apartment", "100", "chi_loop.png" },
                    { 2, 1, 1m, new DateTime(1990, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, 2, "New York Studio", "120", "ny_studio.png" },
                    { 3, 3, 4m, new DateTime(2011, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, 3, "Miami Beach House", "50", "miami_beach.png" },
                    { 4, 2, 3m, new DateTime(2002, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, 4, "Atlanta Suburban House", "70", "atl_house.png" }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "ReservationId", "ReservationEndDate", "ReservationStartDate", "ResidenceId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ResidenceId",
                table: "Reservation",
                column: "ResidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Residence_ClientId",
                table: "Residence",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Residence_LocationId",
                table: "Residence",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Residence");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
