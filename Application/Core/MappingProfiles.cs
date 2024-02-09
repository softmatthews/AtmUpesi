using Application.Core.Notifications;
using Domain.Settings;
using Domain.User;
using Domain.User.Group;
using Application.Features.User;


namespace Application.Core
{
    /// <summary>
    /// MappingProfiles class for a definition of transformations between DTOs and Domain entities
    /// </summary>
    public class MappingProfiles : AutoMapper.Profile
    {
        public MappingProfiles()
        {
            #region Settings

            //CreateMap<License.LicenseInfo, AccessDto>()
            //    .ForMember(d => d.Packages, o => o.MapFrom(s => s.AllowedPackages))
            //    .ForMember(d => d.Features, o => o.MapFrom(s => s.AllowedFeatures))
            //    .ForMember(d => d.Features, o => o.MapFrom(s => s.AllowedFeatures));

            //CreateMap<License.LicenseInfo, Application.Features.Frontend.AccessDto>()
            //   .ForMember(d => d.Packages, o => o.MapFrom(s => s.AllowedPackages))
            //   .ForMember(d => d.Features, o => o.MapFrom(s => s.AllowedFeatures))
            //   .ForMember(d => d.Features, o => o.MapFrom(s => s.AllowedFeatures));

            // Modules
            
            //CreateMap<ModuleSetting, ModuleSettingDto>()
            //    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            //    .ForMember(d => d.SettingType, o => o.MapFrom(s => s.SettingType.ToString()))
            //    .ForMember(d => d.HelperText, o => o.MapFrom(s => s.HelperText))
            //    .ForMember(d => d.ModifiedAt, o => o.MapFrom(s => s.ModifiedAt))
            //    .ForMember(d => d.ModuleId, o => o.MapFrom(s => s.ModuleId))
            //    .ForMember(d => d.Key, o => o.MapFrom(s => s.Key))
            //    .ForMember(d => d.Value, o => o.MapFrom(s => s.Value))
            //    .ForMember(d => d.Choices, o => o.MapFrom(s => s.Choices));

            //CreateMap<Module, ModuleDto>()
            //    .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            //    .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt))
            //    .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            //    .ForMember(d => d.ModuleSettings, o => o.MapFrom(s => s.ModuleSettings));

            #endregion

            #region Transactions
            //CreateMap<TransactionMessage, Transaction>()
            //    .ForMember(d => d.TransactionId, o => o.MapFrom(s => s.TransactionID))
            //    .ForMember(d => d.TransactionType, o => o.MapFrom(s => s.TransactionType))
            //    .ForMember(d => d.ProcessDate, o => o.MapFrom(s => s.ProcessDate))
            //    .ForMember(d => d.TrafficType, o => o.MapFrom(s => s.TrafficType))
            //    .ForMember(d => d.Group, o => o.MapFrom(s => s.Group));
           
            
            #endregion
                     
            #region User

            // Groups
            CreateMap<Group, GroupDto>()
                 .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt))
                 .ForMember(d => d.LastModifiedDate, o => o.MapFrom(s => s.LastModifiedDate))
                 .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                 .ForMember(d => d.UsersCount, o => o.MapFrom(s => s.GroupUsers.Count()))
                 .ForMember(d => d.Status, o => o.MapFrom(s => s.Status));

           
            // Rights
            CreateMap<Right, RightsDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => s.CreatedAt))
                .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description));

            // Rights
            //CreateMap<GroupRight, RightsDto>()
            //    .ForMember(d => d.Right, o => o.MapFrom(s => s.Right))
            //    .ForMember(d => d.Group, o => o.MapFrom(s => s.Group));
            //   // .ForMember(d => d.Description, o => o.MapFrom(s => s.Description));

            #endregion

                       
        }
    }
}