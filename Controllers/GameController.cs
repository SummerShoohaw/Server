using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/game")]
    public class GameController : Controller
    {
		DataClass gamedata = new DataClass();


		[HttpGet]
		[Route("{username?}")]
		public Pet GetPet(string username){
			return gamedata.GetPetFromData(username);
		}

		[HttpPost]
		[Route("feed/{username?}")]
		public bool Feed(int number,string username){
			gamedata.Feed(number, username);

			return true;
		}
    }
}
