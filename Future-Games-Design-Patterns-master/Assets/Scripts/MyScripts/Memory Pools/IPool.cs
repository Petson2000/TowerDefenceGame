using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

    public class GameObjectPool : IPool<GameObject>
    {
        private readonly uint m_expandBy;
        private readonly GameObject m_prefab;
        private Transform m_parent;

        private readonly Stack<GameObject> m_objects = new Stack<GameObject>();

        public GameObjectPool(uint initSize, GameObject mPrefab, uint expandBy = 1, Transform mParent = null)
        {
            m_expandBy = (uint) Mathf.Max(1, expandBy);
            m_prefab = mPrefab;
            m_parent = mParent;
            m_prefab.SetActive(false);
            Expand((uint) Mathf.Max(1, initSize));
        }

        private void Expand(uint amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject instance = GameObject.Instantiate(m_prefab, m_parent);
                EmitOnDisable emitOnDisable = instance.AddComponent<EmitOnDisable>();
                emitOnDisable.OnDisableGameObject += UnRent;
                m_objects.Push(instance);
            }
        }

        private void UnRent(GameObject gameObject)
        {
            m_objects.Push(gameObject);
        }

        public GameObject Rent(bool returnActive)
        {
            if (m_objects.Count == 0)
            {
                Expand(m_expandBy);
            }
            GameObject instance = m_objects.Pop();
            
            instance.SetActive(returnActive);
            return instance;
        }
    }
}