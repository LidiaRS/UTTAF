﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UTTAF.Dependencies.Clients
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void Set<T>(ref T objectReference, T newObjectValue, [CallerMemberName] string property = null)
		{
			objectReference = newObjectValue;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}