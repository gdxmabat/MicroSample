using System.Collections.Generic;
using Koa.Domain;

namespace MicroSample.Business.Model
{
    public class UserModel: BaseModel<int>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
