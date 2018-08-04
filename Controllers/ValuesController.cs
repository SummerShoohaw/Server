using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using wechatApi.Models;

namespace Server.Controllers
{
    [Route("api/decrypt")]
    public class ValuesController : Controller
    {
        //this part is to decode the data from wechat api
        private wxHelper helper;
        private IMemoryCache cache;

        public ValuesController(IMemoryCache cache)
        {
            this.cache = cache;
            this.helper = new wxHelper();
        }

        [HttpPost]
        [Route("{code}")]
        public string OnLogin(string code)
        {

            string data = "";
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                data = sr.ReadToEnd();
            }
            AppSession appSession = JsonConvert.DeserializeObject<AppSession>(data);
            string enData = appSession.encryptedData;
            string iv = appSession.iv;
            string key = appSession.sessionKey;
            string res = helper.AES_decrypt(enData, key, iv);
            return res;
        }

    }

    public class AppSession
    {
        public string encryptedData;
        public string iv;
        public string sessionKey;
    }
}
