using System;
using System.Diagnostics;
using System.Net;
using Gate.Kayak;
using Kayak;

namespace Hosting
{
	class MainClass
	{
		public static void Main(string[] args)
		{
#if DEBUG
			Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
			Debug.AutoFlush = true;
#endif
			using (var scheduler = new KayakScheduler(new SchedulerDelegate()))
			using (var server = KayakServer.Factory.CreateGate("Hosting.Startup.Configuration", scheduler))
			using (server.Listen(new IPEndPoint(IPAddress.Any, 8080)))
			{
				scheduler.Start();
			}
		}
	}
}
