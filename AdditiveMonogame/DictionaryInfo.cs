using System;
using System.Collections.Generic;
using AdditiveMonogame.Interface;

namespace AdditiveMonogame
{
    public class DictionaryInfo<T, T1> : IInfo<T, T1>
        where T : notnull
        where T1 : notnull
    {
        public DictionaryInfo(Dictionary<T, T1> dict)
        {
            Dict = dict;
        }

        private Dictionary<T, T1> Dict { get; }

        public T1 Get(T key)
            => Dict[key];

        public void Set(T key, T1 value)
            => Dict[key] = value;
        
        public void ForEach(Action<T, T1> action)
        {
            foreach (var (key, value) in Dict) action(key, value);
        }

        public void Compute(Func<T, T1, T1> compute)
        {
            foreach (var (key, value) in Dict) Dict[key] = compute(key, value);
        }

        public T1 this[T t]
        {
            get => Dict[t];
            set => Dict[t] = value;
        }

        public static DictionaryInfo<T, T1> Empty => new DictionaryInfo<T, T1>(new Dictionary<T, T1>());
    }
}