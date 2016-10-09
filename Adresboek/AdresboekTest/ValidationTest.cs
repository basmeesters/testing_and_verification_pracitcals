using Adresboek;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdresboekTest
{
    
    [TestClass()]
    public class ValidationTest
    {
        // test for validating the name of contacts
        [TestMethod()]
        public void ValidateNameTest()
        {
            // valid names
            string[] trueTests = {
              "",
              "CAR Hoare",
              "C.a.r Hoare",
              ".",
              " "
            };

            // invalid names
            string[] falseTests = {
              "C.. Hoare",
              "Hoare123",
              "#"
            };

            // test
            foreach (string str in trueTests)
                Assert.IsTrue(Validation.ValidateName(str));

            foreach (string str in falseTests)
                Assert.IsFalse(Validation.ValidateName(str));
        }

        // test for validating the email address of a contact
        [TestMethod()]
        public void ValidateEmailTest()
        {
            // valid emails
            string[] trueTests = {
              "@",
              "localpart@domain",
              "localpart@domain.com",
              "localpart@domain.com.com2",
              "loc\"..@[]<>=--0;\"@domain.com",
              "localpart@dom--ain-.com",
              "C.A.R.Hoare@logic.com", 
              "Hoare{1+2=3}@logic.com",
              "tony.\"tony@home.org\".hoare@logic.com",
              "\"wierd: tony@home.org\"@logic.com"
            };

            // invalid emails
            string[] falseTests = {
              "",
              " ",
              "notvalid",
              ".localpart@domain.com",
              "localpart.@domain.com",
              "localpart@domain.com.",
              "..localpart@domain.com",
              "loc@lpart@domain.com",
              "test\"localpart@domain.com",
              "testlocalpart@dom\"ain.com",
              "testl\\ocalpart@domain.com",
              "testlocalpart@do\\main.com",
              "Hoare.logic.com",
              "C..Hoare@logic.com",
              "Hoare@home@logic.com"
            };

            // run tests
            foreach (string str in trueTests)
                Assert.IsTrue(Validation.ValidateEmail(str));

            foreach (string str in falseTests)
                Assert.IsFalse(Validation.ValidateEmail(str));
        }

        // check only the first part of the emailaddress (before the at-sign)
        [TestMethod()]
        public void validateLocalPartTest()
        {
            // valid localparts
            string[] trueTests = {
              "",
              "-",
              "localpart",
              "l.ocalpart",
              "l.o.calpart",
              "loca\"lp\"art",
              "loca\"l@p\"art", 
              "\"\"",
              "C.A.R.Hoare",
              "Hoare{1+2=3}",
              "tony.\"tony@home.org\".hoare",
              "\"wierd: tony@home.org\""
            };

            // invalid localparts
            string[] falseTests = {
              " ",
              "\"",
              "\"±\"",
              "loca..lpart",
              ".localpart",
              "localpart.",
              "..localpart.",
              "loca\"lpart",
              "loc@a\"l@p\"art",
              "Hoare@home"
            };

            // run tests
            foreach (string str in trueTests)
                Assert.IsTrue(Validation.validateLocalPart(str));

            foreach (string str in falseTests)
                Assert.IsFalse(Validation.validateLocalPart(str));
        }

        // test the quoted parts an email address
        [TestMethod()]
        public void validateLocalQuotePartTest()
        {
            // valid quotes
            string[] trueTests = {
              "",
              "..",
              " ",
              "-",
              "l.o.calpart",     
              "C.A.R.Hoare",
              "Hoare{1+2=3}",
              "tony@home.org",
              "wierd: tony@home.org",
              "\\\"",
              "\\\\"
            };

            // invalid quotes
            string[] falseTests = {
              "`",
              "\"",
              "\\"
            };

            // run tests
            foreach (string str in trueTests)
                Assert.IsTrue(Validation.validateLocalQuotePart(str));

            foreach (string str in falseTests)
                Assert.IsFalse(Validation.validateLocalQuotePart(str));
        }


        // validate the last part of an email address (after the at-sign)
        [TestMethod()]
        public void validateDomainPartTest()
        {
            // valid domains
            string[] trueTests = {
              "",
              "domain",
              "domain.com",
              "domain.com.com2",
              "dom--ain-.com"
            };

            // invalid domains
            string[] falseTests = {
              " ",
              ".",
              "domain..com",
              "domain.com.",
              "#",
              "_",
              "dom@in.com"
            };

            // run tests
            foreach (string str in trueTests)
                Assert.IsTrue(Validation.validateDomainPart(str));

            foreach (string str in falseTests)
                Assert.IsFalse(Validation.validateDomainPart(str));
        }

        
        // validate has valid dots
        [TestMethod()]
        public void hasValidDots()
        {
            string[] trueTests = {
                "",
                "a.b",
                "a.b.c.d",
                "ab.cd"
            };
            string[] falseTests = {
                ".a",
                "a.",
                ".a.",
                "a..b"
            };

            // run tests
            foreach (string str in trueTests)
                Assert.IsTrue(Validation.hasValidDots(str));
            
            foreach (string str in falseTests)
                Assert.IsFalse(Validation.hasValidDots(str));
        }
    }
}
