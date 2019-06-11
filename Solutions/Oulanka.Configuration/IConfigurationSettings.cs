using Oulanka.Configuration.Models;

namespace Oulanka.Configuration
{
    public interface IConfigurationSettings
    {

        BaseConfigurationSection GetConfig();
        RouteTableSection GetRouteTable();

        ProviderTableSection GetProviderTable();

    }
}
