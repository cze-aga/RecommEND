using REcommEND.Data;
using REcommEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REcommEND.Services
{
    public class UserService
    {
        private  RecommendationsDbContext _recommendationsDbContext;

        private static List<User> users = new List<User>();


        private static readonly string[] names = new string[] { "Jan", "Marysia", "Zuzanna", "Joe", "Paweł", "Karol", "Amanda", "Neil" };
        private static readonly string[] surnames = new string[] { "Dupa", "O'Neil", "MacDonald", "Gay", "Bailee", "Saigan", "Strip", "Spenser" };
        private static readonly string[] extensions = new string[] { "@gmail.com", "@hotmail.com", "@outlook.com", "@icloud.com", "@yahoo.com" };
        
        
        public UserService(RecommendationsDbContext recommendationsDbContext)
        {
            _recommendationsDbContext = recommendationsDbContext;
            //Random rnd = new Random();
            //for (int i = 0; i < 5; i++)
            //{
            //    string currName = names[rnd.Next(names.Length)];
            //    User user = new User
            //    {
            //        Name = currName + " " + surnames[rnd.Next(surnames.Length)],
            //        Email = currName.ToLower() + extensions[rnd.Next(extensions.Length)],
            //        Document = (rnd.Next(0, 100000) * rnd.Next(0, 100000)).ToString().PadLeft(10, '0'),
            //        Phone = "+1 888-452-1232"
            //    };

            //    _recommendationsDbContext.Users.Add(user);
            //    _recommendationsDbContext.SaveChanges();
            //}
        }
        public List<User> GetAll()
        {
            return _recommendationsDbContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return _recommendationsDbContext.Users.Where(user => user.Id == id).FirstOrDefault();
        }

        public User Create(User user)
        {
            users.Add(user);

            _recommendationsDbContext.Users.Add(user);
            _recommendationsDbContext.SaveChanges();

            return user;
        }

        public void Update(int id, User user)
        {
            User found = _recommendationsDbContext.Users.Where(n => n.Id == id).FirstOrDefault();
            found.Name = user.Name;
            found.Email = user.Email;
            found.Document = user.Document;
            found.Phone = user.Phone;

            _recommendationsDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var elToDelete = _recommendationsDbContext.Users.Where(el => el.Id == id).FirstOrDefault();
            if(elToDelete != null)
            {
                _recommendationsDbContext.Users.Remove(elToDelete);
                _recommendationsDbContext.SaveChanges();
            }

        }
    }
}
