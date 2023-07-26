using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlogApp.Controllers
{
    public class CommentController : Controller
    {
        public BlogContext Context { get; set; }
        public CommentController(BlogContext context)
        {
            Context = context;
        }


        public IActionResult Create(Guid id, Guid idArticle, string title)
        {
            
            {
                if (Context.Comments.Where(x => x.Id == id) != null)
                {
                    Comment comment = new Comment()
                    {
                        Id = id,
                        Title = title,
                        ArticleId = idArticle
                    };
                    Context.Comments.Add(comment);
                    Context.SaveChanges();
                    ViewBag.Name = "Статья создана";
                    return View("Create_comment");
                }
                else
                    return NotFound();
            }
        }

        public string Delete(string title)
        {
            
            {
                Comment comment = new Comment();
                comment = Context.Comments.Where(x => x.Title == title) as Comment;
                Context.Comments.Remove(comment);
                Context.SaveChanges();
                return "Успешно удалено";
            }
        }

        public string Update(string titleOld, string titleNew)
        {
            
            {
                Comment comment = new Comment();
                comment = Context.Comments.Where(x => x.Title == titleOld) as Comment;
                comment.Title = titleNew;
                Context.Comments.Update(comment);
                Context.SaveChanges();
                return "Успешно обновлено";
            }

        }

        public IActionResult ReadAll()
        {
            List<Comment> comments = new List<Comment>();

            
            {
                comments = Context.Comments.ToList();
            }
            return View("ReadAll", comments);
        }

        public IActionResult ReadForId(Guid id)
        {
            
            {
                Comment comment = Context.Comments.Where(x => x.Id == id) as Comment;

                return View("ReadForIdResult", comment);
            }
        }
    }
}
