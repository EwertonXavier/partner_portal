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
    public class OrderController : Controller
    {
        //making client to be used by orderControlller
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer(); //instantiating serializer

        static OrderController()
        {
            //instantianting HTTP client
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44306/api/");

        }

        // GET: order/List
        public ActionResult List()
        {
            //goal: Communicate with order data api to retrieve list of orders
            //curl https://localhost:44306/api/orderdata/listorders
            string url = "orderdata/listorders";
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            Debug.WriteLine(response.StatusCode);
            //read response content into order list
            IEnumerable<OrderDto> order = response.Content.ReadAsAsync<IEnumerable<OrderDto>>().Result;
            Debug.WriteLine(order.Count());
            return View(order);
        }

        //Error Page
        public ActionResult Error()
        {
            return View();
        }

        // GET: order/Details/5
        public ActionResult Details(int id)
        {
            //goal: Communicate with order data api to retrieve a single order
            //curl https://localhost:44306/api/orderdata/findorder
            string url = "orderdata/findorder/" + id;
            //get response
            HttpResponseMessage response = client.GetAsync(url).Result;
            //read response content into orders details page
            OrderDto order = response.Content.ReadAsAsync<OrderDto>().Result;
            return View(order);
        }

        // GET: order/Create
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

        // POST: order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
                // TODO: Add insert logic here
                //goal insert new order on the database using orderdata api
                //curl -d @order.json -H "Content-Type:application/json" https://localhost:44306/api/orderdata/addorder/
                string url = "orderdata/Addorder/";
                Debug.WriteLine(order.Order_Id);

                //transforming object into json
                //serializing
                string jsonpayload = jss.Serialize(order);
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
        /// Route to direct users to edit orders page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View Edit.cshtml</returns>
        // GET: order/Edit/5
        public ActionResult Edit(int id)
        {
            //find order of id on the data base using orderdatacontroller
            string url = "orderdata/findorder/" + id;
            //getting order info
            HttpResponseMessage response = client.GetAsync(url).Result;
            //create part from info retrieved
            OrderDto order = response.Content.ReadAsAsync<OrderDto>().Result;
            //return view

            return View(order);
        }

        // POST: order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, OrderDto order)
        {
            try
            {
                // TODO: Add insert logic here
                //goal update order on the database using orderdata api
                //curl -d @order.json -H "Content-Type:application/json" https://localhost:44306/api/orderdata/Updateorder/5
                string url = "orderdata/Updateorder/" + id;

                //transforming object into json: serializing
                string jsonpayload = jss.Serialize(order);
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
        /// Return view to user confirme deletion of a order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete.cshtml</returns>
        // GET: order/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //find order of id on the data base using orderdatacontroller
            string url = "orderdata/findorder/" + id;
            //getting order info
            HttpResponseMessage response = client.GetAsync(url).Result;
            //create part from info retrieved
            Order order = response.Content.ReadAsAsync<Order>().Result;
            //return view
            return View(order);
        }

        // POST: order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                //goal remove order from database using orderdatacontroller
                // curl -d "" -H "Content-type:application.json" url 
                string url = "orderdata/Deleteorder/" + id;
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
