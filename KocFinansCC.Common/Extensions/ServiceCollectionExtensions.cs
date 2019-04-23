namespace KocFinansCC.Common.Extensions
{
    using System;
    using Config;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Services;
    using Services.Abstract;

    public static class ServiceCollectionExtensions
    {
        public static void AddCreditScoreService(this IServiceCollection services, Action<ServiceSettings> setupAction = null)
        {
            var settings = new ServiceSettings();
            setupAction?.Invoke(settings);

            if (!string.IsNullOrWhiteSpace(settings.CreditScoreServiceUrl))
            {
                var creditScoreService = new CreditScoreService(settings.CreditScoreServiceUrl);
                services.TryAddSingleton<ICreditScoreService>(creditScoreService);
            }
        }

        public static void AddSMSService(this IServiceCollection services, Action<ServiceSettings> setupAction = null)
        {
            var settings = new ServiceSettings();
            setupAction?.Invoke(settings);

            if (!string.IsNullOrWhiteSpace(settings.SMSSenderServiceUrl))
            {
                var smsSenderService = new SMSService(settings.SMSSenderServiceUrl);
                services.TryAddSingleton<ISMSService>(smsSenderService);
            }
        }
    }
}
