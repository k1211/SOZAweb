using Newtonsoft.Json;
using SOZA_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SOZA_web.Controllers.WebApi
{
    [RoutePrefix("api/android")]
    public class AndroidApiController : ApiController
    {
        private static readonly string ANDROID_CLIENT_PASS = "12cb74ahq";

        [Route("token/generate")]
        [HttpPost]
        public string GenerateToken([FromBody]string securityPass)
        {
            if (securityPass != ANDROID_CLIENT_PASS)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            var androidHelper = new AndroidHelper(ApplicationDbContext.Create());
            var token = androidHelper.GenerateToken();
            return token;
        }

        [Route("gpstrace/add")]
        [HttpPost]
        public void AddGpsTrace([FromBody]string json)
        {
            try
            {
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
                string token = jsonObject.Token;
                double lat = jsonObject.Latitude;
                double lon = jsonObject.Longitude;

                var androidHelper = new AndroidHelper(ApplicationDbContext.Create());
                androidHelper.AddGpsTrace(token, lat, lon);
            }
            catch (Exception e)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.ReasonPhrase = e.Message;
                throw new HttpResponseException(response);
            }
        }
    }
}