using System.ComponentModel.DataAnnotations;

namespace MakeATeamBE.Db.Models
{
    public class UserDbo
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
