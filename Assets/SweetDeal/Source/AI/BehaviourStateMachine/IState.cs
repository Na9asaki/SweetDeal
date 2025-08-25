namespace SweetDeal.Source.AI.BehaviourStateMachine
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
    }
}