namespace Identity.ApplicationService.Users.Entites
{
    public class ChangePasswordDtoModel
    {
        public Guid Id { get; set; } 

        public string NewPassword { get; set; } = string.Empty;
    }
}
