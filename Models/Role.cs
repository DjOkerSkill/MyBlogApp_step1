using System;

namespace MyBlogApp.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public RoleEnum Name { get; set; }
    }

    public enum RoleEnum
    {
        Administrator,
        Moderator,
        User
    }
}
