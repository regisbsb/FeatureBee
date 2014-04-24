namespace FeatureBee.UpdateModes.FeatureCache
{
    using System;
    using System.Linq;
    using System.Threading;

    public class TimeLimitedInMemoryCache<TKey, TValue> : InMemoryCache<TKey, TValue, TimeLimitedInMemoryCache<TKey, TValue>.CacheItem>, IDisposable
    {
        public sealed class CacheItem : ICacheItem
        {
            private readonly TimeSpan maxAge;

            public CacheItem(TValue value, TimeSpan maxAge, Action onCacheItemRemoved)
            {
                this.maxAge = maxAge;
                this.Value = value;
                this.OnCacheItemRemoved = onCacheItemRemoved;
                this.Created = DateTime.Now;
            }

            public bool IsInvalidated { get; private set; }
            public TValue Value { get; set; }
            public DateTime Created { get; set; }

            public Action OnCacheItemRemoved { get; private set; }

            public bool IsInvalid()
            {
                return IsInvalidated || this.Created + maxAge <= DateTime.Now;
            }

            public void InvalidateCacheValue()
            {
                IsInvalidated = true;
            }
        }
        
        public TimeSpan MaxEntryAge { get; set; }

        private TimeSpan expiryInterval;

        private Timer expiryTimer;
        private int expiryIsRunning;

        public TimeSpan ExpiryInterval
        {
            get { return this.expiryInterval; }
            set
            {
                this.expiryInterval = value;
                this.DisposeTimer();
                this.expiryTimer = new Timer(o => this.Expire(), null, value, value);
            }
        }

        public TimeLimitedInMemoryCache(TimeSpan maxEntryAge)
            : this(maxEntryAge, maxEntryAge)
        {
        }

        public TimeLimitedInMemoryCache(TimeSpan maxEntryAge, TimeSpan expiryInterval)
        {
            this.MaxEntryAge = maxEntryAge;
            this.ExpiryInterval = expiryInterval;
        }

        private void Expire()
        {
            if (Interlocked.CompareExchange(ref this.expiryIsRunning, 1, 0) == 1)
            {
                // expiry is still running
                return;
            }

            // paranoia
            try
            {
                lock (this.SyncRoot)
                {
                    var toExpire = this.ValueCache.Where(x => x.Value.IsInvalid()).Select(x => x.Key).ToList();
                    toExpire.ForEach(this.InvalidateUnlocked);
                }
            }
            finally
            {
                this.expiryIsRunning = 0;
            }
        }

        protected override void UpdateElementAccess(TKey key, CacheItem cacheItem)
        {
        }

        protected sealed override CacheItem CreateCacheValue(TValue value, Action onCacheItemRemoved)
        {
            return new CacheItem(value, this.MaxEntryAge, onCacheItemRemoved);
        }

        protected override void CacheValueInvalidated(CacheItem cacheItem)
        {
            cacheItem.InvalidateCacheValue();
            cacheItem.OnCacheItemRemoved();
        }

        private void DisposeTimer()
        {
            if (this.expiryTimer == null) return; 

            this.expiryTimer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.Zero);
            this.expiryTimer.Dispose();
            this.expiryTimer = null;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeTimer();
            }
        }
    }
}