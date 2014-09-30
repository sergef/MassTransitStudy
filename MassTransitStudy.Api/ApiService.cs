namespace MassTransitStudy.Api
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dependencies;
    using System.Web.Http.Dispatcher;

    using MassTransitStudy.Api.Properties;
    using Microsoft.Owin.Hosting;

    using Owin;

    using Topshelf;

    public class ApiService : ServiceControl
    {
        public HttpConfiguration WebApiConfiguration { get; set; }

        protected IDisposable WebApplication;

        public bool Start(HostControl hostControl)
        {
            this.WebApplication = WebApp.Start(
                Settings.Default.ServiceBaseAddress,
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
