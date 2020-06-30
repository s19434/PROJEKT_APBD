using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PROJEKT_APBD.DTOs.Request;
using PROJEKT_APBD.DTOs.Response;
using PROJEKT_APBD.Exceptions;
using PROJEKT_APBD.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PROJEKT_APBD.Services
{
    public class CampaignAdvertsService : ICampaignAdvertsService
    {
        private readonly CampaignAdvertsDbContext _context;
        private readonly IConfiguration _config;

        public CampaignAdvertsService(IConfiguration config, CampaignAdvertsDbContext context)
        {
            _config = config;
            _context = context;
        }

        public ClientRegistrationResponse ClientRegistration(ClientRegistrationRequest req)
        {
            if (_context.Clients.Where(p => p.Login.Equals(req.Login))
                .FirstOrDefault() != null)
            {
                throw new LoginException($"Client with login={req.Login} has already exists");
            }

            var hashed = BCrypt.Net.BCrypt.HashPassword(req.Password, 13);

            Client client = new Client
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Email = req.Email,
                Phone = req.Phone,
                Login = req.Login,
                Password = hashed
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            ClientRegistrationResponse resp = new ClientRegistrationResponse
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Login = client.Login
            };
            return resp;
        }

        public RenewTokenResponse RenewToken(RenewTokenRequest req)
        {
            var client = _context.Clients.Where
                (p => p.RefreshToken.Equals(req.RefreshToken))
                .FirstOrDefault();

            if (client == null)
            {
                throw new TokenException("Refresh token incorrect");
            }

            var refreshToken = Guid.NewGuid();
            client.RefreshToken = refreshToken.ToString();

            _context.Update(client);

            var claims = new[]
             {
                new Claim(ClaimTypes.NameIdentifier, client.Login),
                new Claim(ClaimTypes.Name, client.FirstName+" "+client.LastName),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "s19434",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );
            _context.SaveChanges();

            return new RenewTokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            };
        }

        public LoginResponse LoginClient(LoginRequest req)
        {
            var client = _context.Clients.Where(p => p.Login.Equals(req.Login)).SingleOrDefault();
            if (client == null)
            {
                throw new LoginException("Incorrect login");
            }

            if (!BCrypt.Net.BCrypt.Verify(req.Password, client.Password))
            {
                throw new PasswordException("Incorrect password");
            }

            Guid refreshToken = Guid.NewGuid();
            client.RefreshToken = refreshToken.ToString();
            _context.Update(client);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.Login),
                new Claim(ClaimTypes.Name, client.FirstName+" "+client.LastName),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "s19434",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );
            _context.SaveChanges();
            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            };
        }

        public ICollection<GetListOfCampaignsResponse> GetListOfCampaigns()
        {
            List<GetListOfCampaignsResponse> resp = new List<GetListOfCampaignsResponse>();

            var campaigns = _context.Campaigns
                .Join(_context.Clients,
                campaign => campaign.IdClient,
                client => client.IdClient,
                (campaign, client) => new GetListOfCampaignsResponse
                {
                    Campaign = campaign,
                    Client = client
                })
                .OrderByDescending(p => p.Campaign.StartDate)
                .ToList();

            return campaigns;
        }

        public NewCampaignResponse NewCampaign(NewCampaignRequest req)
        {
            if (_context.Buildings.Count() < 2)
            {
                throw new BuildingsException("Count of buildings less than 2 or no buildings in the database");
            }

            var buildingFirst = _context.Buildings.Where(p => p.IdBuilding.Equals(req.FromIdBuilding)).FirstOrDefault();
            var buildingSecond = _context.Buildings.Where(p => p.IdBuilding.Equals(req.ToIdBuilding)).FirstOrDefault();

            if (!buildingFirst.Street.Equals(buildingSecond.Street))
            {
                throw new LocationException("Buildings are not on the same street");
            }

            Campaign campaign = new Campaign
            {
                IdClient = req.IdCampaign,
                StartDate = req.StartDate,
                EndDate = req.EndDate,
                PricePerSquareMeter = req.PricePerSquareMeter,
                FromIdBuilding = req.FromIdBuilding,
                ToIdBuilding = req.ToIdBuilding
            };

            _context.Campaigns.Add(campaign);
            _context.SaveChanges();

            var buildings = _context.Buildings
                .Where(p => p.StreetNumber >= buildingFirst.StreetNumber &&
                p.StreetNumber <= buildingSecond.StreetNumber)
                .OrderBy(p => p.StreetNumber)
                .ToList();



            List<Banner> banners = new List<Banner>
            {
            new Banner
            {
                Name = 123,
                Price = ((buildingFirst.Height * Math.Abs(buildingFirst.StreetNumber - buildingSecond.StreetNumber) * req.PricePerSquareMeter)),
                IdCampaign = campaign.IdCampaign,
                Area = (buildingFirst.Height * Math.Abs(buildingFirst.StreetNumber - buildingSecond.StreetNumber))
            },
            new Banner
            {
                Name = 456,
                Price = ((buildingSecond.Height * Math.Abs(buildingSecond.StreetNumber - buildingFirst.StreetNumber) * req.PricePerSquareMeter)),
                IdCampaign = campaign.IdCampaign,
                Area = (buildingSecond.Height * Math.Abs(buildingSecond.StreetNumber - buildingFirst.StreetNumber))
            }
        };

            _context.Banners.AddRange(banners.First(), banners.Last());
            _context.SaveChanges();

            return new NewCampaignResponse
            {
                Campaign = campaign,
                BannerFirst = banners.First(),
                BannerSecond = banners.Last()
            };
        }
    }
}
