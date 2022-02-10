using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace realima.asterioidz
{
    public class PoolManager : MonoBehaviour
    {
        private static PoolManager _instance;
        public static PoolManager Instance => _instance;

        [SerializeField] bool _fillOnAwake = true;
        [SerializeField] InstanceSet[] projectiles;

        private List<GameObject> _hiddenObjects = new List<GameObject>();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            
            if(_fillOnAwake) FillPool();
        }

        public void ClearChilds()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
                GC.Collect();
            }
        }

        public void FillPool()
        {
            ClearChilds();
            for (int i = 0; i < projectiles.Length; i++)
            {
                for (int b = 0; b < projectiles[i].buffer; b++)
                {
                    int rng = Random.Range(0, projectiles[i].variations.Length);
                    CreateHiddenInstance(projectiles[i].variations[rng]);
                }
            }
        }

        private GameObject CreateHiddenInstance(GameObject go)
        {
            var obj = Instantiate(go, transform);
            obj.SetActive(false);
            _hiddenObjects.Add(obj);
            return obj;
        }

        public GameObject Show(int index = -1, Vector3 offset = new Vector3(), Quaternion rotation = new Quaternion())
        {
            GameObject go;

            index = index > -1 ? index : Random.Range(0, projectiles.Length);
            int rng = projectiles[index].variations.Length > 1 ? Random.Range(0, projectiles[index].variations.Length) : 0;

            if (_hiddenObjects.Count(p => p.name.StartsWith(projectiles[index].variations[rng].name)) == 0)
            {
                go = CreateHiddenInstance(projectiles[index].variations[rng]);
            }
            else
            {
                go = _hiddenObjects.Where(p => p.name.StartsWith(projectiles[index].variations[rng].name)).FirstOrDefault();
            }

            go.transform.position = transform.position + offset;
            go.transform.rotation = rotation;
            go.SetActive(true);
            _hiddenObjects.Remove(go);

            return go;
        }

        public void Hide(GameObject go)
        {
            go.SetActive(false);
            _hiddenObjects.Add(go);
        }

        [System.Serializable]
        public class InstanceSet
        {
            public int buffer;
            public GameObject[] variations;
        }
    }
}