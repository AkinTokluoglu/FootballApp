using Microsoft.AspNetCore.Mvc;
using FootballApp.Entities;
using FootballApp.Dtos;
using FootballApp;
using Microsoft.EntityFrameworkCore;
using FootballApp.Repositories;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly FootballDbContext _context;
    private readonly IUserRepository _userRepository;


    public TeamController(FootballDbContext context,
                          IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    [HttpPost("create-team")]
    public async Task<IActionResult> CreateTeam(CreateTeamDto createTeamDto)
    {
        // Validate member count
        if (createTeamDto.MemberIds.Count > 7)
        {
            return BadRequest("Bu versiyonda oluşturabileceğiniz takım en fazla 7 kişi içerebilir");
        }

        // Check captain existence
        var captain = await _context.Users.FindAsync(createTeamDto.MemberIds.First());
        if (captain == null)
        {
            return NotFound("Captain not found.");
        }

        // Rolü "Captain" yap ve güncelle
        captain.Role = "Captain";
        await _userRepository.UpdateUserAsync(captain); // UserRepository enjekte edilmeli

        // Fetch members
        var members = await _context.Users
            .Where(u => createTeamDto.MemberIds.Contains(u.Id))
            .ToListAsync();

        if (members.Count != createTeamDto.MemberIds.Count)
        {
            return BadRequest("One or more members are invalid");
        }

        // Create team
        var team = new Team
        {
            Name = createTeamDto.Name,
            CaptainId = captain.Id,
            Members = members
        };

        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Team created successfully", teamId = team.Id });

        //createdate de bir sıkıntı var kontrol edebil bir ara farklı saatte kayıt yapıyor.
    }

    [HttpPost("{teamId}/create-match")]
    public async Task<IActionResult> CreateMatch(int teamId, [FromBody] CreateMatchDto createMatchDto)
    {
        var team = await _context.Teams.Include(t => t.Captain).FirstOrDefaultAsync(t => t.Id == teamId);
        if (team == null)
        {
            return NotFound("Team not found.");
        }

        if (team.Captain.Id != int.Parse(User.FindFirst("id")?.Value ?? "0"))
        {
            return Forbid("Only the captain can create a match");
        }

        var match = new Match
        {
            TeamId = team.Id,
            MatchDate = createMatchDto.MatchTime,
            Location = createMatchDto.Location
        };

        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Match created successfully", matchId = match.Id });
    }

    [HttpPost("{teamId}/add-member")]
    public async Task<IActionResult> AddMemberToTeam(int teamId, [FromBody] int userId)
    {
        var team = await _context.Teams.FindAsync(teamId);
        var user = await _context.Users.FindAsync(userId);

        if (team == null || user == null)
        {
            return NotFound("Takım veya kullanıcı bulunamadı.");
        }

        var userTeam = new UserTeam { UserId = userId, TeamId = teamId };
        _context.UserTeams.Add(userTeam);
        await _context.SaveChangesAsync();

        return Ok("Member Added To Team");

        //bu işlemi takım oluştururken senkron bir şekilde yapmam gerekiyor yoksa kullanıcı önce takım sonra üye ekleme yapacak 
    }

}
