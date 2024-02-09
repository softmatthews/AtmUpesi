using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.User.Group
{
    /// <summary>
    /// Groups of users are used to track assignments to ATMs, news and authorization
    /// </summary>
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; } = true;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public string? LastModifiedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public ICollection<GroupUsers> GroupUsers { get; set; } = new List<GroupUsers>();
        public ICollection<GroupATM> GroupATMs { get; set; } = new List<GroupATM>();
        public ICollection<GroupRight> GroupClaims { get; set; } = new List<GroupRight>();
        
    }

}