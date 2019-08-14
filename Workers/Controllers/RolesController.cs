﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Workers.ModelsView;
using Workers.Models;
using Microsoft.AspNetCore.Authorization;

namespace Workers.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<UserAuthentication> _userManager;
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<UserAuthentication> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");

        }
        public IActionResult UserList()
        {
           return View(_userManager.Users.ToList());
        }
        public async Task<IActionResult> Edit(string userId)
        {
       
            UserAuthentication userAuthentication = await _userManager.FindByIdAsync(userId);
            if (userAuthentication != null)
            {
             
                var userRoles = await _userManager.GetRolesAsync(userAuthentication);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = userAuthentication.Id,
                    UserEmail = userAuthentication.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            UserAuthentication userAuthentication = await _userManager.FindByIdAsync(userId);
            if (userAuthentication != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userAuthentication);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
            
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(userAuthentication, addedRoles);

                await _userManager.RemoveFromRolesAsync(userAuthentication, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
        public async Task<IActionResult> AddPerson(string userId)
        {
            UserAuthentication userAuthentication = await _userManager.FindByIdAsync(userId);
            return View(userAuthentication);
        }
    }
}