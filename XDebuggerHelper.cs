using Terraria.ModLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using XxDefinitions;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.UI;
using Microsoft.Xna.Framework;
using Terraria;

namespace XDebuggerHelper
{
	public class XDebuggerHelper : Mod
	{
		public static StaticRefWithFunc<LogWithUsing> L1 = new StaticRefWithFunc<LogWithUsing>(()=>new LogWithUsing("XDebuggerHelper.L1",true));
		public override void Load()
		{
			ModTranslation Translations = this.CreateTranslation("tryGetXDebugger_XDebuggerMode_0");
			Translations.SetDefault("Inexist");
			Translations.AddTranslation(GameCulture.Chinese, "不存在");
			this.AddTranslation(Translations);
			Translations = this.CreateTranslation("tryGetXDebugger_XDebuggerMode_1");
			Translations.SetDefault("Disabled");
			Translations.AddTranslation(GameCulture.Chinese, "已禁用");
			this.AddTranslation(Translations);
			Translations = this.CreateTranslation("tryGetXDebugger_XDebuggerMode_2");
			Translations.SetDefault("Enabled");
			Translations.AddTranslation(GameCulture.Chinese, "已启用");
			this.AddTranslation(Translations);

			uI1 = new UI.XDebuggerHelperUI1();
			uI1.Activate();
			UI1I = new UserInterface();
			UI1I.SetState(uI1);
			uI1U = new UI.XDebuggerHelperUI1Using();
			uI1U.Activate();
			UI1UI = new UserInterface();
			UI1UI.SetState(uI1U);
		}
		public UserInterface UI1I;
		public UI.XDebuggerHelperUI1 uI1;

		public UserInterface UI1UI;
		public UI.XDebuggerHelperUI1Using uI1U;
		public override void UpdateUI(GameTime gameTime)
		{
			if (UI.XDebuggerHelperUI1.Using) UI1I?.Update(gameTime);
			if (UI.XDebuggerHelperUI1Using.Using) UI1UI?.Update(gameTime);
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"XDebuggerHelper: ShowXDebuggers",
					delegate
					{
						if (UI.XDebuggerHelperUI1.Using)
						{
							UI1I.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"XDebuggerHelper: ShowXDebuggers",
					delegate
					{
						if (UI.XDebuggerHelperUI1Using.Using)
						{
							UI1UI.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}
