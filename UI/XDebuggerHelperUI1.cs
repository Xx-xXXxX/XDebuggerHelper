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
			UIDrag.SetRightDrag(Switch);
		}
	}
	public class XDebuggerHelperUI1:UIState
	{
		public static bool Using=false;
		private UIPanel area;
		private UIPanel area2;
		private UIPanel area3;
		public UINamedSwitch XDebuggerSwitch;
		public UIScrollbar uIScrollbar;
		public UIDrag uIDrag;
		public override void OnInitialize()
		{
			area = new UIPanel();
			area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(250, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(250, 0f);
			area.BackgroundColor= new Color(73, 94, 171);
			Append(area);

			area3 = new UIPanel();
			area3.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			area3.Top.Set(0, 0f); // Placing it just a bit below the top of the screen.
			area3.Width.Set(-area3.Left.Pixels, 1f); // We will be placing the following 2 UIElements within this 182x60 area.
			area3.Height.Set(40, 0f);
			area3.BackgroundColor = new Color(73, 94, 171);
			area.Append(area3);

			XDebuggerSwitch = new UINamedSwitch("XDebuggerMode",new RefByDelegate<bool>(()=>XDebugger.DebugMode,(value)=> {
				if (value) XDebugger.DebugMode = true;
				else XDebugger.CloseDebugMode();
			}),null);
			XDebuggerSwitch.Top.Set(1, 0f);
			XDebuggerSwitch.Left.Set(1, 0f);
			XDebuggerSwitch.Width.Set(-XDebuggerSwitch.Left.Pixels-16, 1f);
			XDebuggerSwitch.Height.Set(32, 0f);
			area3.Append(XDebuggerSwitch);

			area2 = new UIPanel();
			area2.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			area2.Top.Set(40, 0f); // Placing it just a bit below the top of the screen.
			area2.Width.Set(-area2.Left.Pixels, 1f); // We will be placing the following 2 UIElements within this 182x60 area.
			area2.Height.Set(-area2.Top.Pixels, 1f);
			area2.BackgroundColor = new Color(73, 94, 171);
			area.Append(area2);

			Items = new UIList();
			Items.Top.Set(1, 0f);
			Items.Left.Set(1, 0f);
			Items.Width.Set(-Items.Left.Pixels-16, 1f);
			Items.Height.Set(-Items.Top.Pixels, 1f);
			Items.ListPadding = 5f;
			area2.Append(Items);

			uIScrollbar = new UIScrollbar();
			uIScrollbar.SetView(100f, 1000f);
			uIScrollbar.Height.Set(0, 1f);
			uIScrollbar.HAlign = 1f;
			area2.Append(uIScrollbar);
			Items.SetScrollbar(uIScrollbar);

			uIDrag = new UIDrag();
			area.Append(uIDrag);
			uIDrag.LeftMouse = false;
			uIDrag.RightMouse = true;
			uIDrag.ThroughAll = true;
			uIDrag.Active = true;
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
			base.Update(gameTime);
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
			
			area = new UIPanel();
			area.Left.Set(0, 0f);
			area.Top.Set(0, 0f);
			area.Width.Set(0, 1f);
			area.Height.Set(0, 1f);
			area.BackgroundColor = new Color(73, 94, 171);

			Switch = new UISwitch(new RefByDelegate<bool>(() => tryGetXDebugger.XDebuggerMode == 2, (value) => { if (tryGetXDebugger.XDebuggerMode != 0) tryGetXDebugger.xDebugger.Using = value; }), new GetByDelegate<string>(
					() => {
						return Language.GetTextValue($"Mods.XDebuggerHelper.tryGetXDebugger_XDebuggerMode_{tryGetXDebugger.XDebuggerMode}");
					}
				));
			Switch.SetCenter(new Vector2(6.5f, 6.5f), new Vector2(0f, 0.5f));
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
