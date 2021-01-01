using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeLewis.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeLewis.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Administrator")]
    public partial class AdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            Input = new InputModel();
        }
        public ApplicationDbContext _context { get; set; }
        public IdentityUser FoundUser { get; set; }
        public bool FoundUserIsMod { get; set; }
        public IList<string> FoundUserRoles { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username or Email Address")]
            public string Username { get; set; }
        }

        public async Task<IActionResult> OnGetMakeModerator(string id)
        {
            var foundUser = await _userManager.FindByIdAsync(id);
            if(foundUser == null)
            {
                StatusMessage = "User was not found";
                return Page();
            }
            var isMod = await _userManager.IsInRoleAsync(foundUser, "Moderator");
            var currentUserAuthorized = User.IsInRole("Administrator");
            if (!isMod && currentUserAuthorized)
            {
                var result = await _userManager.AddToRoleAsync(foundUser, "Moderator");
                if (result.Succeeded)
                {
                    StatusMessage = "User added to moderator successfully";
                }
                else
                {
                    StatusMessage = $"Failed to add role: {result.Errors.Select(x => x.Description)}";
                }
            }
            else
            {
                StatusMessage = "User is already a moderator or current user not authorized";
            }
            return Page();
        }
        public async Task<IActionResult> OnGetRemoveRoles(string id)
        {
            var foundUser = await _userManager.FindByIdAsync(id);
            if (foundUser == null)
            {
                StatusMessage = "User was not found";
                return Page();
            }
            var currentUserAuthorized = User.IsInRole("Administrator");
            if (currentUserAuthorized)
            {
                var roles = await _userManager.GetRolesAsync(foundUser);
                var result = await _userManager.RemoveFromRolesAsync(foundUser, roles);
                if (result.Succeeded)
                {
                    StatusMessage = "User removed from all roles successfully";
                }
                else
                {
                    StatusMessage = $"Failed to remove role: {result.Errors.Select(x => x.Description)}";
                }
            }
            else
            {
                StatusMessage = "Current user not authorized";
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            FoundUser = null;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            FoundUser = await _userManager.FindByEmailAsync(Input.Username);
            if(FoundUser == null)
            {
                FoundUser = await _userManager.FindByNameAsync(Input.Username);
            }
            if(FoundUser == null)
            {
                StatusMessage = "No user found";
            }
            else
            {
                FoundUserRoles = await _userManager.GetRolesAsync(FoundUser);
                FoundUserIsMod = await _userManager.IsInRoleAsync(FoundUser, "Moderator");
            }
            return Page();
        }
    }
}