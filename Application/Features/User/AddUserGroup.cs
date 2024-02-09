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
using Domain.User.Group;

namespace Application.Features.User
{
    public class AddUserGroup
    {
        public class Command : IRequest<Result<Unit>>
        {
            public List<UsersListDto> DtoUsersList { get; set; }

            public Command(List<UsersListDto> dto)
            {
                DtoUsersList = dto;
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly UserManager<AppUser> _userManager;


            public Handler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
            {
                _unitOfWork = unitOfWork;
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                List<ValidationFailure> failures = new();

                try
                {

                    if (request.DtoUsersList.Count < 1)
                    {
                        failures.Add(new ValidationFailure("Setting", $"Error, No user group settings added."));

                        return Result<Unit>.Failure(string.Join("\n", failures.Select(f => f.ErrorMessage).ToList()));
                    }

                    foreach (UsersListDto usergroupdto in request.DtoUsersList)
                    {
                        var user = _userManager.Users.Where(x => x.Id == usergroupdto.Id).FirstOrDefault();


                        if (user == null)
                        {
                            failures.Add(new ValidationFailure("Setting", $"Error, No user found with the details"));
                            return Result<Unit>.Failure(string.Join("\n", failures.Select(f => f.ErrorMessage).ToList()));
                        }

                        if (usergroupdto.Groups.Count > 0)
                        {
                            foreach (var group in usergroupdto.Groups)
                            {
                                var usergroup = await _unitOfWork.Group.GetGroupByIdAsync(group.Id);
                                if (usergroup != null)
                                {
                                    GroupUsers usrgroup = new()
                                    {
                                        User = user,
                                        Group = usergroup
                                    };
                                    await _unitOfWork.GroupUsers.AddAsync(usrgroup);
                                }
                            }

                        }

                    }

                    //var success = await _unitOfWork.Complete();
                }
                catch (Exception ex)
                {
                    failures.Add(new ValidationFailure("Setting", $"Error '{1}' and is discarded - {ex.Message}"));
                }

                if (failures.Count == 0)
                {
                    return Result<Unit>.Success(Unit.Value);
                }
                else
                {
                    return Result<Unit>.Failure(string.Join("\n", failures.Select(f => f.ErrorMessage).ToList()));
                }
            }
        }

    }

}