namespace _10_Identity.WebApiApp.Dependencies.Identites
{
    public class AcademyIdentityErrors : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = "ایمیل وارد شدن قبلا وارد شده است"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = $"نام کاربری {userName} قبلا وارد شده است"
            };
        }

        public override IdentityError InvalidEmail(string? email)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = "ایمیل وارد شده نامعتبر است"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = $"رمز عبور باید حداقل {length} کارکتر باشد"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = "رمز عبور باید جداقل یک کاراکتر داشته باشد"
            };
        }
    }
}
