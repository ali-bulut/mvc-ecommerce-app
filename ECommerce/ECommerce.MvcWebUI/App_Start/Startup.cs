using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ECommerce.MvcWebUI.App_Start.Startup))]

namespace ECommerce.MvcWebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType="ApplicationCookie",
                //kullanıcı sınırladığımız bir sayfaya gitmeye çalışırsa kullanıcıyı göndereceğimiz sayfa
                LoginPath=new PathString("/Account/Login")

            });
        }
    }
}
