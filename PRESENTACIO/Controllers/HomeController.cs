using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft;
using NEGOCIO.Controllers;
using Newtonsoft.Json;
using DATOS;
using System.Text;
using System.Reflection.Emit;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PRESENTACIO.Controllers
{
    public class HomeController : Controller
    {

        #region Calc Ope

        

        #endregion

        #region Connect to API
        Uri baseAddress = new Uri("https://localhost:44334/api");
        HttpClient client;
        
        public HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<resultados> modelList = new List<resultados>();

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/resultados").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<resultados>>(data);
            }
            return View(modelList);
        }

        [HttpPost]
        public ActionResult Index(resultados model, string calculate)
        {

            if(calculate == "+")
            {
                model.result = (model.no1 + model.no2).ToString();
                
            }else if(calculate == "-")
            {
                model.result = (model.no1- model.no2).ToString();
            }else if(calculate == "*")
            {
                model.result = (model.no1 * model.no2).ToString();
            }
            else if(calculate == "/")
            {
                if(model.no2 == 0)
                {
                    model.result = "0";
                }
                model.result = (model.no1 / model.no2).ToString();
            }


            

            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //Enrutamiento de API
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/resultados", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
        #endregion

    }
}