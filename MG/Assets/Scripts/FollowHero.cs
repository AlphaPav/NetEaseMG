using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global{
    //主角类型
    public const int A = 0;
    public const int B = 1;
    public const int C = 2;

    //主角状态
    public const int Distributed = 0;
    public const int Total = 1;

    //最大血量
    public const int MaxHeroBlood = 15;

    //主角变量
    public static int HeroBlood = 1;      	// 主角血量  决定后面的跟班个数

    public static int HeroType = A;     // 主角类型  A or B or C

    //主角
    public static int HeroState = Total;

    public static bool isCameraFollowHero= true;
    public static bool isCameraShaking = false;

    //抗药性
    public static int HeroResistanceA = 0;  // A类抗药性
    public static int HeroResistanceB = 0;  // A类抗药性
    public static int HeroResistanceC = 0;  // A类抗药性

    //生产孩子的位置半径
    public static float ChildRadius = 0.7f;

}

public class FollowHero : MonoBehaviour {
    // 跟随的游戏物品
    private GameObject[] follow;
    
    //运动相关速度
	public float xspeed = 3f;//move force
	public float jumpSpeed = 600f;//jump force

    public GameObject SubHero;

	Rigidbody rb;

    //上次位置
	Vector3 lastPos;

    //延时10帧
    const int Pause = 10;
	int Delay = Pause;

    //保存每个物体的接下来的运动
	public Vector3[][] offset;

    //Shift 持续时间
    const int SetShiftPause = 40;
    private int PauseTime = 0;

    //Jump Code
    float force = 250;
    int JumpNum = 0;

    void Start()
	{
        //每个物体的连续每10帧动画 
		offset = new Vector3[Global.MaxHeroBlood][];

        //初始化变量
        follow = new GameObject[Global.MaxHeroBlood];
		for (int i = 0; i < Global.MaxHeroBlood; i++)
		{
            follow[i] = null;
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
        Global.HeroBlood = 8;
        Global.HeroType = Global.A;
    }
	void Update()
	{
		if (Global.isCameraFollowHero == true) {
			Move ();
		}
        /*
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
    
        // 更新最新的游戏位置
		offset[0][Pause-Delay] = transform.position - lastPos;
		lastPos = transform.position;

        if(Global.HeroState == Global.Distributed)
        {
            //开始移动
            for (int i = 1; i < Global.HeroBlood; i++)
            {
                follow[i].transform.Translate(offset[i][Pause - Delay]);
            }
        }


        //延时--
        Delay--;
        */
	}

    void ChangeToDis()
    {
        SetFollowSubHero();
    }

    void MergeToTotal()
    {
        for(int i = 1; i < Global.HeroBlood; i++)
        {
            if(follow[i] != null)
            {
                Destroy(follow[i]);
                follow[i] = null;
            }
        }
        transform.localScale = 1.667f * transform.lossyScale;
    }

    public void SetFollowSubHero(){
        for (int i = 1; i < Global.HeroBlood; i++)
        {
            if(follow[i] == null)
            {
                //新建物体
                GameObject follower = Instantiate(SubHero);
                follower.SetActive(true);

                //生成孩子的大小，随机大小  0.45 -- 0.65
                Vector3 MyScale = transform.lossyScale * Random.Range(0.4f, 0.7f);
                follower.transform.localScale = MyScale;
                
                //生成孩子的位置
                Vector3 MyOffset = new Vector3(Random.Range(0.6f, 1.7f), Random.Range(1.5f, 2.4f), Random.Range(0.6f, 1.7f));
                
                //决定是 1 or -1
                float flagX = Random.Range(0f, 1f);
                float flagZ = Random.Range(0f, 1f);
                //Debug.Log(flagX);
                flagX = flagX > 0.5 ? 1 : -1;
                flagZ = flagZ > 0.5 ? 1 : -1;

                //判断是否是
                MyOffset = new Vector3(flagX * MyOffset[0], MyOffset[1], MyOffset[2] * flagZ);
                MyOffset = transform.position + MyOffset * Global.ChildRadius;
                follower.transform.position = MyOffset;

                follower.tag = "Untagged";
                //follower.GetComponent<Rigidbody>().isKinematic = true;
                follow[i] = follower;
            }
        }

        transform.localScale = 0.6f * transform.lossyScale;
        transform.Translate(0, 1.2f, 0);

        //删除多余的孩子
        for (int i = Global.HeroBlood; i < Global.MaxHeroBlood; i++)
        {
            if(follow[i] != null)
            {
                Destroy(follow[i]);
                follow[i] = null;
            }
        }
            
    }


	void Move()
	{
        //AD移动
		if (Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(1,0,0) * xspeed * Time.deltaTime, Space.World);
            if (Global.HeroState == Global.Distributed)
            {
                for (int i = 1; i < Global.HeroBlood; i++)
                {
                    Debug.Log(i);
                    follow[i].transform.Translate(new Vector3(1, 0, 0) * xspeed * Time.deltaTime, Space.World);
                }
            }
        }
		if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(-1 * new Vector3(1, 0, 0) * xspeed * Time.deltaTime, Space.World);
            if (Global.HeroState == Global.Distributed)
            {
                for (int i = 1; i < Global.HeroBlood; i++)
                {
                    follow[i].transform.Translate(-1 * new Vector3(1, 0, 0) * xspeed * Time.deltaTime, Space.World);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)  || Input.GetKeyDown(KeyCode.RightShift))
        {
            PauseTime++;
        }
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            PauseTime++;
            if(PauseTime >= SetShiftPause)
            {
                if(Global.HeroState == Global.Distributed)
                {
                    Global.HeroState = Global.Total;
                    MergeToTotal();
                }
                else
                {
                    Global.HeroState = Global.Distributed;
                    ChangeToDis();
                }
                PauseTime = 0;
            }
        }
        else
        {
            PauseTime = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpNum++;
            if (JumpNum > 2) return;
            if (JumpNum == 1)
            {
                rb.AddForce(Vector3.up * force);
                if (Global.HeroState == Global.Distributed)
                {
                    for (int i = 1; i < Global.HeroBlood; i++)
                    {
                        Rigidbody subRB = follow[i].GetComponent<Rigidbody>();
                        subRB.AddForce(Vector3.up * force);
                    }
                }
            }
            else if (JumpNum == 2)
            {

                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * force);
                if (Global.HeroState == Global.Distributed)
                {
                    for (int i = 1; i < Global.HeroBlood; i++)
                    {
                        Rigidbody subRB = follow[i].GetComponent<Rigidbody>();
                        subRB.velocity = new Vector3(subRB.velocity.x, 0, subRB.velocity.z);
                        subRB.AddForce(Vector3.up * force);
                    }
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "WalkableRood")//碰撞的是quad  
        {

            JumpNum = 0;
        }
    }
}


