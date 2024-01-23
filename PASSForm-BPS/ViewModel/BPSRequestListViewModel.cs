using PASSForm_BPS.Models;
using System.ComponentModel.DataAnnotations;

namespace PASSForm_BPS.ViewModel
{
    public class BPSRequestListViewModel
    {

        public string ChemistName { get; set; }
   
        public List<MacChemMapping> macChemMappings { get; set; }
        public List<Hcprequest>  requesthcp { get; set; }

        public List<TerbrickMapping> terbrickMappings { get; set; }

        public List<DisterMapping> disterMappings { get; set; }

        public List<Hcprequest> hspreqteams { get; set; }

        public List<User> users { get; set; }
        public List<Team> teams { get; set; }







    }
}
