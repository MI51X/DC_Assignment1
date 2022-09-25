using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Registry.Models;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace Registry.Controllers
{
    [RoutePrefix("api/Registry")]
    public class AllServicesController : ApiController
    {
        [Route("allServices")]
        [HttpGet]
        public IHttpActionResult GetAllServices(int token)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel responseModel) == false) { return Json(responseModel); }

            string servicesFileLocation = HttpContext.Current.Server.MapPath("~/App_Data/datastore/publish.txt");

            try
            {
                List<PublishModel> allServices = File.ReadLines(servicesFileLocation).Where(x => !string.IsNullOrWhiteSpace(x)).Select(s => JsonConvert.DeserializeObject<PublishModel>(s)).ToList();

                return Ok(allServices);
            }
            catch
            {
                return null;
            }
        }
    }
}
