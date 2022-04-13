using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DoverTechExercise
{
    /// <summary>
    /// Service to handle the heater in a microwave oven
    /// </summary>
    public class HeaterService
    {
        // HeaterOn
        public bool HeaterOn => _timer.Enabled;

        /// <summary>
        /// Base heater time (1 min) in miliseconds
        /// </summary>
        private static readonly int HeaterTimeInMiliseconds = 60000;

        /// <summary>
        /// Remaining heater time in miliseconds
        /// </summary>
        private int _remainingSeconds = 0;

        /// <summary>
        /// Timer for the heater service
        /// </summary>
        private Timer _timer = new Timer(1000);

        /// <summary>
        /// Initialize heater service with a timer
        /// </summary>
        public HeaterService()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += TimerElapsedEvent;
            _timer.AutoReset = true;
        }

        /// <summary>
        /// Turns on the Microwave heater element
        /// </summary>
        private void TurnOnHeater()
        {
            if (_remainingSeconds == 0)
            {
                _remainingSeconds = HeaterTimeInMiliseconds;
                _timer.Start();

                Console.WriteLine("Heater on.");
            }
            else
            {
                _remainingSeconds += HeaterTimeInMiliseconds;
                Console.WriteLine("1 minute added to the timer.");
            }
        }

        /// <summary>
        /// Turns off the Microwave heater element
        /// </summary>
        private void TurnOffHeater()
        {
            _timer.Stop();
            _remainingSeconds = 0;

            Console.WriteLine("PING! Heater off.");
        }

        /// <summary>
        /// Event for when the timer interval is elapsed
        /// </summary>
        /// <param name="source">Publisher object (timer)</param>
        private void TimerElapsedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine($"{_remainingSeconds}ms remaining.");
            if (_remainingSeconds > 0)
                _remainingSeconds -= 1000;
            else
                TurnOffHeater();
        }


        /// <summary>
        /// Subscriber method for StarButtonPressed event in IMicrowaveOvenHW 
        /// </summary>
        /// <param name="source">Publisher object (IMicrowaveHW)</param>
        public void OnStartButtonPressed(object source, EventArgs e)
            => TurnOnHeater();


        /// <summary>
        /// Subscriber method for DoorOpenChanged event in IMicrowaveOvenHW 
        /// </summary>
        /// /// <param name="doorOpen">Receives DoorOpen boolean from IMicrowaveOvenHW</param>
        public void OnDoorOpenChanged(bool doorOpen)
        {
            if (doorOpen && HeaterOn)
                TurnOffHeater();
        }
    }
}
