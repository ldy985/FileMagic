using System;
using System.Collections.Generic;
using ldy985.FileMagic.Abstracts;

namespace ldy985.FileMagic.Core
{
    public class ParsedHandlerProvider : IParsedHandlerProvider
    {
        private readonly Dictionary<Type, List<Action<object>>> _parsedActions = new Dictionary<Type, List<Action<object>>>();

        public void ExecuteHandlers(IRule type, IParsed parsed)
        {
            if (!_parsedActions.TryGetValue(type.GetType(), out List<Action<object>>? handlers))
                return;

            foreach (Action<object>? parsedHandler in handlers)
                parsedHandler.Invoke(parsed);
        }

        public IParsedHandlerProvider AddParsedHandler<TRule, TParsed>(Action<TParsed> action1) where TRule : IRule where TParsed : IParsed
        {
            void Action(object obj)
            {
                action1((TParsed)obj);
            }

            Type type = typeof(TRule);
            if (_parsedActions.TryGetValue(type, out List<Action<object>>? actions))
                actions.Add(Action);
            else
                _parsedActions.Add(type, new List<Action<object>> { Action });

            return this;
        }
    }
}