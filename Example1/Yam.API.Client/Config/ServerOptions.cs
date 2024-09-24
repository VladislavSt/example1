using System.ComponentModel.DataAnnotations;

namespace Yam.API.Client.Config
{
    internal class ServerOptions
    {
        [Required]
        public string AccountUriRegister { get; set; }

        [Required]
        public string AccountUriLogin { get; set; }

        [Required]
        public string AccountUriRefreshtoken { get; set; }

        [Required]
        public string AccountUriInfo { get; set; }
    }
}
