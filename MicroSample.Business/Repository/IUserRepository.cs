using Koa.Persistence.EntityRepository.EntityFrameworkCore.Mapper;
using MicroSample.Domain.Entity;

namespace MicroSample.Business.Repository
{
    public interface IUserRepository : IMapperRepository<User, int>
    {
    }
}
