using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

    public class GameObjectPool : IPool<GameObject>
    {
        //Take object from pool and return it, active or not.

        private uint m_InitSize; //Initial pool size
        private uint m_ExpandBy; //When out of objects, expand by this much
        private GameObject m_prefab;
        private Transform m_parent;
        
        private Stack<GameObject> objects = new Stack<GameObject>();

        public GameObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
        {
            m_InitSize = (uint)Mathf.Max(1, initSize);
            m_ExpandBy = (uint) Mathf.Max(1, expandBy);
            m_prefab = prefab;
            m_parent = parent;
            
            Expand(initSize);
        }

        public void Expand(uint amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject instance = GameObject.Instantiate(m_prefab, m_parent);
                objects.Push(instance);
            }
        }
        
        
        public GameObject Rent(bool returnActive)
        {
            if (objects.Count == 0)
            {
                Expand(m_ExpandBy);
            }
            
            GameObject instance = objects.Pop();
            instance.SetActive(returnActive);
            return instance;
        }
    }
}