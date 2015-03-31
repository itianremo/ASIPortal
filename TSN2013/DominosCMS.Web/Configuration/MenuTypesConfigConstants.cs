using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominosCMS.Web.Configuration
{
    internal class MenuTypesConfigConstants
    {
        public const string ConfigSectionName = "menuTypesConfigurations";
        public const string ConfigurationPropertyName = "menuTypes";
        public const string ConfigurationElementName = "menuType";

        public const string MenuTypeNameAttributeName = "name";
        public const string MenuTypeTitleAttributeName = "title";
        public const string MenuTypeControllerAttributeName = "controller";
        public const string MenuTypeActionAttributeName = "action";
        public const string MenuTypeParamsPropertyName = "params";
        public const string MenuTypeParamsElementName = "param";
    }
}