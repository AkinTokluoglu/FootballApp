using FluentValidation;
using FootballApp.Dtos;
using System.Text.RegularExpressions;

namespace FootballApp.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı zorunludur.")
                .Length(2, 50).WithMessage("İsim 2-50 karakter arasında olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim alanı zorunludur.")
                .Length(2, 50).WithMessage("Soyisim 2-50 karakter arasında olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(100).WithMessage("E-posta 100 karakterden uzun olamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı zorunludur.")
                .Must(BeAValidPassword).WithMessage(
                    "Şifreniz Beşiktaş ortasahası kadar zayıf, " +
                    "Şifre en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, " +
                    "bir küçük harf, bir rakam ve bir özel karakter içermelidir.");

            RuleFor(x => x.Age)
                .InclusiveBetween(13, 100).WithMessage("Yaş 13-100 arasında olmalıdır.");

            RuleFor(x => x.Contact)
                .MaximumLength(200).WithMessage("İletişim bilgisi 200 karakteri geçemez.");

            RuleFor(x => x.PositionsPlayed)
                .MaximumLength(100).WithMessage("Oynadığı pozisyonlar 100 karakteri geçemez.");

            RuleFor(x => x.ProfilePicture)
                .MaximumLength(500).WithMessage("Profil resmi URL'i çok uzun.");
        }

        private bool BeAValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return regex.IsMatch(password);
        }
    }
}
