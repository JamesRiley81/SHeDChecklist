using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHeDChecklist
{
    public class CompleteImages
    {
        public string lText;
        public string tText;
        public CompleteImages(string link, string text)
        {
            lText = link; tText = text;
        }
        public CompleteImages() { }

    }
    public class ImageList
    {
        List<CompleteImages> images = new List<CompleteImages> {new CompleteImages("DogRoss.jpg", "Checklist complete.  What a happy little checklist."), new CompleteImages("SpongeBob.bmp", "Checklist complete."),
            new CompleteImages("BigHead.jpg", "Checklist complete.  Your planet survives for another day."), new CompleteImages("Morpheus.png","Checklist complete."), new CompleteImages("BobRoss.jpg", "Checklist complete."), new CompleteImages("BobRoss2.jpg", "Checklist complete.") };
        Random r = new Random();

        public CompleteImages GetImage()
        {
            int i = r.Next(0, images.Count);
            return images[i];
        }
    }
}
