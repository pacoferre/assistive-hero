using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssistiveHero.Models
{
    public class Item
    {
        public string url = "";
        public string text = "";
        public bool disabled = false;
        public List<Item> childreen = null;

        public Item(string url, string text, bool disabled)
        {
            this.url = url;
            this.text = text;
            this.disabled = disabled;
        }

        public static List<Item> demo = new List<Item>();

        public static List<Item> GetDemo()
        {
            if (demo.Count == 0)
            {
                lock (demo)
                {
                    if (demo.Count == 0)
                    {
                        Item quiero = new Item("/images/demo/quiero.jpg", "Quiero", false);
                        Item colegio = new Item("/images/demo/colegio.jpg", "Colegio", false);
                        Item frutas = new Item("/images/demo/frutas.jpg", "Frutas", false);

                        demo.Add(quiero);

                        demo.Add(colegio);

                        quiero.childreen = new List<Item>(5);
                        colegio.childreen = new List<Item>(5);


                        colegio.childreen.Add(new Item("/images/demo/recreo.jpg", "Recreo", false));
                        colegio.childreen.Add(new Item("/images/demo/pintar.jpg", "Pintar", false));

                        quiero.childreen.Add(new Item("/images/demo/agua.jpg", "Agua", false));
                        quiero.childreen.Add(new Item("/images/demo/pan.jpg", "Pan", false));

                        quiero.childreen.Add(frutas);

                        frutas.childreen = new List<Item>(5);
                        frutas.childreen.Add(new Item("/images/demo/manzana.jpg", "Manzana", false));
                        frutas.childreen.Add(new Item("/images/demo/naranja.jpg", "Naranja", false));
                        frutas.childreen.Add(new Item("/images/demo/pera.jpg", "Pera", false));
                        frutas.childreen.Add(new Item("/images/demo/uvas.jpg", "Uvas", true));
                        frutas.childreen.Add(new Item("/images/demo/platano.jpg", "Plátano", false));
                    }
                }
            }

            return demo;
        }

        public static bool CanSay(ref List<Item> items, string text)
        {
            foreach(Item item in items)
            {
                if (item.text == text)
                {
                    return true;
                }
                if (item.childreen != null)
                {
                    if (CanSay(ref item.childreen, text))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
