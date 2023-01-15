namespace IUP.Toolkits.AI
{
    /// <summary>
    /// Интерфейс действия ИИ-агента.
    /// </summary>
    /// <typeparam name="TState">Тип состояния ИИ-агента.</typeparam>
    public interface IAI_Action<TState>
    {
        /// <summary>
        /// Цена действия.
        /// </summary>
        public int Cost { get; }

        /// <summary>
        /// Проверяет, возможно ли совершить опцию в данный момент.
        /// </summary>
        /// <param name="state">Состояние ИИ-агента в данный момент.</param>
        /// <returns>Возвращает true, если опцию возможно совершить; иначе false.</returns>
        public bool IsPossibleToMakeAction(TState state);

        /// <summary>
        /// Оценивает результат от действия.
        /// </summary>
        /// <param name="state">Состояние ИИ-агента в данный момент.</param>
        /// <returns>Возвращает оценочный результат от действия.</returns>
        public TState EvaluateActionResult(TState state);

        /// <summary>
        /// Совершает действие.
        /// </summary>
        public void MakeAction();
    }
}
