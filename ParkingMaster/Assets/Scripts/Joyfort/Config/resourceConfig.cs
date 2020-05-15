using UnityEngine;
using System;
using System.Security;
using System.Collections.Generic;
namespace Config
{
	public class resourceConfig
	{
		private resourceConfig(){ 
		}
		private static resourceConfig _inst;
		public static resourceConfig getInstace(){
			if (_inst != null) {
				return _inst;
			}
			_inst = new resourceConfig ();
			_inst.InitData ();
			return _inst;
		}
		public Dictionary<string,resourceCell> AllData;
		public resourceCell getCell(string key){
			resourceCell t = null;
			this.AllData.TryGetValue (key, out t);
			return t;
		}
		public resourceCell getCell(int key){
			resourceCell t = null;
			this.AllData.TryGetValue (key.ToString(), out t);
			return t;
		}
		public readonly int RowNum = 23;
		private void InitData(){
			this.AllData = new Dictionary<string,resourceCell> ();
			this.AllData.Add("1",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_01/Car_T_01","",-1f,0f,0f,0f));
			this.AllData.Add("2",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_02/Car_T_02","",-1f,0f,0f,0f));
			this.AllData.Add("3",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_03/Car_T_03","",-1f,0f,0f,0f));
			this.AllData.Add("4",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_04/Car_T_04","",-1f,0f,0f,0f));
			this.AllData.Add("5",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_05/Car_T_05","",-1f,0f,0f,0f));
			this.AllData.Add("6",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_06/Car_T_06","",-1f,0f,0f,0f));
			this.AllData.Add("7",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_07/Car_T_07","",-1f,0f,0f,0f));
			this.AllData.Add("8",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_08/Car_T_08","",-1f,0f,0f,0f));
			this.AllData.Add("9",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_09/Car_T_09","",-1f,0f,0f,0f));
			this.AllData.Add("10",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_10/Car_T_10","",-1f,0f,0f,0f));
			this.AllData.Add("11",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_11/Car_T_11","",-1f,0f,0f,0f));
			this.AllData.Add("12",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_12/Car_T_12","",-1f,0f,0f,0f));
			this.AllData.Add("13",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_13/Car_T_13","",-1f,0f,0f,0f));
			this.AllData.Add("14",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_14/Car_T_14","",-1f,0f,0f,0f));
			this.AllData.Add("15",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_15/Car_T_15","",-1f,0f,0f,0f));
			this.AllData.Add("16",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_16/Car_T_16","",-1f,0f,0f,0f));
			this.AllData.Add("17",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_17/Car_T_17","",-1f,0f,0f,0f));
			this.AllData.Add("18",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_18/Car_T_18","",-1f,0f,0f,0f));
			this.AllData.Add("19",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_19/Car_T_19","",-1f,0f,0f,0f));
			this.AllData.Add("20",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_20/Car_T_20","",-1f,0f,0f,0f));
			this.AllData.Add("21",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_21/Car_T_21","",-1f,0f,0f,0f));
			this.AllData.Add("22",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_22/Car_T_22","",-1f,0f,0f,0f));
			this.AllData.Add("23",new resourceCell("小汽车",1,1,"ResourcesDepend-box/Car_T_23/Car_T_23","",-1f,0f,0f,0f));
		}
	}
	public class resourceCell
	{
		///<summary>
		///备注
		///<summary>
		public readonly string remarks;
		///<summary>
		///随机类型
		///<summary>
		public readonly int restype;
		///<summary>
		///加载类型
		///<summary>
		public readonly int loadtype;
		///<summary>
		///车预制体路径
		///<summary>
		public readonly string prefabpath;
		///<summary>
		///车贴图路径
		///<summary>
		public readonly string texturepath;
		///<summary>
		///缩放比例
		///<summary>
		public readonly float zoomratio;
		///<summary>
		///动物展示X轴偏移
		///<summary>
		public readonly float Xoffset;
		///<summary>
		///动物展示Y轴偏移
		///<summary>
		public readonly float Yoffset;
		///<summary>
		///动物展示Z轴偏移
		///<summary>
		public readonly float Zoffset;
		public resourceCell(string remarks,int restype,int loadtype,string prefabpath,string texturepath,float zoomratio,float Xoffset,float Yoffset,float Zoffset){
			this.remarks = remarks;
			this.restype = restype;
			this.loadtype = loadtype;
			this.prefabpath = prefabpath;
			this.texturepath = texturepath;
			this.zoomratio = zoomratio;
			this.Xoffset = Xoffset;
			this.Yoffset = Yoffset;
			this.Zoffset = Zoffset;
		}
	}
}