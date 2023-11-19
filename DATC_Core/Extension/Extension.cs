using System.Text.RegularExpressions;

namespace DATC_Core.Extension
{
    public static class Extension
    {
        public static string ToVND(this double donGia)
        {
           return donGia.ToString("#,##")+" đ";
        }
        
    }
}
