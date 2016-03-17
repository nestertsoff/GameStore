namespace GameStore.BLL.Infrastructure
{
    using System;
    using System.Linq;

    using FluentValidation;

    using Ninject;

    public class NinjectValidatorFactory : ValidatorFactoryBase
    {
        private readonly IKernel kernel;

        public NinjectValidatorFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (!kernel.GetBindings(validatorType).Any())
            {
                return null;
            }

            return kernel.Get(validatorType) as IValidator;
        }
    }
}