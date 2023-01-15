namespace IUP.BattleSystemPrototype
{
    /// <summary>
    /// Интерфейс повреждаемой сущности.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Наносит урон сущности.
        /// </summary>
        /// <param name="damageType">Тип урона.</param>
        /// <param name="damageAmount">Количество урона.</param>
        public void TakeDamage(DamageType damageType, int damageAmount);
    }
}
