using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace API.Controllers
{
    public class BuggyController :BaseApiController
    {
        private readonly DataContext _context;

        public BuggyController(DataContext context){
            _context = context;
        }
        [Authorize]
        [HttpGet("auth")]

        public ActionResult<string>GetSecret()
        {
            return "secret text";
        }

         [HttpGet("not-found")]

        public ActionResult<AppUser>GetNotFound()
        {
            var thing=_context.Users.Find(-1);

            if(thing==null)return NotFound();

            return thing;
        }

         [HttpGet("server-error")]

        public ActionResult<string>GetServerError()
        {
            
                 var thing=_context.Users.Find(-1);

            var thingToReturn=thing.ToString();
            return thingToReturn;
            
          
        }

         [HttpGet("bad-request")]

        public ActionResult<string>GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }

         
    }
}



/*


"/auth": This endpoint is protected by the "Authorize" attribute, which means that only authenticated users can access it. It returns a string with value "secret text".

"/not-found": This endpoint returns a user from the database, based on an identifier. If no user is found, it returns a 404 Not Found response.

"/server-error": This endpoint returns a string representation of a user from the database. If an error occurs, it may throw an exception, which can cause a 500 Internal Server Error response.

"/bad-request": This endpoint returns a 400 Bad Request response with the message "This was not a good request".
*/