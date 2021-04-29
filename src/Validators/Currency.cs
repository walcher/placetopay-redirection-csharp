using System.Collections.Generic;
using System.Linq;

namespace PlacetoPay.Redirection.Validators
{
    public class Currency : BaseValidator
    {
        public const string CUR_COP = "COP";
        public const string CUR_USD = "USD";
        public const string CUR_MXN = "MXN";
        public const string CUR_AUD = "AUD";
        public const string CUR_AFN = "AFN";
        public const string CUR_AED = "AED";
        public const string CUR_ALL = "ALL";
        public const string CUR_AMD = "AMD";
        public const string CUR_ANG = "ANG";
        public const string CUR_AOA = "AOA";
        public const string CUR_ARS = "ARS";
        public const string CUR_AWG = "AWG";
        public const string CUR_AZN = "AZN";
        public const string CUR_BAM = "BAM";
        public const string CUR_BBD = "BBD";
        public const string CUR_BDT = "BDT";
        public const string CUR_BGN = "BGN";
        public const string CUR_BHD = "BHD";
        public const string CUR_BIF = "BIF";
        public const string CUR_BMD = "BMD";
        public const string CUR_BND = "BND";
        public const string CUR_BOB = "BOB";
        public const string CUR_BOV = "BOV";
        public const string CUR_BRL = "BRL";
        public const string CUR_BSD = "BSD";
        public const string CUR_BTN = "BTN";
        public const string CUR_BWP = "BWP";
        public const string CUR_BYN = "BYN";
        public const string CUR_BZD = "BZD";
        public const string CUR_CAD = "CAD";
        public const string CUR_CDF = "CDF";
        public const string CUR_CHE = "CHE";
        public const string CUR_CHF = "CHF";
        public const string CUR_CHW = "CHW";
        public const string CUR_CLF = "CLF";
        public const string CUR_CLP = "CLP";
        public const string CUR_CNY = "CNY";
        public const string CUR_COU = "COU";
        public const string CUR_CRC = "CRC";
        public const string CUR_CUC = "CUC";
        public const string CUR_CUP = "CUP";
        public const string CUR_CVE = "CVE";
        public const string CUR_CZK = "CZK";
        public const string CUR_DJF = "DJF";
        public const string CUR_DKK = "DKK";
        public const string CUR_DOP = "DOP";
        public const string CUR_DZD = "DZD";
        public const string CUR_EGP = "EGP";
        public const string CUR_ERN = "ERN";
        public const string CUR_ETB = "ETB";
        public const string CUR_EUR = "EUR";
        public const string CUR_FJD = "FJD";
        public const string CUR_FKP = "FKP";
        public const string CUR_GBP = "GBP";
        public const string CUR_GEL = "GEL";
        public const string CUR_GHS = "GHS";
        public const string CUR_GIP = "GIP";
        public const string CUR_GMD = "GMD";
        public const string CUR_GNF = "GNF";
        public const string CUR_GTQ = "GTQ";
        public const string CUR_GYD = "GYD";
        public const string CUR_HKD = "HKD";
        public const string CUR_HNL = "HNL";
        public const string CUR_HRK = "HRK";
        public const string CUR_HTG = "HTG";
        public const string CUR_HUF = "HUF";
        public const string CUR_IDR = "IDR";
        public const string CUR_ILS = "ILS";
        public const string CUR_INR = "INR";
        public const string CUR_IQD = "IQD";
        public const string CUR_IRR = "IRR";
        public const string CUR_ISK = "ISK";
        public const string CUR_JMD = "JMD";
        public const string CUR_JOD = "JOD";
        public const string CUR_JPY = "JPY";
        public const string CUR_KES = "KES";
        public const string CUR_KGS = "KGS";
        public const string CUR_KHR = "KHR";
        public const string CUR_KMF = "KMF";
        public const string CUR_KPW = "KPW";
        public const string CUR_KRW = "KRW";
        public const string CUR_KWD = "KWD";
        public const string CUR_KYD = "KYD";
        public const string CUR_KZT = "KZT";
        public const string CUR_LAK = "LAK";
        public const string CUR_LBP = "LBP";
        public const string CUR_LKR = "LKR";
        public const string CUR_LRD = "LRD";
        public const string CUR_LSL = "LSL";
        public const string CUR_LYD = "LYD";
        public const string CUR_MAD = "MAD";
        public const string CUR_MDL = "MDL";
        public const string CUR_MGA = "MGA";
        public const string CUR_MKD = "MKD";
        public const string CUR_MMK = "MMK";
        public const string CUR_MNT = "MNT";
        public const string CUR_MOP = "MOP";
        public const string CUR_MRO = "MRO";
        public const string CUR_MUR = "MUR";
        public const string CUR_MVR = "MVR";
        public const string CUR_MWK = "MWK";
        public const string CUR_MXV = "MXV";
        public const string CUR_MYR = "MYR";
        public const string CUR_MZN = "MZN";
        public const string CUR_NAD = "NAD";
        public const string CUR_NGN = "NGN";
        public const string CUR_NIO = "NIO";
        public const string CUR_NOK = "NOK";
        public const string CUR_NPR = "NPR";
        public const string CUR_NZD = "NZD";
        public const string CUR_OMR = "OMR";
        public const string CUR_PAB = "PAB";
        public const string CUR_PEN = "PEN";
        public const string CUR_PGK = "PGK";
        public const string CUR_PHP = "PHP";
        public const string CUR_PKR = "PKR";
        public const string CUR_PLN = "PLN";
        public const string CUR_PYG = "PYG";
        public const string CUR_QAR = "QAR";
        public const string CUR_RON = "RON";
        public const string CUR_RSD = "RSD";
        public const string CUR_RUB = "RUB";
        public const string CUR_RWF = "RWF";
        public const string CUR_SAR = "SAR";
        public const string CUR_SBD = "SBD";
        public const string CUR_SCR = "SCR";
        public const string CUR_SDG = "SDG";
        public const string CUR_SEK = "SEK";
        public const string CUR_SGD = "SGD";
        public const string CUR_SHP = "SHP";
        public const string CUR_SLL = "SLL";
        public const string CUR_SOS = "SOS";
        public const string CUR_SRD = "SRD";
        public const string CUR_SSP = "SSP";
        public const string CUR_STD = "STD";
        public const string CUR_SVC = "SVC";
        public const string CUR_SYP = "SYP";
        public const string CUR_SZL = "SZL";
        public const string CUR_THB = "THB";
        public const string CUR_TJS = "TJS";
        public const string CUR_TMT = "TMT";
        public const string CUR_TND = "TND";
        public const string CUR_TOP = "TOP";
        public const string CUR_TRY = "TRY";
        public const string CUR_TTD = "TTD";
        public const string CUR_TWD = "TWD";
        public const string CUR_TZS = "TZS";
        public const string CUR_UAH = "UAH";
        public const string CUR_UGX = "UGX";
        public const string CUR_USN = "USN";
        public const string CUR_UYI = "UYI";
        public const string CUR_UYU = "UYU";
        public const string CUR_UZS = "UZS";
        public const string CUR_VEF = "VEF";
        public const string CUR_VND = "VND";
        public const string CUR_VUV = "VUV";
        public const string CUR_WST = "WST";
        public const string CUR_XAF = "XAF";
        public const string CUR_XAG = "XAG";
        public const string CUR_XAU = "XAU";
        public const string CUR_XBA = "XBA";
        public const string CUR_XBB = "XBB";
        public const string CUR_XBC = "XBC";
        public const string CUR_XBD = "XBD";
        public const string CUR_XCD = "XCD";
        public const string CUR_XDR = "XDR";
        public const string CUR_XOF = "XOF";
        public const string CUR_XPD = "XPD";
        public const string CUR_XPF = "XPF";
        public const string CUR_XPT = "XPT";
        public const string CUR_XSU = "XSU";
        public const string CUR_XTS = "XTS";
        public const string CUR_XUA = "XUA";
        public const string CUR_XXX = "XXX";
        public const string CUR_YER = "YER";
        public const string CUR_ZAR = "ZAR";
        public const string CUR_ZMW = "ZMW";
        public const string CUR_ZWL = "ZWL";

        public static Dictionary<string, string> currencyNames = new Dictionary<string, string>
        {
            { CUR_AFN, "Afghani" },
            { CUR_AED, "UAE Dirham" },
            { CUR_ALL, "Lek" },
            { CUR_AMD, "Armenian Dram" },
            { CUR_ANG, "Netherlands Antillean Guilder" },
            { CUR_AOA, "Kwanza" },
            { CUR_ARS, "Argentine Peso" },
            { CUR_AUD, "Australian Dollar" },
            { CUR_AWG, "Aruban Florin" },
            { CUR_AZN, "Azerbaijanian Manat" },
            { CUR_BAM, "Convertible Mark" },
            { CUR_BBD, "Barbados Dollar" },
            { CUR_BDT, "Taka" },
            { CUR_BGN, "Bulgarian Lev" },
            { CUR_BHD, "Bahraini Dinar" },
            { CUR_BIF, "Burundi Franc" },
            { CUR_BMD, "Bermudian Dollar" },
            { CUR_BND, "Brunei Dollar" },
            { CUR_BOB, "Boliviano" },
            { CUR_BOV, "Mvdol" },
            { CUR_BRL, "Brazilian Real" },
            { CUR_BSD, "Bahamian Dollar" },
            { CUR_BTN, "Ngultrum" },
            { CUR_BWP, "Pula" },
            { CUR_BYN, "Belarusian Ruble" },
            { CUR_BZD, "Belize Dollar" },
            { CUR_CAD, "Canadian Dollar" },
            { CUR_CDF, "Congolese Franc" },
            { CUR_CHE, "WIR Euro" },
            { CUR_CHF, "Swiss Franc" },
            { CUR_CHW, "WIR Franc" },
            { CUR_CLF, "Unidad de Fomento" },
            { CUR_CLP, "Chilean Peso" },
            { CUR_CNY, "Yuan Renminbi" },
            { CUR_COP, "Colombian Peso" },
            { CUR_COU, "Unidad de Valor Real" },
            { CUR_CRC, "Costa Rican Colon" },
            { CUR_CUC, "Peso Convertible" },
            { CUR_CUP, "Cuban Peso" },
            { CUR_CVE, "Cabo Verde Escudo" },
            { CUR_CZK, "Czech Koruna" },
            { CUR_DJF, "Djibouti Franc" },
            { CUR_DKK, "Danish Krone" },
            { CUR_DOP, "Dominican Peso" },
            { CUR_DZD, "Algerian Dinar" },
            { CUR_EGP, "Egyptian Pound" },
            { CUR_ERN, "Nakfa" },
            { CUR_ETB, "Ethiopian Birr" },
            { CUR_EUR, "Euro" },
            { CUR_FJD, "Fiji Dollar" },
            { CUR_FKP, "Falkland Islands Pound" },
            { CUR_GBP, "Pound Sterling" },
            { CUR_GEL, "Lari" },
            { CUR_GHS, "Ghana Cedi" },
            { CUR_GIP, "Gibraltar Pound" },
            { CUR_GMD, "Dalasi" },
            { CUR_GNF, "Guinea Franc" },
            { CUR_GTQ, "Quetzal" },
            { CUR_GYD, "Guyana Dollar" },
            { CUR_HKD, "Hong Kong Dollar" },
            { CUR_HNL, "Lempira" },
            { CUR_HRK, "Kuna" },
            { CUR_HTG, "Gourde" },
            { CUR_HUF, "Forint" },
            { CUR_IDR, "Rupiah" },
            { CUR_ILS, "New Israeli Sheqel" },
            { CUR_INR, "Indian Rupee" },
            { CUR_IQD, "Iraqi Dinar" },
            { CUR_IRR, "Iranian Rial" },
            { CUR_ISK, "Iceland Krona" },
            { CUR_JMD, "Jamaican Dollar" },
            { CUR_JOD, "Jordanian Dinar" },
            { CUR_JPY, "Yen" },
            { CUR_KES, "Kenyan Shilling" },
            { CUR_KGS, "Som" },
            { CUR_KHR, "Riel" },
            { CUR_KMF, "Comoro Franc" },
            { CUR_KPW, "North Korean Won" },
            { CUR_KRW, "Won" },
            { CUR_KWD, "Kuwaiti Dinar" },
            { CUR_KYD, "Cayman Islands Dollar" },
            { CUR_KZT, "Tenge" },
            { CUR_LAK, "Kip" },
            { CUR_LBP, "Lebanese Pound" },
            { CUR_LKR, "Sri Lanka Rupee" },
            { CUR_LRD, "Liberian Dollar" },
            { CUR_LSL, "Loti" },
            { CUR_LYD, "Libyan Dinar" },
            { CUR_MAD, "Moroccan Dirham" },
            { CUR_MDL, "Moldovan Leu" },
            { CUR_MGA, "Malagasy Ariary" },
            { CUR_MKD, "Denar" },
            { CUR_MMK, "Kyat" },
            { CUR_MNT, "Tugrik" },
            { CUR_MOP, "Pataca" },
            { CUR_MRO, "Ouguiya" },
            { CUR_MUR, "Mauritius Rupee" },
            { CUR_MVR, "Rufiyaa" },
            { CUR_MWK, "Malawi Kwacha" },
            { CUR_MXN, "Mexican Peso" },
            { CUR_MXV, "Mexican Unidad de Inversion (UDI)" },
            { CUR_MYR, "Malaysian Ringgit" },
            { CUR_MZN, "Mozambique Metical" },
            { CUR_NAD, "Namibia Dollar" },
            { CUR_NGN, "Naira" },
            { CUR_NIO, "Cordoba Oro" },
            { CUR_NOK, "Norwegian Krone" },
            { CUR_NPR, "Nepalese Rupee" },
            { CUR_NZD, "New Zealand Dollar" },
            { CUR_OMR, "Rial Omani" },
            { CUR_PAB, "Balboa" },
            { CUR_PEN, "Sol" },
            { CUR_PGK, "Kina" },
            { CUR_PHP, "Philippine Peso" },
            { CUR_PKR, "Pakistan Rupee" },
            { CUR_PLN, "Zloty" },
            { CUR_PYG, "Guarani" },
            { CUR_QAR, "Qatari Rial" },
            { CUR_RON, "Romanian Leu" },
            { CUR_RSD, "Serbian Dinar" },
            { CUR_RUB, "Russian Ruble" },
            { CUR_RWF, "Rwanda Franc" },
            { CUR_SAR, "Saudi Riyal" },
            { CUR_SBD, "Solomon Islands Dollar" },
            { CUR_SCR, "Seychelles Rupee" },
            { CUR_SDG, "Sudanese Pound" },
            { CUR_SEK, "Swedish Krona" },
            { CUR_SGD, "Singapore Dollar" },
            { CUR_SHP, "Saint Helena Pound" },
            { CUR_SLL, "Leone" },
            { CUR_SOS, "Somali Shilling" },
            { CUR_SRD, "Surinam Dollar" },
            { CUR_SSP, "South Sudanese Pound" },
            { CUR_STD, "Dobra" },
            { CUR_SVC, "El Salvador Colon" },
            { CUR_SYP, "Syrian Pound" },
            { CUR_SZL, "Lilangeni" },
            { CUR_THB, "Baht" },
            { CUR_TJS, "Somoni" },
            { CUR_TMT, "Turkmenistan New Manat" },
            { CUR_TND, "Tunisian Dinar" },
            { CUR_TOP, "Paanga" },
            { CUR_TRY, "Turkish Lira" },
            { CUR_TTD, "Trinidad and Tobago Dollar" },
            { CUR_TWD, "New Taiwan Dollar" },
            { CUR_TZS, "Tanzanian Shilling" },
            { CUR_UAH, "Hryvnia" },
            { CUR_UGX, "Uganda Shilling" },
            { CUR_USD, "US Dollar" },
            { CUR_USN, "US Dollar (Next day)" },
            { CUR_UYI, "Uruguay Peso en Unidades Indexadas (URUIURUI)" },
            { CUR_UYU, "Peso Uruguayo" },
            { CUR_UZS, "Uzbekistan Sum" },
            { CUR_VEF, "Bol�var" },
            { CUR_VND, "Dong" },
            { CUR_VUV, "Vatu" },
            { CUR_WST, "Tala" },
            { CUR_XAF, "CFA Franc BEAC" },
            { CUR_XAG, "Silver" },
            { CUR_XAU, "Gold" },
            { CUR_XBA, "Bond Markets Unit European Composite Unit (EURCO)" },
            { CUR_XBB, "Bond Markets Unit European Monetary Unit (E.M.U.-6)" },
            { CUR_XBC, "Bond Markets Unit European Unit of Account 9 (E.U.A.-9)" },
            { CUR_XBD, "Bond Markets Unit European Unit of Account 17 (E.U.A.-17)" },
            { CUR_XCD, "East Caribbean Dollar" },
            { CUR_XDR, "SDR (Special Drawing Right)" },
            { CUR_XOF, "CFA Franc BCEAO" },
            { CUR_XPD, "Palladium" },
            { CUR_XPF, "CFP Franc" },
            { CUR_XPT, "Platinum" },
            { CUR_XSU, "Sucre" },
            { CUR_XTS, "Codes specifically reserved for testing purposes" },
            { CUR_XUA, "ADB Unit of Account" },
            { CUR_XXX, "The codes assigned for transactions where no currency is involved" },
            { CUR_YER, "Yemeni Rial" },
            { CUR_ZAR, "Rand" },
            { CUR_ZMW, "Zambian Kwacha" },
            { CUR_ZWL, "Zimbabwe Dollar" },
        };

        public static Dictionary<string, string> currencyNumericCodes = new Dictionary<string, string>
        {
            { CUR_AFN, "971" },
            { CUR_EUR, "978" },
            { CUR_ALL, "008" },
            { CUR_DZD, "012" },
            { CUR_USD, "840" },
            { CUR_AOA, "973" },
            { CUR_XCD, "951" },
            { CUR_ARS, "032" },
            { CUR_AMD, "051" },
            { CUR_AWG, "533" },
            { CUR_AUD, "036" },
            { CUR_AZN, "944" },
            { CUR_BSD, "044" },
            { CUR_BHD, "048" },
            { CUR_BDT, "050" },
            { CUR_BBD, "052" },
            { CUR_BYN, "933" },
            { CUR_BZD, "084" },
            { CUR_XOF, "952" },
            { CUR_BMD, "060" },
            { CUR_INR, "356" },
            { CUR_BTN, "064" },
            { CUR_BOB, "068" },
            { CUR_BOV, "984" },
            { CUR_BAM, "977" },
            { CUR_BWP, "072" },
            { CUR_NOK, "578" },
            { CUR_BRL, "986" },
            { CUR_BND, "096" },
            { CUR_BGN, "975" },
            { CUR_BIF, "108" },
            { CUR_CVE, "132" },
            { CUR_KHR, "116" },
            { CUR_XAF, "950" },
            { CUR_CAD, "124" },
            { CUR_KYD, "136" },
            { CUR_CLP, "152" },
            { CUR_CLF, "990" },
            { CUR_CNY, "156" },
            { CUR_COP, "170" },
            { CUR_COU, "970" },
            { CUR_KMF, "174" },
            { CUR_CDF, "976" },
            { CUR_NZD, "554" },
            { CUR_CRC, "188" },
            { CUR_HRK, "191" },
            { CUR_CUP, "192" },
            { CUR_CUC, "931" },
            { CUR_ANG, "532" },
            { CUR_CZK, "203" },
            { CUR_DKK, "208" },
            { CUR_DJF, "262" },
            { CUR_DOP, "214" },
            { CUR_EGP, "818" },
            { CUR_SVC, "222" },
            { CUR_ERN, "232" },
            { CUR_ETB, "230" },
            { CUR_FKP, "238" },
            { CUR_FJD, "242" },
            { CUR_XPF, "953" },
            { CUR_GMD, "270" },
            { CUR_GEL, "981" },
            { CUR_GHS, "936" },
            { CUR_GIP, "292" },
            { CUR_GTQ, "320" },
            { CUR_GBP, "826" },
            { CUR_GNF, "324" },
            { CUR_GYD, "328" },
            { CUR_HTG, "332" },
            { CUR_HNL, "340" },
            { CUR_HKD, "344" },
            { CUR_HUF, "348" },
            { CUR_ISK, "352" },
            { CUR_IDR, "360" },
            { CUR_XDR, "960" },
            { CUR_IRR, "364" },
            { CUR_IQD, "368" },
            { CUR_ILS, "376" },
            { CUR_JMD, "388" },
            { CUR_JPY, "392" },
            { CUR_JOD, "400" },
            { CUR_KZT, "398" },
            { CUR_KES, "404" },
            { CUR_KPW, "408" },
            { CUR_KRW, "410" },
            { CUR_KWD, "414" },
            { CUR_KGS, "417" },
            { CUR_LAK, "418" },
            { CUR_LBP, "422" },
            { CUR_LSL, "426" },
            { CUR_ZAR, "710" },
            { CUR_LRD, "430" },
            { CUR_LYD, "434" },
            { CUR_CHF, "756" },
            { CUR_MOP, "446" },
            { CUR_MKD, "807" },
            { CUR_MGA, "969" },
            { CUR_MWK, "454" },
            { CUR_MYR, "458" },
            { CUR_MVR, "462" },
            { CUR_MRO, "478" },
            { CUR_MUR, "480" },
            { CUR_XUA, "965" },
            { CUR_MXN, "484" },
            { CUR_MXV, "979" },
            { CUR_MDL, "498" },
            { CUR_MNT, "496" },
            { CUR_MAD, "504" },
            { CUR_MZN, "943" },
            { CUR_MMK, "104" },
            { CUR_NAD, "516" },
            { CUR_NPR, "524" },
            { CUR_NIO, "558" },
            { CUR_NGN, "566" },
            { CUR_OMR, "512" },
            { CUR_PKR, "586" },
            { CUR_PAB, "590" },
            { CUR_PGK, "598" },
            { CUR_PYG, "600" },
            { CUR_PEN, "604" },
            { CUR_PHP, "608" },
            { CUR_PLN, "985" },
            { CUR_QAR, "634" },
            { CUR_RON, "946" },
            { CUR_RUB, "643" },
            { CUR_RWF, "646" },
            { CUR_SHP, "654" },
            { CUR_WST, "882" },
            { CUR_STD, "678" },
            { CUR_SAR, "682" },
            { CUR_RSD, "941" },
            { CUR_SCR, "690" },
            { CUR_SLL, "694" },
            { CUR_SGD, "702" },
            { CUR_XSU, "994" },
            { CUR_SBD, "090" },
            { CUR_SOS, "706" },
            { CUR_SSP, "728" },
            { CUR_LKR, "144" },
            { CUR_SDG, "938" },
            { CUR_SRD, "968" },
            { CUR_SZL, "748" },
            { CUR_SEK, "752" },
            { CUR_CHE, "947" },
            { CUR_CHW, "948" },
            { CUR_SYP, "760" },
            { CUR_TWD, "901" },
            { CUR_TJS, "972" },
            { CUR_TZS, "834" },
            { CUR_THB, "764" },
            { CUR_TOP, "776" },
            { CUR_TTD, "780" },
            { CUR_TND, "788" },
            { CUR_TRY, "949" },
            { CUR_TMT, "934" },
            { CUR_UGX, "800" },
            { CUR_UAH, "980" },
            { CUR_AED, "784" },
            { CUR_USN, "997" },
            { CUR_UYU, "858" },
            { CUR_UYI, "940" },
            { CUR_UZS, "860" },
            { CUR_VUV, "548" },
            { CUR_VEF, "937" },
            { CUR_VND, "704" },
            { CUR_YER, "886" },
            { CUR_ZMW, "967" },
            { CUR_ZWL, "932" },
            { CUR_XBA, "955" },
            { CUR_XBB, "956" },
            { CUR_XBC, "957" },
            { CUR_XBD, "958" },
            { CUR_XTS, "963" },
            { CUR_XXX, "999" },
            { CUR_XAU, "959" },
            { CUR_XPD, "964" },
            { CUR_XPT, "962" },
            { CUR_XAG, "961" },
        };

        /// <summary>
        /// Get currency name.
        /// </summary>
        /// <param name="currency">string</param>
        /// <returns>string</returns>
        public static string CurrencyName(string currency)
        {
            if (currencyNames.TryGetValue(currency, out string name))
            {
                return name;
            }

            return null;
        }

        /// <summary>
        /// Get currenty numeric code.
        /// </summary>
        /// <param name="currency">string</param>
        /// <returns>string</returns>
        public static string CurrencyNumericCode(string currency)
        {
            if (currencyNumericCodes.TryGetValue(currency, out string numericCode))
            {
                return numericCode;
            }

            return null;
        }

        /// <summary>
        /// Get currency code.
        /// </summary>
        /// <param name="numericCode">string</param>
        /// <returns>string</returns>
        public static string NumericCodeToCurrency(string numericCode)
        {
            var currencies = currencyNumericCodes.ToDictionary(x => x.Value, x => x.Key);

            if (currencies.TryGetValue(numericCode, out string code))
            {
                return code;
            }

            return null;
        }

        /// <summary>
        /// Check if currency is valid.
        /// </summary>
        /// <param name="currency">string</param>
        /// <returns>bool</returns>
        public static bool IsValidCurrency(string currency)
        {
            return currencyNames.ContainsKey(currency);
        }
    }
}
