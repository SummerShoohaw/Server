using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {
        DBConnector connector;

        public GameController()
        {
            this.connector = new DBConnector();
        }

    
        [HttpGet]
        [Route("createuser/{username?}")]
        public async Task<StatusCodeResult> CreateUserAsync(string username)
        {
            await connector.OpenConnectionAsync();
            await connector.CreateNewUserAsync(username);
            connector.CloseConnection();
            return new StatusCodeResult(200);
        }

        //check user has a pet or not
        [HttpGet]
        [Route("haspet/{username?}")]
        public async Task<int> HasPetAsync(string username)
        {
            await connector.OpenConnectionAsync();
            int has = await connector.CheckUserHasOrNot(username);
            connector.CloseConnection();
            return has;
        }

        //check system has a user or not
        [HttpGet]
        [Route("hasuser/{username?}")]
        public async Task<int> HasUserAsync(string username)
        {
            await connector.OpenConnectionAsync();
            int has = await connector.HasUserOrNot(username);
            connector.CloseConnection();
            return has;
        }

        
        [HttpGet]
        [Route("getpet/{username?}")]
        public async Task<Pet> GetPetAsync(string username)
        {
            Int32.TryParse(HttpContext.Request.Query["urlnum"].ToString(),out int urlNum);
            string petName = HttpContext.Request.Query["petname"].ToString();
            await connector.OpenConnectionAsync();
            Pet pet = await connector.ReadPetDataAsync(username);
            connector.CloseConnection();
            return pet;
        }

        // querystring: pet url number --> 1 or 2 or 3 or 4 (must be a int)
        //              pet name --> any
        [HttpGet]
        [Route("createPet/{username?}")]
        public async Task<StatusCodeResult> CreatePetAsync(string username)
        {
            Int32.TryParse(HttpContext.Request.Query["urlnum"].ToString(),out int urlnum);
            string petName = HttpContext.Request.Query["petname"].ToString();

            await connector.OpenConnectionAsync();
            await connector.CreateNewPetAsync(username,urlnum,petName);
            connector.CloseConnection();
            return new StatusCodeResult(200);
        }

        //query string details: 
        // content: 1 --> Age
        //          2 --> Weight
        //          3 --> Exercise 
        // number : how many is going to change
        [HttpPost]
        [Route("update/{username?}")]
        public async Task<StatusCodeResult> UpdatePet(string username){
            
            var rbody = "";
            using(StreamReader reader = new StreamReader(Request.Body)){
                rbody = reader.ReadToEnd();
            }
            var strings = rbody.Split("/");
            Int32.TryParse(strings[0], out int content);
            Int32.TryParse(strings[1], out int number);
            await connector.OpenConnectionAsync();
            await connector.UpdatePetDataAsync(username, content, number);
            connector.CloseConnection();

            return new StatusCodeResult(200);
        }
    }
}
