using System;
using System.Collections.Generic;
using System.Threading;
using TDJS_Vision.Node._6_LogicTool.SharedVariable;

namespace TDJS_Vision
{
    /// <summary>
    /// 线程安全的共享变量类
    /// </summary>
    public class SharedVariable : Dictionary<string, SharedVarValue>
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 获取所有共享变量名称
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetNames()
        {
            try
            {
                _lock.EnterReadLock();
                return new List<string>(Keys);
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 通过名称获取对应值
        /// </summary>
        public SharedVarValue GetValue(string name)
        {
            try
            {
                _lock.EnterReadLock();
                if (TryGetValue(name, out SharedVarValue value))
                {
                    return value;
                }
                throw new KeyNotFoundException($"找不到名为“{name}”的共享变量！");
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }
        /// <summary>
        /// 获取数组或列表的指定索引处的元素
        /// </summary>
        public SharedVarValue GetValue(string name, int index)
        {
            try
            {
                _lock.EnterReadLock();
                if (TryGetValue(name, out SharedVarValue value))
                {
                    if (index >= 0)
                    {
                        if (value.Data == null)
                            throw new InvalidOperationException("Data 为 null，无法获取元素。");

                        // 情况1：Data 是数组（Array）
                        if (value.Data is Array array)
                        {
                            if (index >= 0 && index < array.Length)
                                return new SharedVarValue(array.GetValue(index), array.GetValue(index).GetType());
                            else
                                throw new IndexOutOfRangeException($"索引 {index} 超出数组范围 [0, {array.Length - 1}]");
                        }

                        // 情况2：Data 是 IList（如 List<T>, ArrayList 等）
                        if (value.Data is System.Collections.IList list)
                        {
                            if (index >= 0 && index < list.Count)
                                return new SharedVarValue(list[index], list[index].GetType());
                            else
                                throw new IndexOutOfRangeException($"索引 {index} 超出列表范围 [0, {list.Count - 1}]");
                        }

                        // 情况3：不支持索引访问
                        throw new InvalidOperationException($"类型 {value.Type.Name} 不支持按索引访问元素。");
                    }
                    else
                        throw new ArgumentOutOfRangeException(nameof(index), "索引不能为负数。");
                }
                throw new KeyNotFoundException($"找不到名为“{name}”的共享变量！");
            }
            catch (Exception ex)
            {
                throw new Exception($"获取共享变量“{name}”的索引 {index} 处的值失败。请检查变量是否存在或索引是否有效。");
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }


        /// <summary>
        /// 通过名称设置对应值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetValue(string name, SharedVarValue value)
        {
            try
            {
                _lock.EnterWriteLock();
                this[name] = value;
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 检查是否存在指定名称的变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            try
            {
                _lock.EnterReadLock();
                return ContainsKey(name);
            }
            finally
            {
                if (_lock.IsReadLockHeld)
                    _lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 删除指定名称的变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveOne(string name)
        {
            try
            {
                _lock.EnterWriteLock();
                return Remove(name);
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 清空所有变量
        /// </summary>
        public void ClearAll()
        {
            try
            {
                _lock.EnterWriteLock();
                Clear();
            }
            finally
            {
                if (_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }
    }
}