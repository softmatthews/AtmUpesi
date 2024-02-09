using System;
using System.Collections;
using System.Xml;
using System.Xml.Linq;
using Application.Core;
using Application.Interfaces;
using Domain.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Application.Interfaces.Security;

namespace Application.Features.User
{
    public class GetGroupUsers
    {
        public class Query : IRequest<Result<IEnumerable<UsersListDto>>>
        {
            public int? GroupId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<IEnumerable<UsersListDto>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserManager<AppUser> _userManager;


            public Handler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
            {
                _unitOfWork = unitOfWork;
                _userManager = userManager;
            }

            public async Task<Result<IEnumerable<UsersListDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<ValidationFailure> failures = new();

                List<UsersListDto> users = new();

                try
                {

                    var userGroups = await _unitOfWork.GroupUsers.GetUserGroupsByGroupIdAsync(request.GroupId);

                    foreach (var usergroup in userGroups)
                    {
                        var usr = _userManager.Users.Where(x => x.Id == usergroup.UserId).FirstOrDefault();
                        if (usr != null)
                        {
                            UsersListDto user = new()
                            {
                                DisplayName = usr.DisplayName,
                                UserName = usr.UserName,
                                Email = usr.Email,
                                Id = usr.Id
                            };

                            users.Add(user);
                        }
                    }

                }
                catch (Exception ex)
                {
                    failures.Add(new ValidationFailure("Setting", $"Error '{1}' and is discarded - {ex.Message}"));
                }


                return Result<IEnumerable<UsersListDto>>.Success(users);

            }



        }

    }

}