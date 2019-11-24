using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    { 
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? IfLoggedIn = HttpContext.Session.GetInt32("IsLoggedIn");
            if (IfLoggedIn != 1)
            {
                HttpContext.Session.SetInt32("IsLoggedIn", 0);
                return View();
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }


        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {

            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(u=>u.Email == newUser.Email))
                {
                    ModelState.AddModelError("newUser.Email", "This Email has been used!");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("IsLoggedIn", 1);
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser returnUser)
        {
            if (ModelState.IsValid)
            {
                User userInDb = dbContext.Users.FirstOrDefault(u=>u.Email == returnUser.Email);
                if (userInDb == null)
                {
                    ModelState.AddModelError("returnUser.Email", "Invalid Email/Password");
                    return View("Index");
                }
                else
                {
                    PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
                    var result = hasher.VerifyHashedPassword(returnUser, userInDb.Password, returnUser.Password);
                    if (result == 0)
                    {
                        ModelState.AddModelError("returnUser.Password", "Invalid Email/Password");
                        return View("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("UserId", userInDb.userId);
                        HttpContext.Session.SetInt32("IsLoggedIn", 1);
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            else
            {
                return View("Index");
            }
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            int? IfLoggedIn = HttpContext.Session.GetInt32("IsLoggedIn");
            if (IfLoggedIn != 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Dashboard dashboardContext = new Dashboard();
                List<Plan> AllPlans = dbContext.Plans
                    .Include(plan=>plan.Guest)
                    .ThenInclude(planner=>planner.User)
                    .ToList();
                dashboardContext.CurrentUserId = HttpContext.Session.GetInt32("IsLoggedIn").Value;
                dashboardContext.AllPlans = AllPlans;
                return View(dashboardContext);
            }
        }

        [HttpGet]
        [Route("new-wedding")]
        public IActionResult NewWedding()
        {
            int? IfLoggedIn = HttpContext.Session.GetInt32("IsLoggedIn");
            if (IfLoggedIn != 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpPost("add-new-wedding")]
        public IActionResult AddNewWedding(Plan newPlan)
        {
            if (ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                if (today > newPlan.Date)
                {
                    ModelState.AddModelError("Date","Your wedding date can't be in the past");
                    return View("NewWedding");
                }
                else
                {
                    int? creatorId = HttpContext.Session.GetInt32("UserId");
                    newPlan.CreatedBy = creatorId.Value;
                    dbContext.Add(newPlan);
                    dbContext.SaveChanges();
                    int NewPlanId = dbContext.Plans.Last().PlanId;
                    return RedirectToAction("PlanDetail",new{planId=NewPlanId});
                }
            }
            else
            {
                return View("NewWedding");
            }
        }
        
        [HttpGet("detail/{planId}")]
        public IActionResult PlanDetail(int planId)
        {
            int? IfLoggedIn = HttpContext.Session.GetInt32("IsLoggedIn");
            if (IfLoggedIn != 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<User> AllGuests = dbContext.Users
                    .Where(usr=>usr.Wedding.Any(plan=>plan.PlanId == planId))
                    .ToList();
                Plan thisPlan = dbContext.Plans
                    .FirstOrDefault(plan=>plan.PlanId == planId);
                
                DetailWrap thisPlanDetail = new DetailWrap();
                thisPlanDetail.ThisPlan = thisPlan;
                thisPlanDetail.Guests = AllGuests;
                return View(thisPlanDetail);
            }
        }

        [HttpGet("/unrsvp/{planId}")]
        public IActionResult UnRSVP(int planId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            Planner newPlanner = new Planner();
            newPlanner.UserId = userId;
            newPlanner.PlanId = planId;
            dbContext.Add(newPlanner);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("/rsvp/{planId}")]
        public IActionResult RSVP(int planId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;
            Planner rsvp = dbContext.Planners
            .FirstOrDefault(planner=>planner.UserId==userId && planner.PlanId==planId);
            dbContext.Planners.Remove(rsvp);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet("delete/{planId}")]
        public IActionResult DeletePlan(int planId)
        {
            Plan deleteThisPlan = dbContext.Plans.FirstOrDefault(plan=>plan.PlanId == planId);
            dbContext.Plans.Remove(deleteThisPlan);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
    }
}
