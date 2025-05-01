using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EliteRealEstate.Models
{
    public class BlogDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string CoverImage { get; set; }
        public string BlogDetails { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Createdby { get; set; }
        public string Status { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Tags { get; set; }
        public Nullable<System.DateTime> PublishingDate { get; set; }
        public Nullable<int> Blog_readTime { get; set; }
        public Nullable<int> Blog_openCount { get; set; }
        public string UserName { get; set; }  // New Property
        public string ProfileImage { get; set; }  // New Property
    }

    public class BlogPostModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public string BlogDetails { get; set; }
        public string CoverImage { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Createdby { get; set; }
        public string Status { get; set; }
        public bool? IsActive { get; set; }
        public string Tags { get; set; }
        public DateTime? PublishingDate { get; set; }
        public string Profileimage { get; set; }
        public int? Blog_readTime { get; set; }
        public int? Blog_openCount { get; set; }

    }

    public class UserModal
    {
        public int? id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Profileimage { get; set; }
        public string EmpNo { get; set; }
        public int? EmpId { get; set; }
    }

    public class UserProfile
    {
        public int? Empid { get; set; }
        public string EmpNo { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }

        public string Position { get; set; }

        public DateTime? Birthday { get; set; }

        public string MobileNo { get; set; }

        public string LinkedIn { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Address { get; set; }
    }

}