using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class ConsultantController : Controller
    {
        //making client to be used by ConsultantControlller
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer(); //instantiating serializer

        static ConsultantController()
        {
            //instantianting HTTP client
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");

        }

        // GET: Consultant/List
        public ActionResult List()
        {
            //goal: Communicate with consultant data api to retrieve list of consultants
            //curl https://localhost:44306/api/Consultantdata/listConsultants
            string url = "consultantdata/listconsultants";
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);
            //read response content into consultant list
            IEnumerable<Consultant> consultant = response.Content.ReadAsAsync<IEnumerable<Consultant>>().Result;
            Debug.WriteLine(consultant.Count());
            return View(consultant);
        }

        //Error Page
        public ActionResult Error()
        {
            return View();
        }

        // GET: Consultant/Details/5
        public ActionResult Details(int id)
        {
            //goal: Communicate with Consultant data api to retrieve a single Consultant
            //curl https://localhost:44306/api/Consultantdata/findConsultant
            string url = "consultantdata/findConsultant/" + id;
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            //read response content into Consultants details page
            ConsultantDto Consultant = response.Content.ReadAsAsync<ConsultantDto>().Result;
            return View(Consultant);
        }

        // GET: Consultant/Create
        public ActionResult Create()
        {
            //get partners information to display in a select box
            //curl https://localhost:44306/api/partnerdata/listpartners
            string url = "partnerdata/listpartners";
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            //read response content into partners list
            IEnumerable<Partner> partners = response.Content.ReadAsAsync<IEnumerable<Partner>>().Result;

            return View(partners);
        }

        // POST: Consultant/Create
        [HttpPost]
        public ActionResult Create(Consultant consultant)
        {
            try
            {
                // TODO: Add insert logic here
                //goal insert new consultant on the database using Consultantdata api
                //curl -d @consultant.json -H "Content-Type:application/json" https://localhost:44306/api/consultantdata/addconsultant/
                string url = "consultantdata/AddConsultant/";
                Debug.WriteLine(consultant.First_Name);

                //transforming object into json
                //serializing
                string jsonpayload = jss.Serialize(consultant);
                //creating HttpContent from json payload
                HttpContent content = new StringContent(jsonpayload);
                //setting headers
                content.Headers.ContentType.MediaType = "application/json";

                //posting request response
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //case success go to list
                    return RedirectToAction("List");
                }
                else
                { //else go to error page
                    return RedirectToAction("Error");
                }

            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
        /// <summary>
        /// Route to direct users to edit Consultants page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View Edit.cshtml</returns>
        // GET: Consultant/Edit/5
        public ActionResult Edit(int id)
        {
            //find Consultant of id on the data base using Consultantdatacontroller
            string url = "consultantdata/findConsultant/" + id;
            //getting Consultant info
            HttpResponseMessage response = client.GetAsync(url).Result;
            //create part from info retrieved
            ConsultantDto Consultant = response.Content.ReadAsAsync<ConsultantDto>().Result;
            //return view

            return View(Consultant);
        }

        // POST: Consultant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ConsultantDto Consultant)
        {
            try
            {
                // TODO: Add insert logic here
                //goal update Consultant on the database using Consultantdata api
                //curl -d @Consultant.json -H "Content-Type:application/json" https://localhost:44306/api/Consultantdata/UpdateConsultant/5
                string url = "consultantdata/UpdateConsultant/" + id;

                //transforming object into json: serializing
                string jsonpayload = jss.Serialize(Consultant);
                //creating HttpContent from json payload
                HttpContent content = new StringContent(jsonpayload);
                Debug.WriteLine(jsonpayload);
                //setting headers
                content.Headers.ContentType.MediaType = "application/json";

                //posting request response
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //case success go to list
                    return RedirectToAction("List");
                }
                else
                { //else go to error page
                    return RedirectToAction("Error");
                }

            }
            catch
            {
                return RedirectToAction("Error");
            }
        }
        /// <summary>
        /// Return view to user confirme deletion of a Consultant
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete.cshtml</returns>
        // GET: Consultant/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //find Consultant of id on the data base using Consultantdatacontroller
            string url = "consultantdata/findConsultant/" + id;
            //getting Consultant info
            HttpResponseMessage response = client.GetAsync(url).Result;
            //create part from info retrieved
            Consultant Consultant = response.Content.ReadAsAsync<Consultant>().Result;
            //return view
            return View(Consultant);
        }

        // POST: Consultant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                //goal remove Consultant from database using Consultantdatacontroller
                // curl -d "" -H "Content-type:application.json" url 
                string url = "consultantdata/DeleteConsultant/" + id;
                Debug.WriteLine("ID Deleted");
                Debug.WriteLine(id);
                //creating HttpContent from json payload
                HttpContent content = new StringContent("");
                //setting headers
                content.Headers.ContentType.MediaType = "application/json";

                //posting delete request
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    //case success go to list
                    return RedirectToAction("List");
                }
                else
                { //else go to error page
                    return RedirectToAction("Error");
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
