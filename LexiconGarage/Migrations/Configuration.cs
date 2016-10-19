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
            context.Vehicles.AddOrUpdate(
                  v => v.RegNo,
                  new Vehicle {
                      Type = VehicleType.Car,
                      RegNo = "boo124",
                      Owner = "Bo Ohlsson",
                      ParkingTime = new DateTime(2016, 10, 16, 10, 35, 15),
                      NumberOfWheels = 4,
                      Brand = "Volvo",
                      Model = "V40",
                      Weight = 2100
                  }
                  , new Vehicle {
                      Type = VehicleType.Car,
                      RegNo = "ann000",
                      Owner = "Anna Ohlsson",
                      ParkingTime = new DateTime(2016, 10, 1, 15, 0, 15),
                      NumberOfWheels = 4,
                      Brand = "Saab",
                      Model = "XX",
                      Weight = 2000
                  }
                  , new Vehicle {
                      Type = VehicleType.Car,
                      RegNo = "ann00x",
                      Owner = "Anna Ohlsson",
                      ParkingTime = new DateTime(2016, 10, 1, 15, 0, 15),
                      NumberOfWheels = 4,
                      Brand = "Saab",
                      Model = "XX",
                      Weight = 2000
                  }
                  , new Vehicle {
                      Type = VehicleType.Bus,
                      RegNo = "bus126",
                      Owner = "SLx",
                      ParkingTime = new DateTime(2016, 10, 10, 15, 0, 15),
                      NumberOfWheels = 8,
                      Brand = "Saab",
                      Model = "B55",
                      Weight = 4500
                  }
                  , new Vehicle {
                      Type = VehicleType.MotorCycle,
                      RegNo = "mc1234",
                      Owner = "Anna Karlsson",
                      ParkingTime = new DateTime(2016, 9, 30, 10, 35, 15),
                      NumberOfWheels = 2,
                      Brand = "Toyota",
                      Model = "Speedy",
                      Weight = 200
                  }
                  , new Vehicle {
                      Type = VehicleType.MotorCycle,
                      RegNo = "mc1235",
                      Owner = "Sanna Carlsson",
                      ParkingTime = new DateTime(2016, 9, 30, 12, 35, 15),
                      NumberOfWheels = 2,
                      Brand = "Toyota",
                      Model = "Speedy",
                      Weight = 200
                  }
                  , new Vehicle {
                      Type = VehicleType.AeroPlane,
                      RegNo = "airNex",
                      Owner = "SAS",
                      ParkingTime = new DateTime(2016, 10, 16, 22, 0, 0),
                      NumberOfWheels = 9,
                      Brand = "Boeing",
                      Model = "XX",
                      Weight = 8000
                  }
                 , new Vehicle {
                     Type = VehicleType.Boat,
                     RegNo = "SS1234",
                     Owner = "Waxholmsbolaget",
                     ParkingTime = new DateTime(2016, 10, 16, 22, 0, 0),
                     NumberOfWheels = 12,
                     Brand = "SSSSS",
                     Model = "Steam3",
                     Weight = 8000
                 }
                 , new Vehicle {
                     Type = VehicleType.Boat,
                     RegNo = "SS2201",
                     Owner = "Waxholmsbolaget",
                     ParkingTime = new DateTime(2016, 9, 16, 11, 10, 30),
                     NumberOfWheels = 14,
                     Brand = "Sea",
                     Model = "Steamy",
                     Weight = 12000
                 }
                 , new Vehicle {
                     Type = VehicleType.AeroPlane,
                     RegNo = "SAS002",
                     Owner = "SAS",
                     ParkingTime = new DateTime(2016, 9, 16, 11, 12, 32),
                     NumberOfWheels = 14,
                     Brand = "Fly",
                     Model = "FlyX2",
                     Weight = 12000
                 }
                 , new Vehicle {
                     Type = VehicleType.Bus,
                     RegNo = "Buss02",
                     Owner = "SweBus",
                     ParkingTime = new DateTime(2016, 9, 16, 11, 12, 32),
                     NumberOfWheels = 14,
                     Brand = "SaabScania",
                     Model = "RVM23",
                     Weight = 5000
                 });
        }
    }
}
