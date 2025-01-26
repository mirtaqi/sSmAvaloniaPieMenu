using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace AvaloniaPieMenu.Controls
{
    /// <summary>
    /// Interaction logic for RadialMenu.xaml
    /// </summary>
    public class RadialMenu : ItemsControl
    {
        public RadialMenu()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Items.Any() && Items.All(m => m is RadialMenuItem rmi && rmi.ParentMenuItem is null))
            {
                TopLevelMenuItems = this.Items.Cast<RadialMenuItem>().ToArray();
            }
        }

        public static readonly StyledProperty< bool> IsOpenProperty =
            AvaloniaProperty.Register<RadialMenu, bool>(
                nameof(IsOpen),defaultBindingMode:BindingMode.TwoWay);

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly StyledProperty<bool> HalfShiftedItemsProperty =
            AvaloniaProperty.Register<RadialMenu, bool>(
                nameof(HalfShiftedItems),defaultBindingMode:BindingMode.TwoWay);
        public bool HalfShiftedItems
        {
            get { return (bool)GetValue(HalfShiftedItemsProperty); }
            set { SetValue(HalfShiftedItemsProperty, value); }
        }

        public static readonly StyledProperty<RadialMenuCentralItem> CentralItemProperty =
            AvaloniaProperty.Register<RadialMenu, RadialMenuCentralItem>(
                nameof(CentralItem),defaultBindingMode:BindingMode.TwoWay);

        public RadialMenuCentralItem CentralItem
        {
            get { return (RadialMenuCentralItem)GetValue(CentralItemProperty); }
            set { SetValue(CentralItemProperty, value); }
        }

        public static readonly StyledProperty<RadialMenuItem[]?> TopLevelMenuItemsProperty = AvaloniaProperty.Register<RadialMenu, RadialMenuItem[]?>(
            nameof(TopLevelMenuItems), defaultBindingMode: BindingMode.OneWay);

        public RadialMenuItem[]? TopLevelMenuItems
        {
            get { return (RadialMenuItem[]?)GetValue(TopLevelMenuItemsProperty); }
            private set { SetValue(TopLevelMenuItemsProperty, value); }
        }
        public static readonly StyledProperty<RadialMenuItem?> SelectedMenuItemProperty = AvaloniaProperty.Register<RadialMenu, RadialMenuItem?>(
        nameof(SelectedMenuItem), defaultBindingMode: BindingMode.OneWay);

        public RadialMenuItem? SelectedMenuItem
        {
            get { return (RadialMenuItem?)GetValue(SelectedMenuItemProperty); }
            set { SetValue(SelectedMenuItemProperty, value); }
        }

        public void SelectMenuItem(RadialMenuItem? rmi)
        {
            SelectedMenuItem= rmi;
            if (SelectedMenuItem is not null)
            {
                Items.Clear();
                foreach (var radialMenuItem in SelectedMenuItem.SubMenuItems)
                {
                    Items.Add(radialMenuItem);
                }
            }
            else
            {
                Items.Clear();
                foreach (var radialMenuItem in TopLevelMenuItems)
                {
                    Items.Add(radialMenuItem);
                }
            }
            //this.InvalidateArrange();
            //IsOpen=false;
            //IsOpen = true;
            this.InvalidateArrange();
            this.InvalidateMeasure();
            this.InvalidateVisual();
            //IsOpen = false;
            //IsOpen= true;
            // this.InvalidateMeasure();
            //this.inva
        }
        //public new  static readonly StyledProperty<List<RadialMenuItem>> ContentProperty =
        //    AvaloniaProperty.Register<RadialMenu, List<RadialMenuItem>>(
        //        nameof(Content),defaultValue:new List<RadialMenuItem>(), defaultBindingMode:BindingMode.TwoWay);

        //public new List<RadialMenuItem> Content
        //{
        //    get { return (List<RadialMenuItem>)GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}


        static RadialMenu()
        {
            
        }
        

        public override void BeginInit()
        {
            //Content = new List<RadialMenuItem>();
            base.BeginInit();
            
        }

        public override void EndInit()
        {
            base.EndInit();
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            
            for (int i = 0, count = Items.Count; i < count; i++)
            {
                var item = Items[i] as RadialMenuItem;
                item.Index = i;
                item.Count = count;
                item.HalfShifted = HalfShiftedItems;
                item.Width = this.Width;
                item.Height= this.Height;
                item.CenterX = this.Width / 2;
                item.CenterY=this.Height / 2;
                item.OuterRadius = this.Width / 2;
                item.EdgeInnerRadius = item.OuterRadius - 20;
                item.EdgeOuterRadius = item.OuterRadius;
            }
            return base.ArrangeOverride(arrangeSize);
        }
    }
}
