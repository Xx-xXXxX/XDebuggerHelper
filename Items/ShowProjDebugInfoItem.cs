﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace XDebuggerHelper.Items
{
	public class ShowProjDebugInfoItem:ModItem
	{
		public override string Texture => (GetType().Namespace + "." + "SparklingSphere").Replace('.', '/');
		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.rare = ItemRarityID.Gray;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.autoReuse = true;
		}
		public override void HoldItem(Player player)
		{
			Rectangle UsingRect = new Rectangle((int)Main.MouseWorld.X- Size/2, (int)Main.MouseWorld.Y- Size/2, Size, Size);
			foreach (var i in Main.projectile) {
				if (i.active) {
					if (i.Colliding(i.Hitbox,UsingRect)) {
						XxDefinitions.XDebugger.ShowProjDebugInfo info = i.GetGlobalProjectile<XxDefinitions.XDebugger.ShowProjDebugInfo>();
						if (info.DrawTimeLeft <= 0) info.DrawTimeLeft = 1;
						info.DrawTimeLeft += 1;
					}
				}
			}
		}
		public static int Size = 16;
		public override bool UseItem(Player player)
		{
			Rectangle UsingRect = new Rectangle((int)Main.MouseWorld.X - Size / 2, (int)Main.MouseWorld.Y - Size / 2, Size, Size);
			foreach (var i in Main.projectile)
			{
				if (i.active)
				{
					if (i.Colliding(i.Hitbox, UsingRect))
					{
						XxDefinitions.XDebugger.ShowProjDebugInfo info = i.GetGlobalProjectile<XxDefinitions.XDebugger.ShowProjDebugInfo>();
						if (info.DrawTimeLeft <= 0) info.DrawTimeLeft = 1;
						info.DrawTimeLeft += 60;
					}
				}
			}
			return true;
		}
		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Terraria.Utils.DrawRect(spriteBatch, new Rectangle((int)Main.MouseWorld.X - 4, (int)Main.MouseWorld.Y - 4, 8, 8), Color.GreenYellow);
		}
	}
}
