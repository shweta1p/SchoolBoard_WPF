using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kirin_2.Model
{
    public class Menu
    {
        //to call resource dictionary in our code behind
        System.Windows.ResourceDictionary dict = Application.LoadComponent(new Uri("/Kirin_2;component/Assets/IconDictionary.xaml", UriKind.RelativeOrAbsolute)) as ResourceDictionary;

        private List<MenuItemsData> menuList;

        //Our Source List for Menu Items
        public List<MenuItemsData> MenuList
        {
            get
            {
                return menuList;
            }
            set
            {
                menuList = value;

            }

        }



        public class MenuItemsData
        {


            //Icon Name using MaterialDesignTheme
            private string iconfile;
            public string IconFile
            {
                get
                {
                    return iconfile;
                }
                set
                {
                    iconfile = value;
                }
            }

            //Icon Data
            private PathGeometry pathData;
            public PathGeometry PathData
            {
                get
                {
                    return pathData;
                }
                set
                {
                    pathData = value;
                }
            }


            private string menuText;
            public string MenuText
            {
                get
                {
                    return menuText;
                }
                set
                {
                    menuText = value;
                }
            }



            private List<SubMenuItemData> subMenuList;
            public List<SubMenuItemData> SubMenuList
            {
                get
                {
                    return subMenuList;
                }
                set
                {
                    subMenuList = value;
                }




            }

        }


        public class SubMenuItemData
        {
            private PathGeometry pathData;

            public PathGeometry PathData
            {
                get
                {
                    return pathData;
                }
                set
                {
                    pathData = value;
                }
            }

            private string subMenuText;
            public string SubMenuText
            {
                get
                {
                    return subMenuText;
                }
                set
                {
                    subMenuText = value;
                }
            }
        }
    }

    public class ItemMenu
    {
        public ItemMenu()
        {
        }

        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon, string page)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
            Page = page;
            
        }
       
        public ItemMenu(string header, UserControl screen, PackIconKind icon, string page)
        {
            Header = header;
            Screen = screen;
            Icon = icon;
            Page = page;
        }

        public string Header { get;  set; }
        public string Level { get; set; }
        public PackIconKind Icon { get; set; }
        public List<SubItem> SubItems { get;set; }
       
        public UserControl Screen { get;  set; }
        public string Page { get;  set; }
    }

   


    public class SubItem
    {
        public SubItem(string name, string page, UserControl screen = null)
        {
            Name = name;
            Page = page;
            Screen = screen;
        }
        public string Name { get; private set; }
        public UserControl Screen { get; private set; }
       
        public string Page { get; private set; }
    }

    
}
