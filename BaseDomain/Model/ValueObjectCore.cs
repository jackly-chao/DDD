namespace BaseDomain.Model
{
    /// <summary>
    /// 泛型值对象抽象基类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    public abstract class ValueObjectCore<T> where T : ValueObjectCore<T>
    {
        /// <summary>
        /// 重写方法 相等运算
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        /// <summary>
        /// 获取哈希
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        /// <summary>
        /// 重写方法 实体比较 ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(ValueObjectCore<T> a, ValueObjectCore<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        /// <summary>
        /// 重写方法 实体比较 !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(ValueObjectCore<T> a, ValueObjectCore<T> b)
        {
            return !(a == b);
        }

        /// <summary>
        /// 克隆副本
        /// </summary>
        public virtual T Clone()
        {
            return (T)MemberwiseClone();
        }
    }
}
