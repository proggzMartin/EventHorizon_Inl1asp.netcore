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
                context.Organizer.AddRange(
                    new Organizer()
                    {
                        Name = "Lukas Lustriga Lökhus",
                        Email = "liustrigtlustig@email.com",
                        Phone = "1111112222222"
                    }, new Organizer()
                    {
                        Name = "Flinigt plejs",
                        Email = "flin@email.com",
                        Phone = "333333333333"
                    }, new Organizer()
                    {
                        Name = "Gabriellas Gastkåk",
                        Email = "gastkak@email.com",
                        Phone = "444444444444"
                    }, new Organizer()
                    {
                        Name = "Fidolinas fiolkammare",
                        Email = "fidolinas@email.com",
                        Phone = "555555555555"
                    }
                );
                context.SaveChanges();
            }

            if (!context.Event.Any())
            {
                context.Event.AddRange(new Event()
                    {
                        Address = "Fjodors Grusväg 23",
                        Title = "Börjes Fyllekalas 50år",
                        SpotsAvailable = 50,
                        Description = "Vi firar Börje och det blir ett jädrans hålligång med öl och saft.",
                        Date = new DateTime(2021, 06, 06),
                        Organizer = context.Organizer.FirstOrDefault(x => x.Name.Equals("Lukas Lustriga Lökhus")),
                        Place = "Hökö",
                    }, new Event()
                    {
                        Address = "StålFiolas väg 1",
                        Title = "Agata Adaktussons nyhetsevent",
                        SpotsAvailable = 2,
                        Description = "En resa i oerhört primitiva och ofaschinerande nyheter om konflikter och socialpolitiska konflikter som inträffat det senaste året." +
                        "Så lite som möjligt om vetenskapliga fakta och beprövade teorier som möjligt.",
                        Date = new DateTime(2022, 01, 13),
                        Organizer = context.Organizer.FirstOrDefault(x => x.Name.Equals("Flinigt plejs")),
                        Place = "AmsterGrannt",
                    }
                );
                context.SaveChanges();

            }
        }
    }
}
