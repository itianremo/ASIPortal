using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DominosCMS.Web.Configuration
{
    public class MenuTypeCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new MenuTypeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MenuTypeElement)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return MenuTypesConfigConstants.ConfigurationElementName; }
        }

        public MenuTypeElement this[int index]
        {
            get { return (MenuTypeElement)this.BaseGet(index); }
            set
            {
                if (this.BaseGet(index) != null)
                {
                    this.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new MenuTypeElement this[string name]
        {
            get
            {
                return
                    (MenuTypeElement)this.BaseGet(name);
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
