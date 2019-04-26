using System.Collections.Generic;
using Koa.Domain;

namespace MicroSample.Domain.Entity
{
    public class User : BaseEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Post> Posts { get; set; }
    }
}
