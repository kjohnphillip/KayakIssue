using System;
using System.Collections.Generic;
using System.Text;
using Gate;
using Gate.Utils;

namespace Hosting
{
	using BodyAction = Func<Func<ArraySegment<byte>, Action, bool>, Action<Exception>, Action, Action>;
	
	public class Startup
	{
		public static readonly AppDelegate application = (env, result, fault) => 
		{
			if ((string)env["owin.RequestMethod"] == "GET")
			{
				result(
					"200 OK",
					new Dictionary<string, string>
					{
						{ "Content-Type", "text/plain;charset=utf-8" },
					},
					(next, error, complete) =>
					{
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes("GET")), null);
						complete();
						return () => { };
					}
				);
			}
			else if ((string)env["owin.RequestMethod"] == "POST")
			{
				result(
					"200 OK",
					new Dictionary<string, string>
					{
						{ "Content-Type", "text/plain;charset=utf-8" },
					},
					(next, error, complete) =>
					{
						var requestBody = (BodyAction)env["owin.RequestBody"];
						var requestBodyData = requestBody.ToText(Encoding.UTF8);
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes("POST")), null);
						next(new ArraySegment<byte>(Encoding.UTF8.GetBytes(requestBodyData)), null);
						complete();
						return () => { };
					}
				);
			}
			else
			{
				result(
					"404 Not Found",
					new Dictionary<string, string>
					{
					},
					(next, error, complete) =>
					{
						complete();
						return () => { };
					}
				);
			}
		};
		
		public void Configuration(IAppBuilder builder)
		{
			builder.Run(application);
		}
	}
}