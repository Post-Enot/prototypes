namespace IUP.BattleSystemPrototype
{
    public static class EntitiesTags
    {
        /// <summary>
        /// Означает, что сущность летает: т.е., может перемещаться не только по поверхностям, но и над 
        /// пропастью (Abyss).
        /// </summary>
        public const string Flying = "flying";
        /// <summary>
        /// Означает, что сущность занимает поверхность (Surface), и больше на неё никто не может стать.
        /// </summary>
        public const string TakesUpSurface = "takes up surface";
        /// <summary>
        /// Означает, что сущность является поверхностью или её отсутствием (Abyss). В одной клетке может 
        /// находиться только одна сущность с тегом поверхности (Surface).
        /// </summary>
        public const string Surface = "surface";
        /// <summary>
        /// Тег главного героя.
        /// </summary>
        public const string MainHero = "main hero";
    }
}
