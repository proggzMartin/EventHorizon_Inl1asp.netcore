using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EventHorizon.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(EventHorizonContext context, 
                                                   UserManager<Attendee> userManager, 
                                                   RoleManager<IdentityRole> roleManager,
                                                   IConfiguration config)
        {
            context.Database.EnsureCreated();

            //=================
            //initialize roles.
            var expectedRoles = config
                            .GetSection("Roles")
                            .GetChildren()
                            .Select(x => x.Value);

            var rolesInDb = roleManager.Roles.ToList();

            //If a role not belonging in the db has found its way there, delete it.
            foreach (var dbRole in rolesInDb)
                if (!expectedRoles.Any(x => x.Equals(dbRole.Name)))
                    await roleManager.DeleteAsync(dbRole);

            //Make sure all expected roles are in the db.
            rolesInDb = roleManager.Roles.ToList();
            foreach (var expectedRoleName in expectedRoles)
                if (!rolesInDb.Any(x => x.Name.Equals(expectedRoleName)))
                    await roleManager.CreateAsync(new IdentityRole() { Name = expectedRoleName });

            //=================
            //Initialize an admin and organizer account.

            var adminEmail = "admin@email.com";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                Attendee admin1 = new Attendee()
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "Admin",
                    LastName = "Adminsson",
                };
                var admin = await userManager.CreateAsync(admin1, "Abc123!");
                await userManager.AddToRoleAsync(admin1, "Admin");
            }

            var orgEmail = "org@email.com";

            if (await userManager.FindByEmailAsync(orgEmail) == null)
            {
                Attendee org1 = new Attendee()
                {
                    UserName = orgEmail,
                    Email = orgEmail,
                    Name = "Organizer",
                    LastName = "Organizersson"
                };
                var org = await userManager.CreateAsync(org1, "Abc123!");
                await userManager.AddToRoleAsync(org1, "Organizer");

            }

            //=================
            //Initialize regular users.
            //if there are no regular users,
            //that is, if there aren't any user whose email isn't admin or org.
            if (!context.User.Any(x => !(x.Email.Equals(adminEmail) || x.Email.Equals(orgEmail)) ))
            {
                Random r = new Random();

                var firstNames = new string[] { "Lovisa", "SvenElof", "Lisa", "Sven" };
                var lastNames = new string[] { "Svensson", "Rutgersson", "Floktomopedsson", "TrytarGräs" };

                foreach (var firstName in firstNames)
                {
                    foreach (var lastName in lastNames)
                    {
                        var email = firstName + "." + lastName + "@email.com";
                        await userManager.CreateAsync(new Attendee()
                        {
                            UserName = email,
                            Email = email,
                            Name = firstName,
                            LastName = lastName,
                        }, "Abc123!");
                    }
                }
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
                        Place = "AmsterGrannt",
                        EventImage = (files != null && files.Count > 1 ? files[1] : null)
                    }
                );;
                context.SaveChanges();

            }

            if (!context.Feedback.Any())
            {
                context.Feedback.AddRange(new Feedback()
                {
                    Text = "Kanonsajt, hitta allt jag klan ölönska mig!"
                }, new Feedback()
                {
                    Text = "Tycker era fonter är lite tråkiga."
                },
                new Feedback()
                {
                    Text = "Bootstrap är king."
                },
                new Feedback()
                {
                    Text = "Jag gillar att det framgick att det ingick kaka till kaffet i Lovisas 70-årskalas."
                }
                ); ;
                context.SaveChanges();
            }
        }
    }
}
