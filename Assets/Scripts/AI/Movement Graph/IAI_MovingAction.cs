namespace IUP.Toolkits.AI
{
    /// <summary>
    /// Интерфейс действия передвижения ИИ-агента.
    /// </summary>
    /// <typeparam name="TMovingState">Состояние передвижения ИИ-агента.</typeparam>
    public interface IAI_MovingAction<TMovingState> : IAI_Action<TMovingState>
        where TMovingState : IAI_MovingState
    {
        /// <summary>
        /// Стоимость передвижения.
        /// </summary>
        public new int Cost { get; }

        /// <summary>
        /// Возможно ли совершить передвижение в данный момент.
        /// </summary>
        /// <param name="state">Состояние ИИ-агента в текущий момент.</param>
        /// <returns>Возвращает true, если передвижение возможно; иначе false.</returns>
        public new bool IsPossibleToMakeAction(TMovingState state);

        /// <summary>
        /// Предполагает позицию, в которой может оказаться ИИ-агент после совершеиния передвижения.
        /// </summary>
        /// <param name="state">Состояние ИИ-агента в текущий момент.</param>
        /// <returns>Возвращает предполагаемую позицию, в которой окажется ИИ-агент после совершения 
        /// передвижения.</returns>
        public new TMovingState EvaluateActionResult(TMovingState state);

        /// <summary>
        /// Совершает передвижение.
        /// </summary>
        public new void MakeAction();
    }
}
