using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.Multitenancy.Configuration
{
    public class DatabaseCollection : ConfigurationElementCollection
    {
        public DatabaseCollection()
        {
            Add((DatabaseSection)CreateNewElement());
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new DatabaseSection();
        }

        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((DatabaseSection)element).Name;
        }

        public DatabaseSection this[int index]
        {
            get { return (DatabaseSection)BaseGet(index); }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        new public DatabaseSection this[string Name]
        {
            get
            {
                return (DatabaseSection)BaseGet(Name);
            }
        }

        public int IndexOf(DatabaseSection url)
        {
            return BaseIndexOf(url);
        }

        public void Add(DatabaseSection url)
        {
            BaseAdd(url);
        }
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(DatabaseSection url)
        {
            if (BaseIndexOf(url) >= 0)
                BaseRemove(url.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}
