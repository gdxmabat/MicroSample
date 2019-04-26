using Koa.Mapping.ObjectMapper; 
using Koa.Persistence.EntityRepository.EntityFrameworkCore.Mapper; 
using Koa.Persistence.EntityRepository;
using Koa.Persistence.EntityRepository.EntityFrameworkCore;
using MicroSample.Domain.Entity;

namespace MicroSample.Business.Repository
{
    public class UserRepository : MapperRepository<User, int>, IUserRepository
    {
        public UserRepository(IEntityRepository<int> repository, IEfUnitOfWork unitOfWork, IObjectMapper mapper) : base(repository, unitOfWork, mapper)
        {
        }
    }
}
