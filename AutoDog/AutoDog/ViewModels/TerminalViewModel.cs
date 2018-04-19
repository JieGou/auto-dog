using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apex.MVVM;
using AutoDog.Commands;

namespace AutoDog.ViewModels
{
    public class TerminalViewModel : ViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TerminalViewModel"/> class.
        /// </summary>
        public TerminalViewModel()
        {
            StartCommandPromptCommand = new Command(() => { });
            StartNewProcessCommand = new Command(() => { });
            StopProcessCommand = new Command(() => { });
            ClearOutputCommand = new Command(() => { });
        }

        private NotifyingProperty ProcessStateProperty =
          new NotifyingProperty("ProcessState", typeof(string), default(string));

        /// <summary>
        /// Gets or sets the state of the process.
        /// </summary>
        /// <value>
        /// The state of the process.
        /// </value>
        public string ProcessState
        {
            get { return (string)GetValue(ProcessStateProperty); }
            set { SetValue(ProcessStateProperty, value); }
        }

        /// <summary>
        /// Gets the start command prompt command.
        /// </summary>
        public Command StartCommandPromptCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the start new process command.
        /// </summary>
        /// <value>
        /// The start new process command.
        /// </value>
        public Command StartNewProcessCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the stop process command.
        /// </summary>
        /// <value>
        /// The stop process command.
        /// </value>
        public Command StopProcessCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the clear output command.
        /// </summary>
        public Command ClearOutputCommand
        {
            get;
            private set;
        }
    }
}
