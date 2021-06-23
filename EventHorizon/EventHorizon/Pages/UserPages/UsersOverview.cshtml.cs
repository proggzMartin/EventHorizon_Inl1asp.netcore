using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventHorizon.Data;
using EventHorizon.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventHorizon.Pages.UserPages
{
    public class UsersOverviewModel : PageModel
    {
        private readonly EventHorizonContext _context;
        private readonly UserManager<User> _userManager;

        //The users should be shown at Get.
        [BindProperty(SupportsGet = true)]
        public IReadOnlyCollection<UsersOverviewViewModel> Users { get; set; }

        //Using EventHorizonContext because wanan get all users.
        public UsersOverviewModel(EventHorizonContext context,
                                  UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var allUsers = _context.User;
            
            //Needs users in a list in order to be able to add them.
            //The Users-variable is an IReadOnlyCollection, thus
            //cannot have objects added to it progressively, or altered.
            //This is why users need to be middle-stored in a temporary list.
            var tempUsersList = new List<UsersOverviewViewModel>();

            foreach (var u in allUsers)
            {
                var uRoles = await _userManager.GetRolesAsync(u);

                var rolesString = (uRoles == null || uRoles.Count() < 1 ? "No roles" : string.Join(", ", uRoles));

                tempUsersList.Add(new UsersOverviewViewModel()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Roles = rolesString
                });
            }

            Users = tempUsersList;
        }
    }
}
