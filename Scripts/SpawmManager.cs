using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#region -------------------- Enemy Pool ---------------------------
[System.Serializable]
public class EnemyPool
{
    public EnemyController prefab;
    public List<EnemyController> inactiveObjs;
    public List<EnemyController> activeObjs;
    public EnemyController Spawm(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
            EnemyController newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            EnemyController oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(EnemyController obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            EnemyController obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion

#region ------------------- magic pool --------------------------
[System.Serializable]
public class MagicPool
{
    public MagicCtrl prefab;
    public List<MagicCtrl> inactiveObjs;
    public List<MagicCtrl> activeObjs;
    public MagicCtrl Spawm(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
            MagicCtrl newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            MagicCtrl oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(MagicCtrl obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            MagicCtrl obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion

#region ------------------- Coin pool --------------------------
[System.Serializable]
public class CoinPool
{
    public CoinManager prefab;
    public List<CoinManager> inactiveObjs;
    public List<CoinManager> activeObjs;
    public CoinManager Spawm(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
            CoinManager newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            CoinManager oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(CoinManager obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            CoinManager obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion

#region ------------------- Exp pool --------------------------
[System.Serializable]
public class ExpPool
{
    public XPCtrl prefab;
    public List<XPCtrl> inactiveObjs;
    public List<XPCtrl> activeObjs;
    public XPCtrl Spawm(Vector3 position, Transform parent, int xpValue)
    {
        if (inactiveObjs.Count == 0)
        {
            XPCtrl newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            newObj.xp_value = xpValue;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            XPCtrl oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            oldObj.xp_value = xpValue;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(XPCtrl obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            XPCtrl obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion

#region ------------------- Hit Animation pool --------------------------
[System.Serializable]
public class HitAnimPool
{
    public AnimationHit prefab;
    public List<AnimationHit> inactiveObjs;
    public List<AnimationHit> activeObjs;
    public AnimationHit Spawm(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
            AnimationHit newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            AnimationHit oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(AnimationHit obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            AnimationHit obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion

#region ------------------------ Ore pool -------------------------
[System.Serializable]
public class OrePool
{
    public OreManager prefab;
    public List<OreManager> inactiveObjs;
    public List<OreManager> activeObjs;
    public OreManager Spawm(Vector3 position, Transform parent, bool isNew)
    {
        if (inactiveObjs.Count == 0)
        {
            OreManager newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            newObj.isNew = true;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            OreManager oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            oldObj.isNew = isNew;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(OreManager obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            OreManager obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion

#region ------------------------ Sword pool -------------------------
[System.Serializable]
public class SwordPool
{
    public SwordManager prefab;
    public List<SwordManager> inactiveObjs;
    public List<SwordManager> activeObjs;
    public SwordManager Spawm(Vector3 position, Transform parent)
    {
        if (inactiveObjs.Count == 0)
        {
            SwordManager newObj = GameObject.Instantiate(prefab, parent);
            newObj.transform.position = position;
            newObj.name = prefab.name;
            activeObjs.Add(newObj);
            return newObj;
        }
        else
        {
            SwordManager oldObj = inactiveObjs[0];
            oldObj.gameObject.SetActive(true);
            oldObj.transform.SetParent(parent);
            oldObj.transform.position = position;
            activeObjs.Add(oldObj);
            inactiveObjs.RemoveAt(0);
            return oldObj;
        }
    }
    public void Release(SwordManager obj)
    {
        if (activeObjs.Contains(obj))
        {
            inactiveObjs.Add(obj);
            activeObjs.Remove(obj);
            obj.gameObject.SetActive(false);
        }
    }
    // clear pool
    public void Clear()
    {
        while (activeObjs.Count > 0)
        {
            SwordManager obj = activeObjs[0];
            obj.gameObject.SetActive(false);
            activeObjs.RemoveAt(0);
            inactiveObjs.Add(obj);
        }
    }
}
#endregion
public class SpawmManager : MonoBehaviour
{
    // Singleton
    private static SpawmManager m_Instance;
    public static SpawmManager Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = FindObjectOfType<SpawmManager>();
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else if (m_Instance != this)
            Destroy(gameObject);
    }

    [SerializeField] private EnemyPool m_AngryPigPool;
    [SerializeField] private EnemyPool m_BatPool;
    [SerializeField] private EnemyPool m_BlueBirdPool;
    [SerializeField] private EnemyPool m_ChameleonPool;

    [SerializeField] private OrePool m_OreGreenPool;
    [SerializeField] private OrePool m_OrePurplePool;
    [SerializeField] private OrePool m_OreGoldPool;
    [SerializeField] private OrePool m_OreOrangePool;
    [SerializeField] private OrePool m_OreRedPool;
    [SerializeField] private OrePool m_OreCyanPool;
    [SerializeField] private OrePool m_OreGreenSuperPool;

    [SerializeField] private MagicPool m_MagicBoltPool;
    [SerializeField] private MagicPool m_MagicChargedPool;
    [SerializeField] private MagicPool m_MagicCrossedPool;
    [SerializeField] private MagicPool m_MagicPulsePool;
    [SerializeField] private MagicPool m_MagicSparkPool;
    [SerializeField] private MagicPool m_MagicWaveformPool;

    [SerializeField] private CoinPool m_CoinBronzePool;
    [SerializeField] private CoinPool m_CoinSilverPool;
    [SerializeField] private CoinPool m_CoinGoldPool;

    [SerializeField] private ExpPool m_ExpPool;

    [SerializeField] private SwordPool m_SwordEvo2;
    [SerializeField] private SwordPool m_SwordEvo3;

    [SerializeField] private HitAnimPool m_Hit_1_Pool;
    [SerializeField] private HitAnimPool m_Hit_2_Pool;
    [SerializeField] private HitAnimPool m_Hit_3_Pool;
    [SerializeField] private HitAnimPool m_Hit_4_Pool;
    [SerializeField] private HitAnimPool m_Hit_5_Pool;
    [SerializeField] private HitAnimPool m_Hit_6_Pool;

    private void Start()
    {
        SpawmExp(PlayerController.Instance.transform.position, 100);
    }


    #region ---------spawm magic ------------------
    public MagicCtrl SpawmMagic(Vector3 position, string name)
    {
        if (name == "bolt")
        {
            MagicCtrl obj = m_MagicBoltPool.Spawm(position, transform);
            return obj;
        }
        else if (name == "charged")
        {
            MagicCtrl obj = m_MagicChargedPool.Spawm(position, transform);
            return obj;
        }
        else if (name == "crossed")
        {
            MagicCtrl obj = m_MagicCrossedPool.Spawm(position, transform);
            return obj;
        }
        else if (name == "pulse")
        {
            MagicCtrl obj = m_MagicPulsePool.Spawm(position, transform);
            return obj;
        }
        else if (name == "spark")
        {
            MagicCtrl obj = m_MagicSparkPool.Spawm(position, transform);
            return obj;
        }
        else if (name == "waveform")
        {
            MagicCtrl obj = m_MagicWaveformPool.Spawm(position, transform);
            return obj;
        }
        return null;
    }

    public void ReleaseMagic(MagicCtrl obj, string name)
    {
        if (name == "bolt")
            m_MagicBoltPool.Release(obj);

        else if (name == "charged")
            m_MagicChargedPool.Release(obj);

        else if (name == "crossed")
            m_MagicCrossedPool.Release(obj);

        else if (name == "pulse")
            m_MagicPulsePool.Release(obj);

        else if (name == "spark")
            m_MagicSparkPool.Release(obj);

        else if (name == "waveform")
            m_MagicWaveformPool.Release(obj);
    }
    #endregion

    #region ----------- spawm sword ---------------
    public SwordManager SpawmSword(Vector3 position, string name)
    {
        if (name == "SwordEvo2")
        {
            SwordManager obj = m_SwordEvo2.Spawm(position, transform);
            return obj;
        }
        else if (name == "SwordEvo3")
        {
            SwordManager obj = m_SwordEvo3.Spawm(position, transform);
            return obj;
        }
        return null;
    }

    public void ReleaseSword(SwordManager obj, string name)
    {
        if (name == "SwordEvo2")
            m_SwordEvo2.Release(obj);
        else if (name == "SwordEvo3")
            m_SwordEvo3.Release(obj);

    }
    #endregion

    #region ----------- spawm coin ---------------
    public CoinManager SpawmCoin (Vector3 position, string name)
    {
        if (name == "Bronze")
        {
            CoinManager obj = m_CoinBronzePool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Silver")
        {
            CoinManager obj = m_CoinSilverPool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Gold")
        {
            CoinManager obj = m_CoinGoldPool.Spawm(position, transform);
            return obj;
        }
        return null;
    }

    public void ReleaseCoin(CoinManager obj, string name)
    {
        if (name == "Bronze")
            m_CoinBronzePool.Release(obj);
        else if (name == "Silver")
            m_CoinSilverPool.Release(obj);
        else if (name == "Gold")
            m_CoinGoldPool.Release(obj);

    }
    #endregion

    #region ----------------Spawm Enemy-----------------
    public EnemyController SpawmEnemy(Vector3 position, string name)
    {
        if (name == "AngryPig")
        {
            EnemyController obj = m_AngryPigPool.Spawm(position, transform);
            obj.SetStart();
            return obj;
        }
        else if (name == "Bat")
        {
            EnemyController obj = m_BatPool.Spawm(position, transform);
            obj.SetStart();
            return obj;
        }
        else if (name == "BlueBird")
        {
            EnemyController obj = m_BlueBirdPool.Spawm(position, transform);
            obj.SetStart();
            return obj;
        }
        else if (name == "Chameleon")
        {
            EnemyController obj = m_ChameleonPool.Spawm(position, transform);
            obj.SetStart();
            return obj;
        }
        else
            return null;
    }

    public void ReleaseEnemy(EnemyController obj, string name)
    {
        if (name == "AngryPig")
            m_AngryPigPool.Release(obj);
        else if (name == "Bat")
            m_BatPool.Release(obj);
        else if (name == "BlueBird")
            m_BlueBirdPool.Release(obj);
        else if (name == "Chameleon")
            m_ChameleonPool.Release(obj);
    }
    #endregion


    #region ---------Spawm Ore------------
    public OreManager SpawmOre(Vector3 position, string name, bool isNew)
    {
        if (name == "OreGreen")
        {
            OreManager obj = m_OreGreenPool.Spawm(position, transform, isNew);
            return obj;
        }
        else if (name == "OrePurple")
        {
            OreManager obj = m_OrePurplePool.Spawm(position, transform, isNew);
            return obj;
        }
        else if (name == "OreGold")
        {
            OreManager obj = m_OreGoldPool.Spawm(position, transform, isNew);
            return obj;
        }
        else if (name == "OreOrange")
        {
            OreManager obj = m_OreOrangePool.Spawm(position, transform, isNew);
            return obj;
        }
        else if (name == "OreRed")
        {
            OreManager obj = m_OreRedPool.Spawm(position, transform, isNew);
            return obj;
        }
        else if (name == "OreCyan")
        {
            OreManager obj = m_OreCyanPool.Spawm(position, transform, isNew);
            return obj;
        }
        else if (name == "OreGreenSuper")
        {
            OreManager obj = m_OreGreenSuperPool.Spawm(position, transform, isNew);
            return obj;
        }
        else
            return null;
    }

    public void ReleaseOre(OreManager obj, string name)
    {
        if (name == "OreGreen")
            m_OreGreenPool.Release(obj);

        else if (name == "OrePurple")
            m_OrePurplePool.Release(obj);

        else if (name == "OreGold")
            m_OreGoldPool.Release(obj);

        else if (name == "OreOrange")
            m_OreOrangePool.Release(obj);

        else if (name == "OreRed")
            m_OreRedPool.Release(obj);

        else if (name == "OreCyan")
            m_OreCyanPool.Release(obj);

        else if (name == "OreGreenSuper")
            m_OreGreenSuperPool.Release(obj);
    }
    #endregion

    #region ---------Spawm Hit Animtion ------------
    public AnimationHit SpawmHitAnimation(Vector3 position, string name)
    {
        if (name == "Hit_1")
        {
            AnimationHit obj = m_Hit_1_Pool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Hit_2")
        {
            AnimationHit obj = m_Hit_2_Pool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Hit_3")
        {
            AnimationHit obj = m_Hit_3_Pool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Hit_4")
        {
            AnimationHit obj = m_Hit_4_Pool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Hit_5")
        {
            AnimationHit obj = m_Hit_5_Pool.Spawm(position, transform);
            return obj;
        }
        else if (name == "Hit_6")
        {
            AnimationHit obj = m_Hit_6_Pool.Spawm(position, transform);
            return obj;
        }
        else
            return null;
    }

    public void ReleaseHitAnimation(AnimationHit obj, string name)
    {
        if (name == "Hit_1")
            m_Hit_1_Pool.Release(obj);

        else if (name == "Hit_2")
            m_Hit_2_Pool.Release(obj);

        else if (name == "Hit_3")
            m_Hit_3_Pool.Release(obj);

        else if (name == "Hit_4")
            m_Hit_4_Pool.Release(obj);

        else if (name == "Hit_5")
            m_Hit_5_Pool.Release(obj);

        else if (name == "Hit_6")
            m_Hit_6_Pool.Release(obj);

    }
    #endregion

    #region ---------- spawm exp --------------
    public XPCtrl SpawmExp(Vector3 position, int value)
    {
        XPCtrl obj = m_ExpPool.Spawm(position, transform, value);
        return obj;
    }

    public void ReleaseExp(XPCtrl obj)
    {
        m_ExpPool.Release(obj);
    }
    #endregion

}
