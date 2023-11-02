using DATC_Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATC_Core
{
    public static class Notification
    {

        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static bool has_flash()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("Notification") == null)
            {
                return false;
            }
            return true;
        }

        public static void set_flash(String mgs, String mgs_type)
        {
            ModelNotification tb = new ModelNotification();
            tb.mgs = mgs;
            tb.mgs_type = mgs_type;

            _httpContextAccessor.HttpContext.Session.SetString("Notification", JsonConvert.SerializeObject(tb));
        }

        public static ModelNotification get_flash()
        {
            var notificationJson = _httpContextAccessor.HttpContext.Session.GetString("Notification");
            if (notificationJson == null)
            {
                return null;
            }

            _httpContextAccessor.HttpContext.Session.Remove("Notification");

            return JsonConvert.DeserializeObject<ModelNotification>(notificationJson);
        }
    }

}
