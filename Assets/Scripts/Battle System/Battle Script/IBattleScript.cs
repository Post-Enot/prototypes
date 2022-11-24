namespace IUP.Toolkits.BattleSystem
{
    /// <summary>
    /// Сценарий сражения. Отвечает за этапы боя, а также его завершение.
    /// </summary>
    public interface IBattleScript
    {
        public bool IsPerformed { get; }

        public void Start();
    }
}
