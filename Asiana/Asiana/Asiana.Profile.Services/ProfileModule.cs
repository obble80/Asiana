using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using Ninject;
using Asiana.Profile.Services.Reviews;
using Norm;
namespace Asiana.Profile.Services
{
    public class ProfileModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReviewService>().To<ReviewService>();
        }
    }
}
