//using System.Collections.Generic;

//namespace IUP.Toolkits.AI
//{
//    /// <summary>
//    /// Интерфейс набора опций ИИ-агента.
//    /// </summary>
//    public interface IAI_Options
//    {
//        /// <summary>
//        /// Добавляет опцию, если это возможно.
//        /// </summary>
//        /// <param name="option">Опция ИИ-агента.</param>
//        /// <returns>Возвращает true, если добавление прошло успешно; false, если переданная опция уже 
//        /// содержится в наборе опций.</returns>
//        public bool AddOption(IAI_Option option);

//        /// <summary>
//        /// Удаляет опцию.
//        /// </summary>
//        /// <param name="option">Опция ИИ-агента.</param>
//        /// <returns>Возвращает true, если удаление прошло успешно; false, если переданная опция 
//        /// отсутствует в наборе опций.</returns>
//        public bool RemoveOption(IAI_Option option);

//        /// <summary>
//        /// Проверяет, содержит ли набор опций переданную опцию.
//        /// </summary>
//        /// <param name="option">Опция ИИ-агента.</param>
//        /// <returns>Возвращает true, если набор опций содержит переданную опцию; иначе false.</returns>
//        public bool ContainsOption(IAI_Option option);

//        /// <summary>
//        /// Выбирает лучшие доступные в данный момент для выполнения опции.
//        /// </summary>
//        /// <returns>Возвращает список из лучших доступных в данный момент для выполнения опций.</returns>
//        public List<IAI_Option> ChooseBestOptions();
//    }
//}
