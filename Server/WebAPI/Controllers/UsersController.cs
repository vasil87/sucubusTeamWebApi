namespace WebAPI.Controllers
{
    using Models.Users;
    using MovieDb.Data;
    using MovieDb.Models;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        private readonly IRepository<Users> users;

        public UsersController(IRepository<Users> users)
        {
            this.users = users;
        }
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Register(Users user)
        {
            var allUsersQuearable = this.users.All();
            var userWithThisNameCount = allUsersQuearable.Where(x => x.Email == user.Email).Count();
         
            if (userWithThisNameCount > 0)
            {
                return this.BadRequest("Username is taken");
            }

            try
            {
                this.users.Add(user);
                this.users.SaveChanges();
            }
            catch(Exception ex)
            {
                this.BadRequest("some argument for registration is not ok");
            }
        
            return this.Ok(this.users.All().Where(x => x.UserName == user.UserName).FirstOrDefault().UsersId);
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var res= this.users.All().Where( x=>x.Expire == false).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).Select(x=>new { x.FirstName,x.LastName,x.UserName,x.UsersId,x.isMale,x.City,x.Email} ).ToList();
            return this.Ok(res);

        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id<0)
            {
                return this.BadRequest("Id of user can`t be negative");
            }

            var res = GetUser(id);

            return this.Ok(new {res.FirstName,res.LastName,res.UserName,res.UsersId,res.isMale,res.City,res.Email });

        }
        [HttpPost]
        public IHttpActionResult GetUserIdByName(HttpRequestMessage request)
        {
            var email = request.Content.ReadAsStringAsync().Result.Replace("\"", "").Replace("/", "");

            var res = this.users.All().Where(x => x.Expire == false && x.Email == email).FirstOrDefault();
            if (res == null) {
                return this.BadRequest("No such user");
            }

            return this.Ok(res.UsersId);

        }
        [AllowAnonymous]
        //public IHttpActionResult LogIn(LoginUserModel userData)
        //{
        //    var currentUser=this.users.All().Where(x => x.UserName == userData.UserName).FirstOrDefault();
        //    if (currentUser == null)
        //    {
        //        return this.BadRequest("No such Username");
        //    }
        //    if (currentUser.Password != userData.Password)
        //    {
        //        return this.BadRequest("Wrong Password");
        //    }

        //    return this.Ok(currentUser);
        //}

        public IHttpActionResult UpdateUserData(Users userData)
        {
            var currentUser = this.users.All().Where(x => x.UserName == userData.UserName).FirstOrDefault();
            currentUser.City = userData.City;
            currentUser.Email = userData.Email;
            currentUser.FirstName = userData.FirstName;
            currentUser.LastName = userData.LastName;
            //currentUser.Password = userData.Password;
            currentUser.isMale = userData.isMale;
            try
            {
                this.UpdateUser(currentUser);
            }
            catch
            {
                return this.BadRequest("InvalidData");
            }

            return this.Ok(currentUser);
        }

        public IHttpActionResult ExpireUser(int userId)
        {
            var user = GetUser(userId);
            user.Expire = true;
            try
            {
                UpdateUser(user);
            }
            catch
            {
                return this.BadRequest("Can`t expire user");
            }

            return this.Ok(user);
            
        }

        private Users GetUser(int id)
        {

            return this.users.All().Where(x => x.UsersId == id && x.Expire == false).FirstOrDefault();
        }

        private void UpdateUser(Users data)
        {
                this.users.Update(data);
                this.users.SaveChanges();          
        }
    }
}
