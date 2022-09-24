using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Registry.Models;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace Registry.Controllers
{
    [RoutePrefix("api/Registry")]
    public class UnpublishController : ApiController
    {
        [Route("unpublish/{endpoint}")]
        [Route("unpublish")]
        [HttpDelete]
        public IHttpActionResult Delete(int token, string endpoint)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel responseModel) == false) { return Json(responseModel); }

            string servicesFileLocation = HttpContext.Current.Server.MapPath("~/App_Data/datastore/publish.txt");

            try
            {

                List<PublishModel> allServices = File.ReadLines(servicesFileLocation).Where(x => !string.IsNullOrWhiteSpace(x)).Select(s => JsonConvert.DeserializeObject<PublishModel>(s)).ToList();

                int deleted = allServices.RemoveAll(x => x.APIendpoint == endpoint);

                if (deleted > 0)
                {
                    File.WriteAllText(servicesFileLocation, "");
                    foreach(PublishModel service in allServices)
                    {
                        string updatedServices = JsonConvert.SerializeObject(service);
                        File.AppendAllText(servicesFileLocation, Environment.NewLine + updatedServices);
                    }
                    return Ok("Removed rest service description.");

                }else if(deleted == 0)
                {
                    return Ok("No rest service with the specified endpoint was found.");
                }
                else
                {
                    return BadRequest("Delete error");
                }
            }
            catch
            {
                return BadRequest("Delete Error: The text file was empty.");
            }
        }
    }
}
