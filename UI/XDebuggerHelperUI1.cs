using Microsoft.Xna.Framework;
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
using Terraria.UI;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using XxDefinitions;
using XxDefinitions.UIElements;
using XxDefinitions.XDebugger;
namespace XDebuggerHelper.UI
{
	public class XDebuggerHelperUI1Using : UIState {
		public static bool Using = true;
		public UISwitch Switch;
		public override void OnInitialize()
		{
			Switch = new UISwitch(
				new RefByDelegate<bool>(
						() => XDebuggerHelperUI1.Using,
						(value)=> XDebuggerHelperUI1.Using=value
					),
		new ClassValue<string>("XDebuggerHelperUI1")
				) ;
			Switch.Left.Set(-Switch.Width.Pixels - 610, 1f); // Place the resource bar to the left of the hearts.
			Switch.Top.Set(37, 0f); // Placing it just a bit below the top of the screen.
			Append(Switch);
		}
	}
	public class XDebuggerHelperUI1:UIState
	{
		public static bool Using=false;
		private UIPanel area;
		public UINamedSwitch XDebuggerSwitch;
		public UIScrollbar uIScrollbar;
		public override void OnInitialize()
		{
			area = new UIPanel();
			area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(250, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(250, 0f);
			area.BackgroundColor= new Color(73, 94, 171);

			XDebuggerSwitch = new UINamedSwitch(new UISwitch(new RefByDelegate<bool>(()=>XDebugger.DebugMode,(value)=> {
				if (value) XDebugger.DebugMode = true;
				else XDebugger.CloseDebugMode();
			}),null),"XDebuggerMode");
			XDebuggerSwitch.Top.Set(1, 0f);
			XDebuggerSwitch.Left.Set(1, 0f);
			XDebuggerSwitch.Width.Set(-XDebuggerSwitch.Left.Pixels, 1f);
			XDebuggerSwitch.Height.Set(16, 0f);
			area.Append(XDebuggerSwitch);

			Items = new UIList();
			Items.Top.Set(16, 0f);
			Items.Left.Set(6, 0f);
			Items.Width.Set(-Items.Left.Pixels, 1f);
			Items.Height.Set(-Items.Top.Pixels, 1f);
			Items.ListPadding = 5f;
			area.Append(Items);

			uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(-1f-16f, 1f);
			uIScrollbar.Top.Set(16f, 0f);
			uIScrollbar.Left.Set(1, 0f);
			uIScrollbar.HAlign = 1f;
			area.Append(uIScrollbar);
			Items.SetScrollbar(uIScrollbar);

			Append(area);
		}
		public bool XDebuggerUsing {
			get => XDebugger.DebugMode;
			set {
				if (!value)XDebugger.CloseDebugMode();
				else XDebugger.DebugMode = true;
			}
		}
		public List<TryGetXDebugger> AnnouncedDebuggers => XDebugger.AnnouncedDebuggers.Value;
		public UIList Items;
		public override void Update(GameTime gameTime)
		{
			//XDebuggerHelper.L1.Value.Debug(Items._items.Count);
			if (Items == null&& area!=null) {
			}
			if (AnnouncedDebuggers .Count!=Items._items.Count) {
				Items.Clear();
				foreach (var i in AnnouncedDebuggers) {
					ShowXDebugger showXDebugger = new ShowXDebugger(i);
					showXDebugger.Activate();
					Items.Add(showXDebugger);
				}
				
				Items.SetScrollbar(uIScrollbar);
				Items.Recalculate();

			}
		}
	}
	public class ShowXDebugger : UIElement {
		public UISwitch Switch;
		public TryGetXDebugger tryGetXDebugger;
		public UIPanel area;
		public string FullName;
		public UIText text;
		public ShowXDebugger(string FullName) {
			this.FullName = FullName;
			tryGetXDebugger = TryGetXDebugger.GetTryGetXDebugger(FullName);
		}
		public ShowXDebugger(TryGetXDebugger tryGetXDebugger) {
			this.tryGetXDebugger = tryGetXDebugger;
			this.FullName = tryGetXDebugger.FullName;
		}
		public override void OnInitialize()
		{
			Switch = new UISwitch(new RefByDelegate<bool>(() => tryGetXDebugger.XDebuggerMode == 2, (value)=>{ if (tryGetXDebugger.XDebuggerMode != 0) tryGetXDebugger.xDebugger.Using = value; }),new GetByDelegate<string>(
					()=> {
						return Language.GetTextValue($"Mods.XDebuggerHelper.tryGetXDebugger_XDebuggerMode_{tryGetXDebugger.XDebuggerMode}");
					}
				));
			Switch.SetCenter(new Vector2(6.5f,6.5f),new Vector2(0f,0.5f));
			area = new UIPanel();
			area.Left.Set(0, 0f);
			area.Top.Set(0, 0f);
			area.Width.Set(0, 1f);
			area.Height.Set(0, 1f);
			area.BackgroundColor = new Color(73, 94, 171);
			area.Append(Switch);
			text = new UIText(FullName);
			text.Left.Set(14,0f);
			text.Top.Set(0, 0f);
			text.Width.Set(320,0f);
			text.Height.Set(34, 0f);
			area.Append(text);
			Append(area);
			Left.Set(0, 0f);
			Top.Set(0, 0f);
			Height.Set(32, 0f);
			Width.Set(200, 0f);
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (tryGetXDebugger.XDebuggerMode == 0) area.BackgroundColor = Color.Red;
			else if (tryGetXDebugger.XDebuggerMode == 1) area.BackgroundColor = new Color(73, 94, 171);
			else area.BackgroundColor = Color.Green;
			base.Draw(spriteBatch);
		}
	}
	public class UIHoverImageButton : UIImageButton
	{
		internal XxDefinitions.IGetSetValue< string> HoverText;

		public UIHoverImageButton(Texture2D texture, XxDefinitions.IGetSetValue<string> hoverText) : base(texture)
		{
			HoverText = hoverText;
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);

			if (IsMouseHovering)
			{
				Main.hoverItemName = HoverText.Value;
			}
		}
	}
}
