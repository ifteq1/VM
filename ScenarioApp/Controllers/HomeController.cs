using ScenarioApp.Models.Home;
using ScenarioAppModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ScenarioApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HomeViewModel vm)
        {
            if (vm.UploadedFile == null) {

                return Json(new { error = "FileNotFound", code = 404 });
            }

            List<Scenario> scenarios = new List<Scenario>();
            XDocument xdoc = new XDocument();
            StreamReader streamreader = new StreamReader(vm.UploadedFile.InputStream);
            xdoc = XDocument.Load(streamreader);

            foreach (XElement xe in xdoc.Descendants("Scenario"))
            {
                var scenario = new Scenario();
                scenario.ScenarioID = xe.Element("ScenarioID") == null ? 0
                    : Convert.ToInt32(xe.Element("ScenarioID").Value);

                scenario.Name = xe.Element("Name") == null ? string.Empty
                    : xe.Element("Name").Value;

                scenario.Surname = xe.Element("Surname") == null ? string.Empty
                    : xe.Element("Surname").Value;

                scenario.Forename = xe.Element("Forename") == null ? string.Empty
                    : xe.Element("Forename").Value;

                scenario.UserID = xe.Element("UserID") == null ? string.Empty
                    : xe.Element("UserID").Value;

                scenario.SampleDate = xe.Element("SampleDate") == null ? new DateTime()
                    : Convert.ToDateTime(xe.Element("SampleDate").Value);

                scenario.CreationDate = xe.Element("CreationDate") == null ? new DateTime()
                    : Convert.ToDateTime(xe.Element("CreationDate").Value);

                scenario.NumMonths = xe.Element("NumMonths") == null ? 0
                    : Convert.ToInt32(xe.Element("NumMonths").Value);

                scenario.MarketID = xe.Element("MarketID") == null ? 0
                    : Convert.ToInt32(xe.Element("MarketID").Value);

                scenario.NetworkLayerID = xe.Element("NetworkLayerID") == null ? 0
                    : Convert.ToInt32(xe.Element("NetworkLayerID").Value);

                scenarios.Add(scenario);
            }

            if (scenarios.Count == 0)
            {
                return Json(new { error = "FileFormatIncorrect", code = 409 });
            }

            return Json(scenarios);
        }
    }
}