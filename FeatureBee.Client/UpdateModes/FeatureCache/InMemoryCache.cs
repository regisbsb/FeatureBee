namespace FeatureBee.UpdateModes.FeatureCache
{
    using System;
    using System.Collections.Generic;

    public abstract class InMemoryCache<TKey, TValue, TCacheValue>
         where TCacheValue : class, InMemoryCache<TKey, TValue, TCacheValue>.ICacheItem
    {
        public interface ICacheItem
        {
            bool IsInvalidated { get; }
            TValue Value { get; set; }
            Action OnCacheItemRemoved { get; }

            bool IsInvalid();
        }

        protected readonly Dictionary<TKey, TCacheValue> ValueCache = new Dictionary<TKey, TCacheValue>();
        protected object SyncRoot = new object();

        protected abstract TCacheValue CreateCacheValue(TValue value, Action onCacheItemRemoved);
        protected abstract void UpdateElementAccess(TKey key, TCacheValue cacheItem);
        protected abstract void CacheValueInvalidated(TCacheValue cacheItem);

        public virtual int Count
        {
            get { return this.ValueCache.Count; }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            lock (SyncRoot)
            {
                TCacheValue cacheValue = GetCacheValueUnlocked(key);
                if (cacheValue == null) return false; 

                value = cacheValue.Value;
                this.UpdateElementAccess(key, cacheValue);
                return true;
            }
        }

        protected virtual TCacheValue GetCacheValueUnlocked(TKey key)
        {
            TCacheValue cacheValue;
            return this.ValueCache.TryGetValue(key, out cacheValue) ? cacheValue : null;
        }

        public void SetValue(TKey key, TValue value, Action onCacheItemRemoved)
        {
            lock (SyncRoot)
            {
                SetValueUnlocked(key, value, onCacheItemRemoved);
            }
        }

        protected virtual TCacheValue SetValueUnlocked(TKey key, TValue value, Action onCacheItemRemoved)
        {
            TCacheValue cacheValue = GetCacheValueUnlocked(key);
            if (cacheValue == null)
            {
                cacheValue = CreateCacheValue(value, onCacheItemRemoved);
                this.ValueCache[key] = cacheValue;
            }
            else
            {
                cacheValue.Value = value;
            }
            UpdateElementAccess(key, cacheValue);
            return cacheValue;
        }

        public void Invalidate(TKey key)
        {
            lock (SyncRoot)
            {
                InvalidateUnlocked(key);
            }
        }

        protected virtual void InvalidateUnlocked(TKey key)
        {
            var value = GetCacheValueUnlocked(key);
            if (value == null) return;
            this.ValueCache.Remove(key);
            this.CacheValueInvalidated(value);
        }

        public virtual void Flush()
        {
            lock (SyncRoot)
            {
                FlushUnlocked();
            }
        }

        protected virtual void FlushUnlocked()
        {
            this.ValueCache.Clear();
        }

        public List<TKey> GetKeys()
        {
            lock (SyncRoot)
            {
                return new List<TKey>(this.ValueCache.Keys);
            }
        }
    }
}
