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
		public List<TryGetXDebugger> AnnouncedDebuggers => XDebugger.AnnouncedDebuggers.Value;
		public static bool Using=false;
		private UIPanel _area;
		private UIPanel Aarea2;
		public UIRightNamedSwitch AXDebuggerSwitch;
		public UIScrollbar AuIScrollbar;
		public UIDrag uIDrag;
		public UIList AItems;
		private UIPanel Barea;
		public UIList BItems;
		public UIScrollbar BuIScrollbar;
		public int W1 = 350;
		public int W2 = 250;
		public int H1 = 32;
		public int H2=250;
		public override void OnInitialize()
		{
			_area = new UIPanel();
			_area.Width.Set(W1+W2, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			_area.Left.Set(-_area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
			_area.Top.Set(30, 0f); // Placing it just a bit below the top of the screen.
			_area.Height.Set(H2, 0f);
			_area.BackgroundColor = new Color(73, 94, 171);
			Append(_area);

			uIDrag = new UIDrag();
			_area.Append(uIDrag);
			uIDrag.Active = true;

			AXDebuggerSwitch = new UIRightNamedSwitch("XDebuggerMode",new RefByDelegate<bool>(()=>XDebugger.DebugMode,(value)=> {
				if (value) XDebugger.DebugMode = true;
				else XDebugger.CloseDebugMode();
			}),null);
			_area.Append(AXDebuggerSwitch);

			Aarea2 = new UIPanel();
			Aarea2.Left.Set(0, 0f); // Place the resource bar to the left of the hearts.
			Aarea2.Top.Set(H1, 0f); // Placing it just a bit below the top of the screen.
			Aarea2.Width.Set(W1, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			Aarea2.Height.Set(-Aarea2.Top.Pixels, 1f);
			Aarea2.BackgroundColor = new Color(73, 94, 171);
			_area.Append(Aarea2);

			AItems = new UIList();
			AItems.Top.Set(0, 0f);
			AItems.Left.Set(0, 0f);
			AItems.Width.Set(-16, 1f);
			AItems.Height.Set(0, 1f);
			AItems.ListPadding = 5f;
			Aarea2.Append(AItems);

			AuIScrollbar = new UIScrollbar();
			AuIScrollbar.SetView(100f, 1000f);
			AuIScrollbar.Height.Set(0, 1f);
			AuIScrollbar.HAlign = 1f;
			Aarea2.Append(AuIScrollbar);
			AItems.SetScrollbar(AuIScrollbar);

			Barea = new UIPanel();
			Barea.Top.Set(0, 0f); 
			Barea.Height.Set(0, 1f);
			Barea.Left.Set(W1, 0f); // Place the resource bar to the left of the hearts.
			Barea.Width.Set(-Barea.Left.Pixels, 1f); // We will be placing the following 2 UIElements within this 182x60 area.
			Barea.BackgroundColor = new Color(73, 94, 171);
			_area.Append(Barea);

			BItems = new UIList();
			BItems.Top.Set(0, 0f);
			BItems.Left.Set(0, 0f);
			BItems.Width.Set( - 16, 1f);
			BItems.Height.Set(0, 1f);
			BItems.ListPadding = 5f;
			Barea.Append(BItems);

			BuIScrollbar = new UIScrollbar();
			BuIScrollbar.SetView(100f, 1000f);
			BuIScrollbar.Height.Set(0, 1f);
			BuIScrollbar.HAlign = 1f;
			Barea.Append(BuIScrollbar);
			BItems.SetScrollbar(BuIScrollbar);

			UIRightNamedSwitch SwitchUI;
			IGetSetValue<bool> SwitchV = new RefByDelegate<bool>(() => ShowNPCDebugInfo.ShowAlways, (v) => ShowNPCDebugInfo.ShowAlways = v);
			SwitchUI = new UIRightNamedSwitch(
						"ShowNPCDebugInfo",
						SwitchV,
						new XxDefinitions.AbleString(SwitchV)
					);
			BItems.Add(SwitchUI	);
			SwitchV= new RefByDelegate<bool>(() => ShowProjDebugInfo.ShowAlways, (v) => ShowProjDebugInfo.ShowAlways = v);
			SwitchUI = new UIRightNamedSwitch(
						"ShowProjDebugInfo",
						SwitchV,
						new XxDefinitions.AbleString(SwitchV)
					);
			BItems.Add(SwitchUI);

		}
		public bool XDebuggerUsing {
			get => XDebugger.DebugMode;
			set {
				if (!value)XDebugger.CloseDebugMode();
				else XDebugger.DebugMode = true;
			}
		}
		public override void Update(GameTime gameTime)
		{
			//XDebuggerHelper.L1.Value.Debug(Items._items.Count);
			//XDebuggerHelper.L1.Value.Debug($"AnnouncedDebuggers: {AnnouncedDebuggers.Count},AItems:{AItems._items.Count}");
			if (AnnouncedDebuggers.Count!=AItems._items.Count) {
				AItems.Clear();
				UIRightNamedSwitch SwitchUI;
				foreach (var i in AnnouncedDebuggers) {
					//ShowXDebugger showXDebugger = new ShowXDebugger(i);
					//AItems.Add(showXDebugger);

					SwitchUI = new UIRightNamedSwitch
							(
							   i.FullName,
							new RefByDelegate<bool>
								(
								 () => i.XDebuggerMode == 2,
								 (value) => { if (i.XDebuggerMode != 0) i.xDebugger.Using = value; }
								),
						new GetByDelegate<string>
								(
							() => Language.GetTextValue($"Mods.XDebuggerHelper.tryGetXDebugger_XDebuggerMode_{i.XDebuggerMode}")
								)
							);
					AItems.Add
						(
						SwitchUI
						);
					//SwitchUI.Initialize();
				}
				AItems.SetScrollbar(AuIScrollbar);
				AItems.Recalculate();
				AItems.Activate();
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
