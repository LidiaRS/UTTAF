﻿using System.Threading.Tasks;
using System.Windows.Threading;

namespace UTTAF.Desktop.Services
{
	public interface IStartSessionService
	{
		Task StartSessionAsync(DispatcherTimer timer);
	}
}