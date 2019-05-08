using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
                IEnumerable<mvcUserModel> userList;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("User").Result;
                userList = response.Content.ReadAsAsync<IEnumerable<mvcUserModel>>().Result;
     
            
            return View(userList);
        }

        public ActionResult AddorEdit(int id=0)
        {
            if (id == 0)
                return View(new mvcUserModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("User/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcUserModel>().Result);
            }
        }

        [HttpPost]
        public ActionResult AddorEdit(mvcUserModel usr)
        {
            if (usr.Id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("User", usr).Result;
          
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("User/" + usr.Id,usr).Result;
             

            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {

            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("User/" + id.ToString()).Result;
            return RedirectToAction("Index");
        }
    }
}