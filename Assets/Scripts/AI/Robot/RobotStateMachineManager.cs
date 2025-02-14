using AI.Robot.States;

namespace AI.Robot
{
    public class RobotStateMachineManager : BaseState
    {
        #region States

        private BaseState IdleState;
        private BaseState FollowState;
        

        #endregion
        
        public RobotStateMachineManager(RobotBrain robotBrain) : base(robotBrain)
        {
            IdleState = new IdleState(robotBrain, this);
            FollowState = new FollowState(robotBrain, this);
            
            SwitchToIdleState();
        }

        public void SwitchToIdleState()
        {
            SwitchState(IdleState);
        }
        
        public void SwitchToFollowState()
        {
            SwitchState(FollowState);
        }
    }
}