using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using System.Diagnostics;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registration()
        {
            return View();
        }
        // crud
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("Login, Password, ConfirmPassword")] User information)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(information);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again or call system admin");
            }

            return View(information);
        }

        public async Task<IActionResult> ShowUsers(string searched)
        {
            return View(await _context.User.ToListAsync());
        }

        //Edit or Update
        [HttpPost, ActionName("EditPersonalInformation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserInfo(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var users = await _context.User.FirstOrDefaultAsync(s => s.Id == Id);


            if (await TryUpdateModelAsync<User>(
                users, "", s => s.Login, s => s.Password, s => s.ConfirmPassword))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again or call system admin");
                }
            }

            return View(users);
        }

        public async Task<IActionResult> DeleteUser(Guid id, bool? Savechangeserror = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.User.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            if (Savechangeserror.GetValueOrDefault())
            {
                ViewData["DeleteError"] = "Delete failed, please try again later ... ";
            }

            return View(person);
        }


        public IActionResult Authorization()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}