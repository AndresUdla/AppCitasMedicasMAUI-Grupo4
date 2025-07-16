using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCitasMedicasMAUI.Models
{
    public static class ApiConstants
    {
        public static string BaseUrl
        {
            get
            {
#if ANDROID
            return "http://10.0.2.2:5222/";
#elif WINDOWS
            return "http://localhost:5222/";
#else
                return "http://localhost:5222/";
#endif
            }
        }
    }

}

