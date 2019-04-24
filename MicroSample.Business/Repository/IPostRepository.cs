using Koa.Persistence.EntityRepository.EntityFrameworkCore.Mapper; 
using Koa.Persistence.Abstractions.QueryResult;
using Koa.Domain.Search.Page;
using MicroSample.Business.Model;
using MicroSample.Domain.Entity;

namespace MicroSample.Business.Repository
{
    public interface IPostRepository : IMapperRepository<Post, int>
    {
        IPagedQueryResult<PostModel> Find(IPageDescriptor model, string omnisearch);
    }
}
