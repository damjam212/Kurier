//using System;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using webapi;

//namespace webapi
//{
//    public interface IDataSeeder
//    {
//        void SeedData();
//    }

//    public class DataSeeder : IDataSeeder
//    {
//        private readonly MyDbContext _context;

//        public DataSeeder(MyDbContext context)
//        {
//            _context = context;
//        }

//        //public void SeedData()
//        //{
//        //    // Seedowanie danych tylko jeśli nie ma jeszcze żadnych użytkowników
//        //    if (!_context.Users.Any())
//        //    {
//        //        // Seedowanie przykładowych użytkowników
//        //        var users = new[]
//        //        {
//        //        new User { Name = "John Doe" },
//        //        new User { Name = "Jane Smith" }
//        //        // Dodaj więcej użytkowników, jeśli jest taka potrzeba
//        //    };

//        //        _context.Users.AddRange(users);
//        //        _context.SaveChanges();

//        //        // Seedowanie przykładowych dostaw tylko jeśli nie ma jeszcze żadnych dostaw
//        //        if (!_context.UserDelivery.Any())
//        //        {
//        //            var deliveries = new[]
//        //    {
//        //        new UserDelivery
//        //        {
//        //            length = "10",
//        //            width = "5",
//        //            weight = "20",
//        //            height = "15",
//        //            DesAddress = "Destination 1",
//        //            SourceAddress = "Source 1",
//        //            DelAtWeekend = false,
//        //            Prio = "High",
//        //            Owner = users[0].Id // ID użytkownika, do którego należy dostawa
//        //        },
//        //        new UserDelivery
//        //        {
//        //            length = "8",
//        //            width = "6",
//        //            weight = "18",
//        //            height = "12",
//        //            DesAddress = "Destination 2",
//        //            SourceAddress = "Source 2",
//        //            DelAtWeekend = true,
//        //            Prio = "Low",
//        //            Owner = users[1].Id // ID użytkownika, do którego należy dostawa
//        //        }
//        //        // Dodaj więcej dostaw, jeśli jest taka potrzeba
//        //    };


//        //            _context.UserDelivery.AddRange(deliveries);
//        //            _context.SaveChanges();
//        //        }
//        //    }
//        //}
//    }
//}
