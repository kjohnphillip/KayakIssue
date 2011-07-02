using System;
using System.Collections.Generic;
using System.Text;
using Gate;

namespace Hosting
{
	public class Startup
	{
		public static readonly AppDelegate application = (env, result, fault) => 
				result(
					"200 OK",
					new Dictionary<string, string>
					{
						{ "Content-Type", "text/plain;charset=utf-8" },
					},
					(next, error, complete) =>
					{
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Test1\n")), null);
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Test2\n")), null);
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Test3\n")), null);
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Test4\n")), null);
						complete();
						return () => { };
					}
				);
		
		public void Configuration(IAppBuilder builder)
		{
			builder.Run(application);
		}
	}
}