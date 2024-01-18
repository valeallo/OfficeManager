using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Factories
{
    internal class TranslationOffice
    {
        TranslationPortal _translationPortal;
        public TranslationOffice() 
        {

            _translationPortal = new TranslationPortal();
        }
    }
}
