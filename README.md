## Can't Stop Medicine ##

#### 1、 Global ####
		
		//主角类型
		const int A = 0;
		const int B = 1;
		const int C = 2;
		
		//主角变量
		int HeroBlood      	// 主角血量  决定后面的跟班个数
		int HeroType	  	// 主角类型  A or B or C
		int HeroResistance  // 主角抗药性 0-100 百分比免伤
		

		//全局保存的变量
		//场景保存可以使用不关闭场景的方式	

#### 2、Move（处理WSAD） ####
	
	//区别未知
	Amove：
	Bmove：
	Cmove：

#### 3、Follow ####

跟随效果（延时跟随），跟随数量代表主角的血量，根据`Global.HeroBlood`来确定显示的个数。

