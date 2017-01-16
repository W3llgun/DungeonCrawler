using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour {

    public static BonusSpawner instance;

	private void Awake()
	{
		instance = this;
	}

	public List<GameObject> items;
    public List<int> lifeUp;
    public List<int> scoreUp;

    public void Spawn(Vector3 position, int count)
    {
        if (items == null || items.Count == 0)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, items.Count);
            GameObject go = Instantiate(items[rand]);
            var item = go.GetComponent<Item>();
            switch (item.action.type)
            {
                case ItemType.LIFE:
                    SetItem(item, lifeUp);
                    break;
                case ItemType.SCORE:
                    SetItem(item, scoreUp);
                    break;
                default:
                    break;
            }
        }
    }

    void SetItem(Item item, List<int> values)
    {
        int rand = Random.Range(0, values.Count);
        item.SetValue(rand, values[rand]);
    }

}
