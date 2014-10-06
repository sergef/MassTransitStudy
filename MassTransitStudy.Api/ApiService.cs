namespace MassTransitStudy.Api
{
    using System;
    using System.Web.Http;

    using MassTransitStudy.Api.MessageStore;
    using MassTransitStudy.Messages.Properties;

    using Microsoft.Owin.Hosting;

    using Owin;

    using Topshelf;

    public class ApiService : ServiceControl
    {
        public HttpConfiguration WebApiConfiguration { get; set; }

        public IMessageStoreRepository MessageStore { get; set; }

        protected IDisposable WebApplication;

        public bool Start(HostControl hostControl)
        {
            this.MessageStore.CreateSampleMessageSchemaIfNotExists();

            this.WebApplication = WebApp.Start(
                Settings.Default.ApiServiceBaseAddress,
                builder => builder.UseWebApi(this.WebApiConfiguration));

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            WebApplication.Dispose();
            return true;
        }
    }
}
