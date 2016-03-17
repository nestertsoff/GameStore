using GameStore.Web;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace GameStore.Web
{
    using System;
    using System.Web;

    using FluentValidation;
    using FluentValidation.Mvc;

    using GameStore.BLL.Infrastructure;
    using GameStore.IoC.Modules;
    using GameStore.Web.Areas.Common.Models;
    using GameStore.Web.Areas.Common.Validators;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;

    using NLog;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var modules = new INinjectModule[] { new InfrastructureModule("GameStoreDb", "NorthwindDb"), new ApplicationModule() };
            var kernel = new StandardKernel(modules);
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ILogger>()
                .ToMethod(context => LogManager.GetLogger(context.Request.ParentContext.Request.Service.FullName));

            kernel.Bind<IValidator<GameViewModel>>().To<GameViewModelValidator>().InRequestScope();
            kernel.Bind<IValidator<CommentViewModel>>().To<CommentViewModelValidator>().InRequestScope();

            FluentValidationModelValidatorProvider.Configure(
                provider => { provider.ValidatorFactory = new NinjectValidatorFactory(kernel); });
        }
    }
}