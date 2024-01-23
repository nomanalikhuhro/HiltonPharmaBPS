using PASSForm_BPS.Models;


namespace PASSForm_BPS.ViewModel
{
    public class BpsRecordsViewModel
    {
        public Hcprequest hcprequests { get; set; }
        public List<UserChemMapping> userChemMappings { get; set; }

        public List<string> Month { get; set; }

       public string ChemistPanelId { get; set; }


    }
}
