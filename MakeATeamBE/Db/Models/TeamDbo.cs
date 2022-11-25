using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakeATeamBE.Db.Models
{
    public class TeamDbo
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<(int Id, string Name)> Players { get; set; }
        public int AdminId { get; set; }
    }
}
