using WatiN.Core;

namespace ReleaseManager.FunctionalTests.Drivers
{
    public class ComponentList : WatinPageDriver
    {
        public ComponentList(TestDriver driver) : base(driver)
        {
        }

        public virtual AddComponent AddComponent()
        {
            Browser.Link("addComponent").Click();
            return CreatePageDriver<AddComponent>();
        }

        public virtual ComponentList Activate(string component)
        {
            Browser.CheckBox(component + "_Active").Checked = true;
            return this;
        }

        public virtual ComponentList Deactivate(string component)
        {
            Browser.CheckBox(component + "_Active").Checked = false;
            return this;
        }

        public virtual ComponentList SetLocation(string component, string location)
        {
            Browser.TextField(component + "_Location").Value = location;
            return this;
        }

        public virtual ComponentList Save()
        {
            Browser.Button("save").Click();
            return this;
        }
    }
}
