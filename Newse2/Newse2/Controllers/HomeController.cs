using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newse2.Models;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace NewseProj.Controllers
{
    public class HomeController : Controller
    {
        private NewseDBContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = NewseDBContext.GetContext();
        }
        public IActionResult AdminPage(string username)
        {
            AuthorizedUser user = new AuthorizedUser();
            if (username != null)
            {
                user.Id = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Id;
                user.Nickname = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Nickname;
                user.Role = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Role;
            }
            user.Categories = NewseDBContext.GetContext().Categories;
            user.Comments = NewseDBContext.GetContext().Commentaries.Include(x => x.Post).Include(x => x.User).ToList();
            user.Posts = NewseDBContext.GetContext().Posts.ToList();
            return View(user);
        }

        public IActionResult Index(string username, int categoryId, int page = 1)
        {

            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                username = claimUser.Claims.ToList()[0].Value;

            AuthorizedUser user = new AuthorizedUser();
            if (username != null)
            {
                user.Id = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Id;
                user.Nickname = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Nickname;
                user.Role = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Role;
                
            }
            user.CurrentCategory = categoryId;
            user.Categories = NewseDBContext.GetContext().Categories;
            user.Comments = NewseDBContext.GetContext().Commentaries.Include(x => x.Post).Include(x => x.User).ToList();
            user.Posts = NewseDBContext.GetContext().Posts.ToList();

            int pageSize = 10;
            user.Posts = user.Posts.Reverse();
            if(user.CurrentCategory != 0)
            {
                user.Posts = user.Posts.Where(x => x.CategoryId == categoryId).ToList();
            }
            var count = user.Posts.Count();
            var items = user.Posts.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            user.IndexVM = new IndexViewModel(items, pageViewModel);
            return View(user);

        }
        public IActionResult News(int id, string username)
        {
            AuthorizedUser user = new AuthorizedUser();
            user.CurrentPost = id;
            if (username != null)
            {
                user.Id = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Id;
                user.Nickname = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Nickname;
                user.Role = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Role;
            }
            user.Categories = NewseDBContext.GetContext().Categories;
            user.Posts = NewseDBContext.GetContext().Posts.Include(x => x.Category).Include(x => x.Author);
            return View(user);
        }

    }
}