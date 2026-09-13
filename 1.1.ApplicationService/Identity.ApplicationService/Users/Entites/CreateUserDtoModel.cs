namespace Identity.ApplicationService.Users.Entites
{
    public class CreateUserDtoModel : BaseCrudUserDtoModel
    {
        public string Password { get; set; } = string.Empty; 
    }
}
