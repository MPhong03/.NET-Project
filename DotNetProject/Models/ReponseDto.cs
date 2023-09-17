namespace DotNetProject.Models
{
    public class ReponseDto
    {
        public string message { get; set; }
        public string token { get; set; }
        public ReponseDto(string message, string token)
        {
            this.message = message;
            this.token = token;
        }
    }
}
