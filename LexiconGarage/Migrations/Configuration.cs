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
                    Address = "Götgatan 43" }
            };
            context.Members.AddOrUpdate(m => m.UserName, members);

            /*
        context.Vehicles.AddOrUpdate(
              v => v.RegNo,
              // Buss needs two, AeroPlane and Boat requires 3 parking slots
              new Vehicle {
                  Type = VehicleType.Car,
                  RegNo = "BOO124",
                  Owner = "Bo Ohlsson",
                  ParkingTime = new DateTime(2016, 10, 16, 10, 35, 15),
                  NumberOfWheels = 4,
                  Brand = "Volvo",
                  Model = "V40",
                  Weight = 3000,
                  ParkingSlot = 1
              }
              , new Vehicle {
                  Type = VehicleType.Car,
                  RegNo = "ANN000",
                  Owner = "Anna Ohlsson",
                  ParkingTime = new DateTime(2016, 10, 1, 15, 0, 15),
                  NumberOfWheels = 4,
                  Brand = "Saab",
                  Model = "XX",
                  Weight = 2000,
                  ParkingSlot = 2
              }
              , new Vehicle {
                  Type = VehicleType.Car,
                  RegNo = "ANN00X",
                  Owner = "Anna Ohlsson",
                  ParkingTime = new DateTime(2016, 10, 1, 15, 0, 15),
                  NumberOfWheels = 4,
                  Brand = "Saab",
                  Model = "XX",
                  Weight = 2000,
                  ParkingSlot = 3
              }
              , new Vehicle {
                  Type = VehicleType.Bus,
                  RegNo = "BUS126",
                  Owner = "SLx",
                  ParkingTime = new DateTime(2016, 10, 10, 15, 0, 15),
                  NumberOfWheels = 8,
                  Brand = "Saab",
                  Model = "B55",
                  Weight = 4500,
                  ParkingSlot = 6 // Uses slot 6 and 7
              }
              , new Vehicle {
                  Type = VehicleType.MotorCycle,
                  RegNo = "MC1234",
                  Owner = "Anna Karlsson",
                  ParkingTime = new DateTime(2016, 9, 30, 10, 35, 15),
                  NumberOfWheels = 2,
                  Brand = "Toyota",
                  Model = "Speedy",
                  Weight = 200,
                  ParkingSlot = 9 
              }
              , new Vehicle {
                  Type = VehicleType.MotorCycle,
                  RegNo = "MC1235",
                  Owner = "Sanna Carlsson",
                  ParkingTime = new DateTime(2016, 9, 30, 12, 35, 15),
                  NumberOfWheels = 2,
                  Brand = "Toyota",
                  Model = "Speedy",
                  Weight = 200,
                  ParkingSlot = 10
              }
              , new Vehicle {
                  Type = VehicleType.AeroPlane,
                  RegNo = "AIRNEX",
                  Owner = "SAS",
                  ParkingTime = new DateTime(2016, 10, 16, 22, 0, 0),
                  NumberOfWheels = 9,
                  Brand = "Boeing",
                  Model = "XX",
                  Weight = 8000,
                  ParkingSlot = 12  // Uses slots 12, 13 and 14
              }
             , new Vehicle {
                 Type = VehicleType.Boat,
                 RegNo = "SS1234",
                 Owner = "Waxholmsbolaget",
                 ParkingTime = new DateTime(2016, 10, 16, 22, 0, 0),
                 NumberOfWheels = 12,
                 Brand = "SSSSS",
                 Model = "Steam3",
                 Weight = 8000,
                 ParkingSlot = 16   // Uses slots 16, 17 and 18
             }
             , new Vehicle {
                 Type = VehicleType.Boat,
                 RegNo = "SS2201",
                 Owner = "Waxholmsbolaget",
                 ParkingTime = new DateTime(2016, 9, 16, 11, 10, 30),
                 NumberOfWheels = 14,
                 Brand = "Sea",
                 Model = "Steamy",
                 Weight = 12000,
                 ParkingSlot = 19 // Uses slots 19, 20 and 21
             }
             , new Vehicle {
                 Type = VehicleType.AeroPlane,
                 RegNo = "SAS002",
                 Owner = "SAS",
                 ParkingTime = new DateTime(2016, 9, 16, 11, 12, 32),
                 NumberOfWheels = 14,
                 Brand = "Fly",
                 Model = "FlyX2",
                 Weight = 12000,
                 ParkingSlot = 23 // Uses slots 23, 24 and 25
             }
             , new Vehicle {
                 Type = VehicleType.Bus,
                 RegNo = "BUSS02",
                 Owner = "SweBus",
                 ParkingTime = new DateTime(2016, 9, 16, 11, 12, 32),
                 NumberOfWheels = 14,
                 Brand = "SaabScania",
                 Model = "RVM23",
                 Weight = 5000,
                 ParkingSlot = 26 // Uses slots 26 and 27
             });*/
        }
    }
}
