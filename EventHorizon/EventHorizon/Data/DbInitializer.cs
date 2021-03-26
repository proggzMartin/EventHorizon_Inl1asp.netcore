using EventHorizon.Data.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
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
                //load image files
                var x = Directory.GetCurrentDirectory();
                string uploadsFolder = Path.Combine(x, "EventStartImages");
                List<byte[]> files = new List<byte[]>();

                foreach (var filepath in Directory.GetFiles(uploadsFolder))
                {
                    //Only png-files allowed.
                    if(Path.GetExtension(filepath).Equals(".png"))
                    {
                        files.Add(File.ReadAllBytes(filepath));
                    }
                }
                

                context.Event.AddRange(new Event()
                    {
                        Address = "Fjodors Grusväg 23",
                        Title = "Börjes Fyllekalas 50år",
                        SpotsAvailable = 50,
                        Description = "Vi firar Börje och det blir ett jädrans hålligång med öl och saft.",
                        Date = new DateTime(2021, 06, 06),
                        Organizer = context.Organizer.FirstOrDefault(x => x.Name.Equals("Lukas Lustriga Lökhus")),
                        Place = "Hökö",
                        EventImage = (files != null && files.Count > 0 ? files[0] : null)
                    }, new Event()
                    {
                        Address = "StålFiolas väg 1",
                        Title = "Agata Adaktussons nyhetsevent",
                        SpotsAvailable = 2,
                        Description = "En resa i oerhört primitiva och ofaschinerande nyheter om reptilprimitiva militära och socialpolitiska konflikter som inträffat det senaste året." +
                        " Så lite som möjligt om vetenskapliga fakta och beprövade teorier vilka skulle kunna driva mänskligheten framåt på riktigt, bort!.",
                        Date = new DateTime(2022, 01, 13),
                        Organizer = context.Organizer.FirstOrDefault(x => x.Name.Equals("Flinigt plejs")),
                        Place = "AmsterGrannt",
                        EventImage = (files != null && files.Count > 1 ? files[1] : null)
                    }
                );;
                context.SaveChanges();

            }

            if (!context.UserFeedback.Any())
            {
                context.UserFeedback.AddRange(new UserFeedback()
                {
                    Feedback = "Kanonsajt, hitta allt jag klan ölönska mig!"
                }, new UserFeedback()
                {
                    Feedback = "Tycker era fonter är lite tråkiga."
                },
                new UserFeedback()
                {
                    Feedback = "Bootstrap är king."
                },
                new UserFeedback()
                {
                    Feedback = "Jag gillar att det framgick att det ingick kaka till kaffet i Lovisas 70-årskalas."
                }
                ); ;
                context.SaveChanges();
            }
        }
    }
}
