//using System.Collections.Generic;

//namespace IUP.Toolkits.AI
//{
//    /// <summary>
//    /// Набор опций ИИ-агента.
//    /// </summary>
//    public sealed class AI_Options : IAI_Options
//    {
//        /// <summary>
//        /// Инициализирует набор опций ИИ-агента.
//        /// </summary>
//        /// <param name="options">Набор опций ИИ-агента.</param>
//        public AI_Options(params IAI_Option[] options)
//        {
//            _options = new HashSet<IAI_Option>(options);
//        }

//        private readonly HashSet<IAI_Option> _options;

//        public bool AddOption(IAI_Option option)
//        {
//            return _options.AddRelationGroup(option);
//        }

//        public bool RemoveOption(IAI_Option option)
//        {
//            return _options.RemoveRelationGroup(option);
//        }

//        public bool ContainsOption(IAI_Option option)
//        {
//            return _options.ContainsRelationType(option);
//        }

//        public List<IAI_Option> ChooseBestOptions()
//        {
//            int bestCost = int.MinValue;
//            var bestOptions = new List<IAI_Option>();
//            foreach (IAI_Option option in _options)
//            {
//                if (option.IsPossibleToMakeOption())
//                {
//                    if (option.Priority > bestCost)
//                    {
//                        bestOptions.Clear();
//                        bestCost = option.Priority;
//                        bestOptions.AddRelationGroup(option);
//                    }
//                    else if (option.Priority == bestCost)
//                    {
//                        bestOptions.AddRelationGroup(option);
//                    }
//                }
//            }
//            return bestOptions;
//        }
//    }
//}
