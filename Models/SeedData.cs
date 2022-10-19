using Microsoft.EntityFrameworkCore;

namespace Store.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationContext>>());// Подкл к бд
            if (context.User.Any())
            {
                return;
            }


            context.User.AddRange(
                new User
                {
                    Login = "user1@user.com",
                    Password = "qwerty123",
                    ConfirmPassword = "qwerty123",
                },
                new User
                {
                    Login = "user2@user.com",
                    Password = "qwerty123",
                    ConfirmPassword = "qwerty123",
                });

            context.SaveChanges(); // Сохр в бд

        }
    }
}
