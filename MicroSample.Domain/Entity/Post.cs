using Koa.Domain;

namespace MicroSample.Domain.Entity
{
    public class Post : BaseEntity<int>
    {

        public int UserId { get; set; }

        public User User { get; set; }

        public string Content { get; set; }
    }
}