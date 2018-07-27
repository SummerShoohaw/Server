using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
	[Route("api/DB")]
	public class DBController : Controller
	{
		DBConnector con = new DBConnector();
		[HttpGet]
		[Route("{wechatID}")]
		public async Task<string> GetDataAsync(string wechatID){
			var result = await con.ReadPet("select petName,petWeight,petHungry,petAge from Pet_Table join User_Table on Pet_Table.wechatID_Fkey = User_Table.wechatID where User_Table.wechatID = " + wechatID + " and Pet_Table.returned = false;");
			return result;
		}
	}
}
