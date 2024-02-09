using Application.Interfaces.Security;
using Domain.Repositories;
using Domain.User;
using Domain.User.Group;
using Microsoft.AspNetCore.Identity;
using Open.Linq.AsyncExtensions;

namespace Infrastructure.Security
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<AppUser> _userManager;
        public GroupService(IUnitOfWork unitOfWork, IUserAccessor userAccessor, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userAccessor = userAccessor;
            _userManager = userManager;
        }

        public async Task<bool> UserCanAccessATMAsync(string activeATM)
        {

            AppUser userLogged = await GetCurrentUser();
            var groups = await GetUserGroups(userLogged);
            List<GroupATM> userATMs = await GetUserGroupsATMs(groups);

            return userATMs.Any(x => x.ATM.Identifier == activeATM);
        }

        public async Task<AppUser> GetCurrentUser()
        {
            var userName = _userAccessor.GetUsername();
            AppUser userLogged = await _userManager.FindByNameAsync(userName);
            return userLogged;
        }

        public async Task<List<Group>> GetUserGroups(AppUser userLogged)
        {
            List<Group> groups = new();
            List<int> groupIds = new();
            var userGroupListing = await _unitOfWork.GroupUsers.GetAllAsync()
                .Where(x => x.UserId == userLogged.Id).ToList();

            foreach (GroupUsers usrGrp in userGroupListing)
            {
                var group = await _unitOfWork.Group.GetGroupByIdAsync(usrGrp.GroupId);
                if (group != null)
                {
                    groups.Add(group);
                }
            }

            return groups;

        }

        public async Task<List<GroupATM>> GetUserGroupsATMs(List<Group> groups)
        {
            List<GroupATM> groupATMs = new();

            foreach (var group in groups)
            {

                var theGroupATMs = await _unitOfWork.GroupATM.GetGroupATMsAsync(group.Id).ToList();
                foreach (GroupATM theGroupATM in theGroupATMs)
                {
                    groupATMs.Add(theGroupATM);
                }
            }

            return groupATMs;
        }
    }
}
