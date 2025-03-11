using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using VBEngine.Models;

namespace VBEngine.Services
{
    public class RequestService
    {

        public (bool Success, string ErrorMessage) SendRequest(CreateRequestDto cRequest)
        {
            try
            {
                var request = new Request
                {
                    RequestId = Guid.NewGuid(),
                    RequesterId = cRequest.RequesterId,
                    RequsetDetail = JsonConvert.SerializeObject(cRequest.RequestDetail), // Ensure it's JSON
                    CreateDate = DateTime.UtcNow,
                    RequestDate = cRequest.RequestedDate ?? DateTime.UtcNow,
                    RequsetServices = JsonConvert.SerializeObject(cRequest.RequestServices), // Ensure it's JSON
                    RequsetStatus = 1
                };

                using (var dbContext = new DrustEngineContext())
                {
                    dbContext.Requests.Add(request);
                    dbContext.SaveChanges();
                }

                BroadCastRequest(request);
                return (true, string.Empty); ;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return (false, $"Database Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return (false, $"Error: {ex.Message}");
            }
        }

        private void BroadCastRequest(Request request)
        {
            // First, deserialize the string as a JSON array
            var requestedServicesJson = JsonConvert.DeserializeObject<string>(request.RequsetServices);

            // Then, deserialize the JSON array string into a List<string>
            var requestedServices = JsonConvert.DeserializeObject<List<string>>(requestedServicesJson);

            if (requestedServices != null)
            {
                var providers = GetProvidersForServices(requestedServices);
                foreach (var provider in providers)
                {
                    SendNotificationToProvider(provider, request);
                }
            }
        }


        // Method to fetch providers based on requested services
        private List<Provider> GetProvidersForServices(IEnumerable<string> requestedServices)
        {
            List<Provider> providers = new ProviderServices().GetProvidersByServices(JsonConvert.SerializeObject(requestedServices));
            return providers;
        }

        // Method to send a notification to a provider
        private void SendNotificationToProvider(Provider provider, Request request)
        {
            // Logic to notify the provider, e.g., via email, SMS, or API call
            Console.WriteLine($"Notified Provider {provider.FullName} about Request {request.RequestId}");
        }

        public List<Request> GetAllRequests()
        {
            var emptyGuid = new Guid();
            return new List<Request>
            {
                new Request()
                {
                    RequestId = Guid.NewGuid(),
                    RequesterId = 1,
                    RequsetServices = "Gull1, Cacke1",
                    RequsetDetail = "Gully zard, Gully sawz, keky bithday",

                },
                new Request()
                {
                    RequestId = Guid.NewGuid(),
                    RequesterId = 2,
                    RequsetServices = "Gull2, Cacke2",
                    RequsetDetail = "Gully zard, Gully sawz, keky bithday",

                },
                new Request()
                {
                    RequestId = Guid.NewGuid(),
                    RequesterId = 3,
                    RequsetServices = "Gull3, Cacke3",
                    RequsetDetail = "Gully zard, Gully sawz, keky bithday",

                }
            };

        }
    }

}
