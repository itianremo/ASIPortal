using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DominosCMS.Web.Configuration
{
    public class MenuTypeElement : ConfigurationElement
    {
        
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

        [ConfigurationProperty("title", IsRequired = true)]
        public string Title
        {
            get
            {
                return (string)this["title"];
            }
        }
        [ConfigurationProperty("controller", IsRequired = true)]
        public string Controller
        {
            get
            {
                return (string)this["controller"];
            }
        }
        [ConfigurationProperty("action", IsRequired = true)]
        public string Action
        {
            get
            {
                return (string)this["action"];
            }
        }
        [ConfigurationProperty(MenuTypesConfigConstants.MenuTypeParamsPropertyName)]
        public MenuTypeParamsCollection Parameters
        {
            get
            {
                return (MenuTypeParamsCollection)base[MenuTypesConfigConstants.MenuTypeParamsPropertyName];
            }
        }

    }
}
