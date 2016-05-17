using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
  public  class SubProduct
    {
      int subProductId;
      string title;
      string subProductImageUrl;
      List<AttributeValue> attributeValueData;

      public int SubProductId
      {
          get { return subProductId; }
          set { subProductId = value; }
      }

      public string Title
      {
          get { return title; }
          set { title = value; }
      }

      public string SubProductImageUrl
      {
          get { return subProductImageUrl; }
          set { subProductImageUrl = value; }
      }

      public List<AttributeValue> AttributeValueData
      {
         get{return attributeValueData;}
         set{attributeValueData=value;}
      }
    }
}
