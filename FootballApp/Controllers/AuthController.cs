using FluentValidation;
using FootballApp.Dtos;
using FootballApp.Entities;
using FootballApp.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FootballApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserRegisterDto> _validator;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthController> _logger;
        private readonly FootballDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(
            IUserRepository userRepository,
            IValidator<UserRegisterDto> validator,
            IPasswordHasher<User> passwordHasher,
            ILogger<AuthController> logger,
            FootballDbContext context, 
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _validator = validator;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }
        #region Register
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterDto>> Register(UserRegisterDto request)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new UserRegisterResponse
                    {
                        Success = false,
                        Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage))
                    });
                }

                if (await _userRepository.EmailExistsAsync(request.Email))
                {
                    return Conflict(new UserRegisterResponse
                    {
                        Success = false,
                        Message = "Bu e-posta adresi zaten kullanımda. Giriş yapmayı deneyebilirsiniz."
                    });
                }


                var user = new User
                {
                    Name = request.Name.Trim(),
                    Surname = request.Surname.Trim(),
                    Email = request.Email.ToLowerInvariant().Trim(),
                    Age = request.Age,
                    Contact = request.Contact?.Trim(),
                    Position = request.PositionsPlayed?.Trim(),
                    ProfilePictureUrl = request.ProfilePicture?.Trim(),
                    DateCreated = DateTime.UtcNow
                };

                // Hash the password
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);


                await _userRepository.CreateUserAsync(user);

                
                return Ok(new UserRegisterResponse
                {
                    Success = true,
                    Message = "Kayıt başarıyla tamamlandı.",
                    UserId = user.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı kaydı sırasında hata oluştu: {Email}", request.Email);
                return StatusCode(500, new UserRegisterResponse
                {
                    Success = false,
                    Message = "Kayıt işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz."
                });
            }
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Use IPasswordHasher to verify the password
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (passwordVerificationResult != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid email or password.");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { token });
        }

        #endregion

        #region Functions

        #endregion
        //jwt token genereta merkezi bir hale getirilmeli
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}