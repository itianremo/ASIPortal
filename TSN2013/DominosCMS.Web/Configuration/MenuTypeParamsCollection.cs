using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DominosCMS.Web.Configuration
{
    public class MenuTypeParamsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new MenuTypeParamElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MenuTypeParamElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return MenuTypesConfigConstants.MenuTypeParamsElementName; }
        }

        public MenuTypeParamElement this[int index]
        {
            get { return (MenuTypeParamElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new MenuTypeParamElement this[string name]
        {
            get
            {
                return
                    (MenuTypeParamElement)this.BaseGet(name);
            }
        }

        public bool ContainsKey(string keyName)
        {
            bool result = false;
            object[] keys = this.BaseGetAllKeys();
            foreach (object key in keys)
            {
                if ((string)key == keyName)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}