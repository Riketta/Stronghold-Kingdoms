namespace Kingdoms
{
    using System;

    public class URLs
    {
        public static string AccountInfoURL = "http://login.strongholdkingdoms.com/kingdoms/account.php";
        public static string ChatRPCAddress = "";
        public static string GameRPCAddress = "";
        public static string InviteAFriendURL = "http://login.strongholdkingdoms.com/invitefriend";
        public static string ProfileBPPath = "/services/bpEndpoint.php";
        public static string ProfileCardPath = "/services/cardserver.php";
        public static string ProfileCardPathMoreSecure = "/secureservices/cards.php";
        public static string ProfileGamersFirstPaymentURL = "http://login.strongholdkingdoms.com/gamersfirst/product_select.php";
        public static string ProfileNewOffersPath = "/kingdoms/viewRedeemedOffers.php";
        public static string ProfilePath = "/services/auth.php";
        public static string ProfilePaymentURL = "http://login.strongholdkingdoms.com/pay.php";
        public static string ProfileProtocol = "http";
        public static string ProfileServerAddressBigpoint = "cardsxml.prd.external.fireflyops.com";
        public static string ProfileServerAddressCards = "cardsxml.prd.external.fireflyops.com";
        public static string ProfileServerAddressCMS = "news.strongholdkingdoms.com";
        public static string ProfileServerAddressLogin = "login.strongholdkingdoms.com";
        public static string ProfileServerAddressWeb = "login.strongholdkingdoms.com";
        public static string ProfileServerPort = "80";

        public static string AccountMainPage
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/kingdoms/account.php");
            }
        }

        public static string AccountOffersPage
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + ProfileNewOffersPath);
            }
        }

        public static string ChatRPC
        {
            get
            {
                return ("http://" + ChatRPCAddress + ":80/KingdomsChatRPC/KingdomsChat.rem");
            }
        }

        public static string CommunityTranslationPage
        {
            get
            {
                if (Program.mySettings.LanguageIdent == "de")
                {
                    return ("https://" + ProfileServerAddressWeb + "/mod/lang.php/?lang=de");
                }
                if (Program.mySettings.LanguageIdent == "fr")
                {
                    return ("https://" + ProfileServerAddressWeb + "/mod/lang.php/?lang=fr");
                }
                if (Program.mySettings.LanguageIdent == "ru")
                {
                    return ("https://" + ProfileServerAddressWeb + "/mod/lang.php/?lang=ru");
                }
                if (Program.mySettings.LanguageIdent == "es")
                {
                    return ("https://" + ProfileServerAddressWeb + "/mod/lang.php/?lang=es");
                }
                return ("https://" + ProfileServerAddressWeb + "/mod/lang.php/?lang=en");
            }
        }

        public static string FacebookURL
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://www.facebook.com/strongholdkingdoms";

                    case "fr":
                        return "http://www.facebook.com/strongholdkingdoms";

                    case "ru":
                        return "http://www.facebook.com/StrongholdKingdoms";

                    case "es":
                        return "http://www.facebook.com/strongholdkingdoms";
                }
                return "http://www.facebook.com/strongholdkingdoms";
            }
        }

        public static string FAQPage
        {
            get
            {
                return "http://help.strongholdkingdoms.com/wiki/doku.php?id=faq";
            }
        }

        public static string FireflyHomepage
        {
            get
            {
                return "http://www.fireflyworlds.com";
            }
        }

        public static string ForgottenPasswordLink
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/kingdoms/forgotten.php");
            }
        }

        public static string ForgottenPasswordPage
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + ":" + ProfileServerPort + "/main.php?method=forgotten");
            }
        }

        public static string ForumHomepage
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://forum.strongholdkingdoms.com/de/";

                    case "fr":
                        return "http://forum.strongholdkingdoms.com/fr/";

                    case "ru":
                        return "http://forum.strongholdkingdoms.com/ru/";

                    case "es":
                        return "http://forum.strongholdkingdoms.com/es/";

                    case "pl":
                        return "http://forum.strongholdkingdoms.com/pl/";

                    case "br":
                    case "pt":
                        return "http://forum.strongholdkingdoms.com/pt/";

                    case "it":
                        return "http://forum.strongholdkingdoms.com/it/";

                    case "tr":
                        return "http://forum.strongholdkingdoms.com/tr/";
                }
                return "http://forum.strongholdkingdoms.com/en/";
            }
        }

        public static string GameRPC
        {
            get
            {
                return ("http://" + GameRPCAddress + ":80/KingdomsRPC/Kingdoms.rem");
            }
        }

        public static string IPSharingPage
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://www.strongholdkingdoms.de/spielregeln.html";

                    case "fr":
                        return "http://www.strongholdkingdoms.com/ReglesduJeu.html";

                    case "ru":
                        return "http://www.strongholdkingdoms.com/GameRules.ru.html";

                    case "es":
                        return "http://www.strongholdkingdoms.com/GameRules.es.html";

                    case "pl":
                        return "http://www.strongholdkingdoms.com/GameRules.pl.html";

                    case "br":
                    case "pt":
                        return "http://www.strongholdkingdoms.com/GameRules.pt.html";

                    case "it":
                        return "http://www.strongholdkingdoms.com/GameRules.it.html";

                    case "tr":
                        return "http://www.strongholdkingdoms.com/GameRules.tr.html";
                }
                return "http://www.strongholdkingdoms.com/GameRules.html";
            }
        }

        public static string NewsMainPage
        {
            get
            {
                if (!Program.bigpointPartnerInstall)
                {
                    return ("http://" + ProfileServerAddressCMS + "/clientnews.php?lang=" + Program.mySettings.LanguageIdent);
                }
                return ("http://" + ProfileServerAddressCMS + "/clientnews.php?lang=" + Program.mySettings.LanguageIdent + "&noads=1");
            }
        }

        public static string PrivacyPolicy
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.de.html";

                    case "fr":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.fr.html";

                    case "ru":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.ru.html";

                    case "es":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.es.html";

                    case "pl":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.pl.html";

                    case "br":
                    case "pt":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.pt.html";

                    case "it":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.it.html";

                    case "tr":
                        return "http://www.strongholdkingdoms.com/PrivacyPolicy.tr.html";
                }
                return "http://slogin.strongholdkingdoms.com/PrivacyPolicy.html";
            }
        }

        public static string ServerNewsFeed
        {
            get
            {
                return ("http://" + ProfileServerAddressCMS + "/servernews.php?lang=" + Program.mySettings.LanguageIdent);
            }
        }

        public static string shieldDesignerURL
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/shield/shield.html");
            }
        }

        public static string StrongholdCollectionLink_de
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/shc/de");
            }
        }

        public static string StrongholdCollectionLink_en
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/shc/en");
            }
        }

        public static string StrongholdCollectionLink_fr
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/shc/fr");
            }
        }

        public static string StrongholdCollectionLink_ru
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/shc/ru");
            }
        }

        public static string Supportpage
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "https://support.strongholdkingdoms.com/?de";

                    case "fr":
                        return "https://support.strongholdkingdoms.com/?fr";

                    case "ru":
                        return "https://support.strongholdkingdoms.com/?ru";

                    case "es":
                        return "https://support.strongholdkingdoms.com/?es";

                    case "pl":
                        return "https://support.strongholdkingdoms.com/?pl";

                    case "it":
                        return "https://support.strongholdkingdoms.com/?it";

                    case "tr":
                        return "https://support.strongholdkingdoms.com/?tr";
                }
                return "https://support.strongholdkingdoms.com/?en";
            }
        }

        public static string TechnicalFAQPage
        {
            get
            {
                return "http://help.strongholdkingdoms.com/wiki/doku.php?id=technical_faq";
            }
        }

        public static string TermsAndConditions
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.de.html";

                    case "fr":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.fr.html";

                    case "ru":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.ru.html";

                    case "es":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.es.html";

                    case "pl":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.pl.html";

                    case "br":
                    case "pt":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.pt.html";

                    case "it":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.it.html";

                    case "tr":
                        return "http://www.strongholdkingdoms.com/TermsOfUse.tr.html";
                }
                return "http://www.strongholdkingdoms.com/TermsOfUse.html";
            }
        }

        public static string TutorialPage
        {
            get
            {
                return "http://help.strongholdkingdoms.com/wiki/doku.php?id=beginners_guide";
            }
        }

        public static string TwitterURL
        {
            get
            {
                return "https://twitter.com/playstronghold";
            }
        }

        public static string WikiPage
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://hilfe.strongholdkingdoms.de/";

                    case "fr":
                        return "http://aide.strongholdkingdoms.com/";

                    case "ru":
                        return "http://help.ru.strongholdkingdoms.com";

                    case "es":
                        return "http://ayuda.strongholdkingdoms.com";

                    case "pl":
                        return "http://pomoc.strongholdkingdoms.com";

                    case "br":
                    case "pt":
                        return "http://ajuda.strongholdkingdoms.com";

                    case "it":
                        return "http://aiuto.strongholdkingdoms.com";

                    case "tr":
                        return "http://yardim.strongholdkingdoms.com";
                }
                return "http://help.strongholdkingdoms.com";
            }
        }

        public static string WorldListPage
        {
            get
            {
                return ("http://" + ProfileServerAddressWeb + "/kingdoms/gameWorldList.php");
            }
        }

        public static string YoutubeURL
        {
            get
            {
                switch (Program.mySettings.LanguageIdent)
                {
                    case "de":
                        return "http://www.youtube.com/user/fireflyworlds?ob=0";

                    case "fr":
                        return "http://www.youtube.com/user/fireflyworlds?ob=0";

                    case "ru":
                        return "http://www.youtube.com/user/fireflyworlds?ob=0";
                }
                return "http://www.youtube.com/user/fireflyworlds?ob=0";
            }
        }
    }
}

