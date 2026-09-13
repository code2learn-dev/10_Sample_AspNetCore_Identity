namespace Identity.WebAppRazorPage.Dependencies.Identities
{
    public class CustomIdentityErrors : IdentityErrorDescriber
    {
        public override IdentityError InvalidEmail(string? email)
        {
            return new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = "آدرس ایمیل وارد شده نامعتبر است"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = "ایمیل وارد شده تکراری می باشد"
            };
        }

        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = "نقش وارد شده تکراری می باشد"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "رمز عبور وارد شده باید شامل حروف باشد"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = $"رمز عبور وارد شده باید حداقل {length} حرف باشد"
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "رمز عبور باید شامل عدد باشد"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = $"نام کاربری {userName} تکراری می باشد"
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError()
            {
                Code = nameof(InvalidToken),
                Description = "کد وارد شده نامعتبر می باشد"
            };
        } 

		public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError()
            {
                Code = nameof(UserAlreadyInRole),
                Description = "نقش انتخابی قبلا برای کاربر انتخاب شده است"
            };
        }
    }
}
