using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndroidApp.Models
{
    [Table("AppUser")]
    public class AppUser
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int AppUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }

    [Table("UploadedImages")]
    public class UploadedImage
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int UploadedImageId { get; set; }
        public string FilePath { get; set; }
        public int AppUserId { get; set; }
    }
}
