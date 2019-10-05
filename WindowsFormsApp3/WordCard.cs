using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WindowsFormsApp3
{   public enum LanguagePart { Существительное, Прилагательное, Глагол, Наречие, Числительное, Местоимение, Прочие }
    [Serializable]

    public class WordCard
    {
        [XmlElement]
        public string EnglishWord;
        public List<String> RussianWord;
        public LanguagePart typeL;
        private int error = 0;
        private int n = 0;

    public WordCard(String english, List<String> russian, LanguagePart typeL)
    {
            this.EnglishWord = english;
            this.RussianWord = russian;
            this.typeL = typeL;
    }

        public WordCard()
        {
            this.EnglishWord =null;
            this.RussianWord =null;
            this.typeL = LanguagePart.Прочие;
        }

        public override string ToString()
  
        {
            String russian="";

            foreach (String pair in RussianWord)
            {
                russian = russian  + pair + ", ";
            }
            return "Английское слово: " + EnglishWord + " Русский перевод: " + russian;

        }

        public LanguagePart langPart
        {

            get { return typeL; }
        }

        public int Error
        {

            get { return error; }
        }

        public bool translationCheck(String rus) {
            foreach (String s in RussianWord) {
                if (s == rus) return true;
            }
            error++;
            return false;
        }


        public String translationRus(String rus)
        {
            foreach (String s in RussianWord)
            {
                if (s == rus) return EnglishWord;
            } 

            return null;
            
        }

   


       
    }

   
}
