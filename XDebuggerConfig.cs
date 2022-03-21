using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

using XxDefinitions.XDebugger;
using Newtonsoft.Json;

namespace XDebuggerHelper
{
	//public class XDebuggerConfig : ModConfig
	//{
	//	public override bool NeedsReload(ModConfig pendingConfig)
	//	{
	//		return true;
	//	}
	//	public override ConfigScope Mode => ConfigScope.ClientSide;
	//	[JsonIgnore]
	//	[Label("XDebugger.DebugMode")]
	//	public bool DebugMode { 
	//		get=>XxDefinitions.XDebugger.XDebugger.DebugMode;
	//		set {
	//			if (!value)
	//			{
	//				XxDefinitions.XDebugger.XDebugger.CloseDebugMode();
	//			}
	//			else {
	//				XxDefinitions.XDebugger.XDebugger.DebugMode = true;
	//			}
	//		}
	//	}
	//	[JsonIgnore]
	//	[Label("所有公布的XDebugger")]
	//	[Tooltip("因为一些问题，不要添加或删除")]
	//	public List<XDebuggerShown> AnnouncedXDebuggerList {
	//		get => XDebuggerHelper.xDebuggerShowns.Value;
	//	}
	//	[JsonIgnore]
	//	[Label("所有公布的XDebugger2")]
	//	[CustomModConfigItem(typeof(Terraria.ModLoader.Config.UI.A))]
	//	public XDebuggerShown[] AnnouncedXDebuggerList2
	//	{
	//		get => XDebuggerHelper.xDebuggerShowns2.Value;
	//	}
	//}
	//public class XDebuggerShown {
	//	public readonly string FullName;
	//	private readonly XxDefinitions.XDebugger.TryGetXDebugger tryGetXDebugger;
	//	public bool Using {
	//		get {
	//			if (tryGetXDebugger.XDebuggerMode == 2) return tryGetXDebugger.xDebugger.Using;
	//			else return false;
	//		}
	//		set {
	//			if (tryGetXDebugger.XDebuggerMode == 2) tryGetXDebugger.xDebugger.Using = value;
	//		}
	//	}
	//	public XDebuggerShown(string FullName) {
	//		this.FullName = FullName;
	//		this.tryGetXDebugger = XxDefinitions.XDebugger.TryGetXDebugger.GetTryGetXDebugger(FullName);
	//	}
	//}
	public class XDebuggerHelperConfig : ModConfig
	{
		public override ConfigScope Mode =>ConfigScope.ClientSide;
		[Label("UISwitchMode")]
		public bool UIMode
		{
			get => UI.XDebuggerHelperUI1Using.Using;
			set
			{
				UI.XDebuggerHelperUI1Using.Using = value;
			}
		}
	}

}
