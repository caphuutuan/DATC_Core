using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATC_Core.Models;

namespace SnoopShop
{
    public class CheckSlug
    {
        //error class

        DATCCoreMineDBContext db = new DATCCoreMineDBContext();
        public bool KiemTraSlug(String Table, String Slug, int? id)
        {
            switch (Table)
            {
                case "Category":
                    if (id != null)
                    {
                        if (db.Categoryies.Where(m => m.Published == true && m.CateId != id).Count() > 0)
                            return false;
                    }
                    else
                    {
                        if (db.Categoryies.Where(m => m.Published == true).Count() > 0)
                            return false;
                    }
                    break;
                case "Topic":
                    break;
                case "Post":
                    break;
                case "Product":
                    break;
            }
            return true;


        }

    }
}