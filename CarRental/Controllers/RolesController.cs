using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _rolemanager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _rolemanager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _rolemanager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await _rolemanager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
