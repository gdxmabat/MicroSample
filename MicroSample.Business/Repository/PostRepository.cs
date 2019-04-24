using Koa.Persistence.EntityRepository.EntityFrameworkCore.Mapper;
using Koa.Persistence.EntityRepository.EntityFrameworkCore;
using Koa.Persistence.EntityRepository;
using Koa.Persistence.Abstractions.QueryResult; 
using Koa.Mapping.ObjectMapper; 
using Koa.Domain.Search.Page; 
using Koa.Domain.Specification;
using MicroSample.Business.Model;
using MicroSample.Domain.Entity;
using System;
using System.Collections.Generic;
using MicroSample.Domain.Specification.Post;
using System.Linq;

namespace MicroSample.Business.Repository
{
    public class PostRepository : MapperRepository<Post, int>, IPostRepository
    {
        private readonly IEntityRepository<int> repository;

        public PostRepository(IEntityRepository<int> repository, IEfUnitOfWork unitOfWork, IObjectMapper mapper) : base(repository, unitOfWork, mapper)
        {
            this.repository = repository;
        }

        public IPagedQueryResult<PostModel> Find(IPageDescriptor model, string omnisearch)
        {
            var spec = Specification<Post>.True;
            //Specification<Post> spec = TrueSpecification<Post>.Default;

            if (!omnisearch.IsNullOrEmpty())
            {
                spec = new Specification<Post>(x => x.Content.Contains(omnisearch));
            }

            return null;
            //return this.Query<PostModel>(model, spec);
        }

        public IEnumerable<PostModel> GetUserPost(int userId)
        {
            var posts = this.repository.Where(new PostByUserIdSpecification(userId)).ToArray();
            var models = this.Mapper.Map<Post, PostModel>(posts).ToArray();
            return models;
        }
    }
}
