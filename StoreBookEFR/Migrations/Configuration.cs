namespace StoreBookEFR.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    using StoreBookEFR.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoreBookEFR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StoreBookEFR.Models.ApplicationDbContext";
        }

        protected override void Seed(StoreBookEFR.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                // If the database is empty, populate sample data in it

                CreateUser(context, "admin@gmail.com", "123", "System Administrator");
                CreateUser(context, "DaleCarnegie@gmail.com", "123", "Dale Carnegie");
                CreateUser(context, "AjahnBrahm@gmail.com", "123", "Ajahn Brahm");
                CreateUser(context, "JohnMcEwan@gmail.com", "123", "John McEwan");
                CreateUser(context, "JonMarks@gmail.com", "123", "Jon Marks");
                CreateUser(context, "AndyHunt@gmail.com", "123", "Andy Hunt");

                CreateRole(context, "Administrators");
                AddUserToRole(context, "admin@gmail.com", "Administrators");

                CreateBook(context,
                     title: "The Pragmatic Programmer",
                     description: @"Andy Hunt is an IT writer on software development. He is also the co-author of many famous books in the same field. He is also one of the 17 authors of the Agile manifesto and is also the founder of the Agile Alliance.
                     The Pragmatic Programmer is the best illustrated book of pitfalls and different aspects of software development. Whether you are a new programmer or an experienced person, this book will help you understand the issues around especially how you protect your product, drawn from the author's own experience.",
                     price: 240000,
                     date: new DateTime(2017, 11, 12, 15, 32, 33),
                     authorUsername: "AndyHunt@gmail.com"
                );


                CreateBook(context,
                 title: "Dac Nhan Tam",
                 description: @"Dale Carnegie's Winning People's Heart is the only self-help book that has consistently topped The New York Times' best-selling books list for 10 consecutive years.</p>
                 Published in 1936, with sales of more than 15 million copies, up to now, the book has been translated into almost all languages, including Vietnam, and has received enthusiastic reception from readers. fake in most countries.</p>
                 Virtue has a very wide spread power. Dac Nhan Tam is the art of winning people's hearts, making everyone love you. Win the human heart and the Talent in each of us. Winning Nhan Tam in that sense needs to be acquired by understanding yourself, being honest with yourself, understanding and caring for those around you in order to see and reveal their hidden potentials, help them grow to new heights. This is the highest art of man and the deepest meaning drawn from Dale Carnegie's golden principles.</p>",
                 price: 89000,
                 date: new DateTime(2018, 04, 05, 17, 36, 52),
                 authorUsername: "DaleCarnegie@gmail.com"
                );

                CreateBook(context,
                 title: "Let Go Of Sadness No",
                 description: @"Letting go - sad not is learning how to accept, knowing what is enough, knowing when to let go. Work, family, friends, from social problems to personal problems, all create a burden that is suppressed in the heart that cannot be released. Maybe at some point, you will no longer have the courage to face what lies ahead.</p>
                            Have a positive outlook on life, be optimistic in all matters, stay calm, tactfully behave more gently.If people know how to let go of hatred, let go of the pursuits in their hearts, get rid of the words greed - anger - delusion, then they will automatically feel peace, peace and serenity in their souls. </ p > ",
                 price: 200000,
                date: new DateTime(2015, 04, 11, 08, 10, 40),
                authorUsername: "AjahnBrahm@gmail.com"
                );

                CreateBook(context,
                 title: "Oxford English for Information Technology",
                 description: @"Oxford English for Information Technology provides an easy-to-read vocabulary list containing commonly used IT terms, with 25 units corresponding to 25 topics and sub-fields in the Information Technology industry.</p>
                 Documents, images, materials, etc. in the book are selected and quoted from IT textbooks, famous Information Technology magazines, etc. Therefore, the knowledge and information about the IT industry in the book are close to reality. </p>
                 In more detail, each unit has activities and exercises that focus on all four foreign language skills: Listening - Speaking - Reading - Writing. In particular, the book also provides interviews with experts, employees, etc. in the field of Information Technology (book with accompanying CD) so that learners can practice listening. </p>",
                 price: 350000,
                 date: new DateTime(2019, 11, 05, 20, 23, 53),
                 authorUsername: "JohnMcEwan@gmail.com"
                );

                CreateBook(context,
                     title: "Check Your English Vocabulary for Computers and Information Technology",
                     description: @"This book is designed to help non-native English speakers understand and grasp the IT terminology and core knowledge of this industry. Therefore, the knowledge is presented in a way that is as easy to understand and as user-friendly as possible.</p>
                     Check Your English Vocabulary for Computers and Information Technology offers classroom activities and activities suitable for self-study. The content of the book uses a variety of interesting and engaging activities such as crossword puzzles, speaking activities, and more. From there, learning will become less stressful and no longer boring. </p>",
                     price: 220000,
                     date: new DateTime(2014, 06, 22, 21, 36, 34),
                     authorUsername: "JonMarks@gmail.com"
                );

                context.SaveChanges();

            }
        }
        private void CreateUser(ApplicationDbContext context,
string email, string password, string fullName)
        {
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                FullName = fullName
            };

            var userCreateResult = userManager.Create(user, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(roleName));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }
        }

        private void AddUserToRole(ApplicationDbContext context, string userName, string roleName)
        {
            var user = context.Users.First(u => u.UserName == userName);
            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var addAdminRoleResult = userManager.AddToRole(user.Id, roleName);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }

        private void CreateBook(ApplicationDbContext context,
    string title, string description, int price, DateTime date, string authorUsername)
        {
            var books = new Books();
            books.Title = title;
            books.Description = description;
            books.Date = date;
            books.Price = price;
            books.Author = context.Users.Where(u => u.UserName == authorUsername).FirstOrDefault();
            context.Books.Add(books);
        }
    }
}
