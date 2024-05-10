using PASSForm_BPS.Models;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace PASSForm_BPS.ViewModel
{
    public class BPSRequestListViewModel
    {

        public string ChemistName { get; set; }
        public int WFID { get; set; }

        public List<string> ChemistCodes { get; set; }
        public List<MacChemMapping> macChemMappings { get; set; }
        public List<Hcprequest>  requesthcp { get; set; }

        public List<TerbrickMapping> terbrickMappings { get; set; }

        public List<DisterMapping> disterMappings { get; set; }

        public List<Hcprequest> hspreqteams { get; set; }

        public List<User> users { get; set; }
        public List<Team> teams { get; set; }

        public List<tblproduct> tblproducts { get; set; }

        public List<BPSrequestpap> BPSrequestpaps { get; set; }

        public List<Bpssalesrecordpap> Bpssalesrecordpaps { get; set; }

        public List<BPSrequestpapIvInjection> BPSrequestpapIvInjections { get; set; }

        public List<BpssalesrecordpapIvInjection> bpssalesrecordpapIvInjections { get; set; }

        public List<Distributer> Distributers { get; set; }

        public List<Macrobrick> Macrobricks { get; set; }

        public List<Chemist> chemists { get; set; }

        public List<BPSIvInjectionViewViewModel> BPSIvInjectionViewViewModels { get; set; }


        public Dictionary<string, List<ExpandoObject>> SalesPAPDataPharmacies { get; set; }





    }
}
