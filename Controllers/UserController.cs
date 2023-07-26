using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

namespace MyBlogApp.Controllers
{
    public class UserController : Controller
    {
        public BlogContext Context { get; set; }
        public UserController(BlogContext context)
        {
            Context = context;
        }

        [HttpPost]
        [Route("authenticate")]
        public User Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) ||
            String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User user = Context.Users.Where(x => x.Login == login) as User;

            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            return user;
        }
        public IActionResult Create(string name, string lastname, int age)
        {

            if (Context.Users.Where(x => x.Name == name).Where(y => y.LastName == lastname).Where(y => y.Age == age) == null)
            {
                User user = new User()
                {
                    Name = name,
                    LastName = lastname,
                    Age = age
                };
                Context.Users.Add(user);
                Context.SaveChanges();
                ViewBag.Name = "Пользователь зарегистирован";
                return View("Create_successfully");
            }
            else
            {
                ViewBag.Name = "Такой пользователь уже существует";
                return View("Create_not_successful");
            }


        }
        public IActionResult ReadAll()
        {
            List<User> users = new List<User>();



            users = Context.Users.ToList();

            return View("ReadAll", users);
        }

        [HttpPost]
        public IActionResult ReadForId(Guid id)
        {
            User user = new User();


            user = Context.Users.Where(x => x.Id == id) as User;
        
            if (user != null)
            {
                return View("ReadForIdResult", user);
            }
            else
                return null;
        }

        public string Delete(Guid id)
        {
            
            {
                User user = new User();
                user = Context.Users.Where(x => x.Id == id) as User;
                Context.Users.Remove(user);
                Context.SaveChanges();
                return "Успешно удалено";
            }
        }

        public string Update(Guid id, string name, string lastname, int age)
        {
            
            {
                User user = new User();
                user = Context.Users.Where(x => x.Id == id) as User;
                user.Name = name;
                user.Age = age;
                user.LastName = lastname;
                Context.Users.Update(user);
                Context.SaveChanges();
                return "Успешно обновлено";
            }
        }

    }
}
