namespace LexiconGarage.Migrations {
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconGarage.DAL.GarageContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LexiconGarage.DAL.GarageContext context) {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            VehicleType[] vehicleTypes = new VehicleType[] {
                new VehicleType { TypeInSwedish = "Motorcykel" },
                new VehicleType { TypeInSwedish = "Bil" },
                new VehicleType { TypeInSwedish = "Buss" },
                new VehicleType { TypeInSwedish = "Flygplan" },
                new VehicleType { TypeInSwedish = "Båt" }
            };

            context.VehicleTypes.AddOrUpdate(t => t.TypeInSwedish, vehicleTypes);
            Member[] members = new Member[] {
                new Member { UserName = "Karl",
                    Name = "Karl Larsson",
                    TelNumber = "0702-12345",
                    Address = "Stationsgatan 3" },
                new Member { UserName = "Karl-2",
                    Name = "Karl Larsson",
                    TelNumber = "08-123465",
                    Address = "Sveavägen 103" },
                new Member { UserName = "Lisa",
                    Name = "Lisa Persson",
                    TelNumber = "070-123456",
                    Address = "Stationsgatan 3" },
                new Member { UserName = "Anna",
                    Name = "Anna Svensson",
                    TelNumber = "08-123465",
                    Address = "Sveavägen 103" },
                new Member { UserName = "Olle",
                    Name = "Olle Olson",
                    TelNumber = "08-9876543",
                    Address = "Götgatan 43" },
                 new Member { UserName = "SL",// 5
                    Name = "Stockholms Lokaltrafik",
                    TelNumber = "08-111222",
                    Address = "Bussgatan 33" },
                  new Member { UserName = "SAS", // 6
                    Name = "SAS Norr",
                    TelNumber = "0709-666 444",
                    Address = "Norrgatan 43" },
                  new Member { UserName = "WAX-1", // 7
                    Name = "Waxholmsbolaget",
                    TelNumber = "0709-777 444",
                    Address = "Strandvägen 110" }
            };
            context.Members.AddOrUpdate(m => m.UserName, members);

            context.SaveChanges();

            context.Vehicles.AddOrUpdate(
              v => v.RegNo,
              // Buss needs two, AeroPlane and Boat requires 3 parking slots
              new Vehicle {
                  VehicleTypeId = vehicleTypes[1].Id,
                  RegNo = "BOO124",
                  MemberId = members[0].Id,
                  ParkingTime = new DateTime(2016, 10, 16, 10, 35, 15),
                  NumberOfWheels = 4,
                  Brand = "Volvo",
                  Model = "V40",
                  Weight = 3000,
                  ParkingSlot = 1
              }
              , new Vehicle {
                  VehicleTypeId = vehicleTypes[1].Id,
                  RegNo = "ANN000",
                  MemberId = members[4].Id,
                  ParkingTime = new DateTime(2016, 10, 1, 15, 0, 15),
                  NumberOfWheels = 4,
                  Brand = "Saab",
                  Model = "V4",
                  Weight = 2000,
                  ParkingSlot = 2
              }
              , new Vehicle {
                  VehicleTypeId = vehicleTypes[1].Id,
                  RegNo = "ANN00X",
                  MemberId = members[4].Id,
                  ParkingTime = new DateTime(2016, 10, 1, 15, 0, 15),
                  NumberOfWheels = 4,
                  Brand = "Volvo",
                  Model = "V70",
                  Weight = 2000,
                  ParkingSlot = 3
              }
              , new Vehicle {
                  VehicleTypeId = vehicleTypes[2].Id,
                  RegNo = "BUS126",
                  MemberId = members[5].Id,
                  ParkingTime = new DateTime(2016, 10, 10, 15, 0, 15),
                  NumberOfWheels = 8,
                  Brand = "Scania",
                  Model = "B55",
                  Weight = 4500,
                  ParkingSlot = 6 // Uses slot 6 and 7
              }
              , new Vehicle {
                  VehicleTypeId = vehicleTypes[0].Id,
                  RegNo = "MC1234",
                  MemberId = members[3].Id,
                  ParkingTime = new DateTime(2016, 9, 30, 10, 35, 15),
                  NumberOfWheels = 2,
                  Brand = "Yamaha",
                  Model = "XC1000",
                  Weight = 200,
                  ParkingSlot = 9
              }
              , new Vehicle {
                  VehicleTypeId = vehicleTypes[0].Id,
                  RegNo = "MC1235",
                  MemberId = members[2].Id,
                  ParkingTime = new DateTime(2016, 9, 30, 12, 35, 15),
                  NumberOfWheels = 2,
                  Brand = "Kawasaki",
                  Model = "1000X",
                  Weight = 200,
                  ParkingSlot = 10
              }
              , new Vehicle {
                  VehicleTypeId = vehicleTypes[3].Id,
                  RegNo = "AIRNEX",
                  MemberId = members[6].Id,
                  ParkingTime = new DateTime(2016, 10, 16, 22, 0, 0),
                  NumberOfWheels = 9,
                  Brand = "Boeing",
                  Model = "747",
                  Weight = 8000,
                  ParkingSlot = 12  // Uses slots 12, 13 and 14
              }
             , new Vehicle {
                 VehicleTypeId = vehicleTypes[4].Id,
                 RegNo = "SS1234",
                 MemberId = members[7].Id,
                 ParkingTime = new DateTime(2016, 10, 16, 22, 0, 0),
                 NumberOfWheels = 12,
                 Brand = "SSSSS",
                 Model = "Steam3",
                 Weight = 8000,
                 ParkingSlot = 16   // Uses slots 16, 17 and 18
             }
             , new Vehicle {
                 VehicleTypeId = vehicleTypes[4].Id,
                 RegNo = "SS2201",
                 MemberId = members[7].Id,
                 ParkingTime = new DateTime(2016, 9, 16, 11, 10, 30),
                 NumberOfWheels = 14,
                 Brand = "Sea",
                 Model = "Steamy",
                 Weight = 12000,
                 ParkingSlot = 19 // Uses slots 19, 20 and 21
             }
             , new Vehicle {
                 VehicleTypeId = vehicleTypes[3].Id,
                 RegNo = "SAS002",
                 MemberId = members[6].Id,
                 ParkingTime = new DateTime(2016, 9, 16, 11, 12, 32),
                 NumberOfWheels = 14,
                 Brand = "Fly",
                 Model = "FlyX2",
                 Weight = 12000,
                 ParkingSlot = 23 // Uses slots 23, 24 and 25
             }
             , new Vehicle {
                 VehicleTypeId = vehicleTypes[2].Id,
                 RegNo = "BUSS02",
                 MemberId = members[5].Id,
                 ParkingTime = new DateTime(2016, 9, 16, 11, 12, 32),
                 NumberOfWheels = 14,
                 Brand = "SaabScania",
                 Model = "RVM23",
                 Weight = 5000,
                 ParkingSlot = 26 // Uses slots 26 and 27
             });
        }
    }
}
