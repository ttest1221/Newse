using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newse2.Models;

namespace Newse2.Controllers
{
    public class UserController : Controller
    {
       
        public IActionResult AddUser(string username, string login, string password)
        {
            bool isUnique = !(NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).Any() && NewseDBContext.GetContext().Users.Where(x => x.Login == login).Any());
            if (isUnique == true)
            {
                NewseDBContext.GetContext().Users.Add(new User()
                {
                    Nickname = username,
                    Login = login,
                    Password = password,
                    DateOfRegistration = DateTime.Now,
                    Role = "Пользователь"
                });
                NewseDBContext.GetContext().SaveChanges();
            }


            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult AddComment(int postId, string content, string username)
        {
            AuthorizedUser user = new AuthorizedUser();
            if (username != null)
            {
                user.Id = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Id;
                user.Nickname = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Nickname;
                user.Role = NewseDBContext.GetContext().Users.Where(x => x.Nickname == username).FirstOrDefault().Role;
            }

            user.Categories = NewseDBContext.GetContext().Categories;
            user.CurrentPost = postId;
       
            if(user.Id != 0)
            {
                NewseDBContext.GetContext().Commentaries.Add(new Commentary()
                {
                    UserId = user.Id,
                    PostId = postId,
                    CommentaryContent = content,
                    DateOfWritten = DateTime.Now
                });
                NewseDBContext.GetContext().SaveChanges();
                return RedirectToAction($"News", "Home", new { id = postId, username = username });
            }
            return RedirectToAction("Login", "Access");
           
        }
    }
}
