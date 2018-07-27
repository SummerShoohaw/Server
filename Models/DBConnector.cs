using System;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Server.Models
{
    public class DBConnector
    {
		public MySqlConnectionStringBuilder builder { get; set; }
		public DBConnector(){
			builder = new MySqlConnectionStringBuilder
            {
                Server = "db-6250final.mysql.database.azure.com",
                Database = "mysql",
                UserID = "admin-final@db-6250final",
                Password = "6250Webtools",
                SslMode = MySqlSslMode.Required,
            };
		}
        
		public async Task Writing(string script){
			using (var conn = new MySqlConnection(builder.ConnectionString)){
				Console.WriteLine("openning connection for Insert Data");
				await conn.OpenAsync();

				using (var command = conn.CreateCommand()){
					command.CommandText = script;
					await command.ExecuteNonQueryAsync();
					Console.WriteLine("script running finished");
				}
				Console.WriteLine("Closing connection");
				conn.Close();
			}
		}
        
		public async Task<string> ReadPet(string script){
			using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                Console.WriteLine("Opening connection");
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = script;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
						await reader.ReadAsync();
						return JsonConvert.SerializeObject(new
						{
							petName = reader.GetString(0),
							petWeight = reader.GetInt32(1),
							petHungry = reader.GetInt32(2),
							petAge = reader.GetInt32(3),
						});
                    }
                }
            }
		}

    }
}
