using Domain.Enums;
using Domain.User.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.User
{
    using global::Domain.User.Account;


    /*
     * Account DTO
     */
   
        public class AccountDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;            
            public string AccountNumber { get; set; } = null!;
            public double Amount { get; set; }
            public string Currency { get; set; } = null!;
            public bool Status { get; set; } = true;
            public DateTime LastModifiedDate { get; set; } = DateTime.Now;
            public string? LastModifiedBy { get; set; } = null!;
            public DateTime CreatedAt { get; set; } = DateTime.Now;
        }

 
    /**
    * Withdrawal request Dto
    */
    public class WithdrawalDto
    {
        public string AccountNumber { get; set; } = null!;
        public string IdNo { get; set; } = null!;
        public double Amount { get; set; } 
        public string Currency { get; set; } = null!;
    }

    /**
    * Deposit request Dto
    */
     public class DepositDto
    {
        public string AccountNumber { get; set; } = null!;
        public string IdNo { get; set; } = null!;
        public double Amount { get; set; } 
        public string Currency { get; set; } = null!;
    }
    
    /**
     * Users
     */
    public class UsersListDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int? GroupsCount { get; set; }
        public List<GroupDto> Groups { get; set; } = new();
    }

    /**
     * Groups 
     */
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; } = true;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = new DateTime();
        public int UsersCount { get; set; } = 0;
    }
    public class CreateGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; } = true;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = new DateTime();
        public ICollection<RightsDto> Rights { get; set; } = new List<RightsDto>();

    }

    /**
     * Rights
     */
    public class GroupRightDto
    {
        public int Id { get; set; }
        public RightsDto Right { get; set; } = null!;
        public int RightId { get; set; }
        public Group Group { get; set; } = null!;
        public int GroupId { get; set; }
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
    public class RightsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public ERights Type { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = new DateTime();
    }

    /**
     * News
     */
    public class CreateGroupNewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<GroupDto> Groups { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = new DateTime();

    }
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = new DateTime();

    }


}
