using System;
using Kayak;

namespace Hosting
{
	interface IKayakDelegate : ISchedulerDelegate
	{
		void OnStart(IScheduler scheduler, string[] args);
	}
}

