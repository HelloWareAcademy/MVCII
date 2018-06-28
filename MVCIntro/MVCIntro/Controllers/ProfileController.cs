using MVCIntro.Models;
using MVCIntro.Models.Repo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCIntro.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Intro()
        {
            return View();
        }
        public System.Web.Mvc.ActionResult GetId(int id, string name)
        {
            ViewBag.Name = name;
            ViewBag.Id = id;
            return View();
        }
        public String GetString()
        {
            return "Hello, I'm a string";
        }

        [HttpGet]
        public ViewResult MyInfo()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ReceiveUserInfo(int? id, string name, string email)
        {
            User user = new User();

            user.Id = id ?? 0;
            user.Name = name;
            user.Email = email;

            //User database to save the user
            //save user to database

            //1: connection 
            string connectionString = "Data Source=.;Initial Catalog=Testing;Integrated Security=True;";

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            //2: form query
            string query = string.Format("INSERT INTO [Testing].[dbo].[User] ([Id],[Name], [Email]) VALUES ('{0}','{1}', '{2}');",user.Id,user.Name,user.Email);

            //3: Create a sql command
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            //4: Execute this query
            sqlCommand.ExecuteNonQuery();

            //5: Close Connection
            sqlConnection.Close();

            //UserRepo.Users.Add(user);
            return View(user);
        }



        public ActionResult GetMyInfo(int id)
        {
            //select the user with the specified id from the database
            //
            User chosenUser = UserRepo.Users.Where((user) => user.Id == id).First();

            return View(chosenUser);
        }
    }
}