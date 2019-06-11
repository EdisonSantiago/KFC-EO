using NUnit.Framework;
using Oulanka.Domain.Models;

namespace Oulanka.Tests.Oulanka.Domain.Models
{
    [TestFixture]
    [Category("Model Tests")]
    public class SettingTests
    {
        [Test]
        public void Can_create_a_setting()
        {
            var setting = new Setting { Name = "New Setting" };

            Assert.That(setting.Name == "New Setting");
        }

        [Test]
        public void Setting_created_is_not_null()
        {
            var setting = new Setting { Name = "New Setting" };
            Assert.IsNotNull(setting);
        }
    }
}