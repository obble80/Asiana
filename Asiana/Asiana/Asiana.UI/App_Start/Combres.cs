[assembly: WebActivator.PreApplicationStartMethod(typeof(Asiana.UI.App_Start.CombresHook), "PreStart")]
namespace Asiana.UI.App_Start {
	using System.Web.Routing;
	using Combres;
	
    public static class CombresHook {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}