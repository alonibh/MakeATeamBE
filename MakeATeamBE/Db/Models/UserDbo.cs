using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeATeamBE.Db.Models
{
    public class UserDbo
    {
        // TODO change to int/long
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<int> Teams { get; set; }
    }
}
