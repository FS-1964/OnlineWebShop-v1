using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebShop.PL.Util
{
    public static class ConnectionState
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static async Task<bool> IsConnectedToInternet()
        {
            int Desc;

            return await Task.Run(() =>
              {
                  return InternetGetConnectedState(out Desc, 0);
              });
        }
    }
}
