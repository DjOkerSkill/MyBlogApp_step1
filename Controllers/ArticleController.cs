using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlogApp.Controllers
{
    public class ArticleController : Controller
    {

        public BlogContext Context { get; set; }
        public ArticleController(BlogContext context)
        {
            Context = context;
        }



        public IActionResult Create(Guid id, string title, string description)
        {
            
            {
                if (Context.Users.Where(x => x.Id == id) != null)
                {
                    Article article = new Article()
                    {
                        Title = title,
                        Description = description,
                        UserId = id
                    };
                    Context.Articles.Add(article);
                    Context.SaveChanges();
                    ViewBag.Name = "Статья создана";
                    return View("Create_article");
                }
                else
                    return NotFound();
            }
        }

        public string Update(string titleOld, string titleNew, string descriptionNew)
        {
            
            {
                Article article = new Article();
                article = Context.Articles.Where(x => x.Title == titleOld) as Article;
                article.Title = titleNew;
                article.Description = descriptionNew;
                Context.Articles.Update(article);
                Context.SaveChanges();
                return "Успешно обновлено";
            }

        }

        public string Delete(string title)
        {
            
            {
                Article article = new Article();
                article = Context.Articles.Where(x => x.Title == title) as Article;
                Context.Articles.Remove(article);
                Context.SaveChanges();
                return "Успешно удалено";
            }
        }

        public IActionResult ReadAll()
        {
            List<Article> articles = new List<Article>();

            
            {
                articles = Context.Articles.ToList();
            }
            return View("ReadAll", articles);
        }

        public IActionResult ReadForId(Guid id)
        {
            Article article = new Article();

            
            {
                User user = Context.Users.Where(x => x.Id == id) as User;

                if (user != null)
                {
                    List<Article> articles = user.Articles;

                    return View("ReadForIdResult", articles);
                }
                else
                    return null;
            }
        }
    }
}
