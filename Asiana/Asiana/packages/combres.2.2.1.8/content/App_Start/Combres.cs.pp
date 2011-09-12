[assembly: WebActivator.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.CombresHook), "PreStart")]
namespace $rootnamespace$.App_Start {
	using System.Web.Routing;
	using Combres;
	
    public static class CombresHook {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}