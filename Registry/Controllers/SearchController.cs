using Newtonsoft.Json;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Registry.Controllers {

    [RoutePrefix("api/Registry")]

    public class SearchController : ApiController {
        
        [Route("search/{searchname}")]
        [Route("search")]
        [HttpGet]
        public IHttpActionResult Get(int token, String searchname) {

            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel responseModel) == false) { return Json(responseModel); }

            string publocraw = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/datastore/publish.txt");

            List<PublishModel> pm = File.ReadAllLines(publocraw).Where(x =>!string.IsNullOrWhiteSpace(x)).Select(v => JsonConvert.DeserializeObject<PublishModel>(v)).ToList();

            List<PublishModel> results = new List<PublishModel>();
            

            foreach (PublishModel p in pm) {
                if (p.Name == searchname) {
                    results.Add(p);
                }// end of if
            }// end of foreach

            return Ok(results);

        }// end of get

    }// end of SearchController
    
}// end of registry
