using System;
using System.Xml;
using System.Xml.Linq;
using LamedalCore.domain.Attributes;
using Xunit;

namespace LamedalCore.Test.Tests.lib.XML
{
    public sealed class XML_Setup_Test
    {
        private readonly LamedalCore_ _lamed = LamedalCore_.Instance;

        [Fact]
        [Test_Method("Fix_InvalidXML()")]
        public void Convert_2ValidXML_Test()
        {
            Assert.Equal("Operation 2 &gt; 3", _lamed.lib.XML.Setup.Fix_InvalidXML("Operation 2 > 3"));
        }

        [Fact]
        [Test_Method("XML_Attribute()")]
        public void XML_Attribute_Test()
        {
            #region Test1: <param name=\"paramLine\">The parameter line.</param>
            string nameValue;
            string value = _lamed.lib.XML.Setup.XML_Attribute("<param name=\"paramLine\">The parameter line.</param>", "param", "name", out nameValue);

            Assert.Equal(value, "The parameter line.");
            Assert.Equal(nameValue, "paramLine");
            #endregion

            #region Test2: <param name=">">Return the dictionaryionary<stringing, tuple< type, attributeuteibute></param>
            // <param name=">">Return the dictionaryionary<stringing, tuple< type, attributeuteibute></param>

            #endregion
        }

        [Fact]
        [Test_Method("XML_Format()")]
        public void XML_Format_Test()
        {
            #region Test1: simple test
            string unformattedXml = "<?xml version=\"1.0\"?><book><author>Lewis, C.S.</author><title>The Four Loves</title></book>";
            var formatedXML = _lamed.lib.XML.Setup.XML_Format(unformattedXml);
            var result = 
@"<book>
  <author>Lewis, C.S.</author>
  <title>The Four Loves</title>
</book>";

            Assert.Equal(result, formatedXML);
            #endregion

            #region &AMP test
            unformattedXml = "<?xml version=\"1.0\"?><book><author>Lewis, C.S.<A HREF=\"/SEARCH?Q=TEST+SYNTAX&AMP;IE=UTF-8&AMP;GBV=1&AMP;SEI=CAQDWNMKMAACGABIJ4NQCQ\">HERE<\\A></author><title>The Four Loves</title></book>";
            unformattedXml = unformattedXml.ToLower();
            formatedXML = _lamed.lib.XML.Setup.XML_Format(unformattedXml, true);
            result =
@"<book>
  <author>lewis, c.s.<a href=""/search?q=test+syntax&amp;ie=utf-8&amp;gbv=1&amp;sei=caqdwnmkmaacgabij4nqcq"">here</a></author>
  <title>the four loves</title>
</book>";

            Assert.Equal(result, formatedXML);
            #endregion

            #region CaseTest
            unformattedXml = "<!doctype html><book><author>Lewis, C.S.</author><title>The Four Loves</title></book>";
            formatedXML = _lamed.lib.XML.Setup.XML_Format(unformattedXml, true);
            result =
@"<book>
  <author>Lewis, C.S.</author>
  <title>The Four Loves</title>
</book>";
            Assert.Equal(result, formatedXML);
            #endregion

            // Exception
            unformattedXml = "<?xml version=\"1.0\"?><book><author>Lewis, C.S.<A HREF=\"/SEARCH?Q=TEST+SYNTAX&AMP;IE=UTF-8&AMP;GBV=1&AMP;SEI=CAQDWNMKMAACGABIJ4NQCQ\">HERE<\\A></author><title>The Four Loves</title></bookk>";
            Assert.Throws<XmlException>(()=>_lamed.lib.XML.Setup.XML_Format(unformattedXml, true));
        }

        [Fact]
        [Test_Method("XML_Attribute()")]
        public void XML_Format_Test2()
        {
            #region Input

            var input =
@"<!doctype html><html itemscope="""" itemtype=""http://schema.org/searchresultspage"" lang=""en""><head><meta content=""text/html; charset=utf-8"" http-equiv=""content-type""><meta content=""/images/branding/googleg/1x/googleg_standard_color_128dp.png"" itemprop=""image""><link href=""/images/branding/product/ico/googleg_lodp.ico"" rel=""shortcut icon""><noscript><meta content=""0;url=/search?q=test+syntax;ie=utf-8;gbv=1;sei=uxadwir0fmudgaav17habg"" http-equiv=""refresh""><style>table,div,span,p{display:none}</style><div style=""display:block"">please click <a href=""/search?q=test+syntax;ie=utf-8;gbv=1;sei=uxadwir0fmudgaav17habg"">here</a> if you are not redirected within a few seconds.</div></noscript><title>test syntax - google search</title><style>#gb{font:13px/27px arial,sans-serif;height:30px}#gbz,#gbg{position:absolute;white-space:nowrap;top:0;height:30px;z-index:1000}#gbz{left:0;padding-left:4px}#gbg{right:0;padding-right:5px}#gbs{background:transparent;position:absolute;top:-999px;visibility:hidden;z-index:998;right:0}.gbto #gbs{background:#fff}#gbx3,#gbx4{background-color:#2d2d2d;background-image:none;_background-image:none;background-position:0 -138px;background-repeat:repeat-x;border-bottom:1px solid #000;font-size:24px;height:29px;_height:30px;opacity:1;filter:alpha(opacity=100);position:absolute;top:0;width:100%;z-index:990}#gbx3{left:0}#gbx4{right:0}#gbb{position:relative}#gbbw{left:0;position:absolute;top:30px;width:100%}.gbtcb{position:absolute;visibility:hidden}#gbz .gbtcb{right:0}#gbg .gbtcb{left:0}.gbxx{display:none !important}.gbxo{opacity:0 !important;filter:alpha(opacity=0) !important}.gbm{position:absolute;z-index:999;top:-999px;visibility:hidden;text-align:left;border:1px solid #bebebe;background:#fff;-moz-box-shadow:-1px 1px 1px rgba(0,0,0,.2);-webkit-box-shadow:0 2px 4px rgba(0,0,0,.2);box-shadow:0 2px 4px rgba(0,0,0,.2)}.gbrtl .gbm{-moz-box-shadow:1px 1px 1px rgba(0,0,0,.2)}.gbto .gbm,.gbto #gbs{top:29px;visibility:visible}#gbz .gbm{left:0}#gbg .gbm{right:0}.gbxms{background-color:#ccc;display:block;position:absolute;z-index:1;top:-1px;left:-2px;right:-2px;bottom:-2px;opacity:.4;-moz-border-radius:3px;filter:progid:dximagetransform.microsoft.blur(pixelradius=5);*opacity:1;*top:-2px;*left:-5px;*right:5px;*bottom:4px;-ms-filter:""progid:dximagetransform.microsoft.blur(pixelradius=5)"";opacity:1\0/;top:-4px\0/;left:-6px\0/;right:5px\0/;bottom:4px\0/}.gbma{position:relative;top:-1px;border-style:solid dashed dashed;border-color:transparent;border-top-color:#c0c0c0;display:-moz-inline-box;display:inline-block;font-size:0;height:0;line-height:0;width:0;border-width:3px 3px 0;padding-top:1px;left:4px}#gbztms1,#gbi4m1,#gbi4s,#gbi4t{zoom:1}.gbtc,.gbmc,.gbmcc{display:block;list-style:none;margin:0;padding:0}.gbmc{background:#fff;padding:10px 0;position:relative;z-index:2;zoom:1}.gbt{position:relative;display:-moz-inline-box;display:inline-block;line-height:27px;padding:0;vertical-align:top}.gbt{*display:inline}.gbto{box-shadow:0 2px 4px rgba(0,0,0,.2);-moz-box-shadow:0 2px 4px rgba(0,0,0,.2);-webkit-box-shadow:0 2px 4px rgba(0,0,0,.2)}.gbzt,.gbgt{cursor:pointer;display:block;text-decoration:none !important}span#gbg6,span#gbg4{cursor:default}.gbts{border-left:1px solid transparent;border-right:1px solid transparent;display:block;*display:inline-block;padding:0 5px;position:relative;z-index:1000}.gbts{*display:inline}.gbzt .gbts{display:inline;zoom:1}.gbto .gbts{background:#fff;border-color:#bebebe;color:#36c;padding-bottom:1px;padding-top:2px}.gbz0l .gbts{color:#fff;font-weight:bold}.gbtsa{padding-right:9px}#gbz .gbzt,#gbz .gbgt,#gbg .gbgt{color:#ccc!important}.gbtb2{display:block;border-top:2px solid transparent}.gbto .gbzt .gbtb2,.gbto .gbgt .gbtb2{border-top-width:0}.gbtb .gbts{background:url(//ssl.gstatic.com/gb/images/b_8d5afc09.png);_background:url(//ssl.gstatic.com/gb/images/b8_3615d64d.png);background-position:-27px -22px;border:0;font-size:0;padding:29px 0 0;*padding:27px 0 0;width:1px}.gbzt:hover,.gbzt:focus,.gbgt-hvr,.gbgt:focus{background-color:#4c4c4c;background-image:none;_background-image:none;background-position:0 -102px;background-repeat:repeat-x;outline:none;text-decoration:none !important}.gbpdjs .gbto .gbm{min-width:99%}.gbz0l .gbtb2{border-top-color:#dd4b39!important}#gbi4s,#gbi4s1{font-weight:bold}#gbg6.gbgt-hvr,#gbg6.gbgt:focus{background-color:transparent;background-image:none}.gbg4a{font-size:0;line-height:0}.gbg4a .gbts{padding:27px 5px 0;*padding:25px 5px 0}.gbto .gbg4a .gbts{padding:29px 5px 1px;*padding:27px 5px 1px}#gbi4i,#gbi4id{left:5px;border:0;height:24px;position:absolute;top:1px;width:24px}.gbto #gbi4i,.gbto #gbi4id{top:3px}.gbi4p{display:block;width:24px}#gbi4id{background-position:-44px -101px}#gbmpid{background-position:0 0}#gbmpi,#gbmpid{border:none;display:inline-block;height:48px;width:48px}#gbmpiw{display:inline-block;line-height:9px;padding-left:20px;margin-top:10px;position:relative}#gbmpi,#gbmpid,#gbmpiw{*display:inline}#gbg5{font-size:0}#gbgs5{padding:5px !important}.gbto #gbgs5{padding:7px 5px 6px !important}#gbi5{background:url(//ssl.gstatic.com/gb/images/b_8d5afc09.png);_background:url(//ssl.gstatic.com/gb/images/b8_3615d64d.png);background-position:0 0;display:block;font-size:0;height:17px;width:16px}.gbto #gbi5{background-position:-6px -22px}.gbn .gbmt,.gbn .gbmt:visited,.gbnd .gbmt,.gbnd .gbmt:visited{color:#dd8e27 !important}.gbf .gbmt,.gbf .gbmt:visited{color:#900 !important}.gbmt,.gbml1,.gbmlb,.gbmt:visited,.gbml1:visited,.gbmlb:visited{color:#36c !important;text-decoration:none !important}.gbmt,.gbmt:visited{display:block}.gbml1,.gbmlb,.gbml1:visited,.gbmlb:visited{display:inline-block;margin:0 10px}.gbml1,.gbmlb,.gbml1:visited,.gbmlb:visited{*display:inline}.gbml1,.gbml1:visited{padding:0 10px}.gbml1-hvr,.gbml1:focus{outline:none;text-decoration:underline !important}#gbpm .gbml1{display:inline;margin:0;padding:0;white-space:nowrap}.gbmlb,.gbmlb:visited{line-height:27px}.gbmlb-hvr,.gbmlb:focus{outline:none;text-decoration:underline !important}.gbmlbw{color:#ccc;margin:0 10px}.gbmt{padding:0 20px}.gbmt:hover,.gbmt:focus{background:#eee;cursor:pointer;outline:0 solid black;text-decoration:none !important}.gbm0l,.gbm0l:visited{color:#000 !important;font-weight:bold}.gbmh{border-top:1px solid #bebebe;font-size:0;margin:10px 0}#gbd4 .gbmc{background:#f5f5f5;padding-top:0}#gbd4 .gbsbic::-webkit-scrollbar-track:vertical{background-color:#f5f5f5;margin-top:2px}#gbmpdv{background:#fff;border-bottom:1px solid #bebebe;-moz-box-shadow:0 2px 4px rgba(0,0,0,.12);-o-box-shadow:0 2px 4px rgba(0,0,0,.12);-webkit-box-shadow:0 2px 4px rgba(0,0,0,.12);box-shadow:0 2px 4px rgba(0,0,0,.12);position:relative;z-index:1}#gbd4 .gbmh{margin:0}.gbmtc{padding:0;margin:0;line-height:27px}.gbmcc:last-child:after,#gbmpal:last-child:after{content:'\0a\0a';white-space:pre;position:absolute}#gbmps{*zoom:1}#gbd4 .gbpc,#gbmpas .gbmt{line-height:17px}#gbd4 .gbpgs .gbmtc{line-height:27px}#gbd4 .gbmtc{border-bottom:1px solid #bebebe}#gbd4 .gbpc{display:inline-block;margin:16px 0 10px;padding-right:50px;vertical-align:top}#gbd4 .gbpc{*display:inline}.gbpc .gbps,.gbpc .gbps2{display:block;margin:0 20px}#gbmplp.gbps{margin:0 10px}.gbpc .gbps{color:#000;font-weight:bold}.gbpc .gbpd{margin-bottom:5px}.gbpd .gbmt,.gbpd .gbps{color:#666 !important}.gbpd .gbmt{opacity:.4;filter:alpha(opacity=40)}.gbps2{color:#666;display:block}.gbp0{display:none}.gbp0 .gbps2{font-weight:bold}#gbd4 .gbmcc{margin-top:5px}.gbpmc{background:#fef9db}.gbpmc .gbpmtc{padding:10px 20px}#gbpm{border:0;*border-collapse:collapse;border-spacing:0;margin:0;white-space:normal}#gbpm .gbpmtc{border-top:none;color:#000 !important;font:11px arial,sans-serif}#gbpms{*white-space:nowrap}.gbpms2{font-weight:bold;white-space:nowrap}#gbmpal{*border-collapse:collapse;border-spacing:0;border:0;margin:0;white-space:nowrap;width:100%}.gbmpala,.gbmpalb{font:13px arial,sans-serif;line-height:27px;padding:10px 20px 0;white-space:nowrap}.gbmpala{padding-left:0;text-align:left}.gbmpalb{padding-right:0;text-align:right}#gbmpasb .gbps{color:#000}#gbmpal .gbqfbb{margin:0 20px}.gbp0 .gbps{*display:inline}a.gbiba{margin:8px 20px 10px}.gbmpiaw{display:inline-block;padding-right:10px;margin-bottom:6px;margin-top:10px}.gbxv{visibility:hidden}.gbmpiaa{display:block;margin-top:10px}.gbmpia{border:none;display:block;height:48px;width:48px}.gbmpnw{display:inline-block;height:auto;margin:10px 0;vertical-align:top} .gbqfb,.gbqfba,.gbqfbb{-moz-border-radius:2px;-webkit-border-radius:2px;border-radius:2px;cursor:default !important;display:inline-block;font-weight:bold;height:29px;line-height:29px;min-width:54px;*min-width:70px;padding:0 8px;text-align:center;text-decoration:none !important;-moz-user-select:none;-webkit-user-select:none}.gbqfb:focus,.gbqfba:focus,.gbqfbb:focus{border:1px solid #4d90fe;-moz-box-shadow:inset 0 0 0 1px rgba(255, 255, 255, 0.5);-webkit-box-shadow:inset 0 0 0 1px rgba(255, 255, 255, 0.5);box-shadow:inset 0 0 0 1px rgba(255, 255, 255, 0.5);outline:none}.gbqfb-hvr:focus,.gbqfba-hvr:focus,.gbqfbb-hvr:focus{-webkit-box-shadow:inset 0 0 0 1px #fff,0 1px 1px rgba(0,0,0,.1);-moz-box-shadow:inset 0 0 0 1px #fff,0 1px 1px rgba(0,0,0,.1);box-shadow:inset 0 0 0 1px #fff,0 1px 1px rgba(0,0,0,.1)}.gbqfb-no-focus:focus{border:1px solid #3079ed;-moz-box-shadow:none;-webkit-box-shadow:none;box-shadow:none}.gbqfb-hvr,.gbqfba-hvr,.gbqfbb-hvr{-webkit-box-shadow:0 1px 1px rgba(0,0,0,.1);-moz-box-shadow:0 1px 1px rgba(0,0,0,.1);box-shadow:0 1px 1px rgba(0,0,0,.1)}.gbqfb::-moz-focus-inner,.gbqfba::-moz-focus-inner,.gbqfbb::-moz-focus-inner{border:0}.gbqfba,.gbqfbb{border:1px solid #dcdcdc;border-color:rgba(0,0,0,.1);color:#444 !important;font-size:11px}.gbqfb{background-color:#4d90fe;background-image:-webkit-gradient(linear,left top,left bottom,from(#4d90fe),to(#4787ed));background-image:-webkit-linear-gradient(top,#4d90fe,#4787ed);background-image:-moz-linear-gradient(top,#4d90fe,#4787ed);background-image:-ms-linear-gradient(top,#4d90fe,#4787ed);background-image:-o-linear-gradient(top,#4d90fe,#4787ed);background-image:linear-gradient(top,#4d90fe,#4787ed);filter:progid:dximagetransform.microsoft.gradient(startcolorstr='#4d90fe',endcolorstr='#4787ed');border:1px solid #3079ed;color:#fff!important;margin:0 0}.gbqfb-hvr{border-color:#2f5bb7}.gbqfb-hvr:focus{border-color:#2f5bb7}.gbqfb-hvr,.gbqfb-hvr:focus{background-color:#357ae8;background-image:-webkit-gradient(linear,left top,left bottom,from(#4d90fe),to(#357ae8));background-image:-webkit-linear-gradient(top,#4d90fe,#357ae8);background-image:-moz-linear-gradient(top,#4d90fe,#357ae8);background-image:-ms-linear-gradient(top,#4d90fe,#357ae8);background-image:-o-linear-gradient(top,#4d90fe,#357ae8);background-image:linear-gradient(top,#4d90fe,#357ae8)}.gbqfb:active{background-color:inherit;-webkit-box-shadow:inset 0 1px 2px rgba(0, 0, 0, 0.3);-moz-box-shadow:inset 0 1px 2px rgba(0, 0, 0, 0.3);box-shadow:inset 0 1px 2px rgba(0, 0, 0, 0.3)}.gbqfba{background-color:#f5f5f5;background-image:-webkit-gradient(linear,left top,left bottom,from(#f5f5f5),to(#f1f1f1));background-image:-webkit-linear-gradient(top,#f5f5f5,#f1f1f1);background-image:-moz-linear-gradient(top,#f5f5f5,#f1f1f1);background-image:-ms-linear-gradient(top,#f5f5f5,#f1f1f1);background-image:-o-linear-gradient(top,#f5f5f5,#f1f1f1);background-image:linear-gradient(top,#f5f5f5,#f1f1f1);filter:progid:dximagetransform.microsoft.gradient(startcolorstr='#f5f5f5',endcolorstr='#f1f1f1')}.gbqfba-hvr,.gbqfba-hvr:active{background-color:#f8f8f8;background-image:-webkit-gradient(linear,left top,left bottom,from(#f8f8f8),to(#f1f1f1));background-image:-webkit-linear-gradient(top,#f8f8f8,#f1f1f1);background-image:-moz-linear-gradient(top,#f8f8f8,#f1f1f1);background-image:-ms-linear-gradient(top,#f8f8f8,#f1f1f1);background-image:-o-linear-gradient(top,#f8f8f8,#f1f1f1);background-image:linear-gradient(top,#f8f8f8,#f1f1f1);filter:progid:dximagetransform.microsoft.gradient(startcolorstr='#f8f8f8',endcolorstr='#f1f1f1')}.gbqfbb{background-color:#fff;background-image:-webkit-gradient(linear,left top,left bottom,from(#fff),to(#fbfbfb));background-image:-webkit-linear-gradient(top,#fff,#fbfbfb);background-image:-moz-linear-gradient(top,#fff,#fbfbfb);background-image:-ms-linear-gradient(top,#fff,#fbfbfb);background-image:-o-linear-gradient(top,#fff,#fbfbfb);background-image:linear-gradient(top,#fff,#fbfbfb);filter:progid:dximagetransform.microsoft.gradient(startcolorstr='#ffffff',endcolorstr='#fbfbfb')}.gbqfbb-hvr,.gbqfbb-hvr:active{background-color:#fff;background-image:-webkit-gradient(linear,left top,left bottom,from(#fff),to(#f8f8f8));background-image:-webkit-linear-gradient(top,#fff,#f8f8f8);background-image:-moz-linear-gradient(top,#fff,#f8f8f8);background-image:-ms-linear-gradient(top,#fff,#f8f8f8);background-image:-o-linear-gradient(top,#fff,#f8f8f8);background-image:linear-gradient(top,#fff,#f8f8f8);filter:progid:dximagetransform.microsoft.gradient(startcolorstr='#ffffff',endcolorstr='#f8f8f8')}.gbqfba-hvr,.gbqfba-hvr:active,.gbqfbb-hvr,.gbqfbb-hvr:active{border-color:#c6c6c6;-webkit-box-shadow:0 1px 1px rgba(0,0,0,.1);-moz-box-shadow:0 1px 1px rgba(0,0,0,.1);box-shadow:0 1px 1px rgba(0,0,0,.1);color:#222 !important}.gbqfba:active,.gbqfbb:active{-webkit-box-shadow:inset 0 1px 2px rgba(0,0,0,.1);-moz-box-shadow:inset 0 1px 2px rgba(0,0,0,.1);box-shadow:inset 0 1px 2px rgba(0,0,0,.1)} #gbmpas{max-height:220px}#gbmm{max-height:530px}.gbsb{-webkit-box-sizing:border-box;display:block;position:relative;*zoom:1}.gbsbic{overflow:auto}.gbsbis .gbsbt,.gbsbis .gbsbb{-webkit-mask-box-image:-webkit-gradient(linear,left top,right top,color-stop(0,rgba(0,0,0,.1)),color-stop(.5,rgba(0,0,0,.8)),color-stop(1,rgba(0,0,0,.1)));left:0;margin-right:0;opacity:0;position:absolute;width:100%}.gbsb .gbsbt:after,.gbsb .gbsbb:after{content:"""";display:block;height:0;left:0;position:absolute;width:100%}.gbsbis .gbsbt{background:-webkit-gradient(linear,left top,left bottom,from(rgba(0,0,0,.2)),to(rgba(0,0,0,0)));background-image:-webkit-linear-gradient(top,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:-moz-linear-gradient(top,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:-ms-linear-gradient(top,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:-o-linear-gradient(top,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:linear-gradient(top,rgba(0,0,0,.2),rgba(0,0,0,0));height:6px;top:0}.gbsb .gbsbt:after{border-top:1px solid #ebebeb;border-color:rgba(0,0,0,.3);top:0}.gbsb .gbsbb{-webkit-mask-box-image:-webkit-gradient(linear,left top,right top,color-stop(0,rgba(0,0,0,.1)),color-stop(.5,rgba(0,0,0,.8)),color-stop(1,rgba(0,0,0,.1)));background:-webkit-gradient(linear,left bottom,left top,from(rgba(0,0,0,.2)),to(rgba(0,0,0,0)));background-image:-webkit-linear-gradient(bottom,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:-moz-linear-gradient(bottom,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:-ms-linear-gradient(bottom,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:-o-linear-gradient(bottom,rgba(0,0,0,.2),rgba(0,0,0,0));background-image:linear-gradient(bottom,rgba(0,0,0,.2),rgba(0,0,0,0));bottom:0;height:4px}.gbsb .gbsbb:after{border-bottom:1px solid #ebebeb;border-color:rgba(0,0,0,.3);bottom:0} </style><style>.star{float:left;margin-top:1px;overflow:hidden}._yhd{font-size:11px}.j{width:34em}body,td,div,.p,a{font-family:arial,sans-serif;tap-highlight-color:rgba(255,255,255,0)}body{margin:0}a img{border:0}#gbar{float:left;height:22px;padding-left:2px;font-size:13px}.gsfi,.gsfs{font-size:17px}.w,.q:active,.q:visited,.tbotu{color:#11c}a.gl{text-decoration:none}._umd a:link{color:#0e1cb3}#foot{padding:0 8px}#foot a{white-space:nowrap}h3{font-size:16px;font-weight:normal;margin:0;padding:0}#res h3{display:inline}.hd{height:1px;position:absolute;top:-1000em}.g,body,html,table,.std{font-size:13px}.g{margin-bottom:23px;margin-top:0;zoom:1}ol li,ul li{list-style:none}h1,ol,ul,li{margin:0;padding:0}#mbend h2{font-weight:normal}.e{margin:2px 0 0.75em}#leftnav a{text-decoration:none}#leftnav h2{color:#767676;font-weight:normal;margin:0}#nav{border-collapse:collapse;margin-top:17px;text-align:left}#nav td{text-align:center}.nobr{white-space:nowrap}.ts{border-collapse:collapse}.s br{display:none}.csb{display:block;height:40px}.images_table td{line-height:17px;padding-bottom:16px}.images_table img{border:1px solid #ccc;padding:1px}#tbd,#abd{display:block;min-height:1px}#abd{padding-top:3px}#tbd li{display:inline}._itd,._jtd{margin-bottom:8px}#tbd .tbt li{display:block;font-size:13px;line-height:1.2;padding-bottom:3px;padding-left:8px;text-indent:-8px}.tbos,.b{font-weight:bold}em{font-weight:bold;font-style:normal}.mime{color:#1a0dab;font-weight:bold;font-size:x-small}._lwd{right:-2px !important;overflow:hidden}.soc a{text-decoration:none}.soc{color:#808080}._ac a{text-decoration:none}._ac{color:#808080}._kgd{color:#e7711b}#_vbb{border:1px solid #e0e0e0;margin-left:-8px;margin-right:-8px;padding:15px 20px 5px}._m3b{font-size:32px}._egc{color:#777;font-size:16px;margin-top:5px}._h0d{color:#777;font-size:14px;margin-top:5px}._hlh{border:1px solid #e0e0e0;padding-left:20px}._tki{border:1px solid #e0e0e0;padding:5px 20px}#vob{border:1px solid #e0e0e0;padding:15px 15px}#_nyc{font-size:22px;line-height:22px;padding-bottom:5px}#vob_st{line-height:1.24}._tsb{border-width:1px;border-style:solid;border-color:#eee;background-color:#fff;position:relative;margin-bottom:26px}._peb,._qeb,._usb{font-family:arial;font-weight:lighter}._peb{margin-bottom:5px}._peb{font-size:xx-large}._qeb{font-size:medium}._usb{font-size:small}._tsb{margin-left:-8px;margin-right:-15px;padding:20px 20px 24px}._roc{border-spacing:0px 2px}._soc{max-width:380px;text-overflow:ellipsis;white-space:nowrap;overflow:hidden;padding-left:0px}._v9b{padding-left:15px;white-space:nowrap;color:#666}._poc{padding-left:0px}._rkc{color:#212121}._hob{color:#878787}._lmf{color:#093}._jmf{color:#c00}._kmf{padding:1px}._ckg{color:#dd4b39}.gssb_a{padding:0 10px !important}.gssb_c{left:132px !important;right:295px !important;top:78px !important;width:572px !important}.gssb_c table{font-size:16px !important}.gssb_e{border:1px solid #ccc !important;border-top-color:#d9d9d9 !important}.gssb_i{background:#eee !important}#res{padding:0 8px}#rhs_block{padding-top:43px}#_fqd{padding:0 8px}#subform_ctrl{font-size:11px;height:17px;margin:5px 3px 0 17px}.taf{padding-bottom:3px}._chd{padding:20px 0 3px}._bhd{padding:20px 0 3px}#topstuff .e{padding-bottom:6px}.slk .sld{width:250px}.slk{margin-bottom:-3px}.slk ._z3b{padding-bottom:5px;width:250px}._qpd{margin-top:1px;margin-bottom:-11px}._zuc{color:#545454}._auc{padding-top:2px;padding-bottom:1px}._buc{padding-top:1px;margin-bottom:14px}.ac,.st{line-height:1.24}.mfr,#ofr{font-size:16px;margin:1em 0;padding:0 8px}._tli{padding-bottom:25px}.s{color:#545454}.ac,._jee{color:#545454}a.fl,._cd a,.osl a{color:#1a0dab;text-decoration:none}a:link{color:#1a0dab;cursor:pointer}#tads a:link{color:#1a0dab}#tads .soc a:link{color:#808080}#tads ._ac a:link{color:#808080}._ac a:link{color:#808080}._ac a:visited{color:#808080}._ac a:hover{color:#808080;text-decoration:underline}a:visited{color:#61c}.blg a{text-decoration:none}cite,cite a:link{color:#006621;font-style:normal}#tads cite{color:#006621}.kv{font-size:15px}.kvs{margin-top:1px}.kv,.kvs,.slp{display:block;margin-bottom:1px}.kt{border-spacing:2px 0;margin-top:1px}#mbend li{margin:20px 8px 0 0}.f{color:#808080}._pjb{color:#093}h4.r{display:inline;font-size:small;font-weight:normal}.g{line-height:1.2}._spb{display:inline-block;vertical-align:top;overflow:hidden;position:relative}._gnc{margin:0 0 2em 1.3em}._gnc li{list-style-type:disc}.osl{color:#777;margin-top:4px}.r{font-size:16px;margin:0}.spell{font-size:16px}.spell_orig{font-size:13px}.spell_orig a{text-decoration:none}.spell_orig b i{font-style:normal;font-weight:normal}.th{border:1px solid #ebebeb}.ts td{padding:0}.videobox{padding-bottom:3px}.slk a{text-decoration:none}#leftnav a:hover,#leftnav .tbou a:hover,.slk h3 a,a:hover{text-decoration:underline}#mn{table-layout:fixed;width:100%}#leftnav a{color:#222;font-size:13px}#leftnav{padding:43px 4px 4px 0}.tbos{color:#dd4b39}._aed{border-top:1px solid #efefef;font-size:13px;margin:10px 0 14px 10px;padding:0}.tbt{margin-bottom:28px}#tbd{padding:0 0 0 16px}.tbou a{color:#222}#center_col{border:0;padding:0 8px 0 0}#topstuff .e{padding-top:3px}#topstuff .sp_cnt{padding-top:6px}#ab_name{color:#dd4b39;font:20px ""arial"";margin-left:15px}._fld{border-bottom:1px solid #dedede;height:56px;padding-top:1px}#resultstats{color:#999;font-size:13px;overflow:hidden;white-space:nowrap}.mslg>td{padding-right:1px;padding-top:2px}.slk .sld{margin-top:2px;padding:5px 0 5px 5px}._mvd,.fmp{padding-top:3px}.close_btn{overflow:hidden}#fll a,#bfl a{color:#1a0dab !important;margin:0 12px;text-decoration:none !important}.ng{color:#dd4b39}#mss{margin:.33em 0 0;padding:0;display:table}._my{display:inline-block;float:left;white-space:nowrap;padding-right:16px}#mss p{margin:0;padding-top:5px}.tn{border-bottom:1px solid #ebebeb;display:block;float:left;height:59px;line-height:54px;min-width:980px;padding:0;position:relative;white-space:nowrap}._uxb,a._uxb{color:#777;cursor:pointer;display:inline-block;font-family:arial,sans-serif;font-size:small;height:54px;line-height:54px;margin:0 8px;padding:0 8px;text-decoration:none;white-space:nowrap}._ihd{border-bottom:3px solid #dd4b39;color:#dd4b39;font-weight:bold;margin:2px 8px 0}a._jhd:hover{color:black;text-decoration:none;white-space:nowrap}body{margin:0;padding:0}._sxc{display:inline-block;float:left;margin-top:2px}._hhd,a._hhd{margin-left:1px}.sd{line-height:43px;padding:0 8px 0 9px}a:active,.osl a:active,.tbou a:active,#leftnav a:active{color:#dd4b39}#_xud a:active,#bfl a:active{color:#dd4b39 !important}.csb{background:url(/images/nav_logo229.png) no-repeat;overflow:hidden}.close_btn{background:url(/images/nav_logo229.png) no-repeat -138px -84px;height:14px;width:14px;display:block}.star{background:url(/images/nav_logo229.png) no-repeat -94px -245px;height:13px;width:65px;display:block}.star div,.star span{background:url(/images/nav_logo229.png) no-repeat 0 -245px;height:13px;width:65px;display:block}._nbb{display:inline;margin:0 3px;outline-color:transparent;overflow:hidden;position:relative}._nbb>div{outline-color:transparent}._o0{border-color:transparent;border-style:solid dashed dashed;border-top-color:green;border-width:4px 4px 0 4px;cursor:pointer;display:inline-block;font-size:0;height:0;left:4px;line-height:0;outline-color:transparent;position:relative;top:-3px;width:0}._o0{margin-top:-4px}.am-dropdown-menu{display:block;background:#fff;border:1px solid #dcdcdc;font-size:13px;left:0;padding:0;position:absolute;right:auto;white-space:nowrap;z-index:3}._ykb{list-style:none;white-space:nowrap}._ykb:hover{background-color:#eee}a._zkb{color:#333;cursor:pointer;display:block;padding:7px 18px;text-decoration:none}#tads a._zkb{color:#333}.sfbgg{background:#f1f1f1;border-bottom:1px solid #e5e5e5;height:71px}#logocont{z-index:1;padding-left:4px;padding-top:4px}#logo{display:block;height:49px;margin-top:12px;margin-left:12px;overflow:hidden;position:relative;width:137px}#logo img{left:0;position:absolute;top:-41px}.lst-a{background:white;border:1px solid #d9d9d9;border-top-color:silver;width:570px}.lst-a:hover{border:1px solid #b9b9b9;border-top:1px solid #a0a0a0;box-shadow:inset 0 1px 2px rgba(0,0,0,0.1);-webkit-box-shadow:inset 0 1px 2px rgba(0,0,0,0.1);-moz-box-shadow:inset 0 1px 2px rgba(0,0,0,0.1)}.lst-td{border:none;padding:0}.tia input{border-right:none;padding-right:0}.tia{padding-right:0}.lst{background:none;border:none;color:#000;font:16px arial,sans-serif;float:left;height:22px;margin:0;padding:3px 6px 2px 9px;vertical-align:top;width:100%;word-break:break-all}.lst:focus{outline:none}.lst-b{background:none;border:none;height:26px;padding:0 6px 0 12px}.ds{border-right:1px solid #e7e7e7;position:relative;height:29px;margin-left:17px;z-index:100}.lsbb{background-image:-moz-linear-gradient(top,#4d90fe,#4787ed);background-image:-ms-linear-gradient(top,#4d90fe,#4787ed);background-image:-o-linear-gradient(top,#4d90fe,#4787ed);background-image:-webkit-gradient(linear,left top,left bottom,from(#4d90fe),to(#4787ed));background-image:-webkit-linear-gradient(top,#4d90fe,#4787ed);background-image:linear-gradient(top,#4d90fe,#4787ed);border:1px solid #3079ed;border-radius:2px;background-color:#4d90fe;height:27px;width:68px}.lsbb:hover{background-image:-moz-linear-gradient(top,#4d90fe,#357ae8);background-image:-ms-linear-gradient(top,#4d90fe,#357ae8);background-image:-o-linear-gradient(top,#4d90fe,#357ae8);background-image:-webkit-gradient(linear,left top,left bottom,from(#4d90fe),to(#357ae8));background-image:-webkit-linear-gradient(top,#4d90fe,#357ae8);background-color:#357ae8;background-image:linear-gradient(top,#4d90fe,#357ae8);border:1px solid #2f5bb7}.lsb{background:transparent;background-position:0 -343px;background-repeat:repeat-x;border:none;color:#000;cursor:default;font:15px arial,sans-serif;height:29px;margin:0;vertical-align:top;width:100%}.lsb:active{-moz-box-shadow:inset 0 1px 2px rgba(0,0,0,0.3);-webkit-box-shadow:inset 0 1px 2px rgba(0,0,0,0.3);box-shadow:inset 0 1px 2px rgba(0,0,0,0.3);background:transparent;color:transparent;overflow:hidden;position:relative;width:100%}.sbico{color:transparent;display:inline-block;height:15px;margin:0 auto;margin-top:2px;width:15px;overflow:hidden}</style><script>(function(){window.google={kei:'uxadwir0fmudgaav17habg',kexpi:'1351903,1352240,1352273,1352352,1352443,3700336,4029815,4032678,4038012,4043492,4045841,4048347,4062666,4065786,4068550,4068560,4069838,4069840,4070139,4072602,4072777,4073405,4073728,4073915,4073959,4076095,4076316,4076931,4076999,4078010,4078430,4078438,4079105,4079874,4079894,4081039,4082618,4082940,4083030,4083113,4083476,4084029,4084343,4084717,4085413,4085683,4086011,4087708,4088429,4088436,4088448,4088551,4088643,4089003,4089106,4089538,4090352,4090368,4090657,4090806,4090884,4090894,4091060,4091302,4092897,4092934,4093120,8300096,8300273,8300484,8507381,8507419,8507860,8508043,8508624,8509066,10200083,10200096,13500021,13500023',authuser:0,kscs:'c9c918f0_24'};google.khl='en';})();(function(){google.lc=[];google.li=0;google.getei=function(a){for(var b;a&&(!a.getattribute||!(b=a.getattribute(""eid"")));)a=a.parentnode;return b||google.kei};google.getlei=function(a){for(var b=null;a&&(!a.getattribute||!(b=a.getattribute(""leid"")));)a=a.parentnode;return b};google.https=function(){return""https:""==window.location.protocol};google.ml=function(){return null};google.wl=function(a,b){try{google.ml(error(a),!1,b)}catch(c){}};google.time=function(){return(new date).gettime()};google.log=function(a,b,c,d,g){a=google.logurl(a,b,c,d,g);if(""""!=a){b=new image;var e=google.lc,f=google.li;e[f]=b;b.onerror=b.onload=b.onabort=function(){delete e[f]};window.google&&window.google.vel&&window.google.vel.lu&&window.google.vel.lu(a);b.src=a;google.li=f+1}};google.logurl=function(a,b,c,d,g){var e="""",f=google.ls||"""";c||-1!=b.search(""&ei="")||(e=""&ei=""+google.getei(d),-1==b.search(""&lei="")&&(d=google.getlei(d))&&(e+=""&lei=""+d));a=c||""/""+(g||""gen_204"")+""?atyp=i&ct=""+a+""&cad=""+b+e+f+""&zx=""+google.time();/^http:/i.test(a)&&google.https()&&(google.ml(error(""a""),!1,{src:a,glmm:1}),a="""");return a};google.y={};google.x=function(a,b){google.y[a.id]=[a,b];return!1};google.lq=[];google.load=function(a,b,c){google.lq.push([[a],b,c])};google.loadall=function(a,b){google.lq.push([a,b])};}).call(this);(function(){var b=[function(){google.c&&google.tick(""load"",""dcl"")}];google.dcl=!1;google.dclc=function(a){google.dcl?a():b.push(a)};function c(){if(!google.dcl){google.dcl=!0;for(var a;a=b.shift();)a()}}window.addeventlistener?(document.addeventlistener(""domcontentloaded"",c,!1),window.addeventlistener(""load"",c,!1)):window.attachevent&&window.attachevent(""onload"",c);}).call(this);</script><script type=""text/javascript"">(function(){try{var e=this;var aa=function(a,b,c,d){d=d||{};d._sn=[""cfg"",b,c].join(""."");window.gbar.logger.ml(a,d)};var g=window.gbar=window.gbar||{},h=window.gbar.i=window.gbar.i||{},ba;function _tvn(a,b){a=parseint(a,10);return isnan(a)?b:a}function _tvf(a,b){a=parsefloat(a);return isnan(a)?b:a}function _tvv(a){return!!a}function p(a,b,c){(c||g)[a]=b}g.bv={n:_tvn(""2"",0),r:"""",f:"".66.41."",e:""1300102,3700336"",m:_tvn(""1"",1)}; function ca(a,b,c){var d=""on""+b;if(a.addeventlistener)a.addeventlistener(b,c,!1);else if(a.attachevent)a.attachevent(d,c);else{var f=a[d];a[d]=function(){var a=f.apply(this,arguments),b=c.apply(this,arguments);return void 0==a?b:void 0==b?a:b&&a}}}var da=function(a){return function(){return g.bv.m==a}},ea=da(1),fa=da(2);p(""sb"",ea);p(""kn"",fa);h.a=_tvv;h.b=_tvf;h.c=_tvn;h.i=aa;var q=window.gbar.i.i;var r=function(){},u=function(){},ia=function(a){var b=new image,c=ga;b.onerror=b.onload=b.onabort=function(){try{delete ha[c]}catch(d){}};ha[c]=b;b.src=a;ga=c+1},ha=[],ga=0;p(""logger"",{il:u,ml:r,log:ia});var v=window.gbar.logger;var y={},ja={},z=[],ka=h.b(""0.1"",.1),la=h.a(""1"",!0),ma=function(a,b){z.push([a,b])},na=function(a,b){y[a]=b},oa=function(a){return a in y},a={},b=function(a,b){a[a]||(a[a]=[]);a[a].push(b)},d=function(a){b(""m"",a)},pa=function(a,b){var c=document.createelement(""script"");c.src=a;c.async=la;math.random()<ka&&(c.onerror=function(){c.onerror=null;r(error(""bundle load failed: name=""+(b||""unk"")+"" url=""+a))});(document.getelementbyid(""xjsc"")||document.getelementsbytagname(""body"")[0]|| document.getelementsbytagname(""head"")[0]).appendchild(c)},f=function(a){for(var b=0,c;(c=z[b])&&c[0]!=a;++b);!c||c[1].l||c[1].s||(c[1].s=!0,qa(2,a),c[1].url&&pa(c[1].url,a),c[1].libs&&e&&e(c[1].libs))},ra=function(a){b(""gc"",a)},sa=null,ta=function(a){sa=a},qa=function(a,b,c){if(sa){a={t:a,b:b};if(c)for(var d in c)a[d]=c[d];try{sa(a)}catch(f){}}};p(""mdc"",y);p(""mdi"",ja);p(""bnc"",z);p(""qgc"",ra);p(""qm"",d);p(""qd"",a);p(""lb"",f);p(""mcf"",na);p(""bcf"",ma);p(""aq"",b);p(""mdd"",""""); p(""has"",oa);p(""trh"",ta);p(""tev"",qa);if(h.a(""1"")){var ua=h.a(""1""),va=h.a(""""),wa=h.a(""""),xa=window.gapi={},ya=function(a,b){var c=function(){g.dgl(a,b)};ua?d(c):(b(""gl"",c),f(""gl""))},za={},aa=function(a){a=a.split("":"");for(var b;(b=a.pop())&&za[b];);return!b},e=function(a){function b(){for(var b=a.split("":""),d=0,f;f=b[d];++d)za[f]=1;for(b=0;d=z[b];++b)d=d[1],(f=d.libs)&&!d.l&&d.i&&aa(f)&&d.i()}g.dgl(a,b)},h=window.___jsl={};h.h=""m;/_/scs/abc-static/_/js/k=gapi.gapi.en.ht56ryqzvz8.o/m=__features__/rt=j/d=1/rs=ahpooo9bonef1l6m_fnbxcrkqyntbnmzbg"";h.ms=""https://apis.google.com""; h.m="""";h.l=[];ua||z.push([""gl"",{url:""//ssl.gstatic.com/gb/js/abc/glm_e7bb39a7e1a24581ff4f8d199678b1b9.js""}]);var ba={pu:va,sh:"""",si:wa,hl:""en""};y.gl=ba;p(""load"",ya,xa);p(""dgl"",ya);p(""agl"",aa);h.o=ua};var ca=h.b(""0.1"",.001),da=0; function _mltoken(a,b){try{if(1>da){da++;var c,d=a;b=b||{};var f=encodeuricomponent,k=[""//www.google.com/gen_204?atyp=i&zx="",(new date).gettime(),""&jexpid="",f(""28834""),""&srcpg="",f(""prop=1""),""&jsr="",math.round(1/ca),""&ogev="",f(""uxadwpdufejygaapkaa4cg""),""&ogf="",g.bv.f,""&ogrp="",f(""1""),""&ogv="",f(""1484623828.0""),""&oggv=""+f(""es_plusone_gc_20170111.0_p0""),""&ogd="",f(""com""),""&ogc="",f(""zaf""),""&ogl="",f(""en"")];b._sn&&(b._sn= ""og.""+b._sn);for(var m in b)k.push(""&""),k.push(f(m)),k.push(""=""),k.push(f(b[m]));k.push(""&emsg="");k.push(f(d.name+"":""+d.message));var n=k.join("""");ea(n)&&(n=n.substr(0,2e3));c=n;var l=window.gbar.logger._aem(a,c);ia(l)}}catch(t){}}var ea=function(a){return 2e3<=a.length},fa=function(a,b){return b};function ga(a){r=a;p(""_itl"",ea,v);p(""_aem"",fa,v);p(""ml"",r,v);a={};y.er=a}h.a("""")?ga(function(a){throw a;}):h.a(""1"")&&math.random()<ca&&ga(_mltoken);var _e=""left"",ha=h.a(""""),j=function(a,b){var c=a.classname;i(a,b)||(a.classname+=(""""!=c?"" "":"""")+b)},k=function(a,b){var c=a.classname;b=new regexp(""\\s?\\b""+b+""\\b"");c&&c.match(b)&&(a.classname=c.replace(b,""""))},i=function(a,b){b=new regexp(""\\b""+b+""\\b"");a=a.classname;return!(!a||!a.match(b))},ia=function(a,b){i(a,b)?k(a,b):j(a,b)},ja=function(a,b){a[b]=function(c){var d=arguments;g.qm(function(){a[b].apply(this,d)})}},ka=function(a){a=[""//www.gstatic.com"",""/og/_/js/d=1/k="", ""og.og2.en_us.2fdt2iyu7zq.o"",""/rt=j/m="",a,""/rs="",""aa2yrtv942cmvwk2_8wn63uqfjesasyija""];ha&&a.push(""?host=www.gstatic.com&bust=og.og2.en_us.apyx5xfmn7y.du"");a=a.join("""");pa(a)};p(""ca"",j);p(""cr"",k);p(""cc"",i);h.k=j;h.l=k;h.m=i;h.n=ia;h.p=ka;h.q=ja;var la=[""gb_71"",""gb_155""],ma;function na(a){ma=a}function oa(a){var b=ma&&!a.href.match(/.*\/accounts\/clearsid[?]/)&&encodeuricomponent(ma());b&&(a.href=a.href.replace(/([?&]continue=)[^&]*/,""$1""+b))}function pa(a){window.gapplication&&(a.href=window.gapplication.gettaburl(a.href))}function qa(a){try{var b=(document.forms[0].q||"""").value;b&&(a.href=a.href.replace(/([?&])q=[^&]*|$/,function(a,d){return(d||""&"")+""q=""+encodeuricomponent(b)}))}catch(c){q(c,""sb"",""pq"")}} var ra=function(){for(var a=[],b=0,c;c=la[b];++b)(c=document.getelementbyid(c))&&a.push(c);return a},sa=function(){var a=ra();return 0<a.length?a[0]:null},ta=function(){return document.getelementbyid(""gb_70"")},l={},m={},ua={},n={},o=void 0,za=function(a,b){try{var c=document.getelementbyid(""gb"");j(c,""gbpdjs"");p();va(document.getelementbyid(""gb""))&&j(c,""gbrtl"");if(b&&b.getattribute){var d=b.getattribute(""aria-owns"");if(d.length){var f=document.getelementbyid(d);if(f){var k=b.parentnode;if(o==d)o=void 0, k(k,""gbto"");else{if(o){var m=document.getelementbyid(o);if(m&&m.getattribute){var n=m.getattribute(""aria-owner"");if(n.length){var l=document.getelementbyid(n);l&&l.parentnode&&k(l.parentnode,""gbto"")}}}wa(f)&&xa(f);o=d;j(k,""gbto"")}}}}d(function(){g.tg(a,b,!0)});ya(a)}catch(t){q(t,""sb"",""tg"")}},$a=function(a){d(function(){g.close(a)})},ab=function(a){d(function(){g.rdd(a)})},va=function(a){var b,c=document.defaultview;c&&c.getcomputedstyle?(a=c.getcomputedstyle(a,""""))&&(b=a.direction):b=a.currentstyle? a.currentstyle.direction:a.style.direction;return""rtl""==b},cb=function(a,b,c){if(a)try{var d=document.getelementbyid(""gbd5"");if(d){var f=d.firstchild,k=f.firstchild,m=document.createelement(""li"");m.classname=b+"" gbmtc"";m.id=c;a.classname=""gbmt"";m.appendchild(a);if(k.haschildnodes()){c=[[""gbkc""],[""gbf"",""gbe"",""gbn""],[""gbkp""],[""gbnd""]];for(var d=0,n=k.childnodes.length,f=!1,l=-1,t=0,c;c=c[t];t++){for(var t=0,g;g=c[t];t++){for(;d<n&&i(k.childnodes[d],g);)d++;if(g==b){k.insertbefore(m,k.childnodes[d]|| null);f=!0;break}}if(f){if(d+1<k.childnodes.length){var u=k.childnodes[d+1];i(u.firstchild,""gbmh"")||bb(u,c)||(l=d+1)}else if(0<=d-1){var v=k.childnodes[d-1];i(v.firstchild,""gbmh"")||bb(v,c)||(l=d)}break}0<d&&d+1<n&&d++}if(0<=l){var w=document.createelement(""li""),x=document.createelement(""div"");w.classname=""gbmtc"";x.classname=""gbmt gbmh"";w.appendchild(x);k.insertbefore(w,k.childnodes[l])}g.addhover&&g.addhover(a)}else k.appendchild(m)}}catch(ab){q(ab,""sb"",""al"")}},bb=function(a,b){for(var c=b.length, d=0;d<c;d++)if(i(a,b[d]))return!0;return!1},db=function(a,b,c){cb(a,b,c)},eb=function(a,b){cb(a,""gbe"",b)},fb=function(){d(function(){g.pcm&&g.pcm()})},gb=function(){d(function(){g.pca&&g.pca()})},hb=function(a,b,c,d,f,k,m,n,l,t){d(function(){g.paa&&g.paa(a,b,c,d,f,k,m,n,l,t)})},ib=function(a,b){l[a]||(l[a]=[]);l[a].push(b)},jb=function(a,b){m[a]||(m[a]=[]);m[a].push(b)},kb=function(a,b){ua[a]=b},lb=function(a,b){n[a]||(n[a]=[]);n[a].push(b)},ya=function(a){a.preventdefault&&a.preventdefault();a.returnvalue= !1;a.cancelbubble=!0},mb=null,xa=function(a,b){p();if(a){nb(a,""opening&hellip;"");q(a,!0);b=""undefined""!=typeof b?b:1e4;var c=function(){ob(a)};mb=window.settimeout(c,b)}},pb=function(a){p();a&&(q(a,!1),nb(a,""""))},ob=function(a){try{p();var b=a||document.getelementbyid(o);b&&(nb(b,""this service is currently unavailable.%1$splease try again later."",""%1$s""),q(b,!0))}catch(c){q(c,""sb"",""sdhe"")}},nb=function(a,b,c){if(a&&b){var d=wa(a);if(d){if(c){d.innerhtml="""";b=b.split(c);c=0;for(var f;f=b[c];c++){var k=document.createelement(""div""); k.innerhtml=f;d.appendchild(k)}}else d.innerhtml=b;q(a,!0)}}},q=function(a,b){(b=void 0!==b?b:!0)?j(a,""gbmsgo""):k(a,""gbmsgo"")},wa=function(a){for(var b=0,c;c=a.childnodes[b];b++)if(i(c,""gbmsg""))return c},p=function(){mb&&window.cleartimeout(mb)},qb=function(a){var b=""inner""+a;a=""offset""+a;return window[b]?window[b]:document.documentelement&&document.documentelement[a]?document.documentelement[a]:0},rb=function(){return!1},sb=function(){return!!o};p(""so"",sa);p(""sos"",ra);p(""si"",ta);p(""tg"",za); p(""close"",$a);p(""rdd"",ab);p(""addlink"",db);p(""addextralink"",eb);p(""pcm"",fb);p(""pca"",gb);p(""paa"",hb);p(""ddld"",xa);p(""ddrd"",pb);p(""dderr"",ob);p(""rtl"",va);p(""op"",sb);p(""bh"",l);p(""abh"",ib);p(""dh"",m);p(""adh"",jb);p(""ch"",n);p(""ach"",lb);p(""eh"",ua);p(""aeh"",kb);ba=h.a("""")?pa:qa;p(""qs"",ba);p(""setcontinuecb"",na);p(""pc"",oa);p(""bsy"",rb);h.d=ya;h.j=qb;var tb={};y.base=tb;z.push([""m"",{url:""//ssl.gstatic.com/gb/js/sem_4f127ea60a7a890a4674c20bf04155d7.js""}]);g.sg={c:""1""};p(""wg"",{rg:{}});var ub={tiw:h.c(""15000"",0),tie:h.c(""30000"",0)};y.wg=ub;var vb={thi:h.c(""10000"",0),thp:h.c(""180000"",0),tho:h.c(""5000"",0),tet:h.b(""0.5"",0)};y.wm=vb;if(h.a(""1"")){var wb=h.a("""");z.push([""gc"",{auto:wb,url:""//ssl.gstatic.com/gb/js/abc/gci_91f30755d6a6b787dcc2a4062e6e9824.js"",libs:""googleapis.client:plusone:gapi.iframes""}]);var xb={version:""gci_91f30755d6a6b787dcc2a4062e6e9824.js"",index:"""",lang:""en""};y.gc=xb;var yb=function(a){window.googleapis&&window.iframes?a&&a():(a&&ra(a),f(""gc""))};p(""lgc"",yb);h.a(""1"")&&p(""lpwf"",yb)};window.__pvt="""";if(h.a(""1"")&&h.a(""1"")){var zb=function(a){yb(function(){b(""pw"",a);f(""pw"")})};p(""lpw"",zb);z.push([""pw"",{url:""//ssl.gstatic.com/gb/js/abc/pwm_45f73e4df07a0e388b0fa1f3d30e7280.js""}]);var bb=[],cb=function(a){bb[0]=a},db=function(a,b){b=b||{};b._sn=""pw"";r(a,b)},eb={signed:bb,elog:db,base:""https://plusone.google.com/u/0"",loadtime:(new date).gettime()};y.pw=eb;var fb=function(a,b){var c=b.split(""."");b=function(){var b=arguments;a(function(){for(var a=g,d=0,f=c.length-1;d<f;++d)a=a[c[d]];a[c[d]].apply(a,b)})};for(var d=g,f=0,k=c.length-1;f< k;++f)d=d[c[f]]=d[c[f]]||{};return d[c[f]]=b};fb(zb,""pw.clk"");fb(zb,""pw.hvr"");p(""su"",cb,g.pw)};var gb=[1,2,3,4,5,6,9,10,11,13,14,28,29,30,34,35,37,38,39,40,41,42,43,48,49,500];var hb=h.b(""0.001"",1e-4),ib=h.b(""1"",1),jb=!1,kb=!1;if(h.a(""1"")){var lb=math.random();lb<hb&&(jb=!0);lb<ib&&(kb=!0)}var r=null; function mb(a,b){var c=hb,d=jb,f;f=a;if(!r){r={};for(var k=0;k<gb.length;k++){var m=gb[k];r[m]=!0}}if(f=!!r[f])c=ib,d=kb;if(d){var d=encodeuricomponent,n=""es_plusone_gc_20170111.0_p0"",l;g.rp?(l=g.rp(),l=""-1""!=l?l:""1""):l=""1"";f=(new date).gettime();var k=d(""28834""),m=d(""uxadwpdufejygaapkaa4cg""),t=g.bv.f,c=d(""1"");l=d(l);var c=math.round(1/c),t=d(""1484623828.0""),n=n?""&oggv=""+d(n):"""",g=d(""com""),u=d(""en""), v=d(""zaf""),w;w=0;h.a("""")&&(w|=1);h.a("""")&&(w|=2);h.a("""")&&(w|=4);a=[""//www.google.com/gen_204?atyp=i&zx="",f,""&oge="",a,""&ogex="",k,""&ogev="",m,""&ogf="",t,""&ogp="",c,""&ogrp="",l,""&ogsr="",c,""&ogv="",t,n,""&ogd="",g,""&ogl="",u,""&ogc="",v,""&ogus="",w];if(b){""ogw""in b&&(a.push(""&ogw=""+b.ogw),delete b.ogw);var x;f=[];for(x in b)0!=f.length&&f.push("",""),f.push(nb(x)),f.push("".""),f.push(nb(b[x]));x=f.join("""");""""!=x&&(a.push(""&ogad=""),a.push(d(x)))}ia(a.join(""""))}} function nb(a){""number""==typeof a&&(a+="""");return""string""==typeof a?a.replace(""."",""%2e"").replace("","",""%2c""):a}u=mb;p(""il"",u,v);var ob={};y.il=ob;var pb=function(a,b,c,d,f,k,m,n,l,t){d(function(){g.paa(a,b,c,d,f,k,m,n,l,t)})},qb=function(){d(function(){g.prm()})},rb=function(a){d(function(){g.spn(a)})},sb=function(a){d(function(){g.sps(a)})},tb=function(a){d(function(){g.spp(a)})},ub={""27"":""//ssl.gstatic.com/gb/images/silhouette_24.png"",""27"":""//ssl.gstatic.com/gb/images/silhouette_24.png"",""27"":""//ssl.gstatic.com/gb/images/silhouette_24.png""},vb=function(a){return(a=ub[a])||""//ssl.gstatic.com/gb/images/silhouette_24.png""}, wb=function(){d(function(){g.spd()})};p(""spn"",rb);p(""spp"",tb);p(""sps"",sb);p(""spd"",wb);p(""paa"",pb);p(""prm"",qb);ib(""gbd4"",qb); if(h.a("""")){var xb={d:h.a(""""),e:"""",sanw:h.a(""""),p:""//ssl.gstatic.com/gb/images/silhouette_96.png"",cp:""1"",xp:h.a(""1""),mg:""%1$s (delegated)"",md:""%1$s (default)"",mh:""220"",s:""1"",pp:vb,ppl:h.a(""""),ppa:h.a(""""), ppm:""google+ page""};y.prf=xb};var s,yb,w,zb,x=0,$b=function(a,b,c){if(a.indexof)return a.indexof(b,c);if(array.indexof)return array.indexof(a,b,c);for(c=null==c?0:0>c?math.max(0,a.length+c):c;c<a.length;c++)if(c in a&&a[c]===b)return c;return-1},y=function(a,b){return-1==$b(a,x)?(q(error(x+""_""+b),""up"",""caa""),!1):!0},bc=function(a,b){y([1,2],""r"")&&(s[a]=s[a]||[],s[a].push(b),2==x&&window.settimeout(function(){b(ac(a))},0))},cc=function(a,b,c){if(y([1],""nap"")&&c){for(var d=0;d<c.length;d++)yb[c[d]]=!0;g.up.spl(a,b,""nap"",c)}},dc= function(a,b,c){if(y([1],""aop"")&&c){if(w)for(var d in w)w[d]=w[d]&&-1!=$b(c,d);else for(w={},d=0;d<c.length;d++)w[c[d]]=!0;g.up.spl(a,b,""aop"",c)}},ec=function(){try{if(x=2,!zb){zb=!0;for(var a in s)for(var b=s[a],c=0;c<b.length;c++)try{b[c](ac(a))}catch(d){q(d,""up"",""tp"")}}}catch(d){q(d,""up"",""mtp"")}},ac=function(a){if(y([2],""ssp"")){var b=!yb[a];w&&(b=b&&!!w[a]);return b}};zb=!1;s={};yb={};w=null; var x=1,fc=function(a){var b=!1;try{b=a.cookie&&a.cookie.match(""pref"")}catch(c){}return!b},gc=function(){try{return!!e.localstorage&&""object""==typeof e.localstorage}catch(a){return!1}},hc=function(a){return a&&a.style&&a.style.behavior&&""undefined""!=typeof a.load},ic=function(a,b,c,d){try{fc(document)||(d||(b=""og-up-""+b),gc()?e.localstorage.setitem(b,c):hc(a)&&(a.setattribute(b,c),a.save(a.id)))}catch(f){f.code!=domexception.quota_exceeded_err&&q(f,""up"",""spd"")}},jc=function(a,b,c){try{if(fc(document))return""""; c||(b=""og-up-""+b);if(gc())return e.localstorage.getitem(b);if(hc(a))return a.load(a.id),a.getattribute(b)}catch(d){d.code!=domexception.quota_exceeded_err&&q(d,""up"",""gpd"")}return""""},kc=function(a,b,c){a.addeventlistener?a.addeventlistener(b,c,!1):a.attachevent&&a.attachevent(""on""+b,c)},lc=function(a){for(var b=0,c;c=a[b];b++){var d=g.up;c=c in d&&d[c];if(!c)return!1}return!0},mc=function(a,b){try{if(fc(a))return-1;var c=a.cookie.match(/ogpc=([^;]*)/);if(c&&c[1]){var d=c[1].match(new regexp(""\\b""+ b+""-([0-9]+):""));if(d&&d[1])return parseint(d[1],10)}}catch(f){f.code!=domexception.quota_exceeded_err&&q(f,""up"",""gcc"")}return-1};p(""up"",{r:bc,nap:cc,aop:dc,tp:ec,ssp:ac,spd:ic,gpd:jc,aeh:kc,aal:lc,gcc:mc});var z=function(a,b){a[b]=function(c){var d=arguments;g.qm(function(){a[b].apply(this,d)})}};z(g.up,""sl"");z(g.up,""si"");z(g.up,""spl"");z(g.up,""dpc"");z(g.up,""iic"");g.mcf(""up"",{sp:h.b(""0.01"",1),tld:""com"",prid:""1""});function nc(){function a(){for(var b;(b=k[m++])&&""m""!=b[0]&&!b[1].auto;);b&&(qa(2,b[0]),b[1].url&&pa(b[1].url,b[0]),b[1].libs&&e&&e(b[1].libs));m<k.length&&settimeout(a,0)}function b(){0<f--?settimeout(b,0):a()}var c=h.a(""1""),d=h.a(""""),f=3,k=z,m=0,n=window.gbaronready;if(n)try{n()}catch(l){q(l,""ml"",""or"")}d?p(""ldb"",a):c?ca(window,""load"",b):b()}p(""rdl"",nc);}catch(e){window.gbar&&gbar.logger&&gbar.logger.ml(e,{""_sn"":""cfg.init""});}})(); (function(){try{var a=window.gbar;a.mcf(""pm"",{p:""""});}catch(e){window.gbar&&gbar.logger&&gbar.logger.ml(e,{""_sn"":""cfg.init""});}})(); (function(){try{var a=window.gbar;a.mcf(""mm"",{s:""1""});}catch(e){window.gbar&&gbar.logger&&gbar.logger.ml(e,{""_sn"":""cfg.init""});}})(); (function(){try{var d=window.gbar.i.i;var e=window.gbar;var f=e.i;var g=f.c(""1"",0),h=/\bgbmt\b/,k=function(a){try{var b=document.getelementbyid(""gb_""+g),c=document.getelementbyid(""gb_""+a);b&&f.l(b,h.test(b.classname)?""gbm0l"":""gbz0l"");c&&f.k(c,h.test(c.classname)?""gbm0l"":""gbz0l"")}catch(l){d(l,""sj"",""ssp"")}g=a},m=e.qs,n=function(a){var b;b=a.href;var c=window.location.href.match(/.*?:\/\/[^\/]*/)[0],c=new regexp(""^""+c+""/search\\?"");(b=c.test(b))&&!/(^|\\?|&)ei=/.test(a.href)&&(b=window.google)&&b.kexpi&&(a.href+=""&ei=""+b.kei)},p=function(a){m(a); n(a)},q=function(){if(window.google&&window.google.sn){var a=/.*hp$/;return a.test(window.google.sn)?"""":""1""}return""-1""};e.rp=q;e.slp=k;e.qs=p;e.qsi=n;}catch(e){window.gbar&&gbar.logger&&gbar.logger.ml(e,{""_sn"":""cfg.init""});}})(); (function(){try{var a=this;var b=window.gbar;var c=b.i;var d=c.a,e=c.c,f={cty:""zaf"",cv:""1484623828"",dbg:d(""""),ecv:""0"",ei:e(""uxadwpdufejygaapkaa4cg""),ele:d(""1""),esr:e(""0.1""),evts:[""mousedown"",""touchstart"",""touchmove"",""wheel"",""keydown""],gbl:""es_plusone_gc_20170111.0_p0"",hd:""com"",hl:""en"",irp:d(""1""),pid:e(""1""), snid:e(""28834""),to:e(""300000""),u:e(""""),vf:"".66.41.""},g=f,h=[""bndcfg""],k=a;h[0]in k||!k.execscript||k.execscript(""var ""+h[0]);for(var l;h.length&&(l=h.shift());)h.length||void 0===g?k=k[l]?k[l]:k[l]={}:k[l]=g;}catch(e){window.gbar&&gbar.logger&&gbar.logger.ml(e,{""_sn"":""cfg.init""});}})(); (function(){try{window.gbar.rdl();}catch(e){window.gbar&&gbar.logger&&gbar.logger.ml(e,{""_sn"":""cfg.init""});}})(); </script><script>(function(){var a=function(f){for(var g=f.parentelement,d=null,e=0;e<g.childnodes.length;e++){var h=g.childnodes[e];-1<("" ""+h.classname+"" "").indexof("" am-dropdown-menu "")&&(d=h)}""none""==d.style.display?(d.style.display="""",google.log(""hpam"",""&ved=""+f.getattribute(""data-ved""))):d.style.display=""none""},b=[""google"",""sham""],c=this;b[0]in c||!c.execscript||c.execscript(""var ""+b[0]);for(var k;b.length&&(k=b.shift());)b.length||void 0===a?c[k]?c=c[k]:c=c[k]={}:c[k]=a;}).call(this);</script></head><body class=""hsrp"" bgcolor=""#ffffff"" marginheight=""0"" marginwidth=""0"" topmargin=""0""><div id=gb><script>window.gbar&&gbar.eli&&gbar.eli()</script><div id=gbw><div id=gbz><span class=gbtcb></span><ol id=gbzc class=gbtc><li class=gbt><a onclick=gbar.logger.il(1,{t:1}); class=""gbzt gbz0l gbp1"" id=gb_1 href=""https://www.google.com/webhp?tab=ww""><span class=gbtb2></span><span class=gbts>search</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:2}); class=gbzt id=gb_2 href=""https://www.google.com/search?hl=en&tbm=isch&source=og&tab=wi""><span class=gbtb2></span><span class=gbts>images</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:8}); class=gbzt id=gb_8 href=""https://maps.google.com/maps?hl=en&tab=wl""><span class=gbtb2></span><span class=gbts>maps</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:78}); class=gbzt id=gb_78 href=""https://play.google.com/?hl=en&tab=w8""><span class=gbtb2></span><span class=gbts>play</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:36}); class=gbzt id=gb_36 href=""https://www.youtube.com/results?tab=w1""><span class=gbtb2></span><span class=gbts>youtube</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:5}); class=gbzt id=gb_5 href=""https://news.google.com/nwshp?hl=en&tab=wn""><span class=gbtb2></span><span class=gbts>news</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:23}); class=gbzt id=gb_23 href=""https://mail.google.com/mail/?tab=wm""><span class=gbtb2></span><span class=gbts>gmail</span></a></li><li class=gbt><a onclick=gbar.logger.il(1,{t:49}); class=gbzt id=gb_49 href=""https://drive.google.com/?tab=wo""><span class=gbtb2></span><span class=gbts>drive</span></a></li><li class=gbt><a class=gbgt id=gbztm href=""https://www.google.com/intl/en/options/"" onclick=""gbar.tg(event,this)"" aria-haspopup=true aria-owns=gbd><span class=gbtb2></span><span id=gbztms class=""gbts gbtsa""><span id=gbztms1>more</span><span class=gbma></span></span></a><div class=gbm id=gbd aria-owner=gbztm><div id=gbmmb class=""gbmc gbsb gbsbis""><ol id=gbmm class=""gbmcc gbsbic""><li class=gbmtc><a onclick=gbar.logger.il(1,{t:24}); class=gbmt id=gb_24 href=""https://www.google.com/calendar?tab=wc"">calendar</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:51}); class=gbmt id=gb_51 href=""https://translate.google.com/?hl=en&tab=wt"">translate</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:17}); class=gbmt id=gb_17 href=""http://www.google.com/mobile/?hl=en&tab=wd"">mobile</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:10}); class=gbmt id=gb_10 href=""https://www.google.com/search?hl=en&tbo=u&tbm=bks&source=og&tab=wp"">books</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:212}); class=gbmt id=gb_212 href=""https://wallet.google.com/?tab=wa"">wallet</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:6}); class=gbmt id=gb_6 href=""https://www.google.com/search?hl=en&tbo=u&tbm=shop&source=og&tab=wf"">shopping</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:30}); class=gbmt id=gb_30 href=""https://www.blogger.com/?tab=wj"">blogger</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:27}); class=gbmt id=gb_27 href=""https://www.google.com/finance?tab=we"">finance</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:31}); class=gbmt id=gb_31 href=""https://photos.google.com/?tab=wq&pageid=none"">photos</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:12}); class=gbmt id=gb_12 href=""https://www.google.com/search?hl=en&tbo=u&tbm=vid&source=og&tab=wv"">videos</a></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:25}); class=gbmt id=gb_25 href=""https://docs.google.com/document/?usp=docs_alc"">docs</a></li><li class=gbmtc><div class=""gbmt gbmh""></div></li><li class=gbmtc><a onclick=gbar.logger.il(1,{t:66}); href=""https://www.google.com/intl/en/options/"" class=gbmt>even more &raquo;</a></li></ol><div class=gbsbt></div><div class=gbsbb></div></div></div></li></ol></div><div id=gbg><h2 class=gbxx>account options</h2><span class=gbtcb></span><ol class=gbtc><li class=gbt><a target=_top href=""https://accounts.google.com/servicelogin?hl=en&passive=true&continue=https://www.google.com/search%3fq%3dtest%2bsyntax"" onclick=""gbar.logger.il(9,{l:'i'})"" id=gb_70 class=gbgt><span class=gbtb2></span><span id=gbgs4 class=gbts><span id=gbi4s1>sign in</span></span></a></li><li class=""gbt gbtb""><span class=gbts></span></li><li class=gbt><a class=gbgt id=gbg5 href=""http://www.google.com/preferences?hl=en"" title=""options"" onclick=""gbar.tg(event,this)"" aria-haspopup=true aria-owns=gbd5><span class=gbtb2></span><span id=gbgs5 class=gbts><span id=gbi5></span></span></a><div class=gbm id=gbd5 aria-owner=gbg5><div class=gbmc><ol id=gbom class=gbmcc><li class=""gbkc gbmtc""><a  class=gbmt href=""/preferences?hl=en"">search settings</a></li><li class=gbmtc><div class=""gbmt gbmh""></div></li><li class=""gbkp gbmtc""><a class=gbmt href=""http://www.google.com/history/optout?hl=en"">web history</a></li></ol></div></div></li></ol></div></div><div id=gbx3></div><div id=gbx4></div><script>window.gbar&&gbar.elp&&gbar.elp()</script></div><table id=""mn"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""position:relative""><tr><th width=""132""></th><th width=""573""></th><th width=""278""></th><th></th></tr><tr><td class=""sfbgg"" valign=""top""><div id=""logocont""><h1><a href=""/webhp?hl=en"" style=""background:url(/images/nav_logo229.png) no-repeat 0 -41px;height:37px;width:95px;display:block"" id=""logo"" title=""go to google home""></a></h1></div></td><td class=""sfbgg"" colspan=""2"" valign=""top"" style=""padding-left:0px""><form style=""display:block;margin:0;background:none"" action=""/search"" id=""tsf"" method=""get"" name=""gs""><table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin-top:20px;position:relative""><tr><td><div class=""lst-a""><table cellpadding=""0"" cellspacing=""0""><tr><td class=""lst-td"" width=""555"" valign=""bottom""><div style=""position:relative;zoom:1""><input class=""lst"" value=""test syntax"" title=""search"" autocomplete=""off"" id=""sbhost"" maxlength=""2048"" name=""q"" type=""text""></div></td></tr></table></div></td><td><div class=""ds""><div class=""lsbb""><button class=""lsb"" value=""search"" name=""btng"" type=""submit""><span class=""sbico"" style=""background:url(/images/nav_logo229.png) no-repeat -36px -111px;height:14px;width:13px;display:block""></span></button></div></div></td></tr></table></form></td><td class=""sfbgg"">&nbsp;</td></tr><tr style=""position:relative""><td><div style=""border-bottom:1px solid #ebebeb;height:59px""></div></td><td colspan=""2""><div class=""tn""><div class=""_uxb _ihd _sxc _hhd"">all</div><div class=""_sxc""><a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=isch;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auibq"">images</a></div><div class=""_sxc""><a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=vid;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auibg"">videos</a></div><div class=""_sxc""><a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=nws;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auibw"">news</a></div><div class=""_sxc""><a class=""_uxb _jhd"" href=""https://maps.google.com/maps?q=test+syntax;um=1;ie=utf-8;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auica"">maps</a></div><div class=""_sxc""><a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=bks;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auicq"">books</a></div></div><div style=""border-bottom:1px solid #ebebeb;height:59px""></div></td><td><div style=""border-bottom:1px solid #ebebeb;height:59px""></div></td></tr><tbody data-jibp=""h"" data-jiis=""uc"" id=""desktop-search""><style>._bu,._bu a:link,._bu a:visited,a._bu:link,a._bu:visited{color:#808080}._kbb{color:#61c}.ellip{overflow:hidden;text-overflow:ellipsis;white-space:nowrap}</style><tr><td id=""leftnav"" valign=""top""><div><h2 class=""hd"">search options</h2><ul class=""med"" id=""tbd""><li><ul class=""tbt""><li class=""tbos"" id=""qdr_"">any time</li><li class=""tbou"" id=""qdr_h""><a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:h;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past hour</a></li><li class=""tbou"" id=""qdr_d""><a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:d;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past 24 hours</a></li><li class=""tbou"" id=""qdr_w""><a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:w;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past week</a></li><li class=""tbou"" id=""qdr_m""><a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:m;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past month</a></li><li class=""tbou"" id=""qdr_y""><a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:y;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past year</a></li></ul></li><li><ul class=""tbt""><li class=""tbos"" id=""li_"">all results</li><li class=""tbou"" id=""li_1""><a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=li:1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">verbatim</a></li></ul></li></ul></div></td><td valign=""top""><div id=""center_col""><div class=""sd"" id=""resultstats"">about 63,200,000 results</div><div id=""res""><div id=""topstuff""></div><div id=""search""><div id=""ires""><ol><div class=""g""><h3 class=""r""><a href=""/url?q=https://www.cs.bham.ac.uk/~pxc/nlp/interactivenlp/nlp_syntest.html;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggtmaa;usg=afqjcnepnemerxsc_xbbptmm1-fvdddwqa""><b>syntax test</b></a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>https://www.cs.bham.ac.uk/~pxc/nlp/.../nlp_syn<b>test</b>.html</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ifdaa""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:5i7k6txi3kgj:https://www.cs.bham.ac.uk/~pxc/nlp/interactivenlp/nlp_syntest.html%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagwmaa;usg=afqjcnh3gpguhk4khou5dnm1k-2x-gcfua"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:https://www.cs.bham.ac.uk/~pxc/nlp/interactivenlp/nlp_syntest.html+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgxmaa"">similar</a></li></ul></div></div></div><span class=""st"">complete the following <b>test</b> to find out how much you know about basic <b>syntax</b>. <br>
complete all answers and find out your results. there is no negative marking.</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=http://wiki.bash-hackers.org/commands/classictest;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggzmae;usg=afqjcngbrqx8uuhzznitiepkiqfjt_u-dw"">the classic <b>test</b> command [bash hackers wiki]</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>wiki.bash-hackers.org/commands/classic<b>test</b></cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0igjab""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:rkgzf3ogh-0j:http://wiki.bash-hackers.org/commands/classictest%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagcmae;usg=afqjcngbf6wg3hgg9mchmdyqfnvhtiejgw"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:wiki.bash-hackers.org/commands/classictest+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgdmae"">similar</a></li></ul></div></div></div><span class=""st"">this command allows you to do various <b>tests</b> and sets its exit code to 0 (true) or <br>
1 (false) whenever such a <b>test</b> succeeds or&nbsp;...</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=https://atom.io/packages/test-syntax;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggfmai;usg=afqjcneusgwzzmigo114n8ia6xy_g1gara""><b>test</b>-<b>syntax</b> - atom</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>https://atom.io/packages/<b>test</b>-<b>syntax</b></cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0iidac""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:wu7tzk5bct8j:https://atom.io/packages/test-syntax%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagimai;usg=afqjcnflgpgrch05xhhe53fp7-9x2efdyg"">cached</a></li></ul></div></div></div><span class=""st""><b>test</b>-<b>syntax</b>. a short description of your syntax theme. #test &middot; thedaniel &middot; 2.4.0 176. <br>
0 &middot; repo &middot; bugs &middot; versions &middot; license. flag as spam or malicious&nbsp;...</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggkmam;usg=afqjcnfz4d97jdeokqz1rskh1fhuifclta""><b>test</b> &#269;.1 - <b>syntax</b></a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>www.gymmoldava.sk/icv/sjl/<b>syntax</b>/<b>test</b>-<b>syntax</b>.htm</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ijtad""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:fruxbat76_cj:http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagnmam;usg=afqjcnghpoylqrpeulniftfaklvxrrzveq"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.gymmoldava.sk/icv/sjl/syntax/test-syntax.htm+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgomam"">similar</a></li></ul></div></div></div><span class=""st""><b>s y n t a x</b>. <b>test</b>. ozna&#269; spr�vnu odpove&#271;. uk�&#382; v&#353;etky ot�zky. &lt;= 1 / 9 =&gt;. ak�mi <br>
slovn�mi druhmi je naj&#269;astej&#353;ie vyjadren� podmet? ? z�menom ? pr�davn�m&nbsp;...</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax2.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggqmaq;usg=afqjcneizxncsx7txapbjhsnqn3clhudlg""><b>test</b> &#269;.2 - <b>syntax</b></a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>www.gymmoldava.sk/icv/sjl/<b>syntax</b>/<b>test</b>-<b>syntax</b>2.htm</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ikzae""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:muahpspomoej:http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax2.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagtmaq;usg=afqjcnhbe-56phmrmmu2ojoznpz_scrz0q"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.gymmoldava.sk/icv/sjl/syntax/test-syntax2.htm+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgumaq"">similar</a></li></ul></div></div></div><span class=""st""><b>syntax</b>. <b>test</b>. ozna&#269; spr�vnu odpove&#271;. uk�&#382; v&#353;etky ot�zky. &lt;= 1 / 5 =&gt;. ozna&#269; vetu<br>
, v ktorej je nevyjadren� podmet ? obloha sa zatiahla. ? na oblohe sa zjavili&nbsp;...</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=https://www.ibm.com/support/knowledgecenter/ssphqg_7.2.0/com.ibm.powerha.admngd/ha_admin_test_syntax.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggwmau;usg=afqjcnhxoqnqczigzphqdon0qol9d7u6cg""><b>test syntax</b> - ibm</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>https://www.ibm.com/support/...7.2.../ha_admin_<b>test</b>_<b>syntax</b>.htm</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0imtaf""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:uogyatrejjuj:https://www.ibm.com/support/knowledgecenter/ssphqg_7.2.0/com.ibm.powerha.admngd/ha_admin_test_syntax.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagzmau;usg=afqjcneftftuc_euamcuilu_a_ttmyahnq"">cached</a></li></ul></div></div></div><span class=""st""><b>test syntax</b>. this topic describes the syntax for a test. the syntax for a test is: <br>
test_name, parameter1, parametern|parameter, comments. where: the test<br>
&nbsp;...</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=https://en.wikipedia.org/wiki/test_(unix);sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfgg1may;usg=afqjcnevf3s96eo5g2dmxijlomqcnfw5eq""><b>test</b> (unix) - wikipedia</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>https://en.wikipedia.org/wiki/<b>test</b>_(unix)</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0injag""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:glrupnrkeugj:https://en.wikipedia.org/wiki/test_(unix)%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiag4may;usg=afqjcngnjj9bgysuhyfreulpu9htpqxzha"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:https://en.wikipedia.org/wiki/test_(unix)+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwg5may"">similar</a></li></ul></div></div></div><span class=""st""><b>syntax</b>[edit]. <b>test</b> expression. or [ expression ]</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=http://www.computerhope.com/unix/test.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfgg7mac;usg=afqjcnfpxua8oxbyt5skliff1boawcmy4w"">linux and unix <b>test</b> command information and examples</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>www.computerhope.com/unix/<b>test</b>.htm</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ipdah""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:yfjbhruk8hej:http://www.computerhope.com/unix/test.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiag-mac;usg=afqjcngmfftsohunvndvykg3xzynwydscg"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.computerhope.com/unix/test.htm+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwg_mac"">similar</a></li></ul></div></div></div><span class=""st"">linux and unix test command. test command about test <b>test syntax</b> test examples<br>
. linux and unix main page. about test. checks file types and compares values.</span><br></div></div><div class=""g""><h3 class=""r""><a href=""/url?q=https://www.tutorialspoint.com/software_testing_dictionary/syntax_testing.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfghbmag;usg=afqjcng6zyn0wvaez1pnovxbwra-kh7trg""><b>syntax testing</b> - tutorialspoint</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>https://www.tutorialspoint.com/software_<b>testing</b>.../<b>syntax</b>_<b>testing</b>.htm</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0iqjai""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:beoa7ic6d9uj:https://www.tutorialspoint.com/software_testing_dictionary/syntax_testing.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiahemag;usg=afqjcnhwxcfjtdztvhg_v2apjp4cda4fkq"">cached</a></li></ul></div></div></div><span class=""st""><b>syntax testing</b>, a black box <b>testing</b> technique, involves <b>testing</b> the system inputs <br>
and it is usually automated because <b>syntax testing</b> produces a large number of&nbsp;...</span><br></div></div><div class=""g""><span style=""float:left""><span class=""mime"">[pdf]</span>&nbsp;</span><h3 class=""r""><a href=""/url?q=http://www.stata.com/manuals13/rtest.pdf;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfghgmak;usg=afqjcnelrpovthl-7lpuzqwpxoqwjg4lqq""><b>test</b> - stata</a></h3><div class=""s""><div class=""kv"" style=""margin-bottom:2px""><cite>www.stata.com/manuals13/r<b>test</b>.pdf</cite><div class=""_nbb""><div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0irzaj""><span class=""_o0""></span></div><div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1""><ul><li class=""_ykb""><a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:bw8l3h-kgdej:http://www.stata.com/manuals13/rtest.pdf%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiahjmak;usg=afqjcneombwpwb8i2dfoazps_y0hslxxeg"">cached</a></li><li class=""_ykb""><a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.stata.com/manuals13/rtest.pdf+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwhkmak"">similar</a></li></ul></div></div></div><span class=""st"">stata.com <b>test</b> &#8212; <b>test</b> linear hypotheses after estimation. <b>syntax</b>. menu. <br>
description. options for testparm. options for <b>test</b>. remarks and examples. <br>
stored results.</span><br></div></div></ol></div></div></div><div style=""clear:both;margin-bottom:17px;overflow:hidden""><div style=""font-size:16px;padding:0 8px 1px"">searches related to <b>test syntax</b></div><table border=""0"" cellpadding=""0"" cellspacing=""0""><tr><td valign=""top""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+test+questions;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiitsga"">syntax test <b>questions</b></a></p></td><td valign=""top"" style=""padding-left:10px""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+final+exam;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiitigb"">syntax <b>final exam</b></a></p></td></tr><tr><td valign=""top""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=english+syntax+test;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiitygc""><b>english</b> syntax test</a></p></td><td valign=""top"" style=""padding-left:10px""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+exercises+and+answers;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiucgd"">syntax <b>exercises and answers</b></a></p></td></tr><tr><td valign=""top""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+practice+exercises;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiusge"">syntax <b>practice exercises</b></a></p></td><td valign=""top"" style=""padding-left:10px""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+testing+example;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiuigf"">syntax <b>testing example</b></a></p></td></tr><tr><td valign=""top""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+test+questions+and+answers;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiuygg"">syntax test <b>questions and answers</b></a></p></td><td valign=""top"" style=""padding-left:10px""><p class=""_bmc"" style=""margin:3px 8px""><a href=""/search?ie=utf-8;q=syntax+question+examples;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiivcgh"">syntax <b>question examples</b></a></p></td></tr></table></div></div><div id=""foot""><table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" id=""nav""><tr valign=""top""><td align=""left"" class=""b""><span class=""csb"" style=""background-position:-24px 0;width:28px""></span><b></b></td><td><span class=""csb"" style=""background-position:-53px 0;width:20px""></span><b>1</b></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=10;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>2</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=20;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>3</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=30;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>4</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=40;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>5</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=50;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>6</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=60;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>7</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=70;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>8</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=80;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>9</a></td><td><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=90;sa=n""><span class=""csb"" style=""background-position:-74px 0;width:20px""></span>10</a></td><td class=""b"" style=""text-align:left""><a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=10;sa=n"" style=""text-align:left""><span class=""csb"" style=""background-position:-96px 0;width:71px""></span><span style=""display:block;margin-left:53px"">next</span></a></td></tr></table><p class=""_cd"" id=""bfl"" style=""margin:19px 0 0;text-align:center""><a href=""/advanced_search?q=test+syntax;ie=utf-8;prmd=ivns"">advanced search</a><a href=""/support/websearch/bin/answer.py?answer=134479;hl=en"">search help</a> <a href=""/tools/feedback/survey/html?productid=196;query=test+syntax;hl=en"">send feedback</a></p><div class=""_cd"" id=""fll"" style=""margin:19px auto 19px auto;text-align:center""><a href=""/"">google&nbsp;home</a> <a href=""/intl/en/ads"">advertising&nbsp;programs</a> <a href=""/services"">business solutions</a> <a href=""/intl/en/policies/privacy/"">privacy</a> <a href=""/intl/en/policies/terms/"">terms</a> <a href=""/intl/en/about.html"">about google</a></div></div></td><td id=""rhs_block"" valign=""top""></td></tr></tbody></table><script type=""text/javascript"">(function(){var eventid='uxadwir0fmudgaav17habg';google.kei = eventid;})();</script><script src=""/xjs/_/js/k=xjs.hp.en_us.g-scu6wevuy.o/m=sb_he,d/rt=j/d=1/t=zcms/rs=act90ohazqmjimkn7jto8dvzol1_oaq4wa""></script><script type=""text/javascript"">google.ac&&google.ac.c({""agen"":true,""cgen"":true,""client"":""heirloom-serp"",""dh"":true,""dhqt"":true,""ds"":"""",""fl"":true,""host"":""google.com"",""isbh"":28,""jam"":0,""jsonp"":true,""msgs"":{""cibl"":""clear search"",""dym"":""did you mean:"",""lcky"":""i\u0026#39;m feeling lucky"",""lml"":""learn more"",""oskt"":""input tools"",""psrc"":""this search was removed from your \u003ca href=\""/history\""\u003eweb history\u003c/a\u003e"",""psrl"":""remove"",""sbit"":""search by image"",""srch"":""google search""},""nds"":true,""ovr"":{},""pq"":""test syntax"",""refpd"":true,""rfs"":[""syntax test questions"",""english syntax test"",""syntax practice exercises"",""syntax test questions and answers"",""syntax final exam"",""syntax exercises and answers"",""syntax testing example"",""syntax question examples""],""scd"":10,""sce"":5,""stok"":""plih8yygefbntzei1khxsazwbbg""})</script><script>(function(){window.google.cdo={height:0,width:0};(function(){var a=window.innerwidth,b=window.innerheight;if(!a||!b)var c=window.document,d=""css1compat""==c.compatmode?c.documentelement:c.body,a=d.clientwidth,b=d.clientheight;a&&b&&(a!=google.cdo.width||b!=google.cdo.height)&&google.log("""","""",""/client_204?&atyp=i&biw=""+a+""&bih=""+b+""&ei=""+google.kei);}).call(this);})();</script></body></html>";
            #endregion

            XElement element;
            var formatedXML2 = _lamed.lib.XML.Setup.XML_FormatHTML(input, out element, true);

            #region Test result
            var result =
@"<span>
  <!--ctype ht -->
  <html itemscope="""" itemtype=""http://schema.org/searchresultspage"" lang=""en"">
    <head>
      <meta content=""text/html; charset=utf-8"" http-equiv=""content-type"" />
      <meta content=""/images/branding/googleg/1x/googleg_standard_color_128dp.png"" itemprop=""image"" />
      <link href=""/images/branding/product/ico/googleg_lodp.ico"" rel=""shortcut icon"" />
      <noscript>
        <meta content=""0;url=/search?q=test+syntax;ie=utf-8;gbv=1;sei=uxadwir0fmudgaav17habg"" http-equiv=""refresh"" />
        <div style=""display:block"">please click <a href=""/search?q=test+syntax;ie=utf-8;gbv=1;sei=uxadwir0fmudgaav17habg"">here</a> if you are not redirected within a few seconds.</div>
      </noscript>
      <title>test syntax - google search</title>
    </head>
    <body class=""hsrp"" bgcolor=""#ffffff"" marginheight=""0"" marginwidth=""0"" topmargin=""0"">
      <div id=""gb"">
        <div id=""gbw"">
          <div id=""gbz"">
            <span class=""gbtcb""></span>
            <ol id=""gbzc"" class=""gbtc"">
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:1});"" class=""gbzt gbz0l gbp1"" id=""gb_1"" href=""https://www.google.com/webhp?tab=ww"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">search</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:2});"" class=""gbzt"" id=""gb_2"" href=""https://www.google.com/search?hl=en&amp;tbm=isch&amp;source=og&amp;tab=wi"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">images</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:8});"" class=""gbzt"" id=""gb_8"" href=""https://maps.google.com/maps?hl=en&amp;tab=wl"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">maps</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:78});"" class=""gbzt"" id=""gb_78"" href=""https://play.google.com/?hl=en&amp;tab=w8"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">play</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:36});"" class=""gbzt"" id=""gb_36"" href=""https://www.youtube.com/results?tab=w1"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">youtube</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:5});"" class=""gbzt"" id=""gb_5"" href=""https://news.google.com/nwshp?hl=en&amp;tab=wn"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">news</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:23});"" class=""gbzt"" id=""gb_23"" href=""https://mail.google.com/mail/?tab=wm"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">gmail</span>
                </a>
              </li>
              <li class=""gbt"">
                <a onclick=""gbar.logger.il(1,{t:49});"" class=""gbzt"" id=""gb_49"" href=""https://drive.google.com/?tab=wo"">
                  <span class=""gbtb2""></span>
                  <span class=""gbts"">drive</span>
                </a>
              </li>
              <li class=""gbt"">
                <a class=""gbgt"" id=""gbztm"" href=""https://www.google.com/intl/en/options/"" onclick=""gbar.tg(event,this)"" aria-haspopup=""true"" aria-owns=""gbd"">
                  <span class=""gbtb2""></span>
                  <span id=""gbztms"" class=""gbts gbtsa"">
                    <span id=""gbztms1"">more</span>
                    <span class=""gbma""></span>
                  </span>
                </a>
                <div class=""gbm"" id=""gbd"" aria-owner=""gbztm"">
                  <div id=""gbmmb"" class=""gbmc gbsb gbsbis"">
                    <ol id=""gbmm"" class=""gbmcc gbsbic""></ol>
                  </div>
                </div>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:24});"" class=""gbmt"" id=""gb_24"" href=""https://www.google.com/calendar?tab=wc"">calendar</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:51});"" class=""gbmt"" id=""gb_51"" href=""https://translate.google.com/?hl=en&amp;tab=wt"">translate</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:17});"" class=""gbmt"" id=""gb_17"" href=""http://www.google.com/mobile/?hl=en&amp;tab=wd"">mobile</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:10});"" class=""gbmt"" id=""gb_10"" href=""https://www.google.com/search?hl=en&amp;tbo=u&amp;tbm=bks&amp;source=og&amp;tab=wp"">books</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:212});"" class=""gbmt"" id=""gb_212"" href=""https://wallet.google.com/?tab=wa"">wallet</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:6});"" class=""gbmt"" id=""gb_6"" href=""https://www.google.com/search?hl=en&amp;tbo=u&amp;tbm=shop&amp;source=og&amp;tab=wf"">shopping</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:30});"" class=""gbmt"" id=""gb_30"" href=""https://www.blogger.com/?tab=wj"">blogger</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:27});"" class=""gbmt"" id=""gb_27"" href=""https://www.google.com/finance?tab=we"">finance</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:31});"" class=""gbmt"" id=""gb_31"" href=""https://photos.google.com/?tab=wq&amp;pageid=none"">photos</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:12});"" class=""gbmt"" id=""gb_12"" href=""https://www.google.com/search?hl=en&amp;tbo=u&amp;tbm=vid&amp;source=og&amp;tab=wv"">videos</a>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:25});"" class=""gbmt"" id=""gb_25"" href=""https://docs.google.com/document/?usp=docs_alc"">docs</a>
              </li>
              <li class=""gbmtc"">
                <div class=""gbmt gbmh""></div>
              </li>
              <li class=""gbmtc"">
                <a onclick=""gbar.logger.il(1,{t:66});"" href=""https://www.google.com/intl/en/options/"" class=""gbmt"">even more &amp;raquo;</a>
              </li>
              <div class=""gbsbt""></div>
              <div class=""gbsbb""></div>
              <div id=""gbg"">
                <h2 class=""gbxx"">account options</h2>
                <span class=""gbtcb""></span>
                <ol class=""gbtc"">
                  <li class=""gbt"">
                    <a target=""_top"" href=""https://accounts.google.com/servicelogin?hl=en&amp;passive=true&amp;continue=https://www.google.com/search%3fq%3dtest%2bsyntax"" onclick=""gbar.logger.il(9,{l:'i'})"" id=""gb_70"" class=""gbgt"">
                      <span class=""gbtb2""></span>
                      <span id=""gbgs4"" class=""gbts"">
                        <span id=""gbi4s1"">sign in</span>
                      </span>
                    </a>
                  </li>
                  <li class=""gbt gbtb"">
                    <span class=""gbts""></span>
                  </li>
                  <li class=""gbt"">
                    <a class=""gbgt"" id=""gbg5"" href=""http://www.google.com/preferences?hl=en"" title=""options"" onclick=""gbar.tg(event,this)"" aria-haspopup=""true"" aria-owns=""gbd5"">
                      <span class=""gbtb2""></span>
                      <span id=""gbgs5"" class=""gbts"">
                        <span id=""gbi5""></span>
                      </span>
                    </a>
                    <div class=""gbm"" id=""gbd5"" aria-owner=""gbg5"">
                      <div class=""gbmc"">
                        <ol id=""gbom"" class=""gbmcc""></ol>
                      </div>
                    </div>
                  </li>
                  <li class=""gbkc gbmtc"">
                    <a class=""gbmt"" href=""/preferences?hl=en"">search settings</a>
                  </li>
                  <li class=""gbmtc"">
                    <div class=""gbmt gbmh""></div>
                  </li>
                  <li class=""gbkp gbmtc"">
                    <a class=""gbmt"" href=""http://www.google.com/history/optout?hl=en"">web history</a>
                  </li>
                  <div id=""gbx3""></div>
                  <div id=""gbx4""></div>
                  <table id=""mn"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""position:relative"">
                    <tr>
                      <th width=""132""></th>
                      <th width=""573""></th>
                      <th width=""278""></th>
                      <th></th>
                    </tr>
                    <tr>
                      <td class=""sfbgg"" valign=""top"">
                        <div id=""logocont"">
                          <h1>
                            <a href=""/webhp?hl=en"" style=""background:url(/images/nav_logo229.png) no-repeat 0 -41px;height:37px;width:95px;display:block"" id=""logo"" title=""go to google home""></a>
                          </h1>
                        </div>
                      </td>
                      <td class=""sfbgg"" colspan=""2"" valign=""top"" style=""padding-left:0px"">
                        <form style=""display:block;margin:0;background:none"" action=""/search"" id=""tsf"" method=""get"" name=""gs"" />
                        <table border=""0"" cellpadding=""0"" cellspacing=""0"" style=""margin-top:20px;position:relative"">
                          <tr>
                            <td>
                              <div class=""lst-a"">
                                <table cellpadding=""0"" cellspacing=""0"">
                                  <tr>
                                    <td class=""lst-td"" width=""555"" valign=""bottom"">
                                      <div style=""position:relative;zoom:1"">
                                        <input class=""lst"" value=""test syntax"" title=""search"" autocomplete=""off"" id=""sbhost"" maxlength=""2048"" name=""q"" type=""text"" />
                                      </div>
                                    </td>
                                  </tr>
                                </table>
                              </div>
                            </td>
                            <td>
                              <div class=""ds"">
                                <div class=""lsbb"">
                                  <button class=""lsb"" value=""search"" name=""btng"" type=""submit"">
                                    <span class=""sbico"" style=""background:url(/images/nav_logo229.png) no-repeat -36px -111px;height:14px;width:13px;display:block""></span>
                                  </button>
                                </div>
                              </div>
                            </td>
                          </tr>
                        </table>&lt;/form&gt;</td>
                      <td class=""sfbgg"">&amp;nbsp;</td>
                    </tr>
                    <tr style=""position:relative"">
                      <td>
                        <div style=""border-bottom:1px solid #ebebeb;height:59px""></div>
                      </td>
                      <td colspan=""2"">
                        <div class=""tn"">
                          <div class=""_uxb _ihd _sxc _hhd"">all</div>
                          <div class=""_sxc"">
                            <a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=isch;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auibq"">images</a>
                          </div>
                          <div class=""_sxc"">
                            <a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=vid;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auibg"">videos</a>
                          </div>
                          <div class=""_sxc"">
                            <a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=nws;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auibw"">news</a>
                          </div>
                          <div class=""_sxc"">
                            <a class=""_uxb _jhd"" href=""https://maps.google.com/maps?q=test+syntax;um=1;ie=utf-8;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auica"">maps</a>
                          </div>
                          <div class=""_sxc"">
                            <a class=""_uxb _jhd"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnms;tbm=bks;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq_auicq"">books</a>
                          </div>
                        </div>
                        <div style=""border-bottom:1px solid #ebebeb;height:59px""></div>
                      </td>
                      <td>
                        <div style=""border-bottom:1px solid #ebebeb;height:59px""></div>
                      </td>
                    </tr>
                    <tbody data-jibp=""h"" data-jiis=""uc"" id=""desktop-search"">
                      <tr>
                        <td id=""leftnav"" valign=""top"">
                          <div>
                            <h2 class=""hd"">search options</h2>
                            <ul class=""med"" id=""tbd"">
                              <li>
                                <ul class=""tbt"">
                                  <li class=""tbos"" id=""qdr_"">any time</li>
                                  <li class=""tbou"" id=""qdr_h"">
                                    <a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:h;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past hour</a>
                                  </li>
                                  <li class=""tbou"" id=""qdr_d"">
                                    <a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:d;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past 24 hours</a>
                                  </li>
                                  <li class=""tbou"" id=""qdr_w"">
                                    <a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:w;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past week</a>
                                  </li>
                                  <li class=""tbou"" id=""qdr_m"">
                                    <a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:m;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past month</a>
                                  </li>
                                  <li class=""tbou"" id=""qdr_y"">
                                    <a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=qdr:y;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">past year</a>
                                  </li>
                                </ul>
                              </li>
                              <li>
                                <ul class=""tbt"">
                                  <li class=""tbos"" id=""li_"">all results</li>
                                  <li class=""tbou"" id=""li_1"">
                                    <a class=""q"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;source=lnt;tbs=li:1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqpwuidg"">verbatim</a>
                                  </li>
                                </ul>
                              </li>
                            </ul>
                          </div>
                        </td>
                        <td valign=""top"">
                          <div id=""center_col"">
                            <div class=""sd"" id=""resultstats"">about 63,200,000 results</div>
                            <div id=""res"">
                              <div id=""topstuff""></div>
                              <div id=""search"">
                                <div id=""ires"">
                                  <ol>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=https://www.cs.bham.ac.uk/~pxc/nlp/interactivenlp/nlp_syntest.html;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggtmaa;usg=afqjcnepnemerxsc_xbbptmm1-fvdddwqa"">
                                          <b>syntax test</b>
                                        </a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>https://www.cs.bham.ac.uk/~pxc/nlp/.../nlp_syn<b>test</b>.html</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ifdaa"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:5i7k6txi3kgj:https://www.cs.bham.ac.uk/~pxc/nlp/interactivenlp/nlp_syntest.html%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagwmaa;usg=afqjcnh3gpguhk4khou5dnm1k-2x-gcfua"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:https://www.cs.bham.ac.uk/~pxc/nlp/interactivenlp/nlp_syntest.html+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgxmaa"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">complete the following <b>test</b> to find out how much you know about basic <b>syntax</b>. <br />
complete all answers and find out your results. there is no negative marking.</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=http://wiki.bash-hackers.org/commands/classictest;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggzmae;usg=afqjcngbrqx8uuhzznitiepkiqfjt_u-dw"">the classic <b>test</b> command [bash hackers wiki]</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>wiki.bash-hackers.org/commands/classic<b>test</b></cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0igjab"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:rkgzf3ogh-0j:http://wiki.bash-hackers.org/commands/classictest%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagcmae;usg=afqjcngbf6wg3hgg9mchmdyqfnvhtiejgw"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:wiki.bash-hackers.org/commands/classictest+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgdmae"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">this command allows you to do various <b>tests</b> and sets its exit code to 0 (true) or <br />
1 (false) whenever such a <b>test</b> succeeds or&amp;nbsp;...</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=https://atom.io/packages/test-syntax;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggfmai;usg=afqjcneusgwzzmigo114n8ia6xy_g1gara"">
                                          <b>test</b>-<b>syntax</b> - atom</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>https://atom.io/packages/<b>test</b>-<b>syntax</b></cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0iidac"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:wu7tzk5bct8j:https://atom.io/packages/test-syntax%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagimai;usg=afqjcnflgpgrch05xhhe53fp7-9x2efdyg"">cached</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">
                                          <b>test</b>-<b>syntax</b>. a short description of your syntax theme. #test &amp;middot; thedaniel &amp;middot; 2.4.0 176. <br />
0 &amp;middot; repo &amp;middot; bugs &amp;middot; versions &amp;middot; license. flag as spam or malicious&amp;nbsp;...</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggkmam;usg=afqjcnfz4d97jdeokqz1rskh1fhuifclta"">
                                          <b>test</b> &amp;#269;.1 - <b>syntax</b></a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>www.gymmoldava.sk/icv/sjl/<b>syntax</b>/<b>test</b>-<b>syntax</b>.htm</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ijtad"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:fruxbat76_cj:http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagnmam;usg=afqjcnghpoylqrpeulniftfaklvxrrzveq"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.gymmoldava.sk/icv/sjl/syntax/test-syntax.htm+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgomam"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">
                                          <b>s y n t a x</b>. <b>test</b>. ozna&amp;#269; spr�vnu odpove&amp;#271;. uk�&amp;#382; v&amp;#353;etky ot�zky. &lt;= 1 / 9 =&gt;. ak�mi <br />
slovn�mi druhmi je naj&amp;#269;astej&amp;#353;ie vyjadren� podmet? ? z�menom ? pr�davn�m&amp;nbsp;...</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax2.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggqmaq;usg=afqjcneizxncsx7txapbjhsnqn3clhudlg"">
                                          <b>test</b> &amp;#269;.2 - <b>syntax</b></a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>www.gymmoldava.sk/icv/sjl/<b>syntax</b>/<b>test</b>-<b>syntax</b>2.htm</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ikzae"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:muahpspomoej:http://www.gymmoldava.sk/icv/sjl/syntax/test-syntax2.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagtmaq;usg=afqjcnhbe-56phmrmmu2ojoznpz_scrz0q"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.gymmoldava.sk/icv/sjl/syntax/test-syntax2.htm+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwgumaq"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">
                                          <b>syntax</b>. <b>test</b>. ozna&amp;#269; spr�vnu odpove&amp;#271;. uk�&amp;#382; v&amp;#353;etky ot�zky. &lt;= 1 / 5 =&gt;. ozna&amp;#269; vetu<br />
, v ktorej je nevyjadren� podmet ? obloha sa zatiahla. ? na oblohe sa zjavili&amp;nbsp;...</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=https://www.ibm.com/support/knowledgecenter/ssphqg_7.2.0/com.ibm.powerha.admngd/ha_admin_test_syntax.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfggwmau;usg=afqjcnhxoqnqczigzphqdon0qol9d7u6cg"">
                                          <b>test syntax</b> - ibm</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>https://www.ibm.com/support/...7.2.../ha_admin_<b>test</b>_<b>syntax</b>.htm</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0imtaf"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:uogyatrejjuj:https://www.ibm.com/support/knowledgecenter/ssphqg_7.2.0/com.ibm.powerha.admngd/ha_admin_test_syntax.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiagzmau;usg=afqjcneftftuc_euamcuilu_a_ttmyahnq"">cached</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">
                                          <b>test syntax</b>. this topic describes the syntax for a test. the syntax for a test is: <br />
test_name, parameter1, parametern|parameter, comments. where: the test<br />
&amp;nbsp;...</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=https://en.wikipedia.org/wiki/test_(unix);sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfgg1may;usg=afqjcnevf3s96eo5g2dmxijlomqcnfw5eq"">
                                          <b>test</b> (unix) - wikipedia</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>https://en.wikipedia.org/wiki/<b>test</b>_(unix)</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0injag"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:glrupnrkeugj:https://en.wikipedia.org/wiki/test_(unix)%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiag4may;usg=afqjcngnjj9bgysuhyfreulpu9htpqxzha"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:https://en.wikipedia.org/wiki/test_(unix)+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwg5may"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">
                                          <b>syntax</b>[edit]. <b>test</b> expression. or [ expression ]</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=http://www.computerhope.com/unix/test.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfgg7mac;usg=afqjcnfpxua8oxbyt5skliff1boawcmy4w"">linux and unix <b>test</b> command information and examples</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>www.computerhope.com/unix/<b>test</b>.htm</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0ipdah"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:yfjbhruk8hej:http://www.computerhope.com/unix/test.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiag-mac;usg=afqjcngmfftsohunvndvykg3xzynwydscg"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.computerhope.com/unix/test.htm+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwg_mac"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">linux and unix test command. test command about test <b>test syntax</b> test examples<br />
. linux and unix main page. about test. checks file types and compares values.</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <h3 class=""r"">
                                        <a href=""/url?q=https://www.tutorialspoint.com/software_testing_dictionary/syntax_testing.htm;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfghbmag;usg=afqjcng6zyn0wvaez1pnovxbwra-kh7trg"">
                                          <b>syntax testing</b> - tutorialspoint</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>https://www.tutorialspoint.com/software_<b>testing</b>.../<b>syntax</b>_<b>testing</b>.htm</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0iqjai"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:beoa7ic6d9uj:https://www.tutorialspoint.com/software_testing_dictionary/syntax_testing.htm%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiahemag;usg=afqjcnhwxcfjtdztvhg_v2apjp4cda4fkq"">cached</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">
                                          <b>syntax testing</b>, a black box <b>testing</b> technique, involves <b>testing</b> the system inputs <br />
and it is usually automated because <b>syntax testing</b> produces a large number of&amp;nbsp;...</span>
                                        <br />
                                      </div>
                                    </div>
                                    <div class=""g"">
                                      <span style=""float:left"">
                                        <span class=""mime"">[pdf]</span>&amp;nbsp;</span>
                                      <h3 class=""r"">
                                        <a href=""/url?q=http://www.stata.com/manuals13/rtest.pdf;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqfghgmak;usg=afqjcnelrpovthl-7lpuzqwpxoqwjg4lqq"">
                                          <b>test</b> - stata</a>
                                      </h3>
                                      <div class=""s"">
                                        <div class=""kv"" style=""margin-bottom:2px"">
                                          <cite>www.stata.com/manuals13/r<b>test</b>.pdf</cite>
                                          <div class=""_nbb"">
                                            <div style=""display:inline"" onclick=""google.sham(this);"" aria-expanded=""false"" aria-haspopup=""true"" tabindex=""0"" data-ved=""0ahukewjkvfcm5nlrahxlacakha9rdggq7b0irzaj"">
                                              <span class=""_o0""></span>
                                            </div>
                                            <div style=""display:none"" class=""am-dropdown-menu"" role=""menu"" tabindex=""-1"">
                                              <ul>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/url?q=http://webcache.googleusercontent.com/search%3fq%3dcache:bw8l3h-kgdej:http://www.stata.com/manuals13/rtest.pdf%252btest%2bsyntax%26hl%3den%26ct%3dclnk;sa=u;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqiahjmak;usg=afqjcneombwpwb8i2dfoazps_y0hslxxeg"">cached</a>
                                                </li>
                                                <li class=""_ykb"">
                                                  <a class=""_zkb"" href=""/search?ie=utf-8;q=related:www.stata.com/manuals13/rtest.pdf+test+syntax;tbo=1;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggqhwhkmak"">similar</a>
                                                </li>
                                              </ul>
                                            </div>
                                          </div>
                                        </div>
                                        <span class=""st"">stata.com <b>test</b> &amp;#8212; <b>test</b> linear hypotheses after estimation. <b>syntax</b>. menu. <br />
description. options for testparm. options for <b>test</b>. remarks and examples. <br />
stored results.</span>
                                        <br />
                                      </div>
                                    </div>
                                  </ol>
                                </div>
                              </div>
                            </div>
                            <div style=""clear:both;margin-bottom:17px;overflow:hidden"">
                              <div style=""font-size:16px;padding:0 8px 1px"">searches related to <b>test syntax</b></div>
                              <table border=""0"" cellpadding=""0"" cellspacing=""0"">
                                <tr>
                                  <td valign=""top"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+test+questions;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiitsga"">syntax test <b>questions</b></a>
                                    </p>
                                  </td>
                                  <td valign=""top"" style=""padding-left:10px"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+final+exam;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiitigb"">syntax <b>final exam</b></a>
                                    </p>
                                  </td>
                                </tr>
                                <tr>
                                  <td valign=""top"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=english+syntax+test;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiitygc"">
                                        <b>english</b> syntax test</a>
                                    </p>
                                  </td>
                                  <td valign=""top"" style=""padding-left:10px"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+exercises+and+answers;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiucgd"">syntax <b>exercises and answers</b></a>
                                    </p>
                                  </td>
                                </tr>
                                <tr>
                                  <td valign=""top"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+practice+exercises;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiusge"">syntax <b>practice exercises</b></a>
                                    </p>
                                  </td>
                                  <td valign=""top"" style=""padding-left:10px"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+testing+example;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiuigf"">syntax <b>testing example</b></a>
                                    </p>
                                  </td>
                                </tr>
                                <tr>
                                  <td valign=""top"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+test+questions+and+answers;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiiuygg"">syntax test <b>questions and answers</b></a>
                                    </p>
                                  </td>
                                  <td valign=""top"" style=""padding-left:10px"">
                                    <p class=""_bmc"" style=""margin:3px 8px"">
                                      <a href=""/search?ie=utf-8;q=syntax+question+examples;sa=x;ved=0ahukewjkvfcm5nlrahxlacakha9rdggq1qiivcgh"">syntax <b>question examples</b></a>
                                    </p>
                                  </td>
                                </tr>
                              </table>
                            </div>
                          </div>
                          <div id=""foot"">
                            <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" id=""nav"">
                              <tr valign=""top"">
                                <td align=""left"" class=""b"">
                                  <span class=""csb"" style=""background-position:-24px 0;width:28px""></span>
                                  <b></b>
                                </td>
                                <td>
                                  <span class=""csb"" style=""background-position:-53px 0;width:20px""></span>
                                  <b>1</b>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=10;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>2</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=20;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>3</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=30;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>4</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=40;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>5</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=50;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>6</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=60;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>7</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=70;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>8</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=80;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>9</a>
                                </td>
                                <td>
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=90;sa=n"">
                                    <span class=""csb"" style=""background-position:-74px 0;width:20px""></span>10</a>
                                </td>
                                <td class=""b"" style=""text-align:left"">
                                  <a class=""fl"" href=""/search?q=test+syntax;ie=utf-8;prmd=ivns;ei=uxadwir0fmudgaav17habg;start=10;sa=n"" style=""text-align:left"">
                                    <span class=""csb"" style=""background-position:-96px 0;width:71px""></span>
                                    <span style=""display:block;margin-left:53px"">next</span>
                                  </a>
                                </td>
                              </tr>
                            </table>
                            <p class=""_cd"" id=""bfl"" style=""margin:19px 0 0;text-align:center"">
                              <a href=""/advanced_search?q=test+syntax;ie=utf-8;prmd=ivns"">advanced search</a>
                              <a href=""/support/websearch/bin/answer.py?answer=134479;hl=en"">search help</a>
                              <a href=""/tools/feedback/survey/html?productid=196;query=test+syntax;hl=en"">send feedback</a>
                            </p>
                            <div class=""_cd"" id=""fll"" style=""margin:19px auto 19px auto;text-align:center"">
                              <a href=""/"">google&amp;nbsp;home</a>
                              <a href=""/intl/en/ads"">advertising&amp;nbsp;programs</a>
                              <a href=""/services"">business solutions</a>
                              <a href=""/intl/en/policies/privacy/"">privacy</a>
                              <a href=""/intl/en/policies/terms/"">terms</a>
                              <a href=""/intl/en/about.html"">about google</a>
                            </div>
                          </div>
                        </td>
                        <td id=""rhs_block"" valign=""top""></td>
                      </tr>
                    </tbody>
                  </table>
                </ol>
              </div>
            </ol>
          </div>
        </div>
      </div>
    </body>
  </html>
</span>";

            Assert.Equal(result, formatedXML2);
            #endregion

            // Exception
            Assert.Throws<ArgumentNullException>(()=> _lamed.lib.XML.Setup.XML_FormatHTML("", out element, true));
            Assert.Throws<ArgumentNullException>(()=> _lamed.lib.XML.Setup.XML_FormatHTML(null, out element, true));

        }

        [Fact]
        [Test_Method("XML_ValueBetweenTags()")]
        public void XML_ValueBetweenTags_Test()
        {
            Assert.Equal("", _lamed.lib.XML.Setup.XML_ValueBetweenTags("", "param"));
            Assert.Equal("", _lamed.lib.XML.Setup.XML_ValueBetweenTags(null, "param"));

            var xml = "<param>The parameter line.</param>";
            Assert.Equal("The parameter line.", _lamed.lib.XML.Setup.XML_ValueBetweenTags(xml, "param"));

            xml = "<param name=\"paramLine\">The parameter line.</param>";
            Assert.Equal("name=\"paramLine\">The parameter line.", _lamed.lib.XML.Setup.XML_ValueBetweenTags(xml, "param", true));
            Assert.Throws<InvalidOperationException>(() =>  _lamed.lib.XML.Setup.XML_ValueBetweenTags(xml, "param"));

        }
    }
}
