using ConfigurationProviderNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ConfigurationProvider = ConfigurationProviderNetCore.ConfigurationProvider;

namespace ConfigurationProviderTests
{
    [TestClass]
    public class ConfigurationProviderTests
    {
        private const string emailTemplatesPathKey = "EmailTemplatesPath";
        private const string paymentGatewayServiceUrl = "PaymentGatewayServiceUrl";

        private const string appSettingsSection = "appSettings";
        private const string connectionStringsSection = "connectionStrings";

        private static ConfigurationProviderBase CreateConfigurationProvider(string appSettingsKey, string appSettingsValue)
        {
            var appSettingsSectionName = "AppSettings";
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string> { { appSettingsSectionName + ":" + appSettingsKey, appSettingsValue } });

            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            return new ConfigurationProvider(configurationRoot);
        }

        #region EmailTemplatesPath Tests

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingExistsAndIsSetupCorretly_ReturnsExpectedValue()
        {
            // Arrange
            var expectedEmailTemplatePath = "\\SomeEmailTemplatesFolder";
            var configurationProvider = CreateConfigurationProvider(emailTemplatesPathKey, expectedEmailTemplatePath);

            // Act
            var actualEmailTemplatePath = configurationProvider.EmailTemplatesPath;

            // Assert 
            Assert.AreEqual(expectedEmailTemplatePath, actualEmailTemplatePath, $"We were expecting the actualEmailTemplatePath to be: {expectedEmailTemplatePath}, but found it to be: {actualEmailTemplatePath}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingExistsButDoesNotStartWithBackslash_ReturnsTheValueWithStartingBackslash()
        {
            // Arrange
            var emailTemplatePath = "SomeEmailTemplatesFolder";
            var configurationProvider = CreateConfigurationProvider(emailTemplatesPathKey, emailTemplatePath);
            var expectedEmailTemplatePath = "\\" + emailTemplatePath;

            // Act
            var actualEmailTemplatePath = configurationProvider.EmailTemplatesPath;

            // Assert 
            Assert.AreEqual(expectedEmailTemplatePath, actualEmailTemplatePath, $"We were expecting the actualEmailTemplatePath to be: {expectedEmailTemplatePath}, but found it to be: {actualEmailTemplatePath}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingExistsButValueIsEmpty_ThrowsException()
        {
            // Arrange           
            var configurationProvider = CreateConfigurationProvider(emailTemplatesPathKey, string.Empty);

            // Act & Assert
            Assert.That.ThrowsExceptionWithSpecificWords<ConfigurationErrorsException>(() => _configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is Empty", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingExistsButValueIsWhiteSpaces_ThrowsException()
        {
            // Arrange           
            AddAppSettingInConfigFile(emailTemplatesPathKey, "           ");

            // Act & Assert
            Assert.That.ThrowsExceptionWithSpecificWords<ConfigurationErrorsException>(() => _configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is White Spaces", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingIsMissing_ThrowsException()
        {
            // Arrange           
            // Nothing to arrange

            // Act & Assert
            Assert.That.ThrowsExceptionWithSpecificWords<ConfigurationErrorsException>(() => _configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is Missing", "Required" });
        }

        #endregion EmailTemplatesPath Tests    }
    }
}
