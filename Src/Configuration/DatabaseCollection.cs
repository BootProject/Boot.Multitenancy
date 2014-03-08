using System;
using System.Collections.Generic;
using System.Configuration;

namespace Boot.Multitenancy.Configuration
{
    /// <summary>
    /// DatabaseCollection
    /// TODO: Make generic.
    /// </summary>
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

        public int IndexOf(DatabaseSection section)
        {
            return BaseIndexOf(section);
        }

        public void Add(DatabaseSection section)
        {
            BaseAdd(section);
        }
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        public void Remove(DatabaseSection section)
        {
            if (BaseIndexOf(section) >= 0)
                BaseRemove(section.Name);
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
