namespace Assignment3_Torres_JoseDavid.Services
{
    /*
    * Course:      Web Programming 3
    * Assessment:  Assignment 3
    * Created by:  Jose David Torres
    * Date:        14/11/2023
    * Class Name:  SendGridEmailSenderOptions.cs
    * Description: Class that contains the necessary fields to call the SendGrid API;
    */
    public class SendGridEmailSenderOptions
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
