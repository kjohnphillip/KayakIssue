using System;
using System.Diagnostics;
using System.Net;
using Gate.Kayak;
using Kayak;
using System.Collections.Generic;
using Gate;

namespace Hosting
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var kayakDelegate = new DefaultKayakDelegate();
			var scheduler = KayakScheduler.Factory.Create(kayakDelegate);
			var remainingArguments = new string[]{};
			scheduler.Post(() => kayakDelegate.OnStart(scheduler, remainingArguments));
			
			var listenEndPoint = new IPEndPoint(IPAddress.Any, 8080);
			var context = new Dictionary<string, object>()
					{
					    { "kayak.ListenEndPoint", listenEndPoint },
					    { "kayak.Arguments", remainingArguments }
					};

			var server = KayakServer.Factory.CreateGate(Startup.application, scheduler, context);
			
			scheduler.Post(() =>
			{
			    Console.WriteLine("kayak: binding gate app to '" + listenEndPoint + "'");
			    server.Listen(listenEndPoint);
			});
			
			// blocks until Stop is called 
			scheduler.Start();
		}
	}
}
