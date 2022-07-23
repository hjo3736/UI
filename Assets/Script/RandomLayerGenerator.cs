using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInfo
{

	public string map_name;
	public int floor;
	public int map_num;
	public List<MapInfo> connection;

	public MapInfo(int hierarchy, int floor, int map_num)
	{

		this.floor = (hierarchy - 1) * 5 + floor;
		this.map_num = map_num;
		this.map_name = this.floor.ToString() + "-" + this.map_num.ToString();
		connection = new List<MapInfo>();

	}

}

class Hierarchy
{

	int difficulty = 3;

	private int layer_hierarchy;
	public int[] floor_map_number = new int[5];
	public List<MapInfo> info = new List<MapInfo>();

	public Hierarchy(int hierarchy)
	{
		layer_hierarchy = hierarchy;
		for (int i = 0; i < 5; i++)
		{

			if (i == 0)
			{
				floor_map_number[i] = 1;
				info.Add(new MapInfo(layer_hierarchy, 0, 1));

			}
			else
			{
				floor_map_number[i] = i * hierarchy + Random.Range(-i + 1, i + difficulty + 1);

				for (int j = 1; j <= floor_map_number[i]; j++)
					info.Add(new MapInfo(layer_hierarchy, i, j));

			}

		}

		info[0].connection.Add(info[1]);
		info[1].connection.Add(info[0]);

	}
	public void Create()
	{
		while (ConnectionCheck())
		{
			int a = Random.Range(1, info.Count);
			int b = a;

			for (int i = 0; ; i++)
			{
				b = Random.Range(1, info.Count);

				if ((a != b) && (info[a].floor - 1 == info[b].floor || info[a].floor == info[b].floor || info[a].floor + 1 == info[b].floor)
					&& !info[a].connection.Contains(info[b]))
					break;
			}

			info[a].connection.Add(info[b]);
			info[b].connection.Add(info[a]);
		}
	}

	public bool ConnectionCheck()
	{

		List<MapInfo> check = new List<MapInfo>();
		List<MapInfo> check_temp = new List<MapInfo>();
		check.Add(info[0]);
		check_temp.Add(info[0]);

		for (int i = 0; i < info.Count; i++)
		{

			for (int j = 0; j < check.Count; j++)
			{

				for (int k = 0; k < check[j].connection.Count; k++)
				{
					if (!check_temp.Contains(check[j].connection[k]))
						check_temp.Add(check[j].connection[k]);
					else
						continue;
				}

			}

			check = check_temp.ToList();

		}

		if (check.Count == info.Count)
			return false;
		else
			return true;

	}

	public void Connect(Hierarchy hierarchy)
	{

		int index = Random.Range(info.Count - floor_map_number[4], info.Count);

		info[index].connection.Add(hierarchy.info[0]);
		hierarchy.info[0].connection.Add(info[index]);

	}

}


public class RandomLayerGenerator : MonoBehaviour
{
	void Start()
	{
		Hierarchy hierarchy1 = new Hierarchy(1);
		Hierarchy hierarchy2 = new Hierarchy(2);
		hierarchy1.Create();
		hierarchy2.Create();
		hierarchy1.Connect(hierarchy2);

		for (int i = 0; i < hierarchy1.info.Count; i++)
		{

			for (int j = 0; j < hierarchy1.info[i].connection.Count; j++)
			{

				Debug.Log(hierarchy1.info[i].map_name + " to " + hierarchy1.info[i].connection[j].map_name);

			}

		}

	}
	void Update()
	{

	}
}