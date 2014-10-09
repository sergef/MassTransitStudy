namespace MassTransitStudy.Api
{
    using System;
    using System.Web.Http;

    using MassTransitStudy.Api.MessageStore;
    using MassTransitStudy.Api.Properties;

    using Microsoft.Owin.Hosting;

    using Owin;

    using Topshelf;

    public class ApiService : ServiceControl
    {
        private IDisposable webApplication;

        public HttpConfiguration WebApiConfiguration { get; set; }

        public IMessageStoreRepository MessageStore { get; set; }

        public bool Start(HostControl hostControl)
        {
            this.MessageStore.CreateSampleMessageSchemaIfNotExists();

            this.webApplication = WebApp.Start(
                Settings.Default.ServiceBaseAddress,
                builder => builder.UseWebApi(this.WebApiConfiguration));

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            this.webApplication.Dispose();

            return true;
        }
    }
}
