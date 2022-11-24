namespace IUP.Toolkits.BattleSystem
{
    public interface IBattleLoop
    {
        public bool IsIterating { get; }

        public void StartIteration();
    }
}
