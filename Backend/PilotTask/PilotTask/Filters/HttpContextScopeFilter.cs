using MassTransit;

namespace PilotTask.Filters
{
    public class HttpContextScopeFilter : IFilter<PublishContext>, IFilter<SendContext>, IFilter<ConsumeContext>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HttpContextScopeFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Probe(ProbeContext context)
        {
        }

        public Task Send(PublishContext context, IPipe<PublishContext> next)
        {
            AddPayload(context);
            return next.Send(context);
        }

        public Task Send(SendContext context, IPipe<SendContext> next)
        {
            AddPayload(context);
            return next.Send(context);
        }

        public Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
        {
            AddPayload(context);
            return next.Send(context);
        }

        private void AddPayload(PipeContext context)
        {
            if (this.httpContextAccessor.HttpContext == null)
            {
                return;
            }

            var serviceProvider = this.httpContextAccessor.HttpContext.RequestServices;
            context.GetOrAddPayload(() => serviceProvider);
            context.GetOrAddPayload<IServiceScope>(() => new NoopScope(serviceProvider));
        }

        private class NoopScope : IServiceScope
        {
            public NoopScope(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }
            
            public void Dispose()
            {

            }

            public IServiceProvider ServiceProvider { get; }

        }
    }
}
