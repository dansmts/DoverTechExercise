using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace DoverTechExercise
{
    /// <summary>
    /// Implementation of the microwave oven hardware
    /// </summary>
    public class MicrowaveController : IMicrowaveOvenHW
    {
        // Requirements:

        // A heater – Can be turned on or off
        //  Since the heater cannot be turned on or off by the user, the methods are removed from the
        //  interface as they can only be implemented as public methods. A heater service is used to
        //  handle the heater of the microwave.

        // A door – Can be opened and closed by user
        //  The door is handled by a public method that raises the event to open or close the door.
        //  The heater service is used as a subscriber to to handle turning on or off the heater.

        // A Start Button – Can be pressed by the user
        //  The start button is handled by a public method that raises an event when the start button
        //  is pressed. The heater service is used as a subscriber to handle turning on or off the heater.

        // Door
        private bool _doorOpen = false;
        public bool DoorOpen => _doorOpen;

        //Light
        /// <summary>
        /// Indicates if the light in the Microwave oven is on or off
        /// </summary>
        public bool LightOn => _doorOpen;

        // Events
        public event Action<bool> DoorOpenChanged;
        public event EventHandler StartButtonPressed;


        // public void TurnOffHeater() { }
        // public void TurnOnHeater() { }

        /// <summary>
        /// User interaction for pressing the start button on the microwave
        /// </summary>
        public void PressStart()
        {
            Console.WriteLine("Start button pressed.");

            if (DoorOpen) return;

            OnStartButtonPressed();
        }

        /// <summary>
        /// User interaction for opening or closing the microwave door
        /// </summary>
        public void PressDoorOpen()
        {
            _doorOpen = !_doorOpen;

            Console.WriteLine(_doorOpen ? "Door opened." : "Door closed.");

            OnDoorOpenChanged();
        }

        /// <summary>
        /// Publisher event for when the start button is pressed
        /// </summary>
        protected virtual void OnStartButtonPressed()
        { 
            if (StartButtonPressed != null)
                StartButtonPressed(this, EventArgs.Empty);
        }

        /// <summary>
        /// Publisher event for when the door is opened or closed
        /// </summary>
        protected virtual void OnDoorOpenChanged()
        {
            if (DoorOpenChanged != null)
                DoorOpenChanged(DoorOpen);
        }
    }
}
