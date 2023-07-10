using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MSTART_Hiring_Task.Models;

namespace MSTART_Hiring_Task.Controllers
{
    public class AccountController : Controller
    {
        private readonly MstartContext mstartContext;

        public AccountController(MstartContext mstartContext)
        {
            this.mstartContext = mstartContext;
        }

        public ActionResult Index(string searchString, int page = 1)
        {
            var accounts = mstartContext.Accounts.ToList();

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(u =>

                    u.AccountNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.UserId.ToString().Equals(searchString, StringComparison.OrdinalIgnoreCase) ||
                     u.AccountId.ToString().Equals(searchString, StringComparison.OrdinalIgnoreCase)
                ).ToList();



                ViewBag.CurrentFilter = searchString;
            }
            // Pagination
            int pageSize = 10;
            int totalUsers = accounts.Count;
            int totalPages = (int)Math.Ceiling((double)totalUsers / pageSize);

            accounts = accounts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.HasPrevious = page > 1; // Set the value of ViewBag.HasPrevious
            ViewBag.HasNext = page < totalPages;
            return View(accounts);
        }



        [HttpGet]
        //AddOrEdit Get Method
        // GET: /Account/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: /Account/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Add(Account account)
        {
            if (ModelState.IsValid)
            {
                // Save the account to the database
                mstartContext.Accounts.AddAsync(account);
                mstartContext.SaveChangesAsync();
                ViewBag.Message = "Data insert Successfully  ";
                return RedirectToAction("Index", "Account");
            }

            return View(account);
        }

        //AddOrEdit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
      
     

        [HttpPost]
        public IActionResult SaveAccount(Account account, string save, string saveAndContinue)
        {
            if (ModelState.IsValid)
            {
                // Save the account

                if (saveAndContinue != null)
                {
                    // Continue editing on the same page
                    return View("Edit", account);
                }

                return RedirectToAction("Index");
            }

            return View("Edit", account);
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
                  

                    // Change the account status to "Deleted"
                    user.Account.Status = AccountStatus.Deleted;
                }

                mstartContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
