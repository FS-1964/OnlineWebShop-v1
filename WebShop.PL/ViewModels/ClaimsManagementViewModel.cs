using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebShop.PL.ViewModels
{
    public class ClaimsManagementViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ClaimId { get; set; }
        public string ClaimTyp { get; set; }
        public List<string> AllClaimsList { get; set; }
        public bool ClaimOp { get; set; }//Add=true , delete = false
        public IList<Claim> UserClaims { get; set; }
    }
}
