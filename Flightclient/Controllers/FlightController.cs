using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using flightclient.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace flightclient.Controllers{
    
    public class FlightController:Controller{
        public static Ace52024Context db;
        
        //Dependency Injection  in constructor
        public FlightController(Ace52024Context _db)
        {
            db=_db;
        }
        
        public async Task<ActionResult> ShowFlightDetail(){
            List<Suhasiniflight> flights=new List<Suhasiniflight>();
            ViewBag.Userid=HttpContext.Session.GetInt32("uid");
            if(ViewBag.Userid!=null){
                ViewBag.Flightsource=new SelectList(db.Suhasiniflights,"Flightsource");
                HttpClient client= new HttpClient();

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:5035/api/Flight");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    flights = JsonConvert.DeserializeObject<List<Suhasiniflight>>(EmpResponse);

                }
                //returning the employee list to view  
                return View(flights);
                
            //      List<Suhasiniflight> s=(from i in db.Suhasiniflights select i).ToList();
            
            // return View(s);
            }
           else{
               return RedirectToAction("Login","Login");
            }

           

        }
        public async Task<ActionResult> Bookingdetails(){
            List<Suhasinibooking> bookingbyuser=new List<Suhasinibooking>();
            ViewBag.Userid=HttpContext.Session.GetInt32("uid");
            int user=Convert.ToInt32(ViewBag.Userid);
            Console.WriteLine("useruseruser"+ViewBag.Userid);

            HttpClient client= new HttpClient();

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:5035/api/Booking");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    bookingbyuser = JsonConvert.DeserializeObject<List<Suhasinibooking>>(EmpResponse);
                    bookingbyuser = (from i in bookingbyuser
                    where i.Customerid==user
                    select i).ToList();

                }
                //returning the employee list to view  
                return View(bookingbyuser);
                





            // List<Suhasinibooking> s=(from i in db.Suhasinibookings
            //                          where i.Customerid == user
            // select i).ToList();
            // return View(s);
        }
        public ActionResult Book(string Flightid, int Flightprice){
            ViewBag.Userid=HttpContext.Session.GetInt32("uid");

            Console.WriteLine(Flightprice);
            Suhasinibooking s=new Suhasinibooking();
            s.Flightid = Flightid;
            Random rnd = new Random();
            int num = rnd.Next();
            s.Bookingid= num.ToString();
            s.Customerid=Convert.ToInt32(ViewBag.Userid);
            s.Bookingdate=DateTime.Now;
            s.Bookingtotalcost=Flightprice;
            Console.WriteLine("fpfpfp"+s.Bookingtotalcost);
            TempData["rate"]=Flightprice;
            return View(s);


        }

        [HttpPost]
        public async Task<ActionResult> Book(Suhasinibooking s){
            
            s.Bookingtotalcost=s.Bookingtotalmembers*((int)TempData["rate"]);
            TempData["rate"]=s.Bookingtotalcost;

            Suhasinibooking booking=new Suhasinibooking();
            HttpClient httpClient= new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(s), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5035/api/Booking", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    booking = JsonConvert.DeserializeObject<Suhasinibooking>(apiResponse);
                }


            // db.Suhasinibookings.Add(s);
            // db.SaveChanges();
            // Console.WriteLine("xxxxxx"+s.Bookingtotalcost);
            


            return RedirectToAction("ShowFlightDetail","Flight");

        }
        public ActionResult Showselectedflights(List<Suhasiniflight> s){
            s=JsonConvert.DeserializeObject<List<Suhasiniflight>>(TempData["selected"].ToString());
            Console.WriteLine("cccc"+s.Count);
            return View(s);
        }
        public async Task<ActionResult> GetFlightDetail(){
            List<Suhasiniflight> flights = new List<Suhasiniflight>();
            HttpClient client= new HttpClient();

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:5035/api/Flight");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    flights = JsonConvert.DeserializeObject<List<Suhasiniflight>>(EmpResponse);

                    ViewBag.Flightsources=new SelectList(flights,"Flightsource","Flightsource");
                    ViewBag.Flightdestinations=new SelectList(flights,"Flightdestination","Flightdestination");

                }


            //ViewBag.Flightsources=new SelectList(db.Suhasiniflights,"Flightsource","Flightsource");
            //ViewBag.Flightdestinations=new SelectList(db.Suhasiniflights,"Flightdestination","Flightdestination");
            
            // foreach (var item in ViewBag.Flightsource){
            //         Console.WriteLine("ans"+item);
            //     }
            return View();

        }

        [HttpPost]
        public async Task<ActionResult> GetFlightDetail(Suhasiniflight t ){
            List<Suhasiniflight> flights;
            HttpClient client= new HttpClient();

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:5035/api/Flight");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    flights = JsonConvert.DeserializeObject<List<Suhasiniflight>>(EmpResponse);
                    flights=(from i in flights
            where i.Flightsource==t.Flightsource && i.Flightdestination==t.Flightdestination && i.Flightdate==t.Flightdate
            select i).ToList();
            if(flights.Count==0){
                return View();
            }
            else{
                TempData["selected"]=JsonConvert.SerializeObject(flights);
                return RedirectToAction("Showselectedflights","Flight");
            }

            }
            return RedirectToAction("ShowFlightDetail","Flight");
          
            
            
        
        }
        [HttpGet]
        public async Task<ActionResult> Cancel(string id)
        {
            TempData["bookingid"] = id;
            Suhasinibooking e = new Suhasinibooking();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5035/api/Booking/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Suhasinibooking>(apiResponse);
                }
            }
            return View(e);
        }
       [HttpPost]
        public async Task<ActionResult> Cancel(Suhasinibooking s){
            //TempData["bookingid"] = id;
            string id = Convert.ToString(TempData["bookingid"]);

            Console.WriteLine("deletdetdetedte"+id);
            Suhasinibooking e = new Suhasinibooking();
           using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5035/api/Booking/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            // Suhasinibooking s=db.Suhasinibookings.Find(id);
            // db.Suhasinibookings.Remove(s);
            // db.SaveChanges();
           return RedirectToAction("ShowFlightDetail","Flight");
        }
         [HttpGet]
        public async Task<ActionResult> Cancelflight(string id)
        {
            TempData["flightid"] = id;
            Suhasiniflight e = new Suhasiniflight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5035/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    e = JsonConvert.DeserializeObject<Suhasiniflight>(apiResponse);
                }
            }
            return View(e);
        }
        [HttpPost]
        public async Task<ActionResult> Cancelflight(Suhasiniflight s){
            string id = Convert.ToString(TempData["flightid"]);

            //Console.WriteLine("deletdetdetedte"+id);
            Suhasiniflight e = new Suhasiniflight();
           using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://localhost:5035/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            // Suhasiniflight s=db.Suhasiniflights.Find(id);
            // db.Suhasiniflights.Remove(s);
            // db.SaveChanges();
            return  RedirectToAction("Adminhome","Flight");
        }

        public async  Task<ActionResult> Adminhome(){
            List<Suhasiniflight> flights = new List<Suhasiniflight>();
            HttpClient client= new HttpClient();

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("http://localhost:5035/api/Flight");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    flights = JsonConvert.DeserializeObject<List<Suhasiniflight>>(EmpResponse);

                }
                //returning the employee list to view  
                return View(flights);
            
            //return View(db.Suhasiniflights);
        }
          public ActionResult AddFlight(){

            return View();

        }
        [HttpPost]
         public async Task<ActionResult>  AddFlight(Suhasiniflight e){
            Suhasiniflight Emplobj = new Suhasiniflight();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), 
              Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:5035/api/Flight", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Emplobj = JsonConvert.DeserializeObject<Suhasiniflight>(apiResponse);
                }
            }
            // db.Suhasiniflights.Add(s);
            // db.SaveChanges();
            return RedirectToAction("Adminhome");
        }

        public async Task<ActionResult> Editflight(string id){
            Suhasiniflight s=new Suhasiniflight();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:5035/api/Flight/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    s = JsonConvert.DeserializeObject<Suhasiniflight>(apiResponse);
                }
            }
            Console.WriteLine("abcbcbcbc"+id);
            return View(s);
        }

        [HttpPost]
        public async Task<ActionResult> Editflight(Suhasiniflight s){
            Suhasiniflight receivedemp = new Suhasiniflight();

            using (var httpClient = new HttpClient())
            {
                string id = s.Flightid;
                StringContent content1 = new StringContent(JsonConvert.SerializeObject(s)
         , Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://localhost:5035/api/Flight/" + id, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedemp = JsonConvert.DeserializeObject<Suhasiniflight>(apiResponse);
                }
            }
            // Console.WriteLine("abcbcbcbc"+s.Flightid);
            // db.Suhasiniflights.Update(s);
            // db.SaveChanges();
            return RedirectToAction("Adminhome");
        }

        

}
}