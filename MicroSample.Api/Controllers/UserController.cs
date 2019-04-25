using System.Threading.Tasks;
using Koa.Integration.PubSub;
using MicroSample.Business.Event.User;
using MicroSample.Business.Model;
using MicroSample.Business.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MicroSample.Api.Controllers
{
    /// <summary>
    ///    User Controller
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository repository;

        /// <summary>
        ///   
        /// </summary>
        public UserController(IUserRepository repository)
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
            var models = this.repository.GetAll<UserModel>();
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
            var model = this.repository.FindOne<UserModel>(id);
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
        public async Task<IActionResult> Post([FromBody]UserModel value)
        {
            if (this.ModelState.IsValid)
            {
                var model = await this.repository.ApplyChangesAsync(value);

                return this.CreatedAtAction("Get", new { id = model.Id }, model);
            }
            else
            {
                return this.NoContent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UserModel value)
        {
                //TODO: change for any+specification
            var model = this.repository.FindOne<UserModel>(id);
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
            var model = this.repository.FindOne<UserModel>(id);
            if (model == null)
            {
                return this.NotFound();
            }

            this.repository.Remove(id);
            return this.Ok();
        }
    }
}
