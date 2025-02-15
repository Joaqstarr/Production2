using AI.Drone.States;

namespace AI.Drone
{
    public class DroneStateMachineManager : BaseState
    {
        #region States

        private SearchState _searchState;

        #endregion
        public DroneStateMachineManager(DroneBrain brain) : base(brain)
        {
            _searchState = new SearchState(brain, this);
            SwitchToSearchState();

        }



        public void SwitchToSearchState()
        {
            SwitchState(_searchState);
        }
    }
}