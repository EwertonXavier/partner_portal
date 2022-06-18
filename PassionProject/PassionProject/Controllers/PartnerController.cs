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
    public class PartnerController : Controller
    {
        //making client to be used by PartnerControlller
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer(); //instantiating serializer

        static PartnerController() {
            //instantianting HTTP client
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/partnerdata/");
           
        }

        // GET: Partner/List
        public ActionResult List()
        {
            //goal: Communicate with partner data api to retrieve list of partners
            //curl https://localhost:44306/api/partnerdata/listpartners
            string url = "listpartners";
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);
            //read response content into partners list
            IEnumerable<Partner> partners = response.Content.ReadAsAsync<IEnumerable<Partner>>().Result;
            Debug.WriteLine(partners.Count());
            return View(partners);
        }

        //Error Page
        public ActionResult Error()
        {
            return View();
        }

        // GET: Partner/Details/5
        public ActionResult Details(int id)
        {
            //goal: Communicate with partner data api to retrieve a single partner
            //curl https://localhost:44306/api/partnerdata/findpartner
            string url = "findpartner/"+id;
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            //read response content into partners details page
            Partner partner = response.Content.ReadAsAsync<Partner>().Result;
            return View(partner);
        }

        // GET: Partner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partner/Create
        [HttpPost]
        public ActionResult Create(Partner partner)
        {
            try
            {
                // TODO: Add insert logic here
                //goal insert new partner on the database using partnerdata api
                //curl -d @partner.json -H "Content-Type:application/json" https://localhost:44306/api/partnerdata/addpartner/
                string url = "AddPartner/";
                Debug.WriteLine(partner.Name);

                //transforming object into json
                //serializing
                string jsonpayload = jss.Serialize(partner);
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
        /// Route to direct users to edit partners page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View Edit.cshtml</returns>
        // GET: Partner/Edit/5
        public ActionResult Edit(int id)
        {
            //find partner of id on the data base using partnerdatacontroller
            string url = "findpartner/" + id;
            //getting partner info
            HttpResponseMessage response = client.GetAsync(url).Result;
            //create part from info retrieved
            Partner partner = response.Content.ReadAsAsync<Partner>().Result;
            //return view
            return View(partner);
        }

        // POST: Partner/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Partner partner)
        {
            try
            {
                // TODO: Add insert logic here
                //goal update partner on the database using partnerdata api
                //curl -d @partner.json -H "Content-Type:application/json" https://localhost:44306/api/partnerdata/UpdatePartner/5
                string url = "UpdatePartner/" + id;
                Debug.WriteLine(partner.Name);

                //transforming object into json: serializing
                string jsonpayload = jss.Serialize(partner);
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
        /// Return view to user confirme deletion of a partner
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete.cshtml</returns>
        // GET: Partner/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //find partner of id on the data base using partnerdatacontroller
            string url = "findpartner/" + id;
            //getting partner info
            HttpResponseMessage response = client.GetAsync(url).Result;
            //create part from info retrieved
            Partner partner = response.Content.ReadAsAsync<Partner>().Result;
            //return view
            return View(partner);
        }

        // POST: Partner/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                //goal remove partner from database using partnerdatacontroller
                // curl -d "" -H "Content-type:application.json" url 
                string url = "DeletePartner/" + id;
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
