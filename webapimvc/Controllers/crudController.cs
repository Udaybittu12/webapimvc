using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapimvc.Models;
using System.Net.Http;

namespace webapimvc.Controllers
{
    public class crudController : Controller
    {
        // GET: crud
        public ActionResult Index()                 // to view the total records
        {
            IEnumerable<naveenpreg> empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44314/api/empcrud");    //making url for baseaddress

            var consumeapi = hc.GetAsync("empcrud");       //webapi controller name
            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<naveenpreg>>();
                displaydata.Wait();

                empobj = displaydata.Result;
            }
            return View(empobj);
        }
        public ActionResult create()                // get method of insert
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(naveenpreg insertemp)           // post method of insert
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44314/api/empcrud");

            var insertrecord = hc.PostAsJsonAsync<naveenpreg>("empcrud",insertemp);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if(savedata.IsSuccessStatusCode)
            {
                ViewBag.message = "the user" + insertemp.empname + " is saved successfully..!";
                return RedirectToAction("Index");
            }
            return View("create");
        }
        public ActionResult details(int id)           // get details in gridview
        {
            emp empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44314/api/");

            var consumeapi = hc.GetAsync("empcrud?id=" + id.ToString());
            consumeapi.Wait();
  
            var readdata = consumeapi.Result;
            if(readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<emp>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }
        public ActionResult edit(int id)            // edit the details       &              details code pasted here
        {
            emp empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44314/api/");

            var consumeapi = hc.GetAsync("empcrud?id=" + id.ToString());
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<emp>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }
        [HttpPost]
        public ActionResult edit(emp ec)                // copy same create code here 
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44314/api/empcrud");
            var insertrecord = hc.PutAsJsonAsync<emp>("empcrud", ec);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "emp record not updated....!";
            }
            return View(ec);
        }
        public ActionResult delete(int id)        // delete the id
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44314/api/empcrud");
            var delrecord = hc.DeleteAsync("empcrud/" + id.ToString());
            delrecord.Wait();
            var displaydata = delrecord.Result;
            if(displaydata.IsSuccessStatusCode)
            {
                ViewBag.message = "the user" + id + " is deleted successfully..!";
                return RedirectToAction("index");
            }
            return View("index");
        }


        // mvc 5 controller //
    }
}