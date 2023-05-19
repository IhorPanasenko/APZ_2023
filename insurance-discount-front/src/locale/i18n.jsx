import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import HttpApi from "i18next-http-backend";

i18n
  .use(HttpApi)
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    supportedLngs: ["en", "fr"], // Add any other supported languages
    fallbackLng: "en", // Default language if translation is missing
    detection: {
      order: ["navigator"],
    },
    backend: {
      loadPath: "/locales/{{lng}}.json",
    },
    react: {
      useSuspense: false, // Set to true if using React Suspense
    },
  });

export default i18n;