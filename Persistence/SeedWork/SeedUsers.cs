using Domain.User;
using Domain.User.Group;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace Persistence.SeedWork
{
    public class SeedUsers
    {
        public static async Task Seed(DataContext context,
            UserManager<AppUser> userManager)
        {
            // Users
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new() {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com",
                        Bio = "The Bobman"
                    },
                    new() {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                        Bio = "The Janelady"
                    },
                    new() {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com",
                        Bio = "The Tomcat"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                await context.SaveChangesAsync();
            }
            
            // Groups
            await AddGroupsAsync(context);
            await AddSystemAdminGroupAndUserAsync(context, userManager);

            // Claims
            //await AddEMAILERCN00ClaimsAsync(context);
            //await AddSPOOLERCN00ClaimsAsync(context);
            //await AddMXCN00ClaimsAsync(context);
            //await AddMTCN00ClaimsAsync(context);
            //await AddSETTINGSMXCN00ClaimsAsync(context);
            //await AddSETTINGSMTCN00ClaimsAsync(context);
            //await AddSETTINGSSYSTEMCN00ClaimsAsync(context);

            await context.SaveChangesAsync();

        }

        #region APPLICATION LEVEL
        private static async Task AddSystemAdminGroupAndUserAsync(DataContext context, UserManager<AppUser> userManager)
        {
            var adminGroup = context.Group.Where(m => m.Name == "SYSTEM ADMIN").FirstOrDefault();

            if (adminGroup == null)
            {
                adminGroup = new Group()
                {
                    Name = "SYSTEM ADMIN",
                    Status = true,
                    LastModifiedBy = "System Seed",
                    GroupUsers = new GroupUsers[] { }
                };
                await context.Group.AddAsync(adminGroup);
            }

            var adminUserGroups = context.UserGroups.Where(m => m.Group == adminGroup).ToList();

            var users = new List<AppUser>
                {
                    new() {
                        DisplayName = "Aaron",
                        UserName = "aaron",
                        Email = "aaron@test.com",
                        Bio = "The tester Aaron",
                    }
                };

            foreach (var user in users)
            {
                if (!await userManager.Users.AnyAsync(x => x.UserName == user.UserName))
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");

                    GroupUsers usrgroup = new()
                    {
                        User = user,
                        Group = adminGroup
                    };
                    await context.UserGroups.AddAsync(usrgroup);
                }
            }

            await AddAdminUserClaimsAsync(adminGroup, context, userManager);
        }

        private static async Task AddAdminUserClaimsAsync(Group group, DataContext context, UserManager<AppUser> userManager)
        {
            // For System admin group of users assign all claim rights to them
            var allSystemClaims = context.Right.ToList();
            foreach (var systemClaims in allSystemClaims)
            {
                GroupRight groupClaim = new();
                var claim = context.GroupRight.Where(x => x.Right == systemClaims && x.Group == group).ToList();
                if (claim == null || claim.Count < 1)
                {
                    groupClaim.Right = systemClaims;
                    groupClaim.Group = group;
                    await context.GroupRight.AddAsync(groupClaim);
                }
            }

        }

        private static async Task AddGroupsAsync(DataContext context)
        {

            var adminGroup = context.Group.Where(m => m.Name == "ADMINISTRATOR").FirstOrDefault();
            if (adminGroup == null)
            {
                adminGroup = new Group()
                {
                    Name = "ADMINISTRATOR",
                    Status = true,
                    LastModifiedBy = "System Seed",
                    GroupUsers = new GroupUsers[] { }
                };
                await context.Group.AddAsync(adminGroup);
            }

        }

        #endregion

        #region LICENSE LEVEL
        private static async Task AddEMAILERCN00ClaimsAsync(DataContext context)
        {
            var rightsClaim = context.Right.Where(m => m.Name == "CREATE EMAILER" && m.Feature == "EMAILER-CN00" && m.Type == ERights.CREATE).FirstOrDefault();
            if (rightsClaim == null)
            {
                rightsClaim = new Right()
                {
                    PackageName = "EMAILER",
                    Feature = "EMAILER-CN00",
                    Name = "CREATE EMAILER",
                    Type = ERights.CREATE,
                    Description = "Create emails settings"
                };
                await context.Right.AddAsync(rightsClaim);
            }

            var rightsClaimupdate = context.Right.Where(m => m.Name == "UPDATE EMAILER" && m.Feature == "EMAILER-CN00" && m.Type == ERights.UPDATE).FirstOrDefault();
            if (rightsClaimupdate == null)
            {
                rightsClaimupdate = new Right()
                {
                    PackageName = "EMAILER",
                    Feature = "EMAILER-CN00",
                    Name = "UPDATE EMAILER",
                    Type = ERights.UPDATE,
                    Description = "Update email settings"
                };
                await context.Right.AddAsync(rightsClaimupdate);
            }

            var rightsClaimManage = context.Right.Where(m => m.Name == "MANAGE EMAILER" && m.Feature == "EMAILER-CN00" && m.Type == ERights.MANAGE).FirstOrDefault();
            if (rightsClaimManage == null)
            {
                rightsClaimManage = new Right()
                {
                    PackageName = "EMAILER",
                    Feature = "EMAILER-CN00",
                    Name = "MANAGE EMAILER",
                    Type = ERights.MANAGE,
                    Description = "Manage and control emailer module"
                };
                await context.Right.AddAsync(rightsClaimManage);
            }
        }
        private static async Task AddSPOOLERCN00ClaimsAsync(DataContext context)
        {

            var spoolerRightsClaim = context.Right.Where(m => m.Name == "MANAGE SPOOLER" && m.Feature == "SPOOLER-CN00" && m.Type == ERights.MANAGE).FirstOrDefault();
            if (spoolerRightsClaim == null)
            {
                spoolerRightsClaim = new Right()
                {
                    PackageName = "SPOOLER",
                    Feature = "SPOOLER-CN00",
                    Name = "MANAGE SPOOLER",
                    Type = ERights.MANAGE,
                    Description = "Manage and control spooler module"
                };
                await context.Right.AddAsync(spoolerRightsClaim);
            }

            var rightsClaimCreate = context.Right.Where(m => m.Name == "CREATE SPOOLER" && m.Feature == "SPOOLER-CN00" && m.Type == ERights.CREATE).FirstOrDefault();
            if (rightsClaimCreate == null)
            {
                rightsClaimCreate = new Right()
                {
                    PackageName = "SPOOLER",
                    Feature = "SPOOLER-CN00",
                    Name = "CREATE SPOOLER",
                    Type = ERights.CREATE,
                    Description = "Create spooler settings"
                };
                await context.Right.AddAsync(rightsClaimCreate);
            }
            var rightsClaimRead = context.Right.Where(m => m.Name == "READ SPOOLER" && m.Feature == "SPOOLER-CN00" && m.Type == ERights.READ).FirstOrDefault();
            if (rightsClaimRead == null)
            {
                rightsClaimRead = new Right()
                {
                    PackageName = "SPOOLER",
                    Feature = "SPOOLER-CN00",
                    Name = "READ SPOOLER",
                    Type = ERights.READ,
                    Description = "Read spooler settings"
                };
                await context.Right.AddAsync(rightsClaimRead);
            }

            var rightsClaimupdate = context.Right.Where(m => m.Name == "UPDATE SPOOLER" && m.Feature == "SPOOLER-CN00" && m.Type == ERights.UPDATE).FirstOrDefault();
            if (rightsClaimupdate == null)
            {
                rightsClaimupdate = new Right()
                {
                    PackageName = "SPOOLER",
                    Feature = "SPOOLER-CN00",
                    Name = "UPDATE SPOOLER",
                    Type = ERights.UPDATE,
                    Description = "Update spooler settings"
                };
                await context.Right.AddAsync(rightsClaimupdate);
            }
            var rightsClaimDelete = context.Right.Where(m => m.Name == "DELETE SPOOLER" && m.Feature == "SPOOLER-CN00" && m.Type == ERights.DELETE).FirstOrDefault();
            if (rightsClaimDelete == null)
            {
                rightsClaimDelete = new Right()
                {
                    PackageName = "SPOOLER",
                    Feature = "SPOOLER-CN00",
                    Name = "DELETE SPOOLER",
                    Type = ERights.DELETE,
                    Description = "Delete spooler settings"
                };
                await context.Right.AddAsync(rightsClaimDelete);
            }
        }
        private static async Task AddMXCN00ClaimsAsync(DataContext context)
        {
            var transactionCreateRightsClaim = context.Right.Where(m => m.Name == "CREATE TRANSACTIONS" && m.Feature == "MX-CN00" && m.Type == ERights.CREATE).FirstOrDefault();
            if (transactionCreateRightsClaim == null)
            {
                transactionCreateRightsClaim = new Right()
                {
                    PackageName = "MX",
                    Feature = "MX-CN00",
                    Name = "CREATE TRANSACTIONS",
                    Type = ERights.CREATE,
                    Description = "Upload MX Transactions for processing"
                };
                await context.Right.AddAsync(transactionCreateRightsClaim);
            }
            var transactionViewRightsClaim = context.Right.Where(m => m.Name == "READ TRANSACTIONS" && m.Feature == "MX-CN00" && m.Type == ERights.READ).FirstOrDefault();
            if (transactionViewRightsClaim == null)
            {
                transactionViewRightsClaim = new Right()
                {
                    PackageName = "MX",
                    Feature = "MX-CN00",
                    Name = "READ TRANSACTIONS",
                    Type = ERights.READ,
                    Description = "Read and view MX Transactions"
                };
                await context.Right.AddAsync(transactionViewRightsClaim);
            }

            var transactionUpdateRightsClaim = context.Right.Where(m => m.Name == "UPDATE TRANSACTIONS" && m.Feature == "MX-CN00" && m.Type == ERights.UPDATE).FirstOrDefault();
            if (transactionUpdateRightsClaim == null)
            {
                transactionUpdateRightsClaim = new Right()
                {
                    PackageName = "MX",
                    Feature = "MX-CN00",
                    Name = "UPDATE TRANSACTIONS",
                    Type = ERights.UPDATE,
                    Description = "UPDATE MX Transactions"
                };
                await context.Right.AddAsync(transactionUpdateRightsClaim);
            }

            var transactionDELETERightsClaim = context.Right.Where(m => m.Name == "DELETE TRANSACTIONS" && m.Feature == "MX-CN00" && m.Type == ERights.DELETE).FirstOrDefault();
            if (transactionDELETERightsClaim == null)
            {
                transactionDELETERightsClaim = new Right()
                {
                    PackageName = "MX",
                    Feature = "MX-CN00",
                    Name = "DELETE TRANSACTIONS",
                    Type = ERights.DELETE,
                    Description = "DELETE MX Transactions"
                };
                await context.Right.AddAsync(transactionDELETERightsClaim);
            }
        }

        private static async Task AddMTCN00ClaimsAsync(DataContext context)
        {
            var transactionCreateRightsClaim = context.Right.Where(m => m.Name == "CREATE TRANSACTIONS" && m.Feature == "MT-CN00" && m.Type == ERights.CREATE).FirstOrDefault();
            if (transactionCreateRightsClaim == null)
            {
                transactionCreateRightsClaim = new Right()
                {
                    PackageName = "MT",
                    Feature = "MT-CN00",
                    Name = "CREATE TRANSACTIONS",
                    Type = ERights.CREATE,
                    Description = "Upload MX Transactions for processing"
                };
                await context.Right.AddAsync(transactionCreateRightsClaim);
            }
            var transactionViewRightsClaim = context.Right.Where(m => m.Name == "READ TRANSACTIONS" && m.Feature == "MT-CN00" && m.Type == ERights.READ).FirstOrDefault();
            if (transactionViewRightsClaim == null)
            {
                transactionViewRightsClaim = new Right()
                {
                    PackageName = "MT",
                    Feature = "MT-CN00",
                    Name = "READ TRANSACTIONS",
                    Type = ERights.READ,
                    Description = "Read and view MX Transactions"
                };
                await context.Right.AddAsync(transactionViewRightsClaim);
            }

            var transactionUpdateRightsClaim = context.Right.Where(m => m.Name == "UPDATE TRANSACTIONS" && m.Feature == "MT-CN00" && m.Type == ERights.UPDATE).FirstOrDefault();
            if (transactionUpdateRightsClaim == null)
            {
                transactionUpdateRightsClaim = new Right()
                {
                    PackageName = "MT",
                    Feature = "MT-CN00",
                    Name = "UPDATE TRANSACTIONS",
                    Type = ERights.UPDATE,
                    Description = "UPDATE MX Transactions"
                };
                await context.Right.AddAsync(transactionUpdateRightsClaim);
            }

            var transactionDELETERightsClaim = context.Right.Where(m => m.Name == "DELETE TRANSACTIONS" && m.Feature == "MT-CN00" && m.Type == ERights.DELETE).FirstOrDefault();
            if (transactionDELETERightsClaim == null)
            {
                transactionDELETERightsClaim = new Right()
                {
                    PackageName = "MT",
                    Feature = "MT-CN00",
                    Name = "DELETE TRANSACTIONS",
                    Type = ERights.DELETE,
                    Description = "DELETE MX Transactions"
                };
                await context.Right.AddAsync(transactionDELETERightsClaim);
            }

        }
        private static async Task AddSETTINGSMTCN00ClaimsAsync(DataContext context)
        {
            var transactionCreateRightsClaim = context.Right.Where(m => m.Name == "CREATE SETTINGS" && m.Feature == "SETTINGSMT-CN00" && m.Type == ERights.CREATE).FirstOrDefault();
            if (transactionCreateRightsClaim == null)
            {
                transactionCreateRightsClaim = new Right()
                {
                    PackageName = "MT SETTINGS",
                    Feature = "SETTINGSMT-CN00",
                    Name = "CREATE SETTINGS",
                    Type = ERights.CREATE,
                    Description = "Create MT settings"
                };
                await context.Right.AddAsync(transactionCreateRightsClaim);
            }
            var transactionViewRightsClaim = context.Right.Where(m => m.Name == "READ SETTINGS" && m.Feature == "SETTINGSMT-CN00" && m.Type == ERights.READ).FirstOrDefault();
            if (transactionViewRightsClaim == null)
            {
                transactionViewRightsClaim = new Right()
                {
                    PackageName = "MT SETTINGS",
                    Feature = "SETTINGSMT-CN00",
                    Name = "READ SETTINGS",
                    Type = ERights.READ,
                    Description = "Read and view MT settings"
                };
                await context.Right.AddAsync(transactionViewRightsClaim);
            }

            var settingsUpdateRightsClaim = context.Right.Where(m => m.Name == "UPDATE SETTINGS" && m.Feature == "SETTINGSMT-CN00" && m.Type == ERights.UPDATE).FirstOrDefault();
            if (settingsUpdateRightsClaim == null)
            {
                settingsUpdateRightsClaim = new Right()
                {
                    PackageName = "MT SETTINGS",
                    Feature = "SETTINGSMT-CN00",
                    Name = "UPDATE SETTINGS",
                    Type = ERights.UPDATE,
                    Description = "UPDATE MT SETTINGS"
                };
                await context.Right.AddAsync(settingsUpdateRightsClaim);
            }

            var transactionDELETERightsClaim = context.Right.Where(m => m.Name == "DELETE SETTINGS" && m.Feature == "SETTINGSMT-CN00" && m.Type == ERights.DELETE).FirstOrDefault();
            if (transactionDELETERightsClaim == null)
            {
                transactionDELETERightsClaim = new Right()
                {
                    PackageName = "MT SETTINGS",
                    Feature = "SETTINGSMT-CN00",
                    Name = "DELETE SETTINGS",
                    Type = ERights.DELETE,
                    Description = "DELETE MT SETTINGS"
                };
                await context.Right.AddAsync(transactionDELETERightsClaim);
            }

        }
        private static async Task AddSETTINGSMXCN00ClaimsAsync(DataContext context)
        {
            var transactionCreateRightsClaim = context.Right.Where(m => m.Name == "CREATE SETTINGS" && m.Feature == "SETTINGSMX-CN00" && m.Type == ERights.CREATE).FirstOrDefault();
            if (transactionCreateRightsClaim == null)
            {
                transactionCreateRightsClaim = new Right()
                {
                    PackageName = "MX SETTINGS",
                    Feature = "SETTINGSMX-CN00",
                    Name = "CREATE SETTINGS",
                    Type = ERights.CREATE,
                    Description = "Create MX settings"
                };
                await context.Right.AddAsync(transactionCreateRightsClaim);
            }
            var transactionViewRightsClaim = context.Right.Where(m => m.Name == "READ SETTINGS" && m.Feature == "SETTINGSMX-CN00" && m.Type == ERights.READ).FirstOrDefault();
            if (transactionViewRightsClaim == null)
            {
                transactionViewRightsClaim = new Right()
                {
                    PackageName = "MX SETTINGS",
                    Feature = "SETTINGSMX-CN00",
                    Name = "READ SETTINGS",
                    Type = ERights.READ,
                    Description = "Read and view MX settings"
                };
                await context.Right.AddAsync(transactionViewRightsClaim);
            }

            var settingsUpdateRightsClaim = context.Right.Where(m => m.Name == "UPDATE SETTINGS" && m.Feature == "SETTINGSMX-CN00" && m.Type == ERights.UPDATE).FirstOrDefault();
            if (settingsUpdateRightsClaim == null)
            {
                settingsUpdateRightsClaim = new Right()
                {
                    PackageName = "MX SETTINGS",
                    Feature = "SETTINGSMX-CN00",
                    Name = "UPDATE SETTINGS",
                    Type = ERights.UPDATE,
                    Description = "UPDATE MX SETTINGS"
                };
                await context.Right.AddAsync(settingsUpdateRightsClaim);
            }

            var transactionDELETERightsClaim = context.Right.Where(m => m.Name == "DELETE SETTINGS" && m.Feature == "SETTINGSMX-CN00" && m.Type == ERights.DELETE).FirstOrDefault();
            if (transactionDELETERightsClaim == null)
            {
                transactionDELETERightsClaim = new Right()
                {
                    PackageName = "MX SETTINGS",
                    Feature = "SETTINGSMX-CN00",
                    Name = "DELETE SETTINGS",
                    Type = ERights.DELETE,
                    Description = "DELETE MX SETTINGS"
                };
                await context.Right.AddAsync(transactionDELETERightsClaim);
            }

        }
        private static async Task AddSETTINGSSYSTEMCN00ClaimsAsync(DataContext context)
        {
            var settingsRightsClaim = context.Right.Where(m => m.Name == "MANAGE SETTINGS" && m.Feature == "SETTINGSSYSTEM-CN00" && m.Type == ERights.MANAGE).FirstOrDefault();
            if (settingsRightsClaim == null)
            {
                settingsRightsClaim = new Right()
                {
                    PackageName = "SETTINGS",
                    Feature = "SETTINGSSYSTEM-CN00",
                    Name = "MANAGE SETTINGS",
                    Type = ERights.MANAGE,
                    Description = "Manage and control system settings module"
                };
                await context.Right.AddAsync(settingsRightsClaim);
            }

            // var customersRightsClaim = context.RightsClaim.Where(m => m.Name == "MANAGE USERS" && m.Type == RightTypes.MANAGE).FirstOrDefault();
            // if (customersRightsClaim == null)
            // {
            //     customersRightsClaim = new RightsClaim()
            //     {
            //         PackageName="SETTINGS",
            //         Feature="SETTINGSSYSTEM-CN00",
            //         Name = "MANAGE USERS",
            //         Type = RightTypes.MANAGE,
            //         Description = "Manage and set system users"
            //     };
            //     await context.RightsClaim.AddAsync(customersRightsClaim);
            // }           

            var usersRightsClaim = context.Right.Where(m => m.Name == "MANAGE USERS" && m.Feature == "SETTINGSSYSTEM-CN00" && m.Type == ERights.MANAGE).FirstOrDefault();
            if (usersRightsClaim == null)
            {
                usersRightsClaim = new Right()
                {
                    PackageName = "SETTINGS",
                    Feature = "SETTINGSSYSTEM-CN00",
                    Name = "MANAGE USERS",
                    Type = ERights.MANAGE,
                    Description = "Manage and control system Users"
                };
                await context.Right.AddAsync(usersRightsClaim);
            }

            var transactionRightsClaim = context.Right.Where(m => m.Name == "MANAGE TRANSACTIONS" && m.Feature == "SETTINGSSYSTEM-CN00" && m.Type == ERights.MANAGE).FirstOrDefault();
            if (transactionRightsClaim == null)
            {
                transactionRightsClaim = new Right()
                {
                    PackageName = "TRANSACTIONS",
                    Feature = "SETTINGSSYSTEM-CN00",
                    Name = "MANAGE TRANSACTIONS",
                    Type = ERights.MANAGE,
                    Description = "Manage and control Transactions module"
                };
                await context.Right.AddAsync(transactionRightsClaim);
            }

        }
        #endregion


    }
}
