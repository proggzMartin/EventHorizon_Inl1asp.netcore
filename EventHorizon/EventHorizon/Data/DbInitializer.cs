using EventHorizon.Data.Entities;
using System;
using System.Linq;

namespace EventHorizon.Data
{
    public static class DbInitializer
    {

        public static void Initialize(DataContext context)
        {
            context.Database.EnsureCreated();

            Random r = new Random();

            var firstNames = new string[] { "Lovisa", "SvenElof", "Lisa", "Sven" };
            var lastNames = new string[] { "Svensson", "Rutgersson", "Floktomopedsson", "TrytarGräs" };

            //Attendees has not been seeded.
            if (!context.Attendee.Any())
            {
                foreach(var firstName in firstNames)
                {
                    foreach(var lastName in lastNames)
                    {
                        context.Attendee.Add(new Attendee()
                        {
                            Name = firstName + " " + lastName,
                            Email = firstName + "." + lastName + "@email.com",
                            Phone = r.Next(11111, 99999).ToString()
                        }); ; 
                    }
                }
                context.SaveChanges();
            }

            if(!context.Organizer.Any())
            {
                context.Organizer.Add(new Organizer()
                {
                    Name = "Lukas Lustriga Lökhus",
                    Email = "liustrigtlustig@email.com",
                    Phone = "1111112222222"
                });
                context.Organizer.Add(new Organizer()
                {
                    Name = "Flinigt plejs",
                    Email = "flin@email.com",
                    Phone = "333333333333"
                });
                context.Organizer.Add(new Organizer()
                {
                    Name = "Gabriellas Gastkåk",
                    Email = "gastkak@email.com",
                    Phone = "444444444444"
                });
                context.Organizer.Add(new Organizer()
                {
                    Name = "Fidolinas fiolkammare",
                    Email = "fidolinas@email.com",
                    Phone = "555555555555"
                });
                context.SaveChanges();
            }
        }
    }
}
