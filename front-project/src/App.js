import React from "react";
import { useTranslation} from "react-i18next";

function App() {
  const { t } = useTranslation();
  const { i18n }  = useTranslation();
  let lang = "uk";

  function clickhandler() {
    console.log(lang);
    console.log(lang === "uk");
    if (lang === "uk") {
      i18n.changeLanguage("en");
      lang = "en";
    }
    else {
      i18n.changeLanguage("uk");
      lang = "uk";
    }
  }

  return (
    <>
      <h1>Hello World</h1>
      <div>{t("hello")}</div>
      <button onClick={clickhandler}>Change</button>
    </>
  );
}

export default App;
