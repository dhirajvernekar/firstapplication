using System.Reflection.Metadata;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using DatingApp.api.Models;

namespace DatingApp.api.Data
{
    public class Seed
    {
        public static  void SeedUsers(DataContext context)
        {
            if(!context.Users.Any())
            {
                 var userData=System.IO.File.ReadAllText("Data/UserSeedData.json");
                 var users=JsonConvert.DeserializeObject<List<User>>(userData);
                 foreach(var user in users)
                 {
                    byte[] passwordhash , passwordSalt;
                    CreatePasswordHash("password",out passwordhash,out passwordSalt);
                    user.PasswordHash=passwordhash;
                    user.PasswordSalt=passwordSalt;
                    context.Users.Add(user);
                 }
                 context.SaveChanges();
            }
        }
        public static void CreatePasswordHash(string password,out byte[] passwordHash, out byte[] pappasswordSalt)
          {
               using(var hmac=new System.Security.Cryptography.HMACSHA512())
               {
                    pappasswordSalt=hmac.Key;
                    passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

               }
          }
    }
}