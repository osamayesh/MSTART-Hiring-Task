using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MSTART_Hiring_Task.Models;
using static MSTART_Hiring_Task.Models.Account;
using static MSTART_Hiring_Task.Models.User;

namespace MSTART_Hiring_Task.Controllers
{
    public class UserController : Controller
    {
        private readonly MstartContext mstartContext;

        public UserController(MstartContext mstartContext)
        {
            this.mstartContext = mstartContext;
        }
        // GET: /User/Add
        [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                mstartContext.Users.Add(user);
                mstartContext.SaveChanges();
                ViewBag.Message = "Data insert Successfully  ";
                return RedirectToAction("Index", "User");
            }

            return View(user);
        }


        // GET: Users
        public IActionResult Index(string searchString, int page = 1)
        {
            var users = mstartContext.Users.Include(x => x.Account).ToList();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u =>
                    u.Username.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.Id.ToString().Equals(searchString, StringComparison.OrdinalIgnoreCase)
                ).ToList();



                ViewBag.CurrentFilter = searchString;
            }

            // Pagination
            int pageSize = 10;
            int totalUsers = users.Count;
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.HasPrevious = page > 1; // Set the value of ViewBag.HasPrevious
            ViewBag.HasNext = page < totalPages;

        


            return View(users);
        }
        [HttpGet]
        public ActionResult Edit(int id )
        {
            var data= mstartContext.Users.Where(x => x.Id== id).FirstOrDefault();

            return View(data);
        }
        [HttpPost]
        public ActionResult Edit( User model )
        {
            var data = mstartContext.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            data.Username = model.Username;
            data.Email = model.Email;   
            data.FirstName = model.FirstName;   
            data.LastName = model.LastName;
            data.DateOfBirth= model.DateOfBirth;
            data.Status = model.Status;
            data.gender = model.gender;
            mstartContext.SaveChanges();
            return RedirectToAction("Index");
        }




        // POST: /User/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int[] userIds)
        {
            if (userIds != null && userIds.Length > 0)
            {
                // Retrieve the users to delete
                var usersToDelete = mstartContext.Users.Where(u => userIds.Contains(u.Id)).ToList();

                foreach (var user in usersToDelete)
                {
                    // Change the status of the user to "Deleted"
                    user.Status = UserStatus.Deleted;

                    // Change the account status to "Deleted"
                    user.Account.Status = AccountStatus.Deleted;
                }

                mstartContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }




    }

    

}
