using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OPP_back.Models.Data;
using OPP_back.Models.Dto;
using OPP_back.Models.Dto.Responses;
using OPP_back.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OPP_back.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _DbContext;
        private readonly IPasswordHashService _PasswordHasher;
        private readonly TokenService _TokenService;

        public AuthService(
            AppDbContext DbContext,
            IPasswordHashService PasswordHasher,
            TokenService TokenService
        ) {
            _DbContext = DbContext;
            _PasswordHasher = PasswordHasher;
            _TokenService = TokenService;
        }

        public async Task<Guid?> RegisterUser(string email, string password)
        {
            if (await _DbContext.Users.AnyAsync(u => u.Email == email))
                return null;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _PasswordHasher.HashPassword(password),
                Subjects = [],
                Teams = []
            };

            await _DbContext.Users.AddAsync(user);
            await _DbContext.SaveChangesAsync();

            return user.Id;
        }

        public async Task<TokensResponseDto?> LoginUser(string email, string password)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null ||
                !_PasswordHasher.VerifyPassword(password, user.PasswordHash))
                return null;

            var tokens = new TokensResponseDto
            {
                Access = _TokenService.GenerateAccessToken(user),
                Refresh = _TokenService.GenerateRefreshToken()
            };

            await _DbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokens.Refresh,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsValid = true,
                UserId = user.Id,
                User = user
            });
            await _DbContext.SaveChangesAsync();

            return tokens;
        }

        public async Task<TokensResponseDto?> RefreshTokens(string token)
        {
            var refToken = await _DbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refToken == null ||
                refToken.ExpiresAt < DateTime.UtcNow ||
                !refToken.IsValid)
                return null;

            refToken.IsValid = false;
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Id == refToken.UserId);

            var tokens = new TokensResponseDto
            {
                Access = _TokenService.GenerateAccessToken(user),
                Refresh = _TokenService.GenerateRefreshToken()
            };

            await _DbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokens.Refresh,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsValid = true,
                UserId = user.Id,
                User = user
            });
            await _DbContext.SaveChangesAsync();

            return tokens;
        }

        public async Task<bool> LogoutUser(string token)
        {
            var refToken = await _DbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

            if (refToken == null ||
                refToken.ExpiresAt < DateTime.UtcNow ||
                !refToken.IsValid) 
                return false;

            refToken.IsValid = false;
            await _DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<UserDto?> GetUser(Guid id)
        {
            var user = await _DbContext.Users
                .Include(u => u.Subjects)
                    .ThenInclude(s => s.Tasks)
                        .ThenInclude(tk => tk.AssignedTasks)
                .Include(u => u.Teams)
                    .ThenInclude(tm => tm.Members)
                        .ThenInclude(m => m.AssignedTasks)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Subjects = u.Subjects.Select(s => new SubjectDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        TeamId = s.TeamId,
                        Tasks = s.Tasks.Select(t => new TaskDto
                        {
                            Id = t.Id,
                            Title = t.Title,
                            Description = t.Description,
                            CreateTime = t.CreateTime,
                            DeadLine = t.DeadLine,
                            LeadTime = t.LeadTime,
                            Status = t.Status.ToString(),
                            PosX = t.PosX,
                            PosY = t.PosY,
                            SuperTaskId = t.SuperTaskId,
                            SubTasks = t.SubTasks.Select(st => st.Id).ToList(),
                            AssignedTasks = t.AssignedTasks.Select(at => at.MemberId).ToList()
                        }).ToList()
                    }).ToList(),
                    Teams = u.Teams.Select(tm => new TeamDto
                    {
                        Id = tm.Id,
                        Name= tm.Name,
                        Subjects = u.Subjects.Select(s => s.Id).ToList(),
                        Members = tm.Members.Select(m => new MemberDto
                        {
                            Id = m.Id,
                            Name = m.Name,
                            Surname = m.Surname,
                            Email = m.Email,
                            Specialization = m.Specialization,
                            AssignedTasks = m.AssignedTasks.Select(at => at.TaskId).ToList(),
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<bool> ChangeUser(UserDto data)
        {
            var user = await _DbContext.Users
                .Include(u => u.Subjects)
                    .ThenInclude(s => s.Tasks)
                        .ThenInclude(tk => tk.AssignedTasks)
                .Include(u => u.Teams)
                    .ThenInclude(tm => tm.Members)
                        .ThenInclude(m => m.AssignedTasks)
                .FirstOrDefaultAsync(u => u.Id == data.Id);
            if (user == null)
                return false;

            _DbContext.Subjects.RemoveRange(user.Subjects);
            _DbContext.Teams.RemoveRange(user.Teams);
            user.Teams = new List<Team>();
            user.Subjects = new List<Subject>();
            await _DbContext.SaveChangesAsync();

            var teams = DtoToData_Teams(data.Teams, user);
            var subjects = DtoToData_Subjects(data.Subjects, teams, user);
            var assignedTasks = DtoToData_AssignedTasks(data.Teams, teams, subjects);

            user.Teams = teams;
            await _DbContext.Teams.AddRangeAsync(teams);

            user.Subjects = subjects;
            await _DbContext.Subjects.AddRangeAsync(subjects);

            await _DbContext.AssignedTasks.AddRangeAsync(assignedTasks);
            await _DbContext.SaveChangesAsync();

            return true;
        }

        private List<Team> DtoToData_Teams(List<TeamDto> teamsDto, User user)
        {
            var teams = teamsDto.Select(tm => new Team
            {
                Id = tm.Id,
                Name = tm.Name,
                UserId = user.Id,
                User = user,
                Subjects = new List<Subject>(),
                Members = new List<Member>()
            }).ToList();

            for (int i = 0; i < teamsDto.Count; i++)
            {
                teams[i].Members.AddRange(teamsDto[i].Members.Select(m => new Member
                {
                    Id = m.Id,
                    Name = m.Name,
                    Surname = m.Surname,
                    Email = m.Email,
                    Specialization = m.Specialization,
                    TeamId = teams[i].Id,
                    Team = teams[i],
                    AssignedTasks = new List<AssignedTask>()
                }));
            }

            return teams;
        }

        private List<Subject> DtoToData_Subjects(List<SubjectDto> subjectsDto, List<Team> teams, User user)
        {
            var subjects = subjectsDto.Select(s => new Subject
            {
                Id = s.Id,
                Name = s.Name,
                TeamId = s.TeamId,
                Team = teams.FirstOrDefault(t => t.Id == s.TeamId),
                UserId = user.Id,
                User = user,
                Tasks = new List<Models.Data.Task>()
            }).ToList();

            for (int i = 0; i < subjectsDto.Count; i++)
            {
                subjects[i].Tasks.AddRange(subjectsDto[i].Tasks.Select(t => new Models.Data.Task
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    CreateTime = t.CreateTime,
                    DeadLine = t.DeadLine,
                    LeadTime = t.LeadTime,
                    Status = t.Status.ToLower() == "done" ?
                        Status.Done :
                        t.Status.ToLower() == "inprocess" ?
                            Status.InProcess :
                            Status.DontStarted,
                    PosX = t.PosX,
                    PosY = t.PosY,
                    SuperTaskId = t.SuperTaskId,
                    SuperTask = null,
                    SubTasks = new List<Models.Data.Task>(),
                    AssignedTasks = new List<AssignedTask>()
                }));

                for (int j = 0; j < subjects[i].Tasks.Count; j++)
                {
                    var task = subjects[i].Tasks[j];

                    if (task.SuperTaskId != null)
                    {
                        task.SuperTask = subjects[i].Tasks.First(t => t.Id == task.SuperTaskId);
                        task.SuperTask.SubTasks.Add(task);
                    }
                }
            }

            return subjects;
        }
        
        private List<AssignedTask> DtoToData_AssignedTasks(List<TeamDto> teamsDto, List<Team> teams, List<Subject> subjects)
        {
            var assignedTasks = new List<AssignedTask>();

            for (int i = 0; i < teamsDto.Count; i++)
                for (int j = 0; j < teamsDto[i].Members.Count; j++)
                {
                    var memberDto = teamsDto[i].Members[j];

                    assignedTasks.AddRange(memberDto.AssignedTasks.Select(at => new AssignedTask
                    {
                        MemberId = memberDto.Id,
                        Member = teams[i].Members.First(m => m.Id == memberDto.Id),
                        TaskId = at,
                        Task = subjects[i].Tasks.First(tk => tk.Id == at)
                    }));
                }

            return assignedTasks;
        }
    }
}
