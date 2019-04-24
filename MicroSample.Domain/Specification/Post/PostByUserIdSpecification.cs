using Koa.Domain.Specification;

namespace MicroSample.Domain.Specification.Post
{
    public class PostByUserIdSpecification : Specification<Entity.Post>
    {
        public PostByUserIdSpecification(int userId) : base(x => x.UserId == userId)
        {
        }
    }
}
