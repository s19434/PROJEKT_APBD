using PROJEKT_APBD.DTOs.Request;
using PROJEKT_APBD.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Services
{
    public interface ICampaignAdvertsService
    {
        public ClientRegistrationResponse ClientRegistration(ClientRegistrationRequest req);
        public RenewTokenResponse RenewToken(RenewTokenRequest req);
        public LoginResponse LoginClient(LoginRequest req);
        public ICollection<GetListOfCampaignsResponse> GetListOfCampaigns();
        public NewCampaignResponse NewCampaign(NewCampaignRequest req);
    }
}
