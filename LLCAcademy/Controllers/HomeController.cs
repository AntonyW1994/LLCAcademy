using LLCAcademy.Helpers;
using LLCAcademy.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace LLCAcademy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GetFeedback();
            return View();
        }

        public ActionResult Contact()
        {
            GetAddresses();
            return View();
        }

        public ActionResult AlternativeServices()
        {
            return View();
        }

        public ActionResult Feedback()
        {
            GetFeedback();
            return View();
        }

        public ActionResult Sessions()
        {
            return View(GetSessions());
        }

        public ActionResult SoccerSchools()
        {
            return View(GetSoccerSchools());
        }

        [HttpPost]
        public ActionResult SubmitFeedback(Feedback newFeedback)
        {
            if (!ModelState.IsValid)
                return View("Feedback", newFeedback);

            var connectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    var sql = "INSERT INTO Feedback (FullName, Telephone, Email, FeedbackText, Approved) VALUES (@FullName, @Telephone, @Email, @Feedback, NULL)";
                    connection.Execute(sql, new { FullName = GeneralHelper.ConvertToTitleCase(newFeedback.FullName), Telephone = GeneralHelper.CleanPhoneNumber(newFeedback.Telephone), newFeedback.Email, Feedback = newFeedback.FeedbackText.TrimEnd('\r', '\n') });
                }
                catch (Exception ex)
                {
                    MailHelper.SendErrorNotification("LL Coaching Academy - Error Inserting Testimonial", ex.Message);
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            //return View("FeedbackThankyou");
            //return View(MailHelper.SendMessage("New Feedback on LL Coaching Academy", newFeedback.FullName + "said " + newFeedback.FeedbackText + " on LL Coaching Academy.") ? "FeedbackThankYou" : "Error");
            try
            {
                MailHelper.SendMessage("New Feedback on LL Coaching Academy", newFeedback.FullName + " said " + newFeedback.FeedbackText + " on LL Coaching Academy.");
            return View("FeedbackThankyou");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex;
                return View("Error");
            }
        
        }

        public void GetFeedback()
        {
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sql = "SELECT Id, FullName, FeedbackText FROM Feedback WHERE Approved = 1";
                var feedback = connection.Query<Feedback>(sql);
                ViewBag.Feedback = feedback;
            }
        }

        public void GetAddresses()
        {
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sql = "SELECT Id, Line1, Line2, Line3, Town, County, Postcode, GoogleMapsUrl FROM Address";
                var addresses = connection.Query<Address>(sql);
                ViewBag.Addresses = addresses;
            }
        }

        public List<Session> GetSessions()
        {
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sql = "SELECT ID, Name, Description, StartHour, EndHour, StartDate, EndDate, Price, SpecialPrice, DayOfWeek, AddressID FROM Session WHERE EndDate >= @Today";
                var sessions = connection.Query<Session>(sql, new {DateTime.Today });
                return sessions.ToList();
            }
        }

        public List<SoccerSchool> GetSoccerSchools()
        {
            ViewBag.ApplicationFormPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationFormPath"];
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sql = "SELECT * FROM SoccerSchool WHERE EndDate >= @Today";
                var schools = connection.Query<SoccerSchool>(sql, new {DateTime.Today });
                return schools.ToList();
            }
        }

        public FileResult DownloadApplicationForm(string file)
        {
            var fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(file));
            var response = new FileContentResult(fileBytes, "application/octet-stream") {FileDownloadName = "ApplicationForm.pdf"};
            return response;
        }
    }
}
