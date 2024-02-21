using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newse2.Models;

namespace Newse2.Controllers
{
    public class AddNewsController : Controller
    {
        public IActionResult AddNews(string title, int categoryId,int authorid, string postcontent, string description, string keywords)
        {
            try
            {
                NewseDBContext.GetContext().Posts.Add(new Post()
                {
                    Title = title,
                    CategoryId = categoryId,
                    PostContent = postcontent,
                    AuthorId = authorid,
                    DateOfCreation = DateTime.Now,
                    Description = description,
                    Keywords = keywords
                }) ;
                NewseDBContext.GetContext().SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }



        }
    }
}
