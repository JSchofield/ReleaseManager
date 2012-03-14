using System;
using System.Collections.Generic;
using System.ComponentModel;
using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Driver
{
    public enum EntryMethod
    {
        AppendText,        
        SetValue,
        TypeText
    }

    public abstract class WatinPageDriver
    {
        public WatinPageDriver(TestDriver driver)
        {
            this.Driver = driver;
        }

        public Browser Browser { get { return Driver.Browser; } }

        public TestDriver Driver { get; private set; }

        protected T CreatePageDriver<T>() where T : WatinPageDriver
        {
            return Driver.CreatePageDriver<T>();
        }

        public virtual ComponentList GoToComponentList()
        {
            Browser.Link("goToComponentList").Click();
            return CreatePageDriver<ComponentList>();
        }

        public virtual ReleaseList GoToReleaseList()
        {
            Browser.Link("goToReleaseList").Click();
            return CreatePageDriver<ReleaseList>();
        }

        public void EnterText(string fieldName, string text, EntryMethod method = EntryMethod.SetValue)
        {
            var field = Browser.TextField(fieldName);
            switch (method)
            {
                case EntryMethod.TypeText:
                    field.TypeText(text);
                    break;
                case EntryMethod.SetValue:
                    field.Value = text;
                    break;
                case EntryMethod.AppendText:
                    field.AppendText(text);
                    break;
                default:
                    throw new ArgumentException("Unknown EntryMethod value.");
            }
        }

        public virtual void SetValues(object keysValuePairs)
        {
            foreach (var pair in AnonymousObjectToDictionary(keysValuePairs))
            {
                EnterText(pair.Key, pair.Value, EntryMethod.SetValue);
            }
        }       

        protected IDictionary<string, string> AnonymousObjectToDictionary(object keyValuePairs)
        {
            var dictionary = new Dictionary<string, string>();
            if (keyValuePairs != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(keyValuePairs))
                {
                    dictionary.Add(descriptor.Name, descriptor.GetValue(keyValuePairs).ToString());
                }
            }
            return dictionary;
        }
    }
}
