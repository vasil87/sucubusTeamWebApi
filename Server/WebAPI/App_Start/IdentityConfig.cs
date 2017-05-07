﻿namespace WebAPI
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using WebAPI.Models;
    using MovieDb.Models;
    using MovieDb.Data;
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<MovieDBApiUsers>
    {
        public ApplicationUserManager(IUserStore<MovieDBApiUsers> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<MovieDBApiUsers>(context.Get<MovieDbAPIContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MovieDBApiUsers>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<MovieDBApiUsers>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
