using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DominosCMS.Web.Configuration
{
    public class MenuTypeParamElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
        }

        //dataSource="Articles/List" dataValue="ID" dataText="Title"

        [ConfigurationProperty("dataSource")]
        public string DataSource
        {
            get
            {
                return (string)this["dataSource"];
            }
        }

        [ConfigurationProperty("dataValue")]
        public string DataValue
        {
            get
            {
                return (string)this["dataValue"];
            }
        }

        [ConfigurationProperty("dataText")]
        public string DataText
        {
            get
            {
                return (string)this["dataText"];
            }
        }

    }
}