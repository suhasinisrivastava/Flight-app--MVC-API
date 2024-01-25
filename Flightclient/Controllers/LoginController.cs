using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using flightclient.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;

namespace flightclient.Controllers{
    
    public class LoginController:Controller{
        public static Ace52024Context db;
        
        //Dependency Injection  in constructor
        public LoginController(Ace52024Context _db)
        {
            db=_db;
        }
        public ActionResult Login(){
            return View();
        }
        public ActionResult Register(){
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Suhasinicustomer s){

            List<Suhasinicustomer> EmpInfo = new List<Suhasinicustomer>();

            using (var client = new HttpClient())
            {
                //Passing service base url  
         //       client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:5035/api/Customer");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EmpInfo = JsonConvert.DeserializeObject<List<Suhasinicustomer>>(EmpResponse);
                    var result=(from i in EmpInfo
            where i.Customeremail==s.Customeremail && i.Customerpw==s.Customerpw
            select i).SingleOrDefault();
            
            if(result!=null){
                HttpContext.Session.SetInt32("uid",result.Customerid);
                Console.WriteLine("idididididi"+HttpContext.Session.GetInt32("uid"));
                if(s.Customeremail=="admin@email.com" && s.Customerpw=="admin123"){
                return RedirectToAction("Adminhome","Flight");
                
            }
            
                return RedirectToAction("ShowFlightDetail", "Flight");
            }
            else{
                return View();
            }

                }
                //returning the employee list to view  
                return View();
            }
            
            
        }
        public ActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login");

        }

        [HttpPost]

        public async Task<ActionResult> Register(Suhasinicustomer s){
            if(ModelState.IsValid){
                Suhasinicustomer obj = new Suhasinicustomer();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(s), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5035/api/Customer", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<Suhasinicustomer>(apiResponse);
                }
            }
            // db.Suhasinicustomers.Add(s);
            // db.SaveChanges();
            return RedirectToAction("Login");
            }
            else{
                return View();
            }
        }

}
}