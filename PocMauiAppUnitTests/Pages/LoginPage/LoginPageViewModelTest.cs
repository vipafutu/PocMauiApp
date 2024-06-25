using System.ComponentModel;
using Moq;
using Xunit;
using PocMauiApp.Pages.LoginPage;

namespace PocMauiAppTests.Pages.LoginPage
{
    public class LoginPageViewModelTests
    {
        [Fact]
        public void InitialState_Test()
        {
            // Arrange
            var viewModel = new LoginPageViewModel();

            // Act & Assert
            Assert.Equal(string.Empty, viewModel.Username);
            Assert.Equal(string.Empty, viewModel.Password);
            Assert.False(viewModel.IsLoginButtonEnabled);
        }

        [Fact]
        public void UsernameProperty_Test()
        {
            // Arrange
            var viewModel = new LoginPageViewModel();
            var propertyChangedRaised = false;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.Username))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            viewModel.Username = "TestUser";

            // Assert
            Assert.Equal("TestUser", viewModel.Username);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void PasswordProperty_Test()
        {
            // Arrange
            var viewModel = new LoginPageViewModel();
            var propertyChangedRaised = false;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.Password))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            viewModel.Password = "TestPassword";

            // Assert
            Assert.Equal("TestPassword", viewModel.Password);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void IsLoginButtonEnabledProperty_Test()
        {
            // Arrange
            var viewModel = new LoginPageViewModel();
            var propertyChangedRaised = false;

            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(viewModel.IsLoginButtonEnabled))
                {
                    propertyChangedRaised = true;
                }
            };

            // Act
            viewModel.Username = "TestUser";
            viewModel.Password = "TestPassword";

            // Assert
            Assert.True(viewModel.IsLoginButtonEnabled);
            Assert.True(propertyChangedRaised);
        }

        [Fact]
        public void PropertyChangedEvent_Test()
        {
            // Arrange
            var viewModel = new LoginPageViewModel();
            var propertiesChanged = new List<string>();

            viewModel.PropertyChanged += (sender, e) =>
            {
                propertiesChanged.Add(e.PropertyName);
            };

            // Act
            viewModel.Username = "TestUser";
            viewModel.Password = "TestPassword";

            // Assert
            Assert.Contains(nameof(viewModel.Username), propertiesChanged);
            Assert.Contains(nameof(viewModel.Password), propertiesChanged);
            Assert.Contains(nameof(viewModel.IsLoginButtonEnabled), propertiesChanged);
        }
    }
}