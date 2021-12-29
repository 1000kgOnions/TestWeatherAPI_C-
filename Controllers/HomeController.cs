using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestWeatherAPI.Models;
using System.Net;
using System.Data.Entity;

namespace TestWeatherAPI.Controllers
{
    
    public class HomeController : Controller
    {
        BTL_APIEntities1 db = new BTL_APIEntities1();


        public ActionResult MainPage()
        {
            return View(db.KhoaHocs.ToList());
        }
        public ActionResult Khoahoc(String IDKH)
        {

            KhoaHoc kh = db.KhoaHocs.Single(nameof => nameof.ID_KhoaHoc == IDKH);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }
        public ActionResult Baihoc(string IDKH)
        {
            
            List<BaiHoc> lbh = db.BaiHocs.Where(x => x.ID_KhoaHoc == IDKH).OrderBy(x => x.ID_KhoaHoc).ToList();
            if (lbh.Count == 0)
            {
                ViewBag.SanPham = "Không có sản phẩm nào thuộc loại này";
            }
            KhoaHoc kh = db.KhoaHocs.Single(nameof => nameof.ID_KhoaHoc == IDKH);
            ViewBag.tkh = kh.Ten;         
            return View(lbh);
        }

        [HttpPost]
        public String WeatherDetail(string City)
        {

            // API KEY OPENWEATHERMAP.ORG  
            string appId = "ea09b842ea465ca396dbddb1ca965382";

            //
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", City, appId);
           
            //city
            //http://api.openweathermap.org/data/2.5/weather?q=HaNoi&units=metric&cnt=1&appid=ea09b842ea465ca396dbddb1ca965382

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

             
                ResultViewModel rslt = new ResultViewModel();
                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;
                rslt.Visibility = weatherInfo.visibility;
                rslt.Speed = Convert.ToString(weatherInfo.wind.speed);
                          
                var jsonstring = new JavaScriptSerializer().Serialize(rslt);
            
                return jsonstring;
            }
        }

        //[HttpPost]
        //public String WeatherDetail(float Lat, float Lon)
        //{
    
        //    string appId = "ea09b842ea465ca396dbddb1ca965382";

        //    //toa do
        //    http://api.openweathermap.org/data/2.5/weather?lat=21.0313&lon=105.8516&appid=ea09b842ea465ca396dbddb1ca965382

        //    string url = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=metric&cnt=1&APPID={2}", Lat, Lon, appId);
        //    using (WebClient client = new WebClient())
        //    {
        //        string json = client.DownloadString(url);

        //        //Converting to OBJECT from JSON string.  
        //        RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

        //        //Special VIEWMODEL design to send only required fields not all fields which received from   
        //        //www.openweathermap.org api  
        //        ResultViewModel rslt = new ResultViewModel();

        //        rslt.Country = weatherInfo.sys.country;
        //        rslt.City = weatherInfo.name;
        //        rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
        //        rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
        //        rslt.Description = weatherInfo.weather[0].description;
        //        rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
        //        rslt.Temp = Convert.ToString(weatherInfo.main.temp);
        //        rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
        //        rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
        //        rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
        //        rslt.WeatherIcon = weatherInfo.weather[0].icon;
        //        rslt.Visibility = weatherInfo.visibility;
        //        rslt.Speed = Convert.ToString(weatherInfo.wind.speed);

        //        //Converting OBJECT to JSON String   
        //        var jsonstring = new JavaScriptSerializer().Serialize(rslt);

        //        //Return JSON string.  
        //        return jsonstring;
        //    }
        //}

    }
}