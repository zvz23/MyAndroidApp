using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Headers;

namespace MyAndroidApp.Services
{
    public class APIRequest
    {
        private const string BASE_URL = "http://13.215.47.77:8080/";
        private readonly HttpClient _httpClient;
        public APIRequest()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BASE_URL)
            };

        }

        public async Task<LoginResponse?> Login(string email, string password)
        {

            var jsonContent = new StringContent(
            JsonSerializer.Serialize(new
            {
                email = email,
                password = password
            }),
            Encoding.UTF8,
        "application/json");
            HttpResponseMessage message = await _httpClient.PostAsync("api/users/login", jsonContent);
            string json = await message.Content.ReadAsStringAsync();
            LoginResponse? loginResponse = JsonSerializer.Deserialize<LoginResponse>(json);
            return loginResponse;
        }

        public async Task<RegisterResponse?> Register(RegisterInfo registerInfo)
        {
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(new
            {
                email = registerInfo.email,
                password = registerInfo.password,
                firstName = registerInfo.firstName,
                lastName = registerInfo.lastName
            }),
            Encoding.UTF8,
        "application/json");
            using (HttpResponseMessage message = await _httpClient.PostAsync("api/users/register", jsonContent))
            {
                RegisterResponse? response = null;

                string json = await message.Content.ReadAsStringAsync();
                if (json.Contains("User is successfully registered."))
                {
                    response = new RegisterResponse()
                    {
                        isSuccess = true
                    };
                }
                else
                {
                    response = JsonSerializer.Deserialize<RegisterResponse>(json);
                }
                return response;

            }
        }

        public async Task<PasswordResetResponse?> PasswordReset(PasswordResetInfo passwordResetInfo)
        {
            var jsonContent = new StringContent(
           JsonSerializer.Serialize(new
           {
               email = passwordResetInfo.email,
               newPassword = passwordResetInfo.newPassword
           }),
           Encoding.UTF8,
       "application/json");
            HttpResponseMessage message = await _httpClient.PutAsync("api/users/reset/password", jsonContent);
            string json = await message.Content.ReadAsStringAsync();
            PasswordResetResponse? response = null;

            if (json.Contains("Password successfully reset"))
            {
                response = new PasswordResetResponse()
                {
                    isSuccess = true
                };
            }
            else
            {
                response = JsonSerializer.Deserialize<PasswordResetResponse>(json);

            }
            return response;



        }

        public async Task<List<Image>?> GetImages(int id)
        {
            HttpResponseMessage message = await _httpClient.GetAsync($"api/image/list?ownerId={id}");
            string json = await message.Content.ReadAsStringAsync();
            List<Image>? images = JsonSerializer.Deserialize<List<Image>>(json);
            images ??= new List<Image>();
            return images;
        }

        public async Task<string> UploadImage(string imagePath, int id)
        {
            var fileStreamContent = new StreamContent(File.OpenRead(imagePath));
            string extension = Path.GetExtension(imagePath).Substring(1);
            if (extension == "jpg")
            {
                extension = "jpeg";
            }
            string fileName = Path.GetFileName(imagePath);
            
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue($"image/{extension}");
            var formData = new MultipartFormDataContent();
            formData.Add(fileStreamContent, name: "image", fileName: fileName);
            var response = await _httpClient.PostAsync($"api/image/upload?ownerId={id}", formData);
            return await response.Content.ReadAsStringAsync();
        }

    }

    public class RegisterInfo
    {
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class PasswordResetInfo
    {
        public string email { get; set; }
        public string newPassword { get; set; }
    }

    public class BaseResponse
    {
        public int? status { get; set; }
        public string? error { get; set; }
        public string? message { get; set; }
    }
    public class LoginResponse : BaseResponse
    {
        public int? id { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }

    }


    public class RegisterResponse : BaseResponse
    {
        public bool isSuccess { get; set; } = false;
    }

    public class PasswordResetResponse : BaseResponse
    {
        public bool isSuccess { get; set; } = false;
    }

    public class Image
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }
        public int? userOwner { get; set; }
        public string? viewImage { get; set; }
    }

}
