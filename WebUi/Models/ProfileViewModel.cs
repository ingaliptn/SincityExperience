using Domain.Entities;
using System.Collections.Generic;

namespace WebUi.Models
{
    public class ProfileViewModel : Escort
    {
        public bool IsVideo { get; set; }
        public string VideoFile { get; set; }
        public List<Escort> List { get; set; } = new List<Escort>();
    }
}
