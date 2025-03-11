using VBEngine.Models;
using System.Text.Json;
namespace VBEngine.Services
{
    public partial class ProviderServices
    {
        public List<Provider> GetProvidersByServices(string requestServicesJson)
        {
            using (var dbContext = new DrustEngineContext())
            {
                // Deserialize JSON to list of service names
                var requestedServiceNames = JsonSerializer.Deserialize<List<string>>(requestServicesJson);

                if (requestedServiceNames == null || requestedServiceNames.Count == 0)
                    return new List<Provider>();

                // Get service IDs matching requested service names
                var requestedServiceIds = dbContext.Services
                    .Where(s => requestedServiceNames.Contains(s.ServiceName))
                    .Select(s => s.ServiceId)
                    .ToList();
                if (requestedServiceIds.Count == 0)
                    return new List<Provider>();
                // Get providers offering at least one of the requested services
                var providers = dbContext.Providers
                    .Where(p => dbContext.ProviderServices
                        .Any(ps => ps.ProviderId == p.ProviderId && requestedServiceIds.Contains(ps.ServiceId)))
                    .ToList();

                return providers;
            }
        }


    }
}
