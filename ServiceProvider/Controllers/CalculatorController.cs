using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServiceProvider.Controllers
{
    public class OutputJson
    {
        public int output;
    }

    [RoutePrefix("api/calculator")]
    public class CalculatorController : ApiController
    {
        // GET: AddTwoNumbers
        [Route("addTwoNumbers/{num1}/{num2}")]
        [Route("addTwoNumbers")]
        [HttpGet]
        public IHttpActionResult AddTwoNumbers(int token, int num1, int num2)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel response) == false) { return Json(response);}
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 + num2;
            return Ok(outputJson);
        }
        
        // GET: AddThreeNumbers
        [Route("addThreeNumbers/{num1}/{num2}/{num3}")]
        [Route("addThreeNumbers")]
        [HttpGet]
        public IHttpActionResult AddThreeNumbers(int token, int num1, int num2, int num3)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel response) == false) { return Json(response); }
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 + num2 + num3;
            return Ok(outputJson);
        }
        
        // GET: MultiplyTwoNumbers
        [Route("multiplyTwoNumbers/{num1}/{num2}")]
        [Route("multiplyTwoNumbers")]
        [HttpGet]
        public IHttpActionResult MultiplyTwoNumbers(int token, int num1, int num2)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel response) == false) { return Json(response); }
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 * num2;
            return Ok(outputJson);
        }

        // GET: MultiplyThreeNumbers
        [Route("multiplyThreeNumbers/{num1}/{num2}/{num3}")]
        [Route("multiplyThreeNumbers")]
        [HttpGet]
        public IHttpActionResult MultiplyThreeNumbers(int token, int num1, int num2, int num3)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel response) == false) { return Json(response); }
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 * num2 * num3;
            return Ok(outputJson);
        }

        /*// GET: [OPERATOR NAME][NUMBER OF INPUTS]
        [Route("[OPERATOR NAME][NUMBER OF INPUTS]/{num1}/{num2}/{num3}")]
        [Route("[OPERATOR NAME][NUMBER OF INPUTS]")]
        [HttpGet]
        public IHttpActionResult [OPERATOR NAME][NUMBER OF INPUTS](int token, int num1, int num2, int num3)
        {
            if (new AuthStatusProvider().AuthStatusCheck(token, out AuthStatusProvider.ResponseModel response) == false) { return Json(response); }
            OutputJson outputJson = new OutputJson();
            outputJson.output = [CALCULATION];
            return Ok(outputJson);
        }*/

    }
}
