using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    internal class TranslationPortal
    {
        string _baseLanguage;
        public TranslationPortal() 
        {
    
        
        
        }


        public TranslationProvider CreateTranslationService()
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
