using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PersonalPageProjectF115376.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        public string? Author { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string? UserId { get; set; } 
        public ApplicationUser? User { get; set; } 

        public bool IsAnonymous { get; set; }

        public Comment() { } 
    }


    public class CommentsViewModel
    {
        public List<Comment> ExistingComments { get; set; } = new List<Comment>(); 
        public Comment NewComment { get; set; } = new Comment(); 
    }
}


