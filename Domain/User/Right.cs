using Domain.Enums;
using Domain.User.Group;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    /// <summary>
    /// Application wide token for access to a specific service
    /// </summary>
    public class Right
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public string Feature { get; set; } = null!; 
        public ERights Type { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = new DateTime();
        public ICollection<GroupRight> GroupRights { get; set; } = new List<GroupRight>();

    }
}