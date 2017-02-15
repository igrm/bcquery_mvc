using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bcquery_mvc
{
    public class TranslationService
    {
        private List<TranslationText> translations;
        private List<TranslationItem> translationItems;

        public TranslationService()
        {
            try
            {
                var context = (new TranslationEntities());
                this.translations = context.TranslationTexts.ToList();
                this.translationItems = context.TranslationItems.ToList();
            }
            catch (Exception ex)
            {
                this.translations = new List<TranslationText>();
                this.translationItems = new List<TranslationItem>();
                System.Diagnostics.Debug.WriteLine(String.Format("{0}", ex.ToString()));
            }
        }

        public string GetText(string code)
        {
            string lablel = "";
            try
            {
                lablel = translations.Where(t => t.TranslationItemID == translationItems.Where(r=>r.TranslationItemCode == code).Select(r=>r.TranslationItemID).FirstOrDefault() && t.Language.LanguageCode == "ENG" ).First().TranslationTextValue;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("{0} {1}", code, ex.ToString()));
            }
            return lablel;
        }

    }
}