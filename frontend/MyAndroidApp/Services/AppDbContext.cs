using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MyAndroidApp.Models;

namespace MyAndroidApp.Services
{
    public class AppDbContext
    {
        public AppDbContext(string dbPath, bool createTables=false) 
        {
            _connection = new SQLiteConnection(dbPath);
            if (createTables )
            {
                _createTables();
            }
        }
        private readonly SQLiteConnection _connection;

        private void _createTables()
        {
            _connection.CreateTable<AppUser>();
            _connection.CreateTable<UploadedImage>();
        }

        public int AddUser(AppUser user)
        {
            return _connection.Insert(user);
        }

        public AppUser GetUserById(int id)
        {
            return _connection.Table<AppUser>().FirstOrDefault(au => au.AppUserId == id);
        }

        public AppUser GetUserByEmail(string email)
        {
            return _connection.Table<AppUser>().FirstOrDefault(au => au.Email == email);
        }




    }
}
