using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketEventSearch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatuses",
                columns: table => new
                {
                    TicketStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatuses", x => x.TicketStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UriMainImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateTimeEventStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTimeEventEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schemes",
                columns: table => new
                {
                    SchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.SchemeId);
                    table.ForeignKey(
                        name: "FK_Schemes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Row = table.Column<int>(type: "int", nullable: true),
                    SeatNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Schemes_SchemeId",
                        column: x => x.SchemeId,
                        principalTable: "Schemes",
                        principalColumn: "SchemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TicketStatusId = table.Column<int>(type: "int", nullable: false),
                    UnavailableStatusEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatId",
                        column: x => x.SeatId,
                        principalTable: "Seats",
                        principalColumn: "SeatId");
                    table.ForeignKey(
                        name: "FK_Tickets_TicketStatuses_TicketStatusId",
                        column: x => x.TicketStatusId,
                        principalTable: "TicketStatuses",
                        principalColumn: "TicketStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CityId",
                table: "Locations",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_LocationId",
                table: "Schemes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schemes_Name_SchemeId",
                table: "Schemes",
                columns: new[] { "Name", "SchemeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SchemeId",
                table: "Seats",
                column: "SchemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EventId",
                table: "Tickets",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatId",
                table: "Tickets",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketStatusId",
                table: "Tickets",
                column: "TicketStatusId");

            migrationBuilder.InsertData(
                table: "TicketStatuses",
                columns: new[] { "TicketStatusId", "Name" },
                values: new object[,]
                {
                                { 1, "Free" },
                                { 2, "Unavailable" },
                                { 3, "Ordered" },
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name", "IsActive" },
                values: new object[,]
                {
                         {
                            "4683D27E-B9A4-492C-B082-D3D7C9E6786E",
                            "Опера",
                            true
                         },
                         {
                            "C07076B3-6CFC-4032-B7A7-4F8310CFB50D",
                            "Цирк",
                            true
                         },
                         {
                            "E072EA0E-F385-4850-9443-E04197953A8A",
                            "Театр",
                            true
                         }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityId", "Name", "IsActive" },
                values: new object[,]
                {
                         {
                            "8365E04D-DC59-44A0-A578-61D84349C274",
                            "Минск",
                            true
                         },
                         {
                            "040047C8-D326-43F3-8DC0-392EFD1046DE",
                            "Брест",
                            true
                         },
                         {
                            "E070F4EB-F9F4-4645-AE64-C512FEC1664E",
                            "Гродно",
                            true
                         }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "CityId", "Name", "Address", "Latitude", "Longitude", "IsActive" },
                values: new object[,]
                {
                         {
                            "307642AC-8B65-45DD-89A8-966709B82AFA",
                            "8365E04D-DC59-44A0-A578-61D84349C274",
                            "Государственный оперный театр",
                            "Советская 28",
                            "28.123",
                            "131.123",
                            true
                         }
                });

            migrationBuilder.InsertData(
                table: "Schemes",
                columns: new[] { "SchemeId", "LocationId", "Name", "IsActive" },
                values: new object[,]
                {
                         {
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "307642AC-8B65-45DD-89A8-966709B82AFA",
                            "Стандарт",
                            true
                         }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "SchemeId", "Sector", "Row", "SeatNumber" },
                values: new object[,]
                {
                         {
                            "F0A0507E-FCE1-4C36-8D93-626AFF269ED8",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            1
                         },
                         {
                            "AE5C92AF-A717-4964-B140-27D6FA5C0B6C",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            2
                         },
                         {
                            "91A5EE57-8F83-4AEB-B3C2-7B3CE7A01B03",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            3
                         },
                         {
                            "B035465D-81BF-4734-B477-9F19F88B7C23",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            4
                         },
                         {
                            "253C0256-CFBD-49CB-A564-097807335BCE",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            5
                         },
                         {
                            "8F3D3C62-FC41-4BE7-9876-8BCF2B02524C",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            6
                         },
                         {
                            "52769B51-75EA-445D-9D92-1A5C06FB3B56",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            7
                         },
                         {
                            "45F0DA24-5B0E-44B4-B179-AFBD4FB8829B",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            8
                         },
                         {
                            "75D8DF29-8242-46A5-B5E7-BA0CA7A1C0DE",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            9
                         },
                         {
                            "55CD814D-2D9C-421E-A715-90367F4D0ADE",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            1,
                            10
                         },
                         {
                            "436775E4-903B-4AB1-B31B-FE847E8632FE",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            1
                         },
                         {
                            "40101590-6E56-4C4C-9ED5-B34BA1A27F7B",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            2
                         },
                         {
                            "3EF57248-B4C9-477E-A895-9D4F7A50BD93",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            3
                         },
                         {
                            "919C7498-8A57-49F0-BE00-1947F1F7DCB7",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            4
                         },
                         {
                            "15C0DDC1-55A6-4440-8018-162B5BCDBD10",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            5
                         },
                         {
                            "64155983-D2C3-44F1-8F48-0AA473D181EC",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            6
                         },
                         {
                            "20D1F6D9-F328-4836-A6FD-7E7E63B7C7EA",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            7
                         },
                         {
                            "F2DE74B7-4E4C-4CA9-8981-0057D8C9FE61",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            8
                         },
                         {
                            "1C6505B5-FF54-4A26-98C6-FBACC353EB9B",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            9
                         },
                         {
                            "2735F273-E3F0-4D75-96AC-BDA06F71A684",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            2,
                            10
                         },
                         {
                            "577BDF02-0197-4DEF-ADBE-B79B075D0E2B",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            1
                         },
                         {
                            "3B8B65F0-F12E-4BBF-BE85-57CD5588E178",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            2
                         },
                         {
                            "A95D6BCE-E1FD-4FD8-B28B-8FE35AFE3A3C",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            3
                         },
                         {
                            "D0CB810E-A4BC-4CBA-B9B9-08254468C7C1",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            4
                         },
                         {
                            "77E68447-ECC8-4B27-948C-932E3207899D",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            5
                         },
                         {
                            "E902CBB3-EE30-46CC-B47B-54313F4AE96B",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            6
                         },
                         {
                            "0E6622DD-0A55-4F5F-A6D5-34023749EE4D",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            7
                         },
                         {
                            "2C18209D-6112-42DE-A804-5CDD6BDBB9DB",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            8
                         },
                         {
                            "0FD3A2B6-B4CA-4C5E-9FFB-799AFFE99ED9",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            9
                         },
                         {
                            "C83D43C7-3642-4274-9C8F-CB681C1AE166",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            3,
                            10
                         },
                         {
                            "0BCADCF5-8092-419C-95E3-28F0ADD3AF42",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            1
                         },
                         {
                            "44CBA4DE-8199-4B5E-9F36-AC5A38F6AA1E",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            2
                         },
                         {
                            "561E62BE-6A66-48F4-979A-D496FA7CCB50",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            3
                         },
                         {
                            "B0446A29-42AC-4680-AA7B-41544A03EA08",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            4
                         },
                         {
                            "3EB0B1A0-E113-4377-81B6-17B64AAE99ED",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            5
                         },
                         {
                            "B31E485C-130F-454D-9DA2-B47170438A84",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            6
                         },
                         {
                            "BB11E764-F8BC-4396-A991-53C53DCEFFDF",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            7
                         },
                         {
                            "05180A5B-63F6-46BA-8813-D6D30B21D0E0",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            8
                         },
                         {
                            "56F31C68-1450-49C7-82E9-8FAADE283174",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            9
                         },
                         {
                            "D41A44E7-AE02-4A99-8127-5122B3595CA1",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            4,
                            10
                         },
                         {
                            "286BBA45-4F49-4A15-BFC2-784DA128526A",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            1
                         },
                         {
                            "FFBFEA58-E7A8-47CD-AF47-11F2E6150CF3",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            2
                         },
                         {
                            "D9BE8916-3544-4699-B689-F0ED68399760",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            3
                         },
                         {
                            "66311C46-5456-41CF-8C5F-5D8C85CC29FB",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            4
                         },
                         {
                            "569530F8-9A0B-47BA-864F-209C89663FC8",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            5
                         },
                         {
                            "514796EE-9CEA-4B1E-B2C0-D1B087101A4A",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            6
                         },
                         {
                            "FE1F0E79-54F8-45D6-B780-720D74AF6BF4",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            7
                         },
                         {
                            "47D34E7A-377C-4B42-AE58-70EC35256C6C",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            8
                         },
                         {
                            "FB5173B8-7453-4BA5-A13B-5079CD43D195",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            9
                         },
                         {
                            "79F0263D-478B-4E98-9E28-6ABA73E482BE",
                            "5A456259-3A9F-456E-B5C0-C6E5E618E0B3",
                            "A",
                            5,
                            10
                         }
                });

            //migrationBuilder.InsertData(
            //    table: "Events",
            //    columns: new[] { "EventId", "LocationId", "CategoryId", "Name", "Description", "UriMainImage", "DateTimeEventStart", "DateTimeEventEnd", "IsActive" },
            //    values: new object[,]
            //    {
            //             {
            //                "ADFFF4E8-6B9D-4E8F-93F3-356CB29ADD55",
            //                "307642AC-8B65-45DD-89A8-966709B82AFA",
            //                "4683D27E-B9A4-492C-B082-D3D7C9E6786E",
            //                "test",
            //                "test",
            //                "test.com/fasdfasdfff23f12/f123",
            //                "2024-07-18 18:00:00.0000000",
            //                "2024-07-18 20:00:00.0000000",
            //                true
            //             }
            //    });

            //migrationBuilder.InsertData(
            //    table: "Tickets",
            //    columns: new[] { "TicketId", "EventId", "SeatId", "Price", "TicketStatusId", "UnavailableStatusEnd" },
            //    values: new object[,]
            //    {
            //             {
            //                "55CD814D-2D9C-421E-A715-94285F4D0ADE",
            //                "ADFFF4E8-6B9D-4E8F-93F3-356CB29ADD55",
            //                "8F3D3C62-FC41-4BE7-9876-8BCF2B02524C",
            //                50,
            //                1,
            //                null
            //             }
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "TicketStatuses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Schemes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
