using Koa.Hosting.AspNetCore.Model;
using Koa.Hosting.AspNetCore.Controller;
using MicroSample.Business.Model;
using MicroSample.Business.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MicroSample.Api.Controllers
{
    /// <summary>
    ///    Post Controller
    /// </summary>
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostRepository repository;

        /// <summary>
        ///   
        /// </summary>
        public PostController(IPostRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>     
        [HttpGet]
        public IActionResult Get()
        {
            var models = this.repository.GetAll<PostModel>();
            return this.Json(models);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var model = this.repository.FindOne<PostModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }
            return this.Json(model);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]PostModel value)
        {
            var model = this.repository.ApplyChanges(value);
            return this.CreatedAtAction("Get", new { id = model.Id }, model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PostModel value)
        {
            //TODO: change for any+specification
            var model = this.repository.FindOne<PostModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }
            model = this.repository.ApplyChanges(value);
            return this.Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //TODO: change for any+specification
            var model = this.repository.FindOne<PostModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            this.repository.Remove(id);
            return this.Ok();
        }

    }
}