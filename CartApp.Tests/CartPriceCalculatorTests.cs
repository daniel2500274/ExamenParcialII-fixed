using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CartApp.Tests
{ 
    public class CartPriceCalculatorTests
    { 
        private CartPriceCalculator calculator;

        [SetUp]
        public void Setup()
        { 
            calculator = new CartPriceCalculator(); 
        }
         
        [Test]
        public void EmptyCart_ReturnsZero() //01
        {
            // Arrange
            // Ya no necesitas "new CartPriceCalculator()" aquí, ¡Setup() lo hizo!
            var emptyListOfItems = new List<Item>();
            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(emptyListOfItems, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(0m));
        }
         
        [Test]
        public void SingleItem_100_NoCoupon_NoVip_Total142_00() //02
        { 
            List<Item> items = new List<Item>
            {
                new Item("Taza", 100m, false),
            };

            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(142m));
        }
        [Test]
        public void SingleItem_250_NoCoupon_NoVip_Total280_00() //03
        {
            List<Item> items = new List<Item>
            {
                new Item("Plato", 250m, false),
            };

            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(280m));
        }
        [Test]
        public void TwoItems_80_60_WithPROMO10_Total171_12() // 04
        {
            List<Item> items = new List<Item>
            {
                new Item("A", 80m, false),
                new Item("B", 60m, false),
            };

            string? coupon = "PROMO10";
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(171.12m));

        }
        [Test]
        public void Fragile_210_WithPROMO10_Total226_68() //05
        {
            List<Item> items = new List<Item>
            {
                new Item("Florero", 210m, true), 
            };

            string? coupon = "PROMO10";
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(226.68m));
        }
        [Test]
        public void TwoFragile_90_120_NoCoupon_Total250_20 () //06
        {
            List<Item> items = new List<Item>
            {
                new Item("J1", 90m, true),
                new Item("J2", 120m, true),
            };

            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(250.20m));
        }
        [Test]
        public void ShippingBoundary_199_99_vs_200_00() //07
        {
            // PRIMER CASO 199.99
            List<Item> items = new List<Item>
            {
                new Item("X", 199.99m, false), 
            };

            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(253.99m));

            // SEGUNDO CASO 200.00
            List<Item> items_case2 = new List<Item>
            {
                new Item("Y", 200m, false),
            };

            string? coupon_case2 = null;
            bool isVIP_case2 = false;
            bool emailSend_case2 = false;

            //Act
            decimal result_case2 = calculator.CalculateTotal(items_case2, coupon_case2, isVIP_case2, emailSend_case2);

            //Assert
            Assert.That(result_case2, Is.EqualTo(224m));
        }
        [Test]
        public void NegativePrice_ThrowsArgumentException_WithItemName() //08
        {
            List<Item> items = new List<Item>
            {
                new Item("Defectuoso", -5, false), 
            };

            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            //Act
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                calculator.CalculateTotal(items, coupon, isVIP, emailSend);
            });

            //Assert
            Assert.That(exception.Message, Does.Contain("Defectuoso"));
        }
        [Test]
        public void VipAndPROMO10_TwoItems_100_60_ExpectedTotals() //09
        { 
            List<Item> items = new List<Item>
            {
                new Item("A", 100m, false),
                new Item("B", 60m, false),
            };

            string? coupon = "PROMO10";
            bool isVIP = true;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(182.32m));
        }
        [Test]
        public void InvalidCoupon_IgnoresDiscount_TotalUnchanged() //10
        {
            List<Item> items = new List<Item>
            {
                new Item("X", 100m, false), 
            };

            string? coupon = "PROMO5";
            bool isVIP = false;
            bool emailSend = false;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(142m));
        }
        [Test]
        public void Receipt_Prints_Keywords_ToConsole() //11
        { 
            List<Item> items = new List<Item>
            {
                new Item("X", 100m, false),
            };
            string? coupon = null;
            bool isVIP = false;
            bool emailSend = false;

            // 
            var stringWriter = new StringWriter(); 
            var originalConsoleOut = Console.Out; 
            Console.SetOut(stringWriter);

            string capturedOutput = "";  
            try
            { 
                calculator.CalculateTotal(items, coupon, isVIP, emailSend); 
                capturedOutput = stringWriter.ToString();
            }
            finally
            { 
                Console.SetOut(originalConsoleOut);
            }

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(capturedOutput, Does.Contain("RECIBO"));
                Assert.That(capturedOutput, Does.Contain("Subtotal:") );
                Assert.That(capturedOutput, Does.Contain("TOTAL:"));
                Assert.That(capturedOutput, Does.Contain("X"));
            });
        }
        [Test]
        public void EmailReceipt_FlagTrue_PrintsEmailBlock () //12
        {
            List<Item> items = new List<Item>
            {
                new Item("X", 100m, false),
            };
            string? coupon = null;
            bool isVIP = false;
            bool emailSend = true;

            // 
            var stringWriter = new StringWriter();
            var originalConsoleOut = Console.Out;
            Console.SetOut(stringWriter);

            string capturedOutput = "";
            try
            {
                calculator.CalculateTotal(items, coupon, isVIP, emailSend);
                capturedOutput = stringWriter.ToString();
            }
            finally
            {
                Console.SetOut(originalConsoleOut);
            }

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(capturedOutput, Does.Contain("Enviando Email"));
                Assert.That(capturedOutput, Does.Contain("cliente@example.com"));
                Assert.That(capturedOutput, Does.Contain("Recibo de compra")); 
            });
        }
        [Test]
        public void Combined_Fragile_Promo10_VIP_Email_Total224_44() //13
        {
            List<Item> items = new List<Item>
            {
                new Item("A", 120, true),
                new Item("B", 100, false),
            };

            string? coupon = "PROMO10";
            bool isVIP = true;
            bool emailSend = true;

            //Act
            decimal result = calculator.CalculateTotal(items, coupon, isVIP, emailSend);

            //Assert
            Assert.That(result, Is.EqualTo(224.44m));


            // PARTE 02 VALIDAR IMPRESIONES EN CONSOLA:
            var stringWriter = new StringWriter();
            var originalConsoleOut = Console.Out;
            Console.SetOut(stringWriter);

            string capturedOutput = "";
            try
            {
                calculator.CalculateTotal(items, coupon, isVIP, emailSend);
                capturedOutput = stringWriter.ToString();
            }
            finally
            {
                Console.SetOut(originalConsoleOut);
            }

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(capturedOutput, Does.Contain("Enviando Email"));
                Assert.That(capturedOutput, Does.Contain("cliente@example.com"));
                Assert.That(capturedOutput, Does.Contain("Recibo de compra"));
            });
        }
    }
}