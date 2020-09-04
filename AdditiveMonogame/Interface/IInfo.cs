namespace AdditiveMonogame.Interface
{
    public interface IInfo<T, T1>
    {
        public T1 Get(T key);
        public void Set(T key, T1 value);
    }
}