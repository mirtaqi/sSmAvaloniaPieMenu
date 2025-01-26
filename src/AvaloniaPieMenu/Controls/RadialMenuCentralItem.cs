using System.Linq;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace AvaloniaPieMenu.Controls
{
    /// <summary>
    /// Interaction logic for RadialMenuCentralItem.xaml
    /// </summary>
    public class RadialMenuCentralItem : Button
    {
        static RadialMenuCentralItem()
        {
            
        }

        protected override void OnClick()
        {
            base.OnClick();
            var rm = this.GetVisualAncestors().FirstOrDefault(u => u is RadialMenu rm) as RadialMenu;
            if (rm != null )
            {
                rm.SelectMenuItem(null);
            }
        }
    }
}
