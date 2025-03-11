using VBEngine.Models;

namespace VBEngine.SLA
{
    public class SLAService
    {
        public SLA CreateSLA(Guid offerId)
        {
            return new SLA
            {
                //RequestId = RequestEngine.GetRequestIdByOffer(offerId),
                //ProviderId = RequestEngine.GetProviderIdByOffer(offerId),
                //RequesterId = RequestEngine.GetRequesterIdByOffer(offerId),
                //EndDate = DateTime.Now,
                //Clauses = null
            };
        }

        public void Monitoring(List<Offer> offers)
        {

        }
    }
}
