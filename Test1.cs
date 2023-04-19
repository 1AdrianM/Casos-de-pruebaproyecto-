using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace ProyectoPrestamo.Test
{
    [TestClass]
    public class LoginTest
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected static WindowsDriver<WindowsElement> session;
        protected static RemoteTouchScreen touchScreen;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // Launch the ProyectoPrestamo app using WinAppDriver
            AppiumOptions options = new AppiumOptions();
            options.AddAdditionalCapability("app", "ruta\\ProyectoPrestamo.exe");

            session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), options);

            Assert.IsNotNull(session);
        }

        [ClassCleanup]
        public static void TearDown()
        {
            // Close the session and quit the driver
            if (session != null)
            {
                session.Close();
                session.Quit();
            }
        }

        [TestMethod]
        public void LoginTest_Successful()
        {
            // Enter the credentials and click the login button
            var documento = session.FindElementByAccessibilityId("txtdocumento");
            var clave = session.FindElementByAccessibilityId("txtclave");
            var ingresar = session.FindElementByAccessibilityId("btningresar");

            documento.SendKeys("administrador");
            clave.SendKeys("123");
            ingresar.Click();

            // Verify that the main form is displayed
            var nombreUsuario = session.FindElementByAccessibilityId("lblNombreUsuario");

            Assert.AreEqual("administrador", nombreUsuario.Text);
        }

        [TestMethod]
        public void LoginTest_Unsuccessful()
        {
            // Enter invalid credentials and click the login button
            var documento = session.FindElementByAccessibilityId("txtdocumento");
            var clave = session.FindElementByAccessibilityId("txtclave");
            var ingresar = session.FindElementByAccessibilityId("btningresar");

            documento.SendKeys("usuario_invalido");
            clave.SendKeys("clave_invalida");
            ingresar.Click();

            // Verify that the error message is displayed
            var mensaje = session.FindElementByAccessibilityId("lblMensaje");

            Assert.AreEqual("No se encontraron coincidencias del usuario", mensaje.Text);
        }
    }
}
