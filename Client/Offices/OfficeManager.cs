using Client.enums;
using Client.Portals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Offices
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
                  
                    return new DeliveryOffice();

                case ServiceType.TranslationService:
                    return new TranslationOffice();

                default:
                    throw new InvalidOperationException("Invalid service type.");
            }
        }
    }
}
