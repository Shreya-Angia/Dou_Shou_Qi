using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stubs
{
    public class TeamMember
    {
        public string? Image { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public static TeamMember Shreya { get; } = new TeamMember
        {
            Name = "Shreya",
            Description = "Head of management & Software Developer",
            Image = "profil_shreya.jpg" 
        };

        public static TeamMember Anae { get; } = new TeamMember
        {
            Name = "Anaé",
            Description = "Graphic ressources Manager & Software Developer",
            Image = "profil_anae.jpg" 
        };

        public static TeamMember Gregoire { get; } = new TeamMember 
        {
            Name = "Grégoire",
            Description = "Administrative Manager & Software Developer",
            Image = "profil_gregoire.jpg"
        };

        public static TeamMember Enzo { get; } = new TeamMember
        {
            Name = "Enzo",
            Description = "Project Manager & Software Developer",
            Image = "profil_enzo.jpg"
        };
    }
}
