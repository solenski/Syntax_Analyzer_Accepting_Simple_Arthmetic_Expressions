using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WindowsFormsApp2;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private SimpleEquationSyntaxAnalyser sesa => new SimpleEquationSyntaxAnalyser();

        public string[] OKStrings = new string[] {
            "0*0",
            "0+8",
            "2+4+5-3-2-1",
            "2+4",
            "3+3+3+1",
            "-3+2-1",
            "9+3+4*3/2",
            "0+2",
            "-0+3",
            "-0 + 2",
            "0-5-0+0-0+3-0+3+3+3",
            "130+32",
            "111+ 33+1 2121",
            "12332+33+12121 "};
        public string[] NOKStrings = new string[] {
            "a+c",
            "++1-2",
            "00+3",
            "0+",
            "---2+4+5-3-2-1",
            "-2++++4",
            "/3++3+3+13",
            "--3+2-1",
            "9+/3+4*3/2",
            "02+2",
            "000+2"
        };


        [TestMethod]
        public void TestOK()
        {
            foreach (var tcase in OKStrings)
            {
                Assert.IsTrue(this.sesa.Validate(tcase), tcase);
            }
        }

        [TestMethod]
        public void TestNOK()
        {
            foreach (var tcase in NOKStrings)
            {
                Assert.IsFalse(this.sesa.Validate(tcase), tcase);
            }
        }
    }
}
