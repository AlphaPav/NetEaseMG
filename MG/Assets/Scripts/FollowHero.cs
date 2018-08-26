using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global{
    //主角类型
    public const int A = 0;
    public const int B = 1;
    public const int C = 2;

    //最大血量
    public const int MaxHeroBlood = 15;
	public static bool isCameraFollowHero= true;
    //主角变量
    public static int HeroBlood = 1;      	// 主角血量  决定后面的跟班个数

    public static int HeroType = A;	  	// 主角类型  A or B or C

    //抗药性
    public static int HeroResistance = 0;  // 主角抗药性 0-100 百分比免伤
}

public class FollowHero : MonoBehaviour {
    // 跟随的游戏物品
    public GameObject[] follow;

	GameObject boss;
	public float xspeed = 2f;//move force
	public float jumpSpeed = 700f;//jump force
	Rigidbody rb;

    //上次位置
	Vector3 lastPos;
	const int Pause = 10;
	int Delay = Pause;

	public Vector3[][] offset;

	void Start()
	{
        //每个物体的连续每10帧动画 
		offset = new Vector3[Global.MaxHeroBlood][];

        //初始化变量
		for (int i = 0; i < Global.MaxHeroBlood; i++)
		{
			offset[i] = new Vector3[Pause];

			for (int j = 0; j < Pause; j++)
			{
				offset[i][j] = new Vector3(0, 0, 0);
			}
		}

        //获得刚体组件
		rb = GetComponent<Rigidbody>();
		lastPos = transform.position;

        //设置全局变量
        Global.HeroBlood = 1;
        Global.HeroType = Global.A;
    }
	void Update()
	{
		if (Global.isCameraFollowHero == true) {
			Move ();
		}
        
        //延时10帧, 开始更新游戏对象的位置
		if (Delay == 0)
		{
			for (int i = Global.HeroBlood - 1; i > 0; i--)
			{
				for (int j = 0; j < Pause; j++)
				{
					offset[i][j] = offset[i - 1][j];
				}
			}
			offset[0] = new Vector3[10];
			for (int j = 0; j < Pause; j++)
			{
				offset[0][j] = new Vector3(0, 0, 0);
			}
			Delay = Pause;
		}

        //开始移动
		for (int i = 1; i < Global.HeroBlood; i++)
		{
			follow[i].transform.Translate(offset[i][Pause-Delay]);
		}

        // 更新最新的游戏位置
		offset[0][Pause-Delay] = transform.position - lastPos;
		lastPos = transform.position;


		Delay--;

	}

    public void set(){

    }


	void Move()
	{
        //AD移动
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(new Vector3(1,0,0) * xspeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-1 * new Vector3(1, 0, 0) * xspeed * Time.deltaTime, Space.World);
		}
	}
}


