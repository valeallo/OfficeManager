using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Portals
{
    internal class TranslationPortal
    {
        private static TranslationPortal _instance;
        TranslationProvider _provider;
        private TranslationPortal()
        {
           _provider = InitializeSample();
        }

        public static TranslationPortal Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TranslationPortal();
                }
                return _instance;
            }
        }

        public TranslationProvider GetService()
        {
            return _provider;
        }
        public TranslationProvider InitializeSample()
        {


            TranslationProvider provider = new TranslationProvider("Traduzioni dall'italiano");
            Translation tedesco = new Translation("Tedesco", 30);
            Translation francese = new Translation("Francese", 20);
            Translation arabo = new Translation("Arabo", 50);

            provider.AddTranslation(tedesco);
            provider.AddTranslation(francese);
            provider.AddTranslation(arabo);


            return provider;
        }
    }
}
