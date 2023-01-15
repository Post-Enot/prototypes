namespace IUP.Toolkits.AI
{
    /// <summary>
    /// Интерфейс опции ИИ-агента.
    /// </summary>
    /// <typeparam name="TState">Тип состояния ИИ-агента.</typeparam>
    public interface IAI_Option<TState>
    {
        /// <summary>
        /// Приоритет опции. В первую очередь выбираются опции с наименьшим численным значением приоритета.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Проверяет, возможно ли совершить опцию в данный момент.
        /// </summary>
        /// <returns>Возвращает true, если опцию возможно совершить; иначе false.</returns>
        public bool IsPossibleToMakeOption(TState state);

        /// <summary>
        /// Совершает опцию.
        /// </summary>
        public void MakeOption();
    }
}
