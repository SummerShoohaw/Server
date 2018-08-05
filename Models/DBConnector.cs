using System;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DBConnector
    {
        string[] urls = 
        { 
            "pic-url1", 
            "pic-url2", 
            "pic-url3", 
            "pic-url4" 
        };
        MySqlConnectionStringBuilder builder;
        MySqlConnection connection;
        bool isConnected = false;

        public DBConnector()
        {
            builder = new MySqlConnectionStringBuilder
            {
                Server = "mpdatabaseserver.mysql.database.azure.com",
                Database = "mpdb",
                UserID = "helloworld@mpdatabaseserver",
                Password = "Mpdatabase!",
                SslMode = MySqlSslMode.Required,
            };
            connection = new MySqlConnection(builder.ConnectionString);
        }

        //datatbase operation C: create new user
        public async Task CreateNewUserAsync(string username){
            if(!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO user_table(wechatID) VALUE ('" + username + "');";
            await command.ExecuteNonQueryAsync();
        }

        //database operation C: create new pet
        public async Task CreateNewPetAsync(string username,int petType,string petName){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO pet_table(petName,petUrl,petOwner) VALUES ('" + petName + "','" + petType + "', '" + username + "');"; //Script not finished
            await command.ExecuteNonQueryAsync();
        }

        //check user has pet or not
        public async Task<int> CheckUserHasOrNot(string username){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            var command = connection.CreateCommand();
            command.CommandText = "SELECT count(*) FROM pet_table where petOwner ='" + username + "' and isReturned = false";
            int result = 0;
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    result = reader.GetInt32(0);
                }
            }
            return result;
        }

        //database operation R: read pet data
        public async Task<Pet> ReadPetDataAsync(string username){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            var command = connection.CreateCommand();
            command.CommandText = "call read_pet_procedure('"+username+"');";
            Pet pet = new Pet();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //pet.ID = reader.GetInt32(0);
                    pet.name = reader.GetString(0);
                    pet.age = reader.GetInt32(1);
                    pet.weight = reader.GetInt32(2);
                    pet.exer = reader.GetInt32(3);
                    pet.imageNum = reader.GetInt32(4);
                }
            }
            return pet;
        }

        // database operation U: update pet data
        // number --> add how many
        // content: 1 --> Age
        //          2 --> Weight
        //          3 --> Exercise
        public async Task UpdatePetDataAsync(string username, int cnum, int number){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            string[] contents = { "petAge", "petWeight", "petExercise" };
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Pet_Table SET " + contents[cnum] + " = " + contents[cnum] + "+" + number + " where petOwner = '" + username+"';";
            await command.ExecuteReaderAsync();
        }

        //server-side operation: close connection  
        public void CloseConnection(){
            connection.Close();
            isConnected = false;
        }

        //server-side operation: open connection  
        public async Task OpenConnectionAsync(){
            await connection.OpenAsync();
            isConnected = true;
        }
    }
    //end of class DBConnector

}
