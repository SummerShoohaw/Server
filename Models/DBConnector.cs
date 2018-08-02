using System;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace Server.Models
{
    public class DBConnector
    {
        MySqlConnectionStringBuilder builder;
        MySqlConnection connection;
        bool isConnected = false;

        public DBConnector()
        {
            builder = new MySqlConnectionStringBuilder
            {
                Server = "mpdatabase.mysql.database.azure.com",
                Database = "",
                UserID = "helloworld",
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
            command.CommandText = "INSERT INTO User_Table VALUES (" + username + ");";
            await command.ExecuteNonQueryAsync();
        }

        //database operation C: create new pet
        public async Task CreateNewPetAsync(string username,string petType,string petName){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Pet_Table(....) VALUES ();"; //Script not finished
            await command.ExecuteNonQueryAsync();
        }

        //database operation R: read pet data
        public Pet ReadPetData(string username){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            throw new NotImplementedException("not implemeneted");
        }

        //database operation U: update pet data
        public void UpdatePetData(int petID,Pet newpetdata){
            if (!isConnected)
            {
                throw new Exception("MySQL not connected");
            }
            throw new NotImplementedException("not implemeneted");
        }

        //server-side operation: close connection  
        public void CloseConnection(){
            connection.Close();
            isConnected = false;
        }

        public async Task OpenConnectionAsync(){
            await connection.OpenAsync();
            isConnected = true;
        }
    }
    //end of class DBConnector

}
