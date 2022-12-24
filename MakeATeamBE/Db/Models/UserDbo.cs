using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeATeamBE.Db.Models
{
    public class UserDbo
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<int> Teams { get; set; }
    }
}
