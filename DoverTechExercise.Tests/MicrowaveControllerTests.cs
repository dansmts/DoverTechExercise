using System;
using Xunit;

namespace DoverTechExercise.Tests
{
    public class MicrowaveControllerTests
    {
        private MicrowaveController _microwave = new MicrowaveController();
        private HeaterService _heaterService = new HeaterService();

        public MicrowaveControllerTests()
        {
            _microwave.StartButtonPressed += _heaterService.OnStartButtonPressed;
            _microwave.DoorOpenChanged += _heaterService.OnDoorOpenChanged;
        }

        [Fact]
        public void PressDoorOpen_DoorOpen_LightOn()
        {
            _microwave.PressDoorOpen();
            Assert.True(_microwave.DoorOpen && _microwave.LightOn);
        }

        [Fact]
        public void PressDoorOpen_DoorClosed_LightOff()
        {
            Assert.True(!_microwave.DoorOpen && !_microwave.LightOn);
        }

        [Fact]
        public void PressDoorOpen_WhenChanged_HeaterTurnsOff()
        {
            _microwave.PressStart();
            _microwave.PressDoorOpen();

            Assert.False(_heaterService.HeaterOn);
        }

        [Fact]
        public void PressStart_WhenDoorClosed_HeaterTurnsOn()
        {
            _microwave.PressStart();
            Assert.True(_heaterService.HeaterOn);
        }

        [Fact]
        public void PressStart_WhenDoorClosedAndHeaterOn_TimeAddedToHeater()
        {
            _microwave.PressStart();
            _microwave.PressStart();
            Assert.True(_heaterService.HeaterOn);
        }

        [Fact]
        public void PressStart_WhenDoorOpen_NothingHappens()
        {
            _microwave.PressDoorOpen();
            _microwave.PressStart();
            
            Assert.False(_heaterService.HeaterOn);
        }
    }
}
