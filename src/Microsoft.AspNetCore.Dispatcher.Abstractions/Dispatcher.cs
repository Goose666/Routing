using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Dispatcher
{
    public abstract class EndpointDescriptor
    {
        public abstract string DisplayName { get; }

        public abstract IReadOnlyList<object> Metadata { get; }
    }

    public abstract class AddressDescriptor
    {
        public abstract string DisplayName { get; }
    }

    public abstract class Dispatcher
    {
        public abstract Task Invoke(HttpContext context, IDispatcherFeature dispatcherFeature);
    }

    public interface IDispatcherFeature
    {
        EndpointDescriptor Endpoint { get; set; }

        RequestDelegate RequestDelegate { get; set; }
    }

    public abstract class DispatcherContext : IDispatcherFeature
    {
        public abstract HttpContext HttpContext { get; }

        public abstract EndpointDescriptor Endpoint { get; set; }

        public abstract RequestDelegate RequestDelegate { get; set; }
    }

    public interface IDispatcherEndpointRegistry
    {

    }



    public abstract class AddressTable
    {
    }

    public class EndpointDescriptorCollection : ReadOnlyCollection<EndpointDescriptor>
    {
        public EndpointDescriptorCollection(IList<EndpointDescriptor> endpoints)
            : base(endpoints)
        {
        }
    }
}
