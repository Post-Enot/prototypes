namespace IUP.Toolkits.BattleSystem
{
    /// <summary>
    /// Интерфейс класса логики боевой сцены.
    /// </summary>
    public interface IBattle
    {
        /// <summary>
        /// Запускает логику боевой сцены, используя переданный контекст.
        /// </summary>
        /// <param name="battleContext">Контекст боевой сцены.</param>
        public void Init(IBattleContext battleContext);
    }
}
