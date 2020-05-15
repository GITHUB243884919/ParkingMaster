using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class sceneConfig
	{
		private sceneConfig(){ 
		}
		private static sceneConfig _inst;
		public static sceneConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new sceneConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,sceneCell> AllData;
		public sceneCell getCell(string key){
			sceneCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public sceneCell getCell(int key){
			sceneCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 25;
		private void InitData(){
			this.AllData = new Dictionary<string,sceneCell> ();
			this.AllData.Add("0",new sceneCell("Ui_Text_103","海岛1","Ui_Text_115",new int[]{10101,10301,10601,10801,11001},1,1,0,"UIAtlas/UIIcon/Tiger","dwy_9",1,1,1,0,0,new int[]{80,10,179,3,6},0));
			this.AllData.Add("1",new sceneCell("Ui_Text_103","海岛2","Ui_Text_115",new int[]{20101,20301,20601,20801,21001},1,2,13,"UIAtlas/UIIcon/Tiger","dwy_9",4,1,0,0,0,new int[]{280,10,394,6,30},0));
			this.AllData.Add("2",new sceneCell("Ui_Text_103","海岛3","Ui_Text_115",new int[]{30101,30301,30601,30801,31001},1,3,97,"UIAtlas/UIIcon/Tiger","dwy_9",16,1,0,0,0,new int[]{228,12,380,15,10},0));
			this.AllData.Add("3",new sceneCell("Ui_Text_103","海岛4","Ui_Text_115",new int[]{40101,40301,40601,40801,41001},1,4,203,"UIAtlas/UIIcon/Tiger","dwy_9",64,1,0,0,0,new int[]{320,20,650,12,28},0));
			this.AllData.Add("4",new sceneCell("Ui_Text_103","海岛5","Ui_Text_115",new int[]{50101,50301,50601,50801,51001},1,5,353,"UIAtlas/UIIcon/Tiger","dwy_9",256,1,0,0,0,new int[]{700,28,950,11,40},0));
			this.AllData.Add("5",new sceneCell("Ui_Text_104","城市1","Ui_Text_116",new int[]{60101,60201,60401,60701,60801},2,1,800,"UIAtlas/UIIcon/Tiger","city_scene",1,1,0,0,1,new int[]{150,15,300,6,15},0));
			this.AllData.Add("6",new sceneCell("Ui_Text_104","城市2","Ui_Text_116",new int[]{70101,70201,70401,70701,70801},2,2,900,"UIAtlas/UIIcon/Tiger","city_scene",4,1,0,0,1,new int[]{200,20,560,12,2},0));
			this.AllData.Add("7",new sceneCell("Ui_Text_104","城市3","Ui_Text_116",new int[]{80101,80201,80401,80701,80801},2,3,1000,"UIAtlas/UIIcon/Tiger","city_scene",16,1,0,0,1,new int[]{200,20,500,15,22},0));
			this.AllData.Add("8",new sceneCell("Ui_Text_104","城市4","Ui_Text_116",new int[]{90101,90201,90401,90701,90801},2,4,1100,"UIAtlas/UIIcon/Tiger","city_scene",64,1,0,0,1,new int[]{200,20,520,16,24},0));
			this.AllData.Add("9",new sceneCell("Ui_Text_104","城市5","Ui_Text_116",new int[]{100101,100201,100401,100701,100801},2,5,1200,"UIAtlas/UIIcon/Tiger","city_scene",256,1,0,0,1,new int[]{200,20,520,16,27},0));
			this.AllData.Add("10",new sceneCell("Ui_Text_105","沙漠1","Ui_Text_117",new int[]{110101,110201,110401,110601,111001},3,1,1300,"UIAtlas/UIIcon/Tiger","west_scene",1,1,0,0,2,new int[]{150,15,300,6,15},1));
			this.AllData.Add("11",new sceneCell("Ui_Text_105","沙漠2","Ui_Text_117",new int[]{120101,120201,120401,120601,121001},3,2,1400,"UIAtlas/UIIcon/Tiger","west_scene",4,1,0,0,2,new int[]{200,20,560,12,2},1));
			this.AllData.Add("12",new sceneCell("Ui_Text_105","沙漠3","Ui_Text_117",new int[]{130101,130201,130401,130601,131001},3,3,1500,"UIAtlas/UIIcon/Tiger","west_scene",16,1,0,0,2,new int[]{200,20,500,15,22},1));
			this.AllData.Add("13",new sceneCell("Ui_Text_105","沙漠4","Ui_Text_117",new int[]{140101,140201,140401,140601,141001},3,4,1600,"UIAtlas/UIIcon/Tiger","west_scene",64,1,0,0,2,new int[]{200,20,520,16,24},1));
			this.AllData.Add("14",new sceneCell("Ui_Text_105","沙漠5","Ui_Text_117",new int[]{150101,150201,150401,150601,151001},3,5,1700,"UIAtlas/UIIcon/Tiger","west_scene",256,1,0,0,2,new int[]{200,20,520,16,27},1));
			this.AllData.Add("15",new sceneCell("Ui_Text_106","绿洲1","Ui_Text_119",new int[]{160101,160401,160601,160701,160801},4,1,1800,"UIAtlas/UIIcon/Tiger","dwy_9_m",1,0,0,0,3,new int[]{150,15,300,6,15},0));
			this.AllData.Add("16",new sceneCell("Ui_Text_106","绿洲2","Ui_Text_119",new int[]{170101,170401,170601,170701,170801},4,2,1900,"UIAtlas/UIIcon/Tiger","dwy_9_m",4,0,0,0,3,new int[]{200,20,560,12,2},0));
			this.AllData.Add("17",new sceneCell("Ui_Text_106","绿洲3","Ui_Text_119",new int[]{180101,180401,180601,180701,180801},4,3,2000,"UIAtlas/UIIcon/Tiger","dwy_9_m",16,0,0,0,3,new int[]{200,20,500,15,22},0));
			this.AllData.Add("18",new sceneCell("Ui_Text_106","绿洲4","Ui_Text_119",new int[]{190101,190401,190601,190701,190801},4,4,2100,"UIAtlas/UIIcon/Tiger","dwy_9_m",64,0,0,0,3,new int[]{200,20,520,16,24},0));
			this.AllData.Add("19",new sceneCell("Ui_Text_106","绿洲5","Ui_Text_119",new int[]{200101,200401,200601,200701,200801},4,5,2200,"UIAtlas/UIIcon/Tiger","dwy_9_m",256,0,0,0,3,new int[]{200,20,520,16,27},0));
			this.AllData.Add("20",new sceneCell("Ui_Text_107","花海1","Ui_Text_119",new int[]{160101,160401,160601,160701,160801},5,1,2300,"UIAtlas/UIIcon/Tiger","dwy_9_m",1,0,0,0,4,new int[]{150,15,300,6,15},0));
			this.AllData.Add("21",new sceneCell("Ui_Text_107","花海2","Ui_Text_119",new int[]{170101,170401,170601,170701,170801},5,2,2400,"UIAtlas/UIIcon/Tiger","dwy_9_m",4,0,0,0,4,new int[]{200,20,560,12,2},0));
			this.AllData.Add("22",new sceneCell("Ui_Text_107","花海3","Ui_Text_119",new int[]{180101,180401,180601,180701,180801},5,3,2500,"UIAtlas/UIIcon/Tiger","dwy_9_m",16,0,0,0,4,new int[]{200,20,500,15,22},0));
			this.AllData.Add("23",new sceneCell("Ui_Text_107","花海4","Ui_Text_119",new int[]{190101,190401,190601,190701,190801},5,4,2600,"UIAtlas/UIIcon/Tiger","dwy_9_m",64,0,0,0,4,new int[]{200,20,520,16,24},0));
			this.AllData.Add("24",new sceneCell("Ui_Text_107","花海5","Ui_Text_119",new int[]{200101,200401,200601,200701,200801},5,5,2700,"UIAtlas/UIIcon/Tiger","dwy_9_m",256,0,0,0,4,new int[]{200,20,520,16,27},0));
		}
	}
	public class sceneCell
	{
		///<summary>
		///场景名称
		///<summary>
		public readonly string scenename;
		///<summary>
		///ta调用名称
		///<summary>
		public readonly string tascenename;
		///<summary>
		///场景说明文本
		///<summary>
		public readonly string scenetips;
		///<summary>
		///场景包含动物id
		///<summary>
		public readonly int[] sceneanimal;
		///<summary>
		///场景类型
		///<summary>
		public readonly int scenetype;
		///<summary>
		///场景顺序
		///<summary>
		public readonly int sceneorder;
		///<summary>
		///开启星级
		///<summary>
		public readonly int openstar;
		///<summary>
		///图标
		///<summary>
		public readonly string icon;
		///<summary>
		///场景资源加载
		///<summary>
		public readonly string resourceid;
		///<summary>
		///翻倍系数
		///<summary>
		public readonly int doublenum;
		///<summary>
		///版本开放控制
		///<summary>
		public readonly int israwopen;
		///<summary>
		///起始任务ID
		///<summary>
		public readonly int missionstart;
		///<summary>
		///场景等级
		///<summary>
		public readonly int scenelv;
		///<summary>
		///关联货币
		///<summary>
		public readonly int moneyid;
		///<summary>
		///等级系数
		///<summary>
		public readonly int[] lvcoefficient;
		///<summary>
		///额外游客入场路线
		///<summary>
		public readonly int visitorpath;
		public sceneCell(string scenename,string tascenename,string scenetips,int[] sceneanimal,int scenetype,int sceneorder,int openstar,string icon,string resourceid,int doublenum,int israwopen,int missionstart,int scenelv,int moneyid,int[] lvcoefficient,int visitorpath){
			this.scenename = scenename;
			this.tascenename = tascenename;
			this.scenetips = scenetips;
			this.sceneanimal = sceneanimal;
			this.scenetype = scenetype;
			this.sceneorder = sceneorder;
			this.openstar = openstar;
			this.icon = icon;
			this.resourceid = resourceid;
			this.doublenum = doublenum;
			this.israwopen = israwopen;
			this.missionstart = missionstart;
			this.scenelv = scenelv;
			this.moneyid = moneyid;
			this.lvcoefficient = lvcoefficient;
			this.visitorpath = visitorpath;
		}
	}
}