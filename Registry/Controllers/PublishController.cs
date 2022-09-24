using Newtonsoft.Json;
using Registry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Registry.Controllers {

    [RoutePrefix("api/Registry")]
    
    public class PublishController : ApiController {
        
        [Route("publish")]
        [HttpPost]
        public IHttpActionResult Post(int token, [FromBody]PublishModel model) {

            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel responseModel) == false) { return Json(responseModel); }

            if (ModelState.IsValid) {

                string publocraw = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/datastore/publish.txt");

                string json = JsonConvert.SerializeObject(model);

                System.IO.File.AppendAllText(publocraw, Environment.NewLine + json);
                
                return Ok(model);
            } else {
                return null;
            }// end of if else
        }// end of Post

    }
}
