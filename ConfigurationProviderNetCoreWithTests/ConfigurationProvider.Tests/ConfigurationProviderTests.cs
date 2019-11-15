using ConfigurationProviderNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ConfigProvider = ConfigurationProviderNetCore.ConfigurationProvider;


namespace ConfigurationProvider.Tests
{
    [TestClass]
    public class ConfigurationProviderTests
    {
        private const string appSettingsSectionName = "AppSettings";
        private const string emailTemplatesPathKey = "EmailTemplatesPath";
        private const string paymentGatewayServiceUrlKey = "PaymentGatewayServiceUrl";
        private const string fiscalYearStartKey = "FiscalYearStart";
        private const string notifyOnUploadKey = "NotifyOnUpload";
        private const string dbConnectionKey = "MyDb";

        private static ConfigurationProviderBase CreateConfigurationProvider(string appSettingsKey, string appSettingsValue)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string> { { appSettingsSectionName + ":" + appSettingsKey, appSettingsValue } });

            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            return new ConfigProvider(configurationRoot);
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
            var configurationProvider = CreateConfigurationProvider(emailTemplatesPathKey, appSettingsValue: string.Empty);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is Empty", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingExistsButValueIsWhiteSpaces_ThrowsException()
        {
            // Arrange        
            var configurationProvider = CreateConfigurationProvider(emailTemplatesPathKey, appSettingsValue: "           ");

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is White Spaces", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingExistsButValueIsNull_ThrowsException()
        {
            // Arrange        
            var configurationProvider = CreateConfigurationProvider(emailTemplatesPathKey, appSettingsValue: null);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is Missing", "value is Missing" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void EmailTemplatesPath_WhenConfigSettingIsMissing_ThrowsException()
        {
            // Arrange         
            var configurationProvider = CreateConfigurationProvider(appSettingsKey: null, appSettingsValue: null);


            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.EmailTemplatesPath, new[] { emailTemplatesPathKey, "is Missing", "Required" });
        }

        #endregion EmailTemplatesPath Tests

        #region PaymentGatewayServiceUrl Tests

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void PaymentGatewayServiceUrl_WhenConfigSettingExistsAndIsSetupCorretly_ReturnsExpectedValue()
        {
            // Arrange
            var expectedPaymentGatewayServiceUrl = "http://payments.matlus.com/";
            var configurationProvider = CreateConfigurationProvider(paymentGatewayServiceUrlKey, expectedPaymentGatewayServiceUrl);

            // Act
            var actualPaymentGatewayServiceUrl = configurationProvider.PaymentGatewayServiceUrl;

            // Assert 
            Assert.AreEqual(expectedPaymentGatewayServiceUrl, actualPaymentGatewayServiceUrl, $"We were expecting the actualPaymentGatewayServiceUrl to be: {expectedPaymentGatewayServiceUrl}, but found it to be: {actualPaymentGatewayServiceUrl}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void PaymentGatewayServiceUrl_WhenConfigSettingExistsButDoesNotEndWithForwardSlash_ReturnsTheValueEndingWithForwardSlash()
        {
            // Arrange
            var gatewayServiceUrl = "http://payments.matlus.com";
            var configurationProvider = CreateConfigurationProvider(paymentGatewayServiceUrlKey, gatewayServiceUrl);
            var expectedPaymentGatewayServiceUrl = gatewayServiceUrl + "/";

            // Act
            var actualPaymentGatewayServiceUrl = configurationProvider.PaymentGatewayServiceUrl;

            // Assert 
            Assert.AreEqual(expectedPaymentGatewayServiceUrl, actualPaymentGatewayServiceUrl, $"We were expecting the actualEmailTemplatePath to be: {expectedPaymentGatewayServiceUrl}, but found it to be: {actualPaymentGatewayServiceUrl}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void PaymentGatewayServiceUrl_WhenConfigSettingExistsButValueIsEmpty_ThrowsException()
        {
            // Arrange           
            var configurationProvider = CreateConfigurationProvider(paymentGatewayServiceUrlKey, appSettingsValue: string.Empty);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.PaymentGatewayServiceUrl, new[] { paymentGatewayServiceUrlKey, "is Empty", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void PaymentGatewayServiceUrl_WhenConfigSettingExistsButValueIsWhiteSpaces_ThrowsException()
        {
            // Arrange      
            var configurationProvider = CreateConfigurationProvider(paymentGatewayServiceUrlKey, appSettingsValue: "           ");

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.PaymentGatewayServiceUrl, new[] { paymentGatewayServiceUrlKey, "is White Spaces", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void PaymentGatewayServiceUrl_WhenConfigSettingExistsButValueIsNull_ThrowsException()
        {
            // Arrange      
            var configurationProvider = CreateConfigurationProvider(paymentGatewayServiceUrlKey, appSettingsValue: null);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.PaymentGatewayServiceUrl, new[] { paymentGatewayServiceUrlKey, "is Missing", "value is Missing" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void PaymentGatewayServiceUrl_WhenConfigSettingIsMissing_ThrowsException()
        {
            // Arrange      
            var configurationProvider = CreateConfigurationProvider(appSettingsKey: null, appSettingsValue: null);


            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.PaymentGatewayServiceUrl, new[] { paymentGatewayServiceUrlKey, "is Missing", "Required" });
        }

        #endregion PaymentGatewayServiceUrl Tests

        #region FiscalYearStart Tests

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void FiscalYearStart_WhenConfigSettingExistsAndIsSetupCorretly_ReturnsExpectedValue()
        {
            // Arrange
            var expectedFiscalYearStart = new DateTime(DateTime.Today.Year, 10, 1);
            var configurationProvider = CreateConfigurationProvider(fiscalYearStartKey, expectedFiscalYearStart.ToShortDateString());

            // Act
            var actualFiscalYearStart = configurationProvider.FiscalYearStart;

            // Assert 
            Assert.AreEqual(expectedFiscalYearStart, actualFiscalYearStart, $"We were expecting the actualFiscalYearStart to be: {expectedFiscalYearStart}, but found it to be: {actualFiscalYearStart}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void FiscalYearStart_WhenConfigSettingValueIsEmpty_ReturnsTheDefaultValue()
        {
            // Arrange
            var expectedFiscalYearStart = new DateTime(1, 10, 1);
            var configurationProvider = CreateConfigurationProvider(fiscalYearStartKey, appSettingsValue: string.Empty);

            // Act
            var actualFiscalYearStart = configurationProvider.FiscalYearStart;

            // Assert 
            Assert.AreEqual(expectedFiscalYearStart, actualFiscalYearStart, $"We were expecting the actualFiscalYearStart to be: {expectedFiscalYearStart}, but found it to be: {actualFiscalYearStart}");
        }


        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void FiscalYearStart_WhenConfigSettingValueIsNull_ReturnsTheDefaultValue()
        {
            // Arrange
            var expectedFiscalYearStart = new DateTime(1, 10, 1);
            var configurationProvider = CreateConfigurationProvider(fiscalYearStartKey, appSettingsValue: null);

            // Act
            var actualFiscalYearStart = configurationProvider.FiscalYearStart;

            // Assert 
            Assert.AreEqual(expectedFiscalYearStart, actualFiscalYearStart, $"We were expecting the actualFiscalYearStart to be: {expectedFiscalYearStart}, but found it to be: {actualFiscalYearStart}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void FiscalYearStart_WhenConfigSettingIsMissing_ReturnsTheDefaultValue()
        {
            // Arrange
            var expectedFiscalYearStart = new DateTime(1, 10, 1);
            var configurationProvider = CreateConfigurationProvider(appSettingsKey: null, appSettingsValue: null);

            // Act
            var actualFiscalYearStart = configurationProvider.FiscalYearStart;

            // Assert 
            Assert.AreEqual(expectedFiscalYearStart, actualFiscalYearStart, $"We were expecting the actualFiscalYearStart to be: {expectedFiscalYearStart}, but found it to be: {actualFiscalYearStart}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void FiscalYearStart_WhenConfigSettingValueIsNotParsableToDateTime_ThrowsException()
        {
            // Arrange
            var badDateTime = "13/24/1900";
            var configurationProvider = CreateConfigurationProvider(fiscalYearStartKey, badDateTime);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.FiscalYearStart, new[] { fiscalYearStartKey, "not a valid DateTime" });
        }

        #endregion FiscalYearStart Tests

        #region NotifyOnUpload Tests

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void NotifyOnUpload_WhenConfigSettingHasBeenSetToFalse_ReturnsFalse()
        {
            // Arrange
            var expectedNotifyOnUpload = false;
            var configurationProvider = CreateConfigurationProvider(notifyOnUploadKey, expectedNotifyOnUpload.ToString(null));

            // Act
            var actualNotifyOnUpload = configurationProvider.NotifyOnUpload;

            // Assert 
            Assert.AreEqual(expectedNotifyOnUpload, actualNotifyOnUpload, $"We were expecting the actualNotifyOnUpload to be: {expectedNotifyOnUpload}, but found it to be: {actualNotifyOnUpload}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void NotifyOnUpload_WhenConfigSettingIsNull_ReturnsTrue()
        {
            // Arrange
            var expectedNotifyOnUpload = true;
            var configurationProvider = CreateConfigurationProvider(notifyOnUploadKey, appSettingsValue: null);

            // Act
            var actualNotifyOnUpload = configurationProvider.NotifyOnUpload;

            // Assert 
            Assert.AreEqual(expectedNotifyOnUpload, actualNotifyOnUpload, $"We were expecting the actualNotifyOnUpload to be: {expectedNotifyOnUpload}, but found it to be: {actualNotifyOnUpload}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void NotifyOnUpload_WhenConfigSettingIsEmpty_ReturnsTrue()
        {
            // Arrange
            var expectedNotifyOnUpload = true;
            var configurationProvider = CreateConfigurationProvider(notifyOnUploadKey, appSettingsValue: string.Empty);

            // Act
            var actualNotifyOnUpload = configurationProvider.NotifyOnUpload;

            // Assert 
            Assert.AreEqual(expectedNotifyOnUpload, actualNotifyOnUpload, $"We were expecting the actualNotifyOnUpload to be: {expectedNotifyOnUpload}, but found it to be: {actualNotifyOnUpload}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void NotifyOnUpload_WhenConfigSettingIsMissing_ReturnsTrue()
        {
            // Arrange
            var expectedNotifyOnUpload = true;
            var configurationProvider = CreateConfigurationProvider(appSettingsKey: null, appSettingsValue: null);

            // Act & Assert
            var actualNotifyOnUpload = configurationProvider.NotifyOnUpload;

            // Assert 
            Assert.AreEqual(expectedNotifyOnUpload, actualNotifyOnUpload, $"We were expecting the actualNotifyOnUpload to be: {expectedNotifyOnUpload}, but found it to be: {actualNotifyOnUpload}");
        }


        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void NotifyOnUpload_WhenConfigSettingIsNotParseableToABool_ThrowsException()
        {
            // Arrange
            var configurationProvider = CreateConfigurationProvider(notifyOnUploadKey, "NotABool");

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.NotifyOnUpload, new[] { "not a valid Boolean", "parseable to a Boolean" });
        }

        #endregion NotifyOnUpload Tests

        #region DbConnectionInformation Tests

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void DbConnectionInformation_WhenConfigSettingIsCorrect_ReturnsAValidDbConnectionInformation()
        {
            // Arrange
            var expectedConnectionString = "SomeConnectionString";
            var expectedProviderName = "System.Data.SqlClient";
            var configurationProvider = CreateConfigurationProvider(dbConnectionKey, expectedConnectionString);

            // Act
            var actualDbConnectionInformation = configurationProvider.DbConnectionInformation;

            // Assert 
            Assert.AreEqual(expectedConnectionString, actualDbConnectionInformation.ConnectionString, $"We were expecting the ConnectionString to be: {expectedConnectionString}, but found it to be: {actualDbConnectionInformation.ConnectionString}");
            Assert.AreEqual(expectedProviderName, actualDbConnectionInformation.ProviderName, $"We were expecting the ProviderName to be: {expectedProviderName}, but found it to be: {actualDbConnectionInformation.ProviderName}");
            Assert.AreEqual(dbConnectionKey, actualDbConnectionInformation.ConnectionStringName, $"We were expecting the ConnectionStringName to be: {dbConnectionKey}, but found it to be: {actualDbConnectionInformation.ConnectionStringName}");
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void DbConnectionInformation_WhenConfigSettingIsNull_ThrowsException()
        {
            // Arrange
            var configurationProvider = CreateConfigurationProvider(dbConnectionKey, appSettingsValue: null);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.DbConnectionInformation, new[] { "is Missing", "Required" });
        }

        [TestMethod]
        [TestCategory("Class Integration Test")]
        public void DbConnectionInformation_WhenConfigSettingIsMissing_ThrowsException()
        {
            // Arrange
            var configurationProvider = CreateConfigurationProvider(appSettingsKey: null, appSettingsValue: null);

            // Act & Assert
            AssertThrowsExceptionWithSpecificWords<ConfigurationSettingException>(() => configurationProvider.DbConnectionInformation, new[] { "is Missing", "Required" });
        }

        #endregion DbConnectionInformation Tests

        private static void AssertThrowsExceptionWithSpecificWords<T>(Func<object> methodToExecute, IEnumerable<string> messageParts) where T : Exception
        {
            try
            {
                _ = methodToExecute();
                Assert.Fail($"We were expecting an Exception of type: {typeof(T).Name} to be thrown but no exception was thrown. The exception message was expected to contain the following phrases: {string.Join(',', messageParts)}");
            }
            catch (T e)
            {
                EnsureExceptionMessageContains(e, messageParts);
            }
        }

        private static void EnsureExceptionMessageContains(Exception exception, IEnumerable<string> messageParts)
        {
            var exceptionMessage = new StringBuilder();
            exceptionMessage.AppendLine($"An Exception of Type: {exception.GetType().Name}, was thrown, however the exception message was expected to contain the following Phrases: {string.Join(", ", messageParts)}. However, the exception message did NOT contain the following:\r\n");

            var partNotContained = false;

            foreach (var part in messageParts)
            {
                if (!exception.Message.Contains(part, StringComparison.OrdinalIgnoreCase))
                {
                    exceptionMessage.Append($"{part}, ");
                    partNotContained = true;
                }
            }

            if (partNotContained)
            {
                exceptionMessage.AppendLine("\r\nThe actual exception message is: " + exception.Message);
                throw new AssertFailedException(exceptionMessage.ToString());
            }
        }
    }
}

