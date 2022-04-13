using System;
using System.Threading;

namespace DoverTechExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var microwave = new MicrowaveController();
            var heaterService = new HeaterService();

            microwave.StartButtonPressed += heaterService.OnStartButtonPressed;
            microwave.DoorOpenChanged += heaterService.OnDoorOpenChanged;

            microwave.PressStart();

            Thread.Sleep(3000);
            microwave.PressStart();
            
            Thread.Sleep(6000);
            microwave.PressDoorOpen();
        }
    }
}
