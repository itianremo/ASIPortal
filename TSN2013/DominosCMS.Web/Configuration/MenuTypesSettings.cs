using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DominosCMS.Web.Configuration
{
    public class MenuTypesSettings : ConfigurationSection
    {
        [ConfigurationProperty(MenuTypesConfigConstants.ConfigurationPropertyName, IsDefaultCollection = true)]
        public MenuTypeCollection MenuTypes
        {
            get {
                return (MenuTypeCollection)base[MenuTypesConfigConstants.ConfigurationPropertyName]; 
            }
        }
    }
}