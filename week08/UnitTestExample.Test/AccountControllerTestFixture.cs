using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [Test,
        TestCase("abcd1234", false),
        TestCase("irf@uni-corvinus", false),
        TestCase("irf.uni-corvinus.hu", false),
        TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //arrange
            var accountController = new AccountController();

            //act
            var actualResult = accountController.ValidateEmail(email);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
            //accountController.Register
        }
        //A jelszó legalább 8 karakter hosszú kell legyen, csak az angol ABC betűiből és számokból állhat, és tartalmaznia kell legalább egy kisbetűt, egy nagybetűt és egy számot
        [Test,
           TestCase("abcdAAAA", "irf@uni-corvinus.hu"),
            TestCase("1234AAAA", "irf@uni-corvinus.hu"),
            TestCase("abcd1234", "irf@uni-corvinus.hu"),
            TestCase("aaaA444", "irf@uni-corvinus.hu"),
            TestCase("abcd123A", "irf@uni-corvinus.hu")
            ]
        public void TestValidatePassword(string password, string email)
        {
            //var accountController = new AccountController();

            //var actualResult = accountController.ValidatePassword(password);

            //Assert.AreEqual(password, actualResult);
        }
    }
}
