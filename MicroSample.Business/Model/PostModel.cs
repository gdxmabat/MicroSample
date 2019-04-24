using Koa.Domain;

namespace MicroSample.Business.Model
{
    public class PostModel : BaseModel<int>
    {
        public string UserId { get; set; }

        public string Content { get; set; }
    }
}