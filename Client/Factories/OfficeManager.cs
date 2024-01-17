using Client.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    internal class OfficeManager
    {
        private ServiceType _serviceType;

        public OfficeManager(ServiceType serviceType)
        {
            _serviceType = serviceType;
        }
        public object CreateService()
        {
            switch (_serviceType)
            {
                case ServiceType.RestaurantService:
                    var currentTime = DateTime.Now;
                    return new RestaurantFactory(currentTime).GetRestaurant();

                //case ServiceType.TranslationService:  
                //    string language = "italian"; 
                //    return new TranslationServiceFactory(language);

                default:
                    throw new InvalidOperationException("Invalid service type.");
            }
        }
    }
}
