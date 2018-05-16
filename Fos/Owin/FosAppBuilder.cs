using System.Threading;
using Microsoft.Owin.Builder;

namespace Fos.Owin
{
    internal class FosAppBuilder : AppBuilder
	{
		public CancellationToken OnAppDisposing { get; private set; }
        
		public FosAppBuilder(CancellationToken cancelToken)
		{
			//WARN: Non standard Owin header. Used by Nancy
			OnAppDisposing = cancelToken;
			Properties.Add("host.OnAppDisposing", cancelToken);
		}
	}
}
