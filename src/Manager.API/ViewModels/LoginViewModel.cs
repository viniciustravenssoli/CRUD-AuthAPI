using System.ComponentModel.DataAnnotations;

namespace Manager.API.ViewModels{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Email não pode ser vazio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha não pode ser vazio")]
        public string Password{ get; set; }
    }
}