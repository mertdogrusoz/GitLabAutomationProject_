using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class MergeRequest
    {
        public int? Id { get; set; }
        public int? Iid { get; set; }
        public int? ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? TargetBranch { get; set; }
        public string? SourceBranch { get; set; }
        public int? UserNotesCount { get; set; }
        public int? Upvotes { get; set; }
        public int? Downvotes { get; set; }
        public bool? Draft { get; set; }
        public string? MergeStatus { get; set; }
        public bool? HasConflicts { get; set; }
        public Author? Author { get; set; }
        public string? WebUrl { get; set; }
    }

    public class Author
    {
        public int? Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? State { get; set; }
        public bool? Locked { get; set; }
        public string? AvatarUrl { get; set; }
        public string? WebUrl { get; set; }
    }

}
