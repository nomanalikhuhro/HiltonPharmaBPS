using PASSForm_BPS.Models;
using System.Dynamic;

namespace PASSForm_BPS.ViewModel
{
    public class BPSDetailsViewModel
    {

        public List<BpsRequest> detailbps { get; set; }
        public List<Macrobrick> macrobricks { get; set; }
        public List<Distributer> distributers { get; set; }

        public List<Hspreqteam> hspreqteams { get; set; }

        public List<Hcprequest> requesthcp { get; set; }
        public List<Tblterritorymapping> tblterritorymappings  { get; set; }
        public List<Team> teams  { get; set; }
        public List<User> users  { get; set; }

        public List<MacChemMapping> macChemMappings { get; set; }
        public List<BpsSalesrecord> BpsSalesrecordss { get; set; }


        public Dictionary<string, List<BpsSalesrecord>> BpsSalesrecords { get; set; }

        public List<string> ChemistCodes { get; set; }

        public List<CustomModel_PreDateRange> preDateRanges { get; set; }
        public List<CustomModel_PostDateRange> postDateRanges { get; set; }

        public List<CustomModel_Customers> customersmodels { get; set; }
        public List<CustomModel_Product> productmodels { get; set; }

        public List<Chemist> chemists { get; set; }
        public List<Uploadedfile> uploadedfiles { get; set; }


        public Dictionary<string, List<ExpandoObject>> SalesValuesPre { get; set; }
        public Dictionary<string, List<ExpandoObject>> SalesValuesPost { get; set; }
        public Dictionary<string, List<ExpandoObject>> SalesUnitsPre { get; set; }
        public Dictionary<string, List<ExpandoObject>> SalesUnitsPost { get; set; }

    }

}
