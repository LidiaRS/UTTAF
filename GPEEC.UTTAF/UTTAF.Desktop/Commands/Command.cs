using System;
using System.Windows.Input;

namespace UTTAF.Desktop.Commands
{
	public class Command : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public Command(Action action) : this(action, null)
		{
		}

		public Command(Action action, Func<bool> canExecute)
		{
			if (action is null)
				throw new ArgumentNullException("action is null");

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
			_canExecute == null || _canExecute();

		public void Execute(object parameter) =>
			_execute();
	}
}