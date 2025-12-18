using System.Collections.Generic;
using UnityEngine;

namespace EndlessRunner
{
    public class LevelBlock : BlockBase
    {
        private struct CoinData
        {
            public GameObject coin;
            public Vector3 localPos;
            public Quaternion localRot;
        }

        private readonly List<CoinData> coins = new();


        protected void Awake()
        {
            CacheCoins();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ResetCoins();
        }

        private void CacheCoins()
        {
            coins.Clear();

            foreach (Transform child in GetComponentsInChildren<Transform>(true))
            {
                if (child.CompareTag("Coin"))
                {
                    coins.Add(new CoinData
                    {
                        coin = child.gameObject,
                        localPos = child.localPosition,
                        localRot = child.localRotation
                    });
                }
            }
        }

        private void ResetCoins()
        {
            foreach (var data in coins)
            {
                if (data.coin == null) continue;

                data.coin.transform.localPosition = data.localPos;
                data.coin.transform.localRotation = data.localRot;
                data.coin.SetActive(true);
            }
        }
    }
}
