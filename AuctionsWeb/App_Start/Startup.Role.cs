
using AuctionsWeb.App_Start;
using System.Threading.Tasks;

namespace AuctionsWeb
{
    public partial class Startup
    {
        public async Task ConfigureRole()
        {
            RoleConfig rolesData = new RoleConfig();
            await rolesData.EnsureSeedDataAsync();
        }
    }
}