namespace FeatureBee.Server.Domain.EventHandlers.DatabaseHandlers
{
    using System;
    using FeatureBee.Server.Domain.Models;
    using FeatureBee.Server.Models;

    abstract class DatabaseBroadcasterFor<T> : IDatabaseBroadcasterFor
    {
        public Type ForType
        {
            get
            {
                return typeof(T);
            }
        }

        public abstract void Broadcast(FeatureBeeContext context, object eventBody);
        
        protected static ConditionViewModel ToConditionViewModel(Condition condition)
        {
            return new ConditionViewModel { Type = condition.Type, Values = new PersistableStringCollection(condition.Values) };
        }
    }
}