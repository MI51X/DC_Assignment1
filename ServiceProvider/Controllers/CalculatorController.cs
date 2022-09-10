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
        public OutputJson AddTwoNumbers(int num1, int num2)
        {
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 + num2;
            return outputJson;
        }

        // GET: AddThreeNumbers
        [Route("addThreeNumbers/{num1}/{num2}/{num3}")]
        [Route("addThreeNumbers")]
        [HttpGet]
        public OutputJson AddThreeNumbers(int num1, int num2, int num3)
        {
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 + num2 + num3;
            return outputJson;
        }

        // GET: MultiplyTwoNumbers
        [Route("multiplyTwoNumbers/{num1}/{num2}")]
        [Route("multiplyTwoNumbers")]
        [HttpGet]
        public OutputJson MultiplyTwoNumbers(int num1, int num2)
        {
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 * num2;
            return outputJson;
        }

        // GET: MultiplyThreeNumbers
        [Route("multiplyThreeNumbers/{num1}/{num2}/{num3}")]
        [Route("multiplyThreeNumbers")]
        [HttpGet]
        public OutputJson MultiplyThreeNumbers(int num1, int num2, int num3)
        {
            OutputJson outputJson = new OutputJson();
            outputJson.output = num1 * num2 * num3;
            return outputJson;
        }

    }
}
