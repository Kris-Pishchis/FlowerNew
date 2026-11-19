using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.UIModels
{
    public class ProductUIModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Cost { get; set; }
        public Bitmap? DehashedImage { get; set;  }
    }
}
