using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Modules;

namespace Asiana.Shopping.Payments.Paypal
{
    public class PayPalServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPayPalService>().To<PayPalService>();
        }

    }
}
