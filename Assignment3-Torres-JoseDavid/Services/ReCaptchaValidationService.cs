using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Assignment3_Torres_JoseDavid.Services
{
    /*
    * Course:      Web Programming 3
    * Assessment:  Assignment 3
    * Created by:  Jose David Torres
    * Date:        14/11/2023
    * Class Name:  ReCaptchaValidationService.cs
    * Description: Class that calls the Google ReCaptcha API and verifies the result of the captcha in the backend.
    */
    public class ReCaptchaValidationService
    {
        private readonly string _recaptchaSecretKey;
        private readonly HttpClient _httpClient;

        public ReCaptchaValidationService(IConfiguration configuration, HttpClient httpClient)
        {
            _recaptchaSecretKey = configuration["GoogleReCAPTCHA:SecretKey"];
            _httpClient = httpClient;
        }

        public async Task<bool> IsReCaptchaValid(string recaptchaToken)
        {
            var response = await _httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaSecretKey}&response={recaptchaToken}");

            var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(response);

            return reCaptchaResponse.Success;
        }
    }
    //Response structure of the Google reCaptcha API
    public class ReCaptchaResponse
    {
        public bool Success { get; set; }
        public string[] ErrorCodes { get; set; }
    }
}
