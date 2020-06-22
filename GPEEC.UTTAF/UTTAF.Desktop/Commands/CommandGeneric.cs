using System;
using System.Windows.Input;

namespace UTTAF.Desktop.Commands
{
	public class Command<T> : ICommand
	{
		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		public Command(Action<T> action) : this(action, null)
		{
		}

		public Command(Action<T> action, Func<T, bool> canExecute)
		{
			if (action is null)
				throw new NullReferenceException("action is null");

			_execute = action;

			if (canExecute != null)
				_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter) =>
			_canExecute == null || _canExecute((T)parameter);

		public void Execute(object parameter) =>
			_execute((T)parameter);
	}
}